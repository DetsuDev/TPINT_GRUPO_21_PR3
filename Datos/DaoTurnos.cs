using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DaoTurnos
    {
        private AccesoDatos ds = new AccesoDatos();

        public DataTable getTabla(int idMedico = 0)
        {
            string consulta = @"
            SELECT 
                T.Id_Turno AS ID,
                (P_Med.Nombre + ' ' + P_Med.Apellido) AS Medico,
                Esp.Nombre AS Especialidad,
                P_Pac.DNI AS DNI,
                (P_Pac.Nombre + ' ' + P_Pac.Apellido) AS Paciente,
                CONVERT(VARCHAR(10), T.Fecha, 103) AS Fecha,
                CONVERT(VARCHAR(5), T.Hora) AS Hora,
                T.Observacion AS Observacion,
                T.EstadoTurno AS Estado
            FROM TURNO T
            INNER JOIN PERSONA P_Pac ON T.Id_Persona = P_Pac.Id_Persona
            INNER JOIN MEDICO Med ON T.Id_Medico = Med.Id_Medico
            INNER JOIN PERSONA P_Med ON Med.Id_Persona = P_Med.Id_Persona
            INNER JOIN ESPECIALIDADES Esp ON Med.Id_Especialidad = Esp.Id_Especialidad
            WHERE T.Estado = 1"; 

            if (idMedico > 0)
            {
                consulta += " AND T.Id_Medico = " + idMedico;
            }

            return ds.obtenerTabla(consulta);
        }

        public bool existePaciente(string dni)
        {
            string consulta = $"SELECT COUNT(*) FROM PERSONA WHERE DNI = '{dni.Trim()}'";
            DataTable dt = ds.obtenerTabla(consulta);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
            return false;
        }

        public bool agregarTurno(int idMedico, string dniPaciente, string fecha, string hora, string observacion)
        {
            string consulta = $@"
            INSERT INTO TURNO (Id_Medico, Id_Persona, Fecha, Hora, Observacion, EstadoTurno, Estado)
            VALUES ({idMedico}, (SELECT Id_Persona FROM PERSONA WHERE DNI = '{dniPaciente.Trim()}'), '{fecha}', '{hora}', '{observacion.Trim().Replace("'", "''")}', 'Pendiente', 1);";

            try
            {
                ds.ejecutarConsulta(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable verificarTurnoPaciente(string dniPaciente, string fecha, string hora)
        {
            string consulta = $@"
            SELECT T.Id_Turno 
            FROM TURNO T
            INNER JOIN PERSONA P ON T.Id_Persona = P.Id_Persona
            WHERE P.DNI = '{dniPaciente.Trim()}' 
              AND T.Fecha = '{fecha}' 
              AND CAST(T.Hora AS TIME) = CAST('{hora}' AS TIME)
              AND T.Estado = 1";

            return ds.obtenerTabla(consulta);
        }

        public DataTable verificarTurnoMedico(int idMedico, string fecha, string hora)
        {
            string consulta = $@"
            SELECT Id_Turno 
            FROM TURNO 
            WHERE Id_Medico = {idMedico} 
              AND Fecha = '{fecha}' 
              AND CAST(Hora AS TIME) = CAST('{hora}' AS TIME)
              AND Estado = 1";

            return ds.obtenerTabla(consulta);
        }

        public DataTable obtenerEspecialidadesAlta()
        {
            string consulta = "SELECT Id_Especialidad, Nombre FROM ESPECIALIDADES";
            return ds.obtenerTabla(consulta);
        }

        public DataTable obtenerMedicosDisponibles(int idEspecialidad, string letraDia, string horaTipeada)
        {
            string consultaMedicos = $@"
            SELECT M.Id_Medico, (P.Nombre + ' ' + P.Apellido) AS NombreCompleto
            FROM MEDICO M
            INNER JOIN PERSONA P ON M.Id_Persona = P.Id_Persona
            INNER JOIN DISPONIBILIDAD D ON M.Id_Medico = D.Id_Medico
            WHERE M.Id_Especialidad = {idEspecialidad}
              AND M.Estado = 1
              AND D.DiasDisponibles LIKE '%{letraDia}%'
              AND '{horaTipeada}' >= D.HoraInicio 
              AND '{horaTipeada}' < D.HoraFin";

            return ds.obtenerTabla(consultaMedicos);
        }

        public DataTable getPresentismo(DateTime fechaInicio, DateTime fechaFin)
        {
            string consulta = $@"
            SELECT * FROM TURNO
            WHERE Fecha BETWEEN '{fechaInicio:yyyy-MM-dd}' AND '{fechaFin:yyyy-MM-dd}' AND Estado = 1";

            return ds.obtenerTabla(consulta);
        }
        public DataTable obtenerTurnoPorId(int idTurno)
        {
            string consulta = $@"
            SELECT T.Id_Turno AS ID, T.Id_Medico, Med.Id_Especialidad,
                   (P_Med.Nombre + ' ' + P_Med.Apellido) AS Medico,
                   P_Pac.DNI AS DNI,
                   CONVERT(VARCHAR(10), T.Fecha, 23) AS Fecha,
                   CONVERT(VARCHAR(5), T.Hora) AS Hora,
                   T.Observacion AS Observacion
            FROM TURNO T
            INNER JOIN PERSONA P_Pac ON T.Id_Persona = P_Pac.Id_Persona
            INNER JOIN MEDICO Med ON T.Id_Medico = Med.Id_Medico
            INNER JOIN PERSONA P_Med ON Med.Id_Persona = P_Med.Id_Persona
            WHERE T.Id_Turno = {idTurno}";

            return ds.obtenerTabla(consulta);
        }

        public bool actualizarTurno(int idTurno, string fecha, string hora, string observacion)
        {
            string consulta = $@"
            UPDATE TURNO
            SET Fecha = '{fecha}', Hora = '{hora}', Observacion = '{observacion.Trim().Replace("'", "''")}'
            WHERE Id_Turno = {idTurno};";

            try
            {
                ds.ejecutarConsulta(consulta);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int eliminarTurno(int idTurno)
        {
            string consulta = $"UPDATE TURNO SET Estado = 0 WHERE Id_Turno = {idTurno};";
            try
            {
                ds.ejecutarConsulta(consulta);
                return 1; 
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
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
                Esp.Nombre AS Especialidad, -- Corregido: se llama 'Nombre' en tu DB
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
            INNER JOIN ESPECIALIDADES Esp ON Med.Id_Especialidad = Esp.Id_Especialidad";

            if (idMedico > 0)
            {
                consulta += " WHERE T.Id_Medico = " + idMedico;
            }

            return ds.obtenerTabla(consulta);
        }
        public int agregarTurno(int idMedico, string dniPaciente, string fecha, string hora, string observacion)
        {
            string consultaCompleta = $@"
            DECLARE @IdPersonaPaciente INT;
        
            SELECT @IdPersonaPaciente = Id_Persona 
            FROM PERSONA 
            WHERE DNI = '{dniPaciente.Trim()}';

            IF @IdPersonaPaciente IS NOT NULL
            BEGIN
                INSERT INTO TURNO (Id_Medico, Id_Persona, Fecha, Hora, Observacion, EstadoTurno, Estado)
                VALUES ({idMedico}, @IdPersonaPaciente, '{fecha}', '{hora}', '{observacion.Trim().Replace("'", "''")}', 'Pendiente', 1);
                SELECT 1 AS Resultado;
            END
            ELSE
            BEGIN
                SELECT -1 AS Resultado; 
            END";

            try
            {
                DataTable dt = ds.obtenerTabla(consultaCompleta);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["Resultado"]);
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
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
    }
}

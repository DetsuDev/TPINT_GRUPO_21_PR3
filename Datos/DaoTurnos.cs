using System;
using System.Data;
using Entidades;

namespace Datos
{
    public class DaoTurnos
    {
        AccesoDatos accesoDatos = new AccesoDatos();

        public DataTable getTablaTurnos()
        {
            string consulta = @"
                SELECT T.Id_Turno AS ID, P.DNI, (P.Nombre + ' ' + P.Apellido) AS Paciente,
                       (PM.Nombre + ' ' + PM.Apellido) AS Medico, E.Nombre AS Especialidad,
                       CONVERT(varchar(10), T.Fecha, 103) AS Fecha,
                       CONVERT(varchar(5), T.Hora, 108) AS Hora,
                       T.Observacion AS Observacion, T.EstadoTurno AS Estado
                FROM TURNO T
                INNER JOIN PERSONA P ON T.Id_Persona = P.Id_Persona
                INNER JOIN MEDICO M ON T.Id_Medico = M.Id_Medico
                INNER JOIN PERSONA PM ON M.Id_Persona = PM.Id_Persona
                INNER JOIN ESPECIALIDADES E ON M.Id_Especialidad = E.Id_Especialidad
                WHERE T.Estado = 1";
            return accesoDatos.obtenerTabla(consulta);
        }

        public DataTable getMedicosPorEspecialidad(int idEspecialidad)
        {
            string consulta = $@"
                SELECT M.Id_Medico, (P.Apellido + ', ' + P.Nombre + ' (' + M.Legajo_Medico + ')') AS Medico
                FROM MEDICO M
                INNER JOIN PERSONA P ON M.Id_Persona = P.Id_Persona
                WHERE M.Estado = 1 AND M.Id_Especialidad = {idEspecialidad}";
            return accesoDatos.obtenerTabla(consulta);
        }

        public DataTable getPacientesCombo()
        {
            string consulta = @"
                SELECT P.Id_Persona, (P.Apellido + ', ' + P.Nombre + ' - DNI ' + P.DNI) AS Paciente
                FROM PACIENTE PA
                INNER JOIN PERSONA P ON PA.Id_Persona = P.Id_Persona
                WHERE PA.Estado = 1";
            return accesoDatos.obtenerTabla(consulta);
        }

        public bool existeTurno(int idMedico, string fecha, string hora)
        {
            string consulta = $@"
                SELECT COUNT(*) AS Cant FROM TURNO
                WHERE Id_Medico = {idMedico} AND Fecha = '{fecha}' AND Hora = '{hora}' AND Estado = 1";
            DataTable dt = accesoDatos.obtenerTabla(consulta);
            return dt != null && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["Cant"]) > 0;
        }

        public int agregarTurno(Turno t)
        {
            string consulta = $@"
                INSERT INTO TURNO (Id_Medico, Id_Persona, Fecha, Hora, EstadoTurno, Estado)
                VALUES ({t.IdMedico}, {t.IdPersona}, '{t.Fecha}', '{t.Hora}', 'Pendiente', 1);";
            try
            {
                accesoDatos.ejecutarConsulta(consulta);
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int eliminarTurno(int idTurno)
        {
            string consulta = $"UPDATE TURNO SET Estado = 0 WHERE Id_Turno = {idTurno};";
            try
            {
                accesoDatos.ejecutarConsulta(consulta);
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public DataTable getRankingEspecialidades(string desde, string hasta)
        {
            string consulta = $@"
                SELECT E.Nombre AS Especialidad, COUNT(*) AS CantidadTurnos
                FROM TURNO T
                INNER JOIN MEDICO M ON T.Id_Medico = M.Id_Medico
                INNER JOIN ESPECIALIDADES E ON M.Id_Especialidad = E.Id_Especialidad
                WHERE T.Estado = 1 AND T.Fecha BETWEEN '{desde}' AND '{hasta}'
                GROUP BY E.Nombre
                ORDER BY CantidadTurnos DESC";
            return accesoDatos.obtenerTabla(consulta);
        }

        public DataTable getPresentismo(string desde, string hasta)
        {
            string consulta = $@"
                SELECT
                    SUM(CASE WHEN EstadoTurno = 'Presente' THEN 1 ELSE 0 END) AS Presentes,
                    SUM(CASE WHEN EstadoTurno = 'Ausente' THEN 1 ELSE 0 END) AS Ausentes
                FROM TURNO
                WHERE Estado = 1 AND Fecha BETWEEN '{desde}' AND '{hasta}'";
            return accesoDatos.obtenerTabla(consulta);
        }
    }
}

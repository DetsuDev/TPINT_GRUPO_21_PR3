using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class DaoMedicos
    {
        AccesoDatos accesoDatos = new AccesoDatos();
        public DataTable getTablaMedicos()
        {

            string consulta = "SELECT M.Id_Medico, M.Legajo_Medico, P.Nombre, P.Apellido, " +
                "E.Nombre AS Especialidad, P.Sexo, P.Nacionalidad, P.FechaNacimiento AS FechaNac, " +
                "P.Direccion, L.NombreLocalidad AS Localidad, PR.NombreProvincia AS Provincia, " +
                "P.CorreoElectronico AS Email, P.Telefono, U.Usuario, U.Contrasenia " +
                "FROM MEDICO M " +
                "INNER JOIN PERSONA P ON M.Id_Persona = P.Id_Persona " +
                "INNER JOIN ESPECIALIDADES E ON M.Id_Especialidad = E.Id_Especialidad " +
                "INNER JOIN LOCALIDADES L ON P.Id_Localidad = L.Id_Localidad " +
                "INNER JOIN PROVINCIA PR ON L.Id_Provincia = PR.Id_Provincia " +
                "LEFT JOIN USUARIO U ON P.Id_Persona = U.Id_Persona " +
                "WHERE M.Estado = 1";
            return accesoDatos.obtenerTabla(consulta);
        }

        public DataTable getMedicoPorId(int idMedico)
        {
            string consulta = $@"
                SELECT M.Id_Medico, M.Legajo_Medico, M.Id_Especialidad,
                       P.Id_Persona, P.DNI, P.Nombre, P.Apellido, P.Sexo, P.Nacionalidad,
                       P.FechaNacimiento, P.Direccion, P.CorreoElectronico, P.Telefono,
                       P.Id_Localidad, L.Id_Provincia,
                       U.Usuario, U.Contrasenia,
                       D.DiasDisponibles, D.HoraInicio, D.HoraFin
                FROM MEDICO M
                INNER JOIN PERSONA P ON M.Id_Persona = P.Id_Persona
                INNER JOIN LOCALIDADES L ON P.Id_Localidad = L.Id_Localidad
                LEFT JOIN USUARIO U ON P.Id_Persona = U.Id_Persona
                OUTER APPLY (SELECT TOP 1 DiasDisponibles, HoraInicio, HoraFin FROM DISPONIBILIDAD WHERE Id_Medico = M.Id_Medico) D
                WHERE M.Id_Medico = {idMedico}";
            return accesoDatos.obtenerTabla(consulta);
        }

        public int agregarMedico(Medico m)
        {
            string consulta = $@"
                DECLARE @NuevoIdPersona INT;
                INSERT INTO PERSONA (Id_Localidad, DNI, Nombre, Apellido, Sexo, Nacionalidad, FechaNacimiento, Direccion, CorreoElectronico, Telefono)
                VALUES ({m.IdLocalidad}, '{m.Dni}', '{m.Nombre}', '{m.Apellido}', '{m.Sexo}', '{m.Nacionalidad}', '{m.FechaNacimiento.ToString("yyyy-MM-dd")}', '{m.Direccion}', '{m.CorreoElectronico}', '{m.Telefono}');
                SET @NuevoIdPersona = SCOPE_IDENTITY();

                DECLARE @NuevoIdMedico INT;
                INSERT INTO MEDICO (Legajo_Medico, Id_Persona, Id_Especialidad, Estado)
                VALUES ('{m.LegajoMedico}', @NuevoIdPersona, {m.IdEspecialidad}, 1);
                SET @NuevoIdMedico = SCOPE_IDENTITY();

                INSERT INTO DISPONIBILIDAD (Id_Medico, DiasDisponibles, HoraInicio, HoraFin)
                VALUES (@NuevoIdMedico, '{m.DiasDisponibles}', '{m.HoraInicio}', '{m.HoraFin}');";

            if (!string.IsNullOrWhiteSpace(m.Usuario))
            {
                consulta += $@"
                INSERT INTO USUARIO (Id_Persona, Usuario, Contrasenia, Estado)
                VALUES (@NuevoIdPersona, '{m.Usuario}', '{m.Contrasenia}', 1);";
            }

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

        public int actualizarMedico(Medico m)
        {
            string consulta = $@"
                UPDATE PERSONA SET
                    Nombre = '{m.Nombre}',
                    Apellido = '{m.Apellido}',
                    Sexo = '{m.Sexo}',
                    Nacionalidad = '{m.Nacionalidad}',
                    FechaNacimiento = '{m.FechaNacimiento.ToString("yyyy-MM-dd")}',
                    Direccion = '{m.Direccion}',
                    CorreoElectronico = '{m.CorreoElectronico}',
                    Telefono = '{m.Telefono}',
                    Id_Localidad = {m.IdLocalidad}
                WHERE Id_Persona = {m.IdPersona};

                UPDATE MEDICO SET Id_Especialidad = {m.IdEspecialidad} WHERE Id_Medico = {m.IdMedico};

                DELETE FROM DISPONIBILIDAD WHERE Id_Medico = {m.IdMedico};
                INSERT INTO DISPONIBILIDAD (Id_Medico, DiasDisponibles, HoraInicio, HoraFin)
                VALUES ({m.IdMedico}, '{m.DiasDisponibles}', '{m.HoraInicio}', '{m.HoraFin}');";

            if (!string.IsNullOrWhiteSpace(m.Usuario))
            {
                string setPass = string.IsNullOrWhiteSpace(m.Contrasenia) ? "" : $", Contrasenia = '{m.Contrasenia}'";
                consulta += $@"
                IF EXISTS (SELECT 1 FROM USUARIO WHERE Id_Persona = {m.IdPersona})
                    UPDATE USUARIO SET Usuario = '{m.Usuario}'{setPass} WHERE Id_Persona = {m.IdPersona};
                ELSE
                    INSERT INTO USUARIO (Id_Persona, Usuario, Contrasenia, Estado)
                    VALUES ({m.IdPersona}, '{m.Usuario}', '{m.Contrasenia}', 1);";
            }

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
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class DaoPacientes
    {
        AccesoDatos accesoDatos = new AccesoDatos();

        public DataTable getTablaPacientes()
        {

            string consulta = "SELECT PA.Id_Paciente AS ID, P.DNI, P.Id_Localidad, P.Nombre, P.Apellido, P.Sexo, P.Nacionalidad, P.FechaNacimiento AS FechaNac, P.Direccion, L.NombreLocalidad AS Localidad, PR.NombreProvincia AS Provincia, P.CorreoElectronico AS Email, P.Telefono " +
                               "FROM PACIENTE Pa " +
                               "INNER JOIN PERSONA P ON PA.Id_Persona = P.Id_Persona " +
                               "INNER JOIN LOCALIDADES L ON P.Id_Localidad = L.Id_Localidad " +
                               "INNER JOIN PROVINCIA PR ON L.Id_Provincia = PR.Id_Provincia " +
                               "WHERE PA.Estado = 1";
            return accesoDatos.obtenerTabla(consulta);
        }
        public int agregarPaciente(Paciente pac)
        {
            string consultaCompleta = $@"
                DECLARE @NuevoIdPersona INT;
                INSERT INTO PERSONA (Id_Localidad, DNI, Nombre, Apellido, Sexo, Nacionalidad, FechaNacimiento, Direccion, CorreoElectronico, Telefono)
                VALUES ({pac.IdLocalidad}, '{pac.Dni}', '{pac.Nombre}', '{pac.Apellido}', '{pac.Sexo}', '{pac.Nacionalidad}', '{pac.FechaNacimiento.ToString("yyyy-MM-dd")}', '{pac.Direccion}', '{pac.CorreoElectronico}', '{pac.Telefono}');
                
                SET @NuevoIdPersona = SCOPE_IDENTITY();
                
                INSERT INTO PACIENTE (Id_Persona, Estado)
                VALUES (@NuevoIdPersona, 1);"; 

            try
            {
                accesoDatos.ejecutarConsulta(consultaCompleta); 
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int actualizarPaciente(Paciente pac)
        {
            string consulta = $@"
                UPDATE PERSONA 
                SET Nombre = '{pac.Nombre}', 
                    Apellido = '{pac.Apellido}', 
                    Sexo = '{pac.Sexo}', 
                    Nacionalidad = '{pac.Nacionalidad}', 
                    FechaNacimiento = '{pac.FechaNacimiento.ToString("yyyy-MM-dd")}', 
                    Direccion = '{pac.Direccion}', 
                    CorreoElectronico = '{pac.CorreoElectronico}', 
                    Telefono = '{pac.Telefono}',
                    Id_Localidad = {pac.IdLocalidad} 
                WHERE DNI = '{pac.Dni}';";
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
        public int eliminarPaciente(int idPaciente)
        {
            string consulta = $"UPDATE PACIENTE SET Estado = 0 WHERE Id_Paciente = {idPaciente};";
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

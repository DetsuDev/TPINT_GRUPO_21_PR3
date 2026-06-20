using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                "LEFT JOIN USUARIO U ON P.Id_Persona = U.Id_Persona";
            return accesoDatos.obtenerTabla(consulta);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                               "INNER JOIN PROVINCIA PR ON L.Id_Provincia = PR.Id_Provincia ";
            return accesoDatos.obtenerTabla(consulta);
        }
    }
}

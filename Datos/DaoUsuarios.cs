using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DaoUsuarios
    {

        AccesoDatos accesoDatos = new AccesoDatos();


        public DataTable getTablaUsuario(string usuario, string contrasenia)
        {
            string consulta = "SELECT * FROM USUARIO WHERE Usuario ='"+ usuario + "'AND Contrasenia ='"+ contrasenia + "'AND Estado=1";
            return accesoDatos.obtenerTabla(consulta);
        }
        public DataTable buscarUsuarioConRol(string username, string password)
        {
            string consulta = $@"
                SELECT 
                    U.Usuario, 
                    U.Estado,
                    P.Nombre, 
                    P.Apellido,
                    U.Rol
                FROM dbo.USUARIO U
                INNER JOIN dbo.PERSONA P ON U.Id_Persona = P.Id_Persona
                LEFT JOIN dbo.MEDICO M ON P.Id_Persona = M.Id_Persona
                WHERE U.Usuario = '{username}' AND U.Contrasenia = '{password}';";

            return accesoDatos.obtenerTabla(consulta);
        }
    }
}

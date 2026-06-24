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
    }
}

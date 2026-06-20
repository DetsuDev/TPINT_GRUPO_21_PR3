using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DaoLocalidades
    {
        AccesoDatos accesoDatos = new AccesoDatos();
        public DataTable getTablaLocalidades()
        {
            string consulta = "SELECT * FROM LOCALIDADES";
            return accesoDatos.obtenerTabla(consulta);
        }
    }
}

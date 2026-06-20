using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DaoProvincias
    {
        AccesoDatos accesoDatos = new AccesoDatos();

        public DataTable getTablaProvincias()
        {
            string consulta = "SELECT * FROM PROVINCIA";
            return accesoDatos.obtenerTabla(consulta);
        }
    }
}


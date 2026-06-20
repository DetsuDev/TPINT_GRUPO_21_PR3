using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DaoEspecialidades
    {
        AccesoDatos accesoDatos = new AccesoDatos();

        public DataTable getTablaEspecialidades()
        {
            string consulta = "SELECT * FROM ESPECIALIDADES";
            return accesoDatos.obtenerTabla(consulta);
        }
    }
}

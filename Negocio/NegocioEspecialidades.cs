using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioEspecialidades
    {
        public DataTable getTabla()
        {
            DaoEspecialidades dao = new DaoEspecialidades();
            return dao.getTablaEspecialidades();
        }
    }
}

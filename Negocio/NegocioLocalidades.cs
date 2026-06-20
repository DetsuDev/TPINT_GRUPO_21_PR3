using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioLocalidades
    {
        public DataTable getTabla()
        {
            DaoLocalidades dao = new DaoLocalidades();
            return dao.getTablaLocalidades();
        }
    }
}

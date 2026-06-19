using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioMedicos
    {
        public DataTable getTabla()
        {
            DaoMedicos dao = new DaoMedicos();
            return dao.getTablaMedicos();
        }
    }
}

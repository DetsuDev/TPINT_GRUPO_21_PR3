using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioPacientes
    {
        public DataTable getTabla()
        {
            DaoPacientes dao = new DaoPacientes();
            return dao.getTablaPacientes();
        }
    }
}

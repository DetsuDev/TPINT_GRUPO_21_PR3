using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Negocio
{
    public class NegocioMedicos
    {
        private DaoMedicos dao = new DaoMedicos();

        public DataTable getTabla()
        {
            return dao.getTablaMedicos();
        }

        public DataTable getMedicoPorId(int idMedico)
        {
            return dao.getMedicoPorId(idMedico);
        }

        public bool guardarMedico(Medico m)
        {
            return dao.agregarMedico(m) > 0;
        }

        public bool modificarMedico(Medico m)
        {
            return dao.actualizarMedico(m) > 0;
        }

        public bool eliminarMedico(Medico m)
        {
            return dao.eliminarMedico(m) > 0;
        }

        public DataTable getDiasDisponiblesPorEspecialidad()
        {

        }

        public DataTable getDiasDisponiblesPorMedico()
        {

        }

    }
}

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
    public class NegocioPacientes
    {
        private DaoPacientes dao = new DaoPacientes();

        public DataTable getTabla()
        {
            return dao.getTablaPacientes();
        }

        public bool guardarPaciente(Paciente pac)
        {
            int filasAfectadas = dao.agregarPaciente(pac);
            return filasAfectadas > 0;
        }
        public bool modificarPaciente(Paciente pac)
        {
            int filasAfectadas = dao.actualizarPaciente(pac);
            return filasAfectadas > 0;
        }
        public bool eliminarPaciente(int idPaciente)
        {
            return dao.eliminarPaciente(idPaciente) > 0;
        }
    }
}

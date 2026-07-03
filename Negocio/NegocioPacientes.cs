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
        private DaoPacientes daoPacientes = new DaoPacientes();
        public DataTable getTabla()
        {
            return daoPacientes.getTablaPacientes();
        }

        public DataTable getPacientePorId(int idPaciente)
        {
            return daoPacientes.getPacientePorId(idPaciente);
        }

        public bool guardarPaciente(Paciente pac)
        {
            int filasAfectadas = daoPacientes.agregarPaciente(pac);
            return filasAfectadas > 0;
        }
        public bool modificarPaciente(Paciente pac)
        {
            int filasAfectadas = daoPacientes.actualizarPaciente(pac);
            return filasAfectadas > 0;
        }
        public bool eliminarPaciente(int idPaciente)
        {
            return daoPacientes.eliminarPaciente(idPaciente) > 0;
        }
    }
}

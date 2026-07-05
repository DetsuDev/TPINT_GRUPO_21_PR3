using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NegocioTurnos
    {
        private DaoTurnos daoTurnos = new DaoTurnos();
        public DataTable getTabla(int idMedico = 0)
        {
            DaoTurnos dao = new DaoTurnos();
            return dao.getTabla(idMedico);
        }
        public DataTable obtenerEspecialidadesAlta()
        {
            return daoTurnos.obtenerEspecialidadesAlta();
        }

        public DataTable obtenerMedicosDisponibles(int idEspecialidad, string letraDia, string horaTipeada)
        {
            return daoTurnos.obtenerMedicosDisponibles(idEspecialidad, letraDia, horaTipeada);
        }
        public bool guardarTurno(int idMedico, string dniPaciente, string fecha, string hora, string observacion)
        {
            int filasAfectadas = daoTurnos.agregarTurno(idMedico, dniPaciente, fecha, hora, observacion);
            return filasAfectadas > 0;
        }
    }
}

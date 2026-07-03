using System.Data;
using Datos;
using Entidades;

namespace Negocio
{
    public class NegocioTurnos
    {
        private DaoTurnos dao = new DaoTurnos();

        public DataTable getTabla()
        {
            return dao.getTablaTurnos();
        }

        public DataTable getMedicosPorEspecialidad(int idEspecialidad)
        {
            return dao.getMedicosPorEspecialidad(idEspecialidad);
        }

        public DataTable getPacientesCombo()
        {
            return dao.getPacientesCombo();
        }

        public bool existeTurno(int idMedico, string fecha, string hora)
        {
            return dao.existeTurno(idMedico, fecha, hora);
        }

        public bool guardarTurno(Turno t)
        {
            return dao.agregarTurno(t) > 0;
        }

        public bool eliminarTurno(int idTurno)
        {
            return dao.eliminarTurno(idTurno) > 0;
        }

        public DataTable getRanking(string desde, string hasta)
        {
            return dao.getRankingEspecialidades(desde, hasta);
        }

        public DataTable getPresentismo(string desde, string hasta)
        {
            return dao.getPresentismo(desde, hasta);
        }
    }
}

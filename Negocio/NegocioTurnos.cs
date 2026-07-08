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
            return daoTurnos.getTabla(idMedico);
        }

        public DataTable obtenerEspecialidadesAlta()
        {
            return daoTurnos.obtenerEspecialidadesAlta();
        }

        public DataTable obtenerMedicosDisponibles(int idEspecialidad, string letraDia, string horaTipeada)
        {
            return daoTurnos.obtenerMedicosDisponibles(idEspecialidad, letraDia, horaTipeada);
        }

        public int guardarTurno(int idMedico, string dniPaciente, string fecha, string hora, string observacion)
        {
            if (!daoTurnos.existePaciente(dniPaciente))
            {
                return -1;
            }

            DataTable dtPacienteOcupado = daoTurnos.verificarTurnoPaciente(dniPaciente, fecha, hora);
            if (dtPacienteOcupado != null && dtPacienteOcupado.Rows.Count > 0)
            {
                return -2;
            }

            DataTable dtMedicoOcupado = daoTurnos.verificarTurnoMedico(idMedico, fecha, hora);
            if (dtMedicoOcupado != null && dtMedicoOcupado.Rows.Count > 0)
            {
                return -3;
            }

            bool guardadoCorrecto = daoTurnos.agregarTurno(idMedico, dniPaciente, fecha, hora, observacion);
            return guardadoCorrecto ? 1 : -4;
        }
        public float calcularPresentismo(DateTime fechaInicio, DateTime fechaFin)
        {
            return daoTurnos.calcularPresentismo(fechaInicio, fechaFin);
        }
        public bool eliminarTurno(int idTurno)
        {
            return daoTurnos.eliminarTurno(idTurno) > 0;
        }
    }
}
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

        public int[] sumatoriaPresentismo(DateTime fechaInicio, DateTime fechaFin)
        {
            int[] presentismo = new int[2];
            int totalTurnos = 0;
            int totalConfirmados = 0;
            DataTable dt = daoTurnos.getPresentismo(fechaInicio, fechaFin);

            foreach (DataRow row in dt.Rows)
            {
                totalTurnos++;
                
                if (row["EstadoTurno"] != DBNull.Value && row["EstadoTurno"].ToString() == "Confirmado")
                {
                    totalConfirmados++;
                }
            }
            presentismo[0] = totalTurnos;
            presentismo[1] = totalConfirmados;

            return presentismo;
        }


        public float calcularPresentismo(DateTime fechaInicio, DateTime fechaFin)
        {
            int[] presentismo = sumatoriaPresentismo(fechaInicio, fechaFin);
            int totalTurnos = presentismo[0];
            int totalConfirmados = presentismo[1];
            if (totalTurnos == 0)
            {
                return 0;
            }
            return (float)Math.Round((float)totalConfirmados / totalTurnos * 100, 2);
        }


        public bool eliminarTurno(int idTurno)
        {
            return daoTurnos.eliminarTurno(idTurno) > 0;
        }
    }
}
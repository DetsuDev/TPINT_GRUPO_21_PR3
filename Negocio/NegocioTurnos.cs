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

        public float[] calcularPresentismo(DateTime fechaInicio, DateTime fechaFin)
        {
            float[] presentismo = new float[3];
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
            presentismo[0] = (float)Math.Round((float)totalConfirmados / totalTurnos * 100, 2);
            presentismo[1] = totalConfirmados;
            presentismo[2] = totalTurnos;
            return presentismo;
        }



        public bool eliminarTurno(int idTurno)
        {
            return daoTurnos.eliminarTurno(idTurno) > 0;
        }
    }
}
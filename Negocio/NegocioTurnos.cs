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
            DataTable dt = daoTurnos.calcularPresentismo(fechaInicio, fechaFin);

            ///int size = 3;
           /// float calculoPresentismo[size] = new float(); , arreglate, todo tuyo campeon
            foreach (var x in dt)
            {
               /// if (dt.Rows.Count > 0 && dt.Rows[0]["EstadoTurno"] != DBNull.Value)
                
                     /// 1, almacena el total, 2, almacena los presentes, 3, almacena los ausentes. y que retorne el float y despues lo trabajas
                
            }

             
            return daoTurnos.calcularPresentismo(fechaInicio, fechaFin);
        }
        public bool eliminarTurno(int idTurno)
        {
            return daoTurnos.eliminarTurno(idTurno) > 0;
        }
    }
}
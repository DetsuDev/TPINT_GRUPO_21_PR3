using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

        public DataTable getEspecialidadesAlta()
        {
            return daoTurnos.getEspecialidadesAlta();
        }

        public DataTable getMedicosDisponibles(int idEspecialidad, string letraDia, string horaTipeada)
        {
            return daoTurnos.getMedicosDisponibles(idEspecialidad, letraDia, horaTipeada);
        }

        public DataTable getDisponibilidadPorMedico(int idMedico)
        {
            return daoTurnos.getDisponibilidadPorMedico(idMedico);
        }
        public bool verificarDia(int idMedico, DateTime dia)
        {
            DataTable dt = daoTurnos.verificarDia(idMedico, dia);
            if (dt != null && dt.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public DataTable filtrarRanking(string minFechaString, string maxFechaString)
        {

            DataTable tablaMedicos = getTabla();

            DataTable negocioEspecialidad = new DataTable();

            negocioEspecialidad.Columns.Add("Especialidad", typeof(string));
            negocioEspecialidad.Columns.Add("Cantidad", typeof(int));


            string formato = "yyyy-MM-dd";

            DateTime.TryParseExact(minFechaString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime minFecha);
            DateTime.TryParseExact(maxFechaString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime maxFecha);


            foreach (DataRow dr in tablaMedicos.Rows)
            {
                DateTime fecha = (DateTime)dr["FechaDateTime"];

                if (fecha >= minFecha && fecha <= maxFecha)
                {
                    string especialidad = dr["Especialidad"].ToString();
                    bool encontrada = false;

                    foreach (DataRow fila in negocioEspecialidad.Rows)
                    {
                        if (fila["Especialidad"].ToString() == especialidad)
                        {
                            fila["Cantidad"] = Convert.ToInt32(fila["Cantidad"]) + 1;
                            encontrada = true;
                            break;
                        }
                    }

                    if (!encontrada)
                    {
                        negocioEspecialidad.Rows.Add(especialidad, 1);
                    }
                }
            }



            return negocioEspecialidad;
        }


        public string obtenerDiasDisp(int idMedico)
        {
            NegocioTurnos neg = new NegocioTurnos();
            DataTable dt = neg.getDisponibilidadPorMedico(idMedico);

            string diasDisponibles = "";

            foreach (DataRow dr in dt.Rows)
            {
                string dias = dr["DiasDisponibles"].ToString();

                foreach (char dia in dias)
                {
                    if (!diasDisponibles.Contains(dia))
                    {
                        diasDisponibles += dia;
                    }
                }
            }
            return diasDisponibles;
        }

        public bool verificarTurnoMedico(int idMedico, string fecha, string hora)
        {
            DataTable dt = daoTurnos.verificarTurnoMedico(idMedico, fecha, hora);
            if (dt != null && dt.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool verificarTurnoPaciente(string dniPaciente, string fecha, string hora)
        {
            DataTable dt = daoTurnos.verificarTurnoPaciente(dniPaciente, fecha, hora);
            if (dt != null && dt.Rows.Count > 0)
            {
                return false;
            }
            return true;
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

            DataTable dtMedico = daoTurnos.getTabla(idMedico);
            if (Convert.ToInt32(dtMedico.Rows[0]["DNIMedico"]) == Convert.ToInt32(dniPaciente))
            {
                return -4;
            }

            bool guardadoCorrecto = daoTurnos.agregarTurno(idMedico, dniPaciente, fecha, hora, observacion);
            return guardadoCorrecto ? 1 : -5;
        }

        public int[] calcularPresentismoPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            int[] cPresentismo = new int[4];
            int totalTurnos = 0;
            int totalPresentes = 0;
            int totalPendientes = 0;
            int totalAusentes = 0;
            DataTable dt = daoTurnos.getPresentismo(fechaInicio, fechaFin);

            foreach (DataRow row in dt.Rows)
            {
                totalTurnos++;

                if (row["EstadoTurno"] != DBNull.Value && row["EstadoTurno"].ToString() == "Presente")
                {
                    totalPresentes++;
                }
                if (row["EstadoTurno"] != DBNull.Value && row["EstadoTurno"].ToString() == "Pendiente")
                {
                    totalPendientes++;
                }
                if (row["EstadoTurno"] != DBNull.Value && row["EstadoTurno"].ToString() == "Ausente")
                {
                    totalAusentes++;
                }
            }

            cPresentismo[0] = totalPresentes;
            cPresentismo[1] = totalPendientes;
            cPresentismo[2] = totalAusentes;
            cPresentismo[3] = totalTurnos;
            return cPresentismo;
        }


        public DataTable obtenerTurnoPorId(int idTurno)
        {
            return daoTurnos.obtenerTurnoPorId(idTurno);
        }

        public bool marcarPresentismo(int idTurno, string estado, string observacion)
        {
            return daoTurnos.marcarPresentismo(idTurno, estado, observacion);
        }

        public int modificarTurno(int idTurno, int idMedico, string dniPaciente, string fecha, string hora, string observacion)
        {
            foreach (DataRow r in daoTurnos.verificarTurnoPaciente(dniPaciente, fecha, hora).Rows)
                if (Convert.ToInt32(r["Id_Turno"]) != idTurno) return -2;

            foreach (DataRow r in daoTurnos.verificarTurnoMedico(idMedico, fecha, hora).Rows)
                if (Convert.ToInt32(r["Id_Turno"]) != idTurno) return -3;

            return daoTurnos.actualizarTurno(idTurno, fecha, hora, observacion) ? 1 : 0;
        }

        public bool eliminarTurno(int idTurno)
        {
            return daoTurnos.eliminarTurno(idTurno) > 0;
        }

        public DataTable getTurnos(string dni, string paciente, string fecha, string estado)
        {
            return daoTurnos.getTurnosFiltrados(dni,paciente,fecha,estado);
        }
    }
}
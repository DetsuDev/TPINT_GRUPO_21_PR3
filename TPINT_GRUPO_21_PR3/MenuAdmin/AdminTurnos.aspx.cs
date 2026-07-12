using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3.MenuAdmin
{
    public partial class GestionTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["UsuarioLogueado"];
            Page.MaintainScrollPositionOnPostBack = true;

            if (user == null || user.Rol != "A")
            {
                Response.Redirect("~/SesionInvalida.html");
            }
            if (!IsPostBack)
            {
                lblNombreUsuario.Text = user.persona.Nombre + " " + user.persona.Apellido;
                divEliminar.Visible = false;
                divFormulario.Visible = false; 
                fullscreenOverlay.Style["display"] = "none";
                CargarGrillaTurnos();
                CargarEspecialidadesAlta();
                CargarMedicosAlta();
            }
        }

        private void CargarGrillaTurnos()
        {
            Negocio.NegocioTurnos negocioTurnos = new Negocio.NegocioTurnos();
            DataTable dt = negocioTurnos.getTabla();

            List<string> filtros = new List<string>();
            if (!string.IsNullOrWhiteSpace(txtBuscarDni.Text))
                filtros.Add("DNI LIKE '%" + txtBuscarDni.Text.Trim().Replace("'", "''") + "%'");
            if (!string.IsNullOrWhiteSpace(txtBuscarPaciente.Text))
                filtros.Add("Paciente LIKE '%" + txtBuscarPaciente.Text.Trim().Replace("'", "''") + "%'");
            if (!string.IsNullOrWhiteSpace(txtBuscarFecha.Text))
                filtros.Add("Fecha LIKE '%" + txtBuscarFecha.Text.Trim().Replace("'", "''") + "%'");
            if (!string.IsNullOrEmpty(ddlBuscarEstado.SelectedValue))
                filtros.Add("Estado = '" + ddlBuscarEstado.SelectedValue.Replace("'", "''") + "'");

            DataView dv = dt.DefaultView;
            if (filtros.Count > 0)
            {
                dv.RowFilter = string.Join(" AND ", filtros);
            }

            gvGestionTurnos.DataSource = dv;
            gvGestionTurnos.DataBind();
        }

        private void CargarEspecialidadesAlta()
        {
            NegocioTurnos negocioTurnos = new NegocioTurnos();
            DataTable dt = negocioTurnos.obtenerEspecialidadesAlta();

            ddlAltaEspecialidad.DataSource = dt;
            ddlAltaEspecialidad.DataTextField = "Nombre";
            ddlAltaEspecialidad.DataValueField = "Id_Especialidad";
            ddlAltaEspecialidad.DataBind();
            ddlAltaEspecialidad.Items.Insert(0, new ListItem("-- Seleccione Especialidad --", "0"));
        }

        protected void CargarAltaMedicosSegunEsp()
        {

            if (ddlAltaEspecialidad.SelectedIndex == 0)
            {
                CargarMedicosAlta();
                return;
            }

            NegocioMedicos neg = new NegocioMedicos();

            ddlAltaMedico.Items.Clear();

            int idEspecialidad = Convert.ToInt32(ddlAltaEspecialidad.SelectedValue);

            DataTable dt = neg.getTablaPorEsp(idEspecialidad);
            ddlAltaMedico.DataSource = dt;
            ddlAltaMedico.DataValueField = "Id_Medico";
            ddlAltaMedico.DataTextField = "NombreApellido";
            ddlAltaMedico.DataBind();

        }

        protected string obtenerDiasDisp()
        {
            NegocioTurnos neg = new NegocioTurnos();
            int idMedico = Convert.ToInt32(ddlAltaEspecialidad.SelectedValue);
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

        protected void obtenerHorariosDisponibles()
        {
            if (ddlAltaEspecialidad.SelectedIndex == 0 || cFechasTurnos.SelectedDates.Count == 0)
            {
                ddlHora.Items.Clear();
                ddlHora.Items.Insert(0, new ListItem("Seleccione Medico y Fecha", "0"));
                return;
            }
            int idMedico = Convert.ToInt32(ddlAltaMedico.SelectedValue);
            NegocioTurnos negTurnos = new NegocioTurnos();
            DataTable dtHorarios = negTurnos.getDisponibilidadPorMedico(idMedico);

            TimeSpan horaInicioTS = (TimeSpan)dtHorarios.Rows[0]["HoraInicio"];
            TimeSpan horaFinTS = (TimeSpan)dtHorarios.Rows[0]["HoraFin"];

            int horaInicio = horaInicioTS.Hours;
            int horaFin = horaFinTS.Hours;

            ddlHora.Items.Clear();
            for (int hora = horaInicio; hora < horaFin; hora++)
            {
                string horaBase = hora.ToString("D2");

                string hora00 = horaBase + ":00";
                if (negTurnos.verificarTurnoMedico(idMedico, cFechasTurnos.SelectedDate.ToString("yyyy-MM-dd"), hora00))
                {
                    ddlHora.Items.Add(new ListItem(hora00, hora00));
                }

                string hora30 = horaBase + ":30";
                if (negTurnos.verificarTurnoMedico(idMedico, cFechasTurnos.SelectedDate.ToString("yyyy-MM-dd"), hora30))
                {
                    ddlHora.Items.Add(new ListItem(hora30, hora30));
                }
            }

            return;
        }

        protected void ddlAltaEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAltaMedicosSegunEsp();
            //CargarHorarios(ddlAltaEspecialidad.SelectedIndex);
            /*
            ddlAltaMedico.Items.Clear();

            int idEspecialidad = Convert.ToInt32(ddlAltaEspecialidad.SelectedValue);
            if (idEspecialidad == 0 || cFechasTurnos.SelectedDates.Count == 0 || string.IsNullOrWhiteSpace(txtHora.Text))
            {
                ddlAltaMedico.Items.Insert(0, new ListItem("Complete Fecha, Hora y Especialidad", "0"));
                return;
            }

            try
            {
                DateTime fechaSeleccionada = cFechasTurnos.SelectedDate;
                string letraDia = ObtenerLetraDiaSemana(fechaSeleccionada);
                string horaTipeada = txtHora.Text.Trim();

                Negocio.NegocioTurnos negocioTurnos = new Negocio.NegocioTurnos();
                DataTable dtMedicos = negocioTurnos.obtenerMedicosDisponibles(idEspecialidad, letraDia, horaTipeada);

                if (dtMedicos != null && dtMedicos.Rows.Count > 0)
                {
                    ddlAltaMedico.DataSource = dtMedicos;
                    ddlAltaMedico.DataTextField = "NombreCompleto";
                    ddlAltaMedico.DataValueField = "Id_Medico";
                    ddlAltaMedico.DataBind();
                    ddlAltaMedico.Items.Insert(0, new ListItem("-- Seleccione Médico --", "0"));
                }
                else
                {
                    ddlAltaMedico.Items.Insert(0, new ListItem("No hay médicos disponibles en ese horario", "0"));
                }
            }
            catch (Exception)
            {
                ddlAltaMedico.Items.Insert(0, new ListItem("Error al calcular disponibilidad", "0"));
            }*/
        }

        protected void cFechasTurnos_DayRender(object sender, DayRenderEventArgs e)
        {

            DateTime minDate = DateTime.Now;
            string diasDisponibles = obtenerDiasDisp();
            int idMedico = Convert.ToInt32(ddlAltaMedico.SelectedValue);
            NegocioTurnos neg = new NegocioTurnos();
            bool diaDisponible = false;
            char dia = ' ';

            switch (e.Day.Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dia = 'L';
                    break;

                case DayOfWeek.Tuesday:
                    dia = 'M';
                    break;

                case DayOfWeek.Wednesday:
                    dia = 'X';
                    break;

                case DayOfWeek.Thursday:
                    dia = 'J';
                    break;

                case DayOfWeek.Friday:
                    dia = 'V';
                    break;
            }

            DataTable dtHorarios = neg.getDisponibilidadPorMedico(idMedico);

            if (dtHorarios == null || dtHorarios.Rows.Count == 0)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
                e.Cell.BackColor = System.Drawing.Color.LightGray;
                return;
            }

            TimeSpan horaInicioTS = (TimeSpan)dtHorarios.Rows[0]["HoraInicio"];
            TimeSpan horaFinTS = (TimeSpan)dtHorarios.Rows[0]["HoraFin"];

            int horaInicio = horaInicioTS.Hours;
            int horaFin = horaFinTS.Hours;

            for (int hora = horaInicio; hora < horaFin; hora++)
            {
                string horaBase = hora.ToString("D2");

                string hora00 = horaBase + ":00";

                if (neg.verificarTurnoMedico(idMedico, e.Day.Date.ToString("yyyy-MM-dd"), hora00))
                {
                    diaDisponible = true;
                    break;
                }

                string hora30 = horaBase + ":30";

                if (neg.verificarTurnoMedico(idMedico,  e.Day.Date.ToString("yyyy-MM-dd"), hora30))
                {
                    diaDisponible = true;
                    break;
                }
            }

            if (!diasDisponibles.Contains(dia) || e.Day.Date.AddDays(-1) < minDate)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
                e.Cell.BackColor = System.Drawing.Color.LightGray;
            }
            else if (!diaDisponible)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Red;
                e.Cell.BackColor = System.Drawing.Color.LightSalmon;
            }


        }

        protected void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            
            lblMensajeGeneral.Visible = false;
            lblMensajeErrorPopup.Visible = false;
            lblMensajeErrorPopup.Text = "";

            hCargarTurno.InnerText = "Cargar Nuevo Turno";
            btnCargar.Text = "Agendar Turno";

            txtPaciente.Enabled = true;
            ddlAltaEspecialidad.Enabled = true;
            ddlAltaMedico.Enabled = true;
            rfvDni.Enabled = true;
            revDni.Enabled = true;
            rfvEspecialidad.Enabled = true;
            rfvMedico.Enabled = true;
            hdnIdTurnoEditar.Value = "";

            txtPaciente.Text = "";
            cFechasTurnos.SelectedDates.Clear();
            ddlHora.Items.Clear();
            ddlHora.Items.Insert(0, new ListItem("Seleccione Medico y Fecha", "0"));
            txtHora.Text = "";
            txtHora.Visible = false;
            txtObservacionAlta.Text = "";
            ddlAltaEspecialidad.SelectedIndex = 0;
            ddlAltaMedico.SelectedIndex = 0;

            fullscreenOverlay.Style["display"] = "block"; 
            divFormulario.Visible = true;                 
            divEliminar.Visible = false;
        }

        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            txtPaciente.Text = string.Empty;
            txtHora.Text = string.Empty;
            txtObservacionAlta.Text = string.Empty;
            ddlAltaEspecialidad.SelectedIndex = 0;

            lblMensajeErrorPopup.Visible = false;
            lblMensajeErrorPopup.Text = "";

            txtPaciente.Enabled = true;
            ddlAltaEspecialidad.Enabled = true;
            ddlAltaMedico.Enabled = true;
            CargarMedicosAlta();
            rfvDni.Enabled = true;
            revDni.Enabled = true;
            rfvEspecialidad.Enabled = true;
            rfvMedico.Enabled = true;
            hdnIdTurnoEditar.Value = "";

            fullscreenOverlay.Style["display"] = "none";
            divFormulario.Visible = false;
        }

        private string ObtenerLetraDiaSemana(DateTime fecha)
        {
            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday: return "L";
                case DayOfWeek.Tuesday: return "M";
                case DayOfWeek.Wednesday: return "X";
                case DayOfWeek.Thursday: return "J";
                case DayOfWeek.Friday: return "V";
                default: return "Z";
            }
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            lblMensajeGeneral.Visible = false;
            lblMensajeGeneral.Text = "";
            lblMensajeErrorPopup.Visible = false;
            lblMensajeErrorPopup.Text = "";

            if (!string.IsNullOrEmpty(hdnIdTurnoEditar.Value))
            {
                GuardarModificacion();
                return;
            }

            int idMedico = Convert.ToInt32(ddlAltaMedico.SelectedValue);
            string dniPaciente = txtPaciente.Text.Trim();
            string fecha = cFechasTurnos.SelectedDate.ToString();
            string hora = txtHora.Text.Trim();
            string observacion = txtObservacionAlta.Text.Trim();

            NegocioTurnos negocioTurnos = new NegocioTurnos();
            int resultado = negocioTurnos.guardarTurno(idMedico, dniPaciente, fecha, hora, observacion);

            if (resultado == 1)
            {
                lblMensajeGeneral.Text = "¡Turno agendado con éxito!";
                lblMensajeGeneral.ForeColor = System.Drawing.Color.Green;
                lblMensajeGeneral.Visible = true;

                txtPaciente.Text = "";
                cFechasTurnos.SelectedDates.Clear();
                txtHora.Text = "";
                txtObservacionAlta.Text = "";
                ddlAltaEspecialidad.SelectedIndex = 0;
                ddlAltaMedico.Items.Clear();

                fullscreenOverlay.Style["display"] = "none";

                CargarGrillaTurnos();
            }
            else
            {
                fullscreenOverlay.Style["display"] = "flex";
                lblMensajeErrorPopup.Visible = true;

                if (resultado == -1) lblMensajeErrorPopup.Text = "El DNI ingresado no pertenece a ningún paciente registrado.";
                else if (resultado == -2) lblMensajeErrorPopup.Text = "El paciente ya posee un turno asignado para esa misma fecha y hora.";
                else if (resultado == -3) lblMensajeErrorPopup.Text = "El médico seleccionado ya se encuentra ocupado con otro turno en esa misma fecha y hora.";
                else lblMensajeErrorPopup.Text = "Hubo un error inesperado al procesar el alta del turno.";
            }
        }

        protected void txtFechaHora_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnIdTurnoEditar.Value)) return;
            ddlAltaEspecialidad.SelectedIndex = 0;
            ddlAltaMedico.Items.Clear();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            gvGestionTurnos.PageIndex = 0;
            CargarGrillaTurnos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarDni.Text = "";
            txtBuscarPaciente.Text = "";
            txtBuscarFecha.Text = "";
            ddlBuscarEstado.SelectedValue = "";
            gvGestionTurnos.PageIndex = 0;
            CargarGrillaTurnos();
        }

        protected void gvGestionTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGestionTurnos.PageIndex = e.NewPageIndex;
            CargarGrillaTurnos();
        }

        protected void gvGestionTurnos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
            string idTurno = gvGestionTurnos.Rows[e.NewEditIndex].Cells[1].Text;
            AbrirEdicion(Convert.ToInt32(idTurno));
        }

        private void AbrirEdicion(int idTurno)
        {
            NegocioTurnos negocio = new NegocioTurnos();
            DataTable dt = negocio.obtenerTurnoPorId(idTurno);
            if (dt == null || dt.Rows.Count == 0) return;
            DataRow r = dt.Rows[0];

            hdnIdTurnoEditar.Value = idTurno.ToString();

            hCargarTurno.InnerText = "Modificar Turno";
            btnCargar.Text = "Guardar Cambios";

            txtPaciente.Text = r["DNI"].ToString();
            cFechasTurnos.SelectedDate = Convert.ToDateTime(r["Fecha"]);
            txtHora.Text = r["Hora"].ToString();
            txtObservacionAlta.Text = r["Observacion"] == DBNull.Value ? "" : r["Observacion"].ToString();

            CargarEspecialidadesAlta();
            ddlAltaEspecialidad.SelectedValue = r["Id_Especialidad"].ToString();

            ddlAltaMedico.Items.Clear();
            ddlAltaMedico.Items.Add(new ListItem(r["Medico"].ToString(), r["Id_Medico"].ToString()));

            ocultarEdicion();

        }

        protected void CargarMedicosAlta()
        {
            NegocioMedicos neg = new NegocioMedicos();
            ddlAltaMedico.DataSource = neg.getTablaINA();
            ddlAltaMedico.DataTextField = "NombreApellido";
            ddlAltaMedico.DataValueField = "Id_Medico";
            ddlAltaMedico.DataBind();
            ddlAltaMedico.Items.Insert(0, new ListItem("-- Seleccione Medico --", "0"));
        }

        private void ocultarEdicion()
        {

            txtPaciente.Enabled = false;
            ddlAltaEspecialidad.Enabled = false;
            ddlAltaMedico.Enabled = false;
            rfvDni.Enabled = false;
            revDni.Enabled = false;
            rfvEspecialidad.Enabled = false;
            rfvMedico.Enabled = false;

            lblMensajeGeneral.Visible = false;
            lblMensajeErrorPopup.Visible = false;
            lblMensajeErrorPopup.Text = "";
            fullscreenOverlay.Style["display"] = "block";
            divFormulario.Visible = true;
            divEliminar.Visible = false;
        }

        private void GuardarModificacion()
        {
            int idTurno = Convert.ToInt32(hdnIdTurnoEditar.Value);

            Negocio.NegocioTurnos negocio = new Negocio.NegocioTurnos();
            DataTable dt = negocio.obtenerTurnoPorId(idTurno);
            if (dt == null || dt.Rows.Count == 0) return;
            DataRow r = dt.Rows[0];

            int idMedico = Convert.ToInt32(r["Id_Medico"]);
            string dniPaciente = r["DNI"].ToString();
            string fecha = cFechasTurnos.SelectedDate.ToString();
            string hora = txtHora.Text.Trim();
            string observacion = txtObservacionAlta.Text.Trim();

            int resultado = negocio.modificarTurno(idTurno, idMedico, dniPaciente, fecha, hora, observacion);

            if (resultado == 1)
            {
                lblMensajeGeneral.Text = "¡El turno se modificó correctamente!";
                lblMensajeGeneral.ForeColor = System.Drawing.Color.Green;
                lblMensajeGeneral.Visible = true;

                hdnIdTurnoEditar.Value = "";
                txtPaciente.Enabled = true;
                ddlAltaEspecialidad.Enabled = true;
                ddlAltaMedico.Enabled = true;
                rfvDni.Enabled = true;
                revDni.Enabled = true;
                rfvEspecialidad.Enabled = true;
                rfvMedico.Enabled = true;

                fullscreenOverlay.Style["display"] = "none";
                divFormulario.Visible = false;
                CargarGrillaTurnos();
            }
            else
            {
                fullscreenOverlay.Style["display"] = "flex";
                lblMensajeErrorPopup.Visible = true;
                if (resultado == -2) lblMensajeErrorPopup.Text = "El paciente ya posee un turno para esa misma fecha y hora.";
                else if (resultado == -3) lblMensajeErrorPopup.Text = "El médico ya se encuentra ocupado en esa misma fecha y hora.";
                else lblMensajeErrorPopup.Text = "Hubo un error inesperado al modificar el turno.";
            }
        }

        protected void gvGestionTurnos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGestionTurnos.EditIndex = -1;
            CargarGrillaTurnos();
        }

        protected void gvGestionTurnos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;

            lblMensajeGeneral.Visible = false;

            string idTurno = gvGestionTurnos.Rows[e.RowIndex].Cells[1].Text;
            hdnIdTurnoEliminar.Value = idTurno;

            fullscreenOverlay.Style["display"] = "block"; 
            divEliminar.Visible = true;                  
            divFormulario.Visible = false;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnIdTurnoEliminar.Value))
            {
                int idTurno = Convert.ToInt32(hdnIdTurnoEliminar.Value);

                Negocio.NegocioTurnos negocio = new Negocio.NegocioTurnos();
                bool exito = negocio.eliminarTurno(idTurno);

                if (exito)
                {
                    lblMensajeGeneral.Text = "¡El turno se eliminó correctamente de la base de datos!";
                    lblMensajeGeneral.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMensajeGeneral.Text = "Hubo un error inesperado al intentar eliminar el turno.";
                    lblMensajeGeneral.ForeColor = System.Drawing.Color.Red;
                }

                lblMensajeGeneral.Visible = true;
                hdnIdTurnoEliminar.Value = "";
            }

            divEliminar.Visible = false;
            fullscreenOverlay.Style["display"] = "none";
            CargarGrillaTurnos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            hdnIdTurnoEliminar.Value = "";
            divEliminar.Visible = false;
            fullscreenOverlay.Style["display"] = "none";
            CargarGrillaTurnos();
        }


        private void CargarHorarios(int idMedico)
        {
            ddlHora.Items.Clear();
            NegocioMedicos negMed = new NegocioMedicos();
            Medico med = new Medico { IdMedico = idMedico };

            DataTable dt = negMed.getHorariosMedicoSeleccionado(med);

            ddlHora.Items.Clear();
            if (dt == null || dt.Rows.Count == 0) return;

            object raw = dt.Rows[0]["Horas"];
            if (raw == null || raw == DBNull.Value) return;

            string horaStr = null;
            if (raw is TimeSpan ts) horaStr = ts.ToString(@"hh\:mm");
            else if (raw is DateTime dtVal) horaStr = dtVal.ToString("HH:mm");
            else horaStr = raw.ToString();

            if (horaStr.Length >= 5) horaStr = horaStr.Substring(0, 5);

            if (horaStr == "10:00")
                {
                    ddlHora.Items.Add(new ListItem("10:00hs", "1"));
                    ddlHora.Items.Add(new ListItem("11:00hs", "2"));
                    ddlHora.Items.Add(new ListItem("12:00hs", "3"));
                    ddlHora.Items.Add(new ListItem("13:00hs", "4"));
                    ddlHora.Items.Add(new ListItem("14:00hs", "5"));
                    ddlHora.Items.Add(new ListItem("15:00hs", "6"));
                    ddlHora.Items.Add(new ListItem("16:00hs", "7"));
                    ddlHora.Items.Add(new ListItem("17:00hs", "8"));
                    ddlHora.Items.Add(new ListItem("18:00hs", "9"));
                }
                else
                {
                    ddlHora.Items.Add(new ListItem("08:00hs", "10"));
                    ddlHora.Items.Add(new ListItem("09:00hs", "11"));
                    ddlHora.Items.Add(new ListItem("10:00hs", "12"));
                    ddlHora.Items.Add(new ListItem("11:00hs", "13"));
                    ddlHora.Items.Add(new ListItem("12:00hs", "14"));
                    ddlHora.Items.Add(new ListItem("13:00hs", "15"));
                    ddlHora.Items.Add(new ListItem("14:00hs", "16"));
                    ddlHora.Items.Add(new ListItem("15:00hs", "17"));
                    ddlHora.Items.Add(new ListItem("16:00hs", "18"));
                }
        }

        protected void ddlAltaMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            NegocioMedicos neg = new NegocioMedicos();
            DataTable dt = neg.getTabla();
            ddlAltaEspecialidad.ClearSelection();

            foreach (DataRow dr in dt.Rows)
            {
                if (ddlAltaMedico.SelectedValue == dr["Id_Medico"].ToString())
                {
                    ddlAltaEspecialidad.SelectedValue = dr["Id_Especialidad"].ToString();
                }
            }
            /*
            
            if (int.TryParse(ddlAltaMedico.SelectedValue, out int idMedico))
            {
                CargarHorarios(idMedico);
            }*/
        }

        protected void cFechasTurnos_SelectionChanged(object sender, EventArgs e)
        {

            obtenerHorariosDisponibles();
        }
    }
}
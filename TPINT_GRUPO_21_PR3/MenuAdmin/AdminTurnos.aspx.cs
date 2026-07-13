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

            }
        }

        private void CargarGrillaTurnos()
        {
            NegocioTurnos neg = new NegocioTurnos();
            DataTable dt = neg.getTurnos(txtBuscarDni.Text, txtBuscarPaciente.Text, txtBuscarFecha.Text, ddlBuscarEstado.SelectedValue);
            gvGestionTurnos.DataSource = dt;
            gvGestionTurnos.DataBind();
        }

        private void CargarEspecialidadesAlta()
        {
            NegocioTurnos negocioTurnos = new NegocioTurnos();
            DataTable dt = negocioTurnos.getEspecialidadesAlta();

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

        protected void obtenerHorariosDisponibles()
        {
            if (ddlAltaEspecialidad.SelectedIndex == 0 || cFechasTurnos.SelectedDates.Count == 0)
            {
                ddlHora.Items.Clear();
                ddlHora.Items.Insert(0, new ListItem("Seleccione Medico y Fecha", "0"));
                return;
            }
            int idMedico = Convert.ToInt32(ddlAltaMedico.SelectedValue);
            string dni = txtPaciente.Text.Trim();
            NegocioTurnos negTurnos = new NegocioTurnos();
            DataTable dtHorarios = negTurnos.getDisponibilidadPorMedico(idMedico);

            TimeSpan horaInicioTS = (TimeSpan)dtHorarios.Rows[0]["HoraInicio"];
            TimeSpan horaFinTS = (TimeSpan)dtHorarios.Rows[0]["HoraFin"];

            int horaInicio = horaInicioTS.Hours;
            int horaFin = horaFinTS.Hours;
            bool turnoDisponibleMedico = false;
            bool turnoPacienteDisponible = false;
            string fechaSeleccionada = cFechasTurnos.SelectedDate.ToString("yyyy-MM-dd");
            string horaSeleccionada = "";

            ddlHora.Items.Clear();
            for (int hora = horaInicio; hora < horaFin; hora++)
            {
                horaSeleccionada = hora.ToString("D2") + ":00";
                turnoDisponibleMedico = negTurnos.verificarTurnoMedico(idMedico, fechaSeleccionada, horaSeleccionada);
                turnoPacienteDisponible = negTurnos.verificarTurnoPaciente(dni, fechaSeleccionada, horaSeleccionada);

                if (turnoDisponibleMedico && turnoPacienteDisponible)
                {
                    ddlHora.Items.Add(new ListItem(horaSeleccionada, horaSeleccionada));
                }
            }

            return;
        }

        protected void ddlAltaEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            cFechasTurnos.SelectedDates.Clear();
            ddlHora.Items.Clear();
            ddlHora.Items.Insert(0, new ListItem("Seleccione Medico y Fecha", "0"));
            CargarAltaMedicosSegunEsp();
        }

        protected void cFechasTurnos_DayRender(object sender, DayRenderEventArgs e)
        {

            int idMedico = Convert.ToInt32(ddlAltaMedico.SelectedValue);

            DateTime minDate = DateTime.Now;
            NegocioTurnos neg = new NegocioTurnos();
            string diasDisponibles = neg.obtenerDiasDisp(Convert.ToInt32(ddlAltaMedico.SelectedValue));
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
                if (neg.verificarTurnoMedico(idMedico, e.Day.Date.ToString("yyyy-MM-dd"), hora.ToString("D2") + ":00"))
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

            CargarEspecialidadesAlta();
            CargarMedicosAlta();
            lblMensajeGeneral.Visible = false;
            lblMensajeErrorPopup.Visible = false;
            lblMensajeErrorPopup.Text = "";

            hCargarTurno.InnerText = "Cargar Nuevo Turno";
            btnCargar.Text = "Agendar Turno";

            txtPaciente.Enabled = true;
            ddlAltaEspecialidad.Enabled = true;
            ddlAltaMedico.Enabled = true;
            activarValidaciones();
            hdnIdTurnoEditar.Value = "";

            txtPaciente.Text = "";
            cFechasTurnos.SelectedDates.Clear();
            ddlHora.Items.Clear();
            ddlHora.Items.Insert(0, new ListItem("Seleccione Medico y Fecha", "0"));

            txtObservacionAlta.Text = "";
            ddlAltaEspecialidad.SelectedIndex = 0;
            ddlAltaMedico.SelectedIndex = 0;

            fullscreenOverlay.Style["display"] = "block"; 
            divFormulario.Visible = true;                 
            divEliminar.Visible = false;
        }

        private void activarValidaciones()
        {
            rfvDni.Enabled = true;
            revDni.Enabled = true;
            rfvEspecialidad.Enabled = true;
            rfvMedico.Enabled = true;
        }

        private void limpiarCampos()
        {
            txtPaciente.Text = string.Empty;
            ddlHora.Items.Clear();
            txtObservacionAlta.Text = string.Empty;
            ddlAltaEspecialidad.SelectedIndex = 0;
        }
        
        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            limpiarCampos();

            lblMensajeErrorPopup.Visible = false;
            lblMensajeErrorPopup.Text = "";

            txtPaciente.Enabled = true;
            ddlAltaEspecialidad.Enabled = true;
            ddlAltaMedico.Enabled = true;
            CargarMedicosAlta();
            hdnIdTurnoEditar.Value = "";

            fullscreenOverlay.Style["display"] = "none";
            divFormulario.Visible = false;
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            lblMensajeGeneral.Visible = false;
            lblMensajeGeneral.Text = "";
            lblMensajeErrorPopup.Visible = false;
            lblMensajeErrorPopup.Text = "";

            int idMedico = Convert.ToInt32(ddlAltaMedico.SelectedValue);
            string dniPaciente = txtPaciente.Text.Trim();
            string fecha = cFechasTurnos.SelectedDate.ToString("yyyy-MM-dd");
            string hora = ddlHora.SelectedValue;
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
                ddlHora.Items.Clear();
                txtObservacionAlta.Text = "";
                ddlAltaEspecialidad.SelectedIndex = 0;
                ddlAltaMedico.Items.Clear();

                fullscreenOverlay.Style["display"] = "none";

                CargarGrillaTurnos();
            }
            else
            {
                fullscreenOverlay.Style["display"] = "flex";

                if (resultado == -1) lblMensajeErrorPopup.Text = "El DNI ingresado no pertenece a ningún paciente registrado.";
                else if (resultado == -2) lblMensajeErrorPopup.Text = "El paciente ya posee un turno asignado para esa misma fecha y hora.";
                else if (resultado == -3) lblMensajeErrorPopup.Text = "El médico seleccionado ya se encuentra ocupado con otro turno en esa misma fecha y hora.";
                else if (resultado == -4) lblMensajeErrorPopup.Text = "No puede solicitar un turno para si mismo.";
                else lblMensajeErrorPopup.Text = "Hubo un error inesperado al procesar el alta del turno.";

                lblMensajeErrorPopup.Visible = true;
                lblMensajeErrorPopup.ForeColor = System.Drawing.Color.Red;
            }

            ddlAltaMedico.Items.Insert(0, new ListItem("-- Seleccione Especialidad --", "0"));
            divFormulario.Visible = false;
            fullscreenOverlay.Style["display"] = "none";

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


        protected void CargarMedicosAlta()
        {
            NegocioMedicos neg = new NegocioMedicos();
            ddlAltaMedico.DataSource = neg.getTablaINA();
            ddlAltaMedico.DataTextField = "NombreApellido";
            ddlAltaMedico.DataValueField = "Id_Medico";
            ddlAltaMedico.DataBind();
            ddlAltaMedico.Items.Insert(0, new ListItem("-- Seleccione Medico --", "0"));
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

            string idTurno = gvGestionTurnos.Rows[e.RowIndex].Cells[0].Text;
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

                NegocioTurnos negocio = new NegocioTurnos();
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

        protected void ddlAltaMedico_SelectedIndexChanged(object sender, EventArgs e)
        {

            cFechasTurnos.SelectedDates.Clear();
            //cFechasTurnos.DataBind();
            ddlHora.Items.Clear();
            ddlHora.Items.Insert(0, new ListItem("Seleccione Medico y Fecha", "0"));
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
        }

        protected void cFechasTurnos_SelectionChanged(object sender, EventArgs e)
        {

            obtenerHorariosDisponibles();
        }

        protected void txtPaciente_TextChanged(object sender, EventArgs e)
        {
            cFechasTurnos.SelectedDates.Clear();
            ddlHora.Items.Clear();
            ddlHora.Items.Insert(0, new ListItem("Seleccione Medico y Fecha", "0"));
        }
    }
}
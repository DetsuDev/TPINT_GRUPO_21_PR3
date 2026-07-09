using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
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
            Negocio.NegocioTurnos negocioTurnos = new Negocio.NegocioTurnos();
            DataTable dt = negocioTurnos.obtenerEspecialidadesAlta();

            ddlAltaEspecialidad.DataSource = dt;
            ddlAltaEspecialidad.DataTextField = "Nombre";
            ddlAltaEspecialidad.DataValueField = "Id_Especialidad";
            ddlAltaEspecialidad.DataBind();
            ddlAltaEspecialidad.Items.Insert(0, new ListItem("-- Seleccione Especialidad --", "0"));
        }

        protected void ddlAltaEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlAltaMedico.Items.Clear();

            int idEspecialidad = Convert.ToInt32(ddlAltaEspecialidad.SelectedValue);
            if (idEspecialidad == 0 || string.IsNullOrWhiteSpace(txtFecha.Text) || string.IsNullOrWhiteSpace(txtHora.Text))
            {
                ddlAltaMedico.Items.Insert(0, new ListItem("Complete Fecha, Hora y Especialidad", "0"));
                return;
            }

            try
            {
                DateTime fechaSeleccionada = Convert.ToDateTime(txtFecha.Text);
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
            txtFecha.Text = "";
            txtHora.Text = "";
            txtObservacionAlta.Text = "";
            ddlAltaEspecialidad.SelectedIndex = 0;
            ddlAltaMedico.Items.Clear();

            fullscreenOverlay.Style["display"] = "block"; 
            divFormulario.Visible = true;                 
            divEliminar.Visible = false;
        }

        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            txtPaciente.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtHora.Text = string.Empty;
            txtObservacionAlta.Text = string.Empty;
            ddlAltaEspecialidad.SelectedIndex = 0;
            ddlAltaMedico.Items.Clear();

            lblMensajeErrorPopup.Visible = false;
            lblMensajeErrorPopup.Text = "";

            txtPaciente.Enabled = true;
            ddlAltaEspecialidad.Enabled = true;
            ddlAltaMedico.Enabled = true;
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
            string fecha = txtFecha.Text;
            string hora = txtHora.Text.Trim();
            string observacion = txtObservacionAlta.Text.Trim();

            Negocio.NegocioTurnos negocioTurnos = new Negocio.NegocioTurnos();
            int resultado = negocioTurnos.guardarTurno(idMedico, dniPaciente, fecha, hora, observacion);

            if (resultado == 1)
            {
                lblMensajeGeneral.Text = "¡Turno agendado con éxito!";
                lblMensajeGeneral.ForeColor = System.Drawing.Color.Green;
                lblMensajeGeneral.Visible = true;

                txtPaciente.Text = "";
                txtFecha.Text = "";
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
            Negocio.NegocioTurnos negocio = new Negocio.NegocioTurnos();
            DataTable dt = negocio.obtenerTurnoPorId(idTurno);
            if (dt == null || dt.Rows.Count == 0) return;
            DataRow r = dt.Rows[0];

            hdnIdTurnoEditar.Value = idTurno.ToString();

            hCargarTurno.InnerText = "Modificar Turno";
            btnCargar.Text = "Guardar Cambios";

            txtPaciente.Text = r["DNI"].ToString();
            txtFecha.Text = r["Fecha"].ToString();
            txtHora.Text = r["Hora"].ToString();
            txtObservacionAlta.Text = r["Observacion"] == DBNull.Value ? "" : r["Observacion"].ToString();

            CargarEspecialidadesAlta();
            ddlAltaEspecialidad.SelectedValue = r["Id_Especialidad"].ToString();
            ddlAltaMedico.Items.Clear();
            ddlAltaMedico.Items.Add(new ListItem(r["Medico"].ToString(), r["Id_Medico"].ToString()));

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
            string fecha = txtFecha.Text;
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

        private void CargarFechasCalendar()
        {
        }

        protected void cFechasTurnos_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime minDate = DateTime.Now;

            if (e.Day.Date < minDate || e.Day.IsWeekend)
            {
                e.Day.IsSelectable = false;          
                e.Cell.ForeColor = System.Drawing.Color.Gray;
                e.Cell.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }
}
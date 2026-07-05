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

            if (user == null || user.Rol != "A" )
            {
                Response.Redirect("~/SesionInvalida.html");
            }
            if (!IsPostBack)
            {
                lblNombreUsuario.Text = user.persona.Nombre + " " + user.persona.Apellido;
                divEliminar.Visible = false;
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
                filtros.Add("EstadoTurno = '" + ddlBuscarEstado.SelectedValue.Replace("'", "''") + "'");

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
            if (string.IsNullOrWhiteSpace(txtPaciente.Text) ||
                string.IsNullOrWhiteSpace(txtFecha.Text) ||
                string.IsNullOrWhiteSpace(txtHora.Text) ||
                ddlAltaMedico.SelectedValue == "0" ||
                string.IsNullOrEmpty(ddlAltaMedico.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Por favor, complete todos los campos y seleccione un médico disponible.');", true);
                return;
            }

            int idMedico = Convert.ToInt32(ddlAltaMedico.SelectedValue);
            string dniPaciente = txtPaciente.Text.Trim();
            string fecha = txtFecha.Text;
            string hora = txtHora.Text.Trim();
            string observacion = txtObservacionAlta.Text.Trim();

            Negocio.NegocioTurnos negocioTurnos = new Negocio.NegocioTurnos();

            bool exito = negocioTurnos.guardarTurno(idMedico, dniPaciente, fecha, hora, observacion);

            if (exito)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('¡Turno agendado con éxito!');", true);

                txtPaciente.Text = "";
                txtFecha.Text = "";
                txtHora.Text = "";
                txtObservacionAlta.Text = "";
                ddlAltaEspecialidad.SelectedIndex = 0;
                ddlAltaMedico.Items.Clear();

                CargarGrillaTurnos();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al agendar el turno. Verifique que el DNI del paciente corresponda a un usuario registrado.');", true);
            }
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
            gvGestionTurnos.EditIndex = e.NewEditIndex;
            CargarGrillaTurnos();
        }
        protected void gvGestionTurnos_RowUpdating(object sender, GridViewUpdateEventArgs e) { }
        protected void gvGestionTurnos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGestionTurnos.EditIndex = -1;
            CargarGrillaTurnos();
        }
        protected void gvGestionTurnos_RowDeleting(object sender, GridViewDeleteEventArgs e) { 
            divEliminar.Visible = true;
        }


        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            divEliminar.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divEliminar.Visible = false;
        }
    }
}
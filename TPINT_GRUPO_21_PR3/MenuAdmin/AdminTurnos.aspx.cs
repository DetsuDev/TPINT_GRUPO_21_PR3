using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3.MenuAdmin
{
    public partial class GestionTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogeado"] == null)
            {
                Response.Redirect("~/login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblNombreUsuario.Text = Session["UsuarioLogeado"].ToString();
                divEliminar.Visible = false;
                CargarEspecialidades();
                CargarPacientes();
                CargarHoras();
                CargarGrillaTurnos();
            }
        }

        private void CargarEspecialidades()
        {
            NegocioEspecialidades neg = new NegocioEspecialidades();
            ddlEspecialidad.DataSource = neg.getTabla();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Id_Especialidad";
            ddlEspecialidad.DataBind();
            ddlEspecialidad.Items.Insert(0, new ListItem("-- Especialidad --", ""));
            ddlMedico.Items.Clear();
            ddlMedico.Items.Insert(0, new ListItem("-- Médico --", ""));
        }

        private void CargarMedicos(string idEspecialidad)
        {
            ddlMedico.Items.Clear();
            if (string.IsNullOrEmpty(idEspecialidad))
            {
                ddlMedico.Items.Insert(0, new ListItem("-- Médico --", ""));
                return;
            }

            NegocioTurnos neg = new NegocioTurnos();
            ddlMedico.DataSource = neg.getMedicosPorEspecialidad(Convert.ToInt32(idEspecialidad));
            ddlMedico.DataTextField = "Medico";
            ddlMedico.DataValueField = "Id_Medico";
            ddlMedico.DataBind();
            ddlMedico.Items.Insert(0, new ListItem("-- Médico --", ""));
        }

        private void CargarPacientes()
        {
            NegocioTurnos neg = new NegocioTurnos();
            ddlPaciente.DataSource = neg.getPacientesCombo();
            ddlPaciente.DataTextField = "Paciente";
            ddlPaciente.DataValueField = "Id_Persona";
            ddlPaciente.DataBind();
            ddlPaciente.Items.Insert(0, new ListItem("-- Paciente --", ""));
        }

        private void CargarHoras()
        {
            ddlHora.Items.Clear();
            for (int h = 8; h <= 17; h++)
                ddlHora.Items.Add(new ListItem(h.ToString("00") + ":00"));
            ddlHora.Items.Insert(0, new ListItem("-- Hora --", ""));
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMedicos(ddlEspecialidad.SelectedValue);
        }

        private void CargarGrillaTurnos()
        {
            NegocioTurnos neg = new NegocioTurnos();
            DataTable dt = neg.getTabla();

            List<string> filtros = new List<string>();
            if (!string.IsNullOrWhiteSpace(txtBuscarDni.Text))
                filtros.Add("DNI LIKE '%" + txtBuscarDni.Text.Trim().Replace("'", "''") + "%'");
            if (!string.IsNullOrWhiteSpace(txtBuscarPaciente.Text))
                filtros.Add("Paciente LIKE '%" + txtBuscarPaciente.Text.Trim().Replace("'", "''") + "%'");
            if (!string.IsNullOrWhiteSpace(txtBuscarFecha.Text))
                filtros.Add("Fecha LIKE '%" + txtBuscarFecha.Text.Trim().Replace("'", "''") + "%'");
            if (!string.IsNullOrWhiteSpace(ddlBuscarEstado.SelectedValue))
                filtros.Add("Estado = '" + ddlBuscarEstado.SelectedValue.Replace("'", "''") + "'");

            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Join(" AND ", filtros);
            gvGestionTurnos.DataSource = dv;
            gvGestionTurnos.DataBind();
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

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMedico.SelectedValue) ||
                string.IsNullOrEmpty(ddlPaciente.SelectedValue) ||
                string.IsNullOrWhiteSpace(txtFecha.Text) ||
                string.IsNullOrEmpty(ddlHora.SelectedValue))
            {
                MostrarMensaje("Complete especialidad, médico, paciente, fecha y hora.", false);
                return;
            }

            int idMedico = Convert.ToInt32(ddlMedico.SelectedValue);
            string fecha = Convert.ToDateTime(txtFecha.Text).ToString("yyyy-MM-dd");
            string hora = ddlHora.SelectedValue;

            NegocioTurnos neg = new NegocioTurnos();
            if (neg.existeTurno(idMedico, fecha, hora))
            {
                MostrarMensaje("El médico ya tiene un turno ese día y horario.", false);
                return;
            }

            Turno t = new Turno();
            t.IdMedico = idMedico;
            t.IdPersona = Convert.ToInt32(ddlPaciente.SelectedValue);
            t.Fecha = fecha;
            t.Hora = hora;

            if (neg.guardarTurno(t))
            {
                MostrarMensaje("Se agregó correctamente en la base de datos.", true);
                LimpiarFormulario();
                CargarGrillaTurnos();
            }
            else
            {
                MostrarMensaje("Hubo un error al cargar el turno.", false);
            }
        }

        private void LimpiarFormulario()
        {
            ddlEspecialidad.SelectedIndex = 0;
            ddlMedico.Items.Clear();
            ddlMedico.Items.Insert(0, new ListItem("-- Médico --", ""));
            ddlPaciente.SelectedIndex = 0;
            txtFecha.Text = "";
            ddlHora.SelectedIndex = 0;
        }

        protected void gvGestionTurnos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ViewState["idEliminar"] = gvGestionTurnos.DataKeys[e.RowIndex].Value.ToString();
            divEliminar.Visible = true;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ViewState["idEliminar"]);
            NegocioTurnos neg = new NegocioTurnos();
            if (neg.eliminarTurno(id))
                MostrarMensaje("Se eliminó correctamente de la base de datos.", true);
            else
                MostrarMensaje("Hubo un error al eliminar el registro.", false);

            divEliminar.Visible = false;
            CargarGrillaTurnos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divEliminar.Visible = false;
        }

        private void MostrarMensaje(string texto, bool ok)
        {
            lblMensaje.Text = texto;
            lblMensaje.ForeColor = ok ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }
    }
}

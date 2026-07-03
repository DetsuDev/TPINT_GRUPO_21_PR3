using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3.MenuAdmin
{
    public partial class AdminPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UsuarioLogeado"] == null)
            {
                Response.Redirect("~/SesionInvalida.html");
            }
            if (!IsPostBack)
            {
                lblNombreUsuario.Text = Session["UsuarioLogeado"].ToString();
                divEliminar.Visible = false;
                divFormulario.Visible = false;
                CargarGrillaPacientes();
                CargarddlProvincias();
                CargarFiltroProvincias();
            }
        }


        private void CargarGrillaPacientes()
        {
            NegocioPacientes negocioPacientes = new NegocioPacientes();
            DataTable dt = negocioPacientes.getTabla();

            List<string> filtros = new List<string>();
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                string b = txtBuscar.Text.Trim().Replace("'", "''");
                filtros.Add($"(DNI LIKE '%{b}%' OR Nombre LIKE '%{b}%' OR Apellido LIKE '%{b}%')");
            }
            if (ddlFiltroProvincia.SelectedIndex > 0)
                filtros.Add($"Provincia = '{ddlFiltroProvincia.SelectedItem.Text.Replace("'", "''")}'");

            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Join(" AND ", filtros);

            gvGestionPacientes.DataSource = dv;
            gvGestionPacientes.DataBind();
        }

        private void CargarFiltroProvincias()
        {
            NegocioProvincias negocioProvincias = new NegocioProvincias();
            ddlFiltroProvincia.DataSource = negocioProvincias.getTabla();
            ddlFiltroProvincia.DataTextField = "NombreProvincia";
            ddlFiltroProvincia.DataValueField = "Id_Provincia";
            ddlFiltroProvincia.DataBind();
            ddlFiltroProvincia.Items.Insert(0, new ListItem("-- Todas --", ""));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            gvGestionPacientes.EditIndex = -1;
            gvGestionPacientes.PageIndex = 0;
            CargarGrillaPacientes();
        }

        protected void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            ddlFiltroProvincia.SelectedIndex = 0;
            gvGestionPacientes.EditIndex = -1;
            gvGestionPacientes.PageIndex = 0;
            CargarGrillaPacientes();
        }

        private void CargarddlProvincias()
        {
            DataTable dt = new DataTable();
            NegocioProvincias negocioProvincias= new NegocioProvincias();
            dt = negocioProvincias.getTabla();

            ddlProvincia.DataSource = dt;
            ddlProvincia.DataTextField = "NombreProvincia";
            ddlProvincia.DataValueField = "Id_Provincia";
            ddlProvincia.DataBind();

            ddlProvincia.Items.Insert(0, new ListItem("-- Elija una provincia --", ""));
        }

        protected void gvGestionPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGestionPacientes.PageIndex = e.NewPageIndex;
            CargarGrillaPacientes();
        }
        protected void gvGestionPacientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int idPaciente = Convert.ToInt32(gvGestionPacientes.DataKeys[e.NewEditIndex].Value);
            CargarPacienteEnFormulario(idPaciente);
            fullscreenOverlay.Style["display"] = "block";
            divFormulario.Visible = true;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            fullscreenOverlay.Style["display"] = "block";
            int idPaciente = Convert.ToInt32(((Button)sender).CommandArgument);
            CargarPacienteEnFormulario(idPaciente);
            divFormulario.Visible = true;
        }

        protected void gvGestionPacientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            hdnIdEliminar.Value = gvGestionPacientes.DataKeys[e.RowIndex].Value.ToString();
            fullscreenOverlay.Style["display"] = "block";
            divEliminar.Visible = true;
        }

        protected void gvGestionPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hdnIdEliminar.Value);
            NegocioPacientes negocio = new NegocioPacientes();
            bool exito = negocio.eliminarPaciente(id);

            if (exito)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Se eliminó correctamente de la base de datos.";
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Hubo un error al eliminar el registro.";
            }

            divEliminar.Visible = false;
            fullscreenOverlay.Style["display"] = "none";
            CargarGrillaPacientes();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divEliminar.Visible = false;
            fullscreenOverlay.Style["display"] = "none";
        }
        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidadesFormulario(ddlProvincia.SelectedValue);
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (string.IsNullOrWhiteSpace(txtDni.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtNacionalidad.Text) ||
                string.IsNullOrWhiteSpace(txtFechaNac.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrEmpty(ddlProvincia.SelectedValue) ||
                string.IsNullOrEmpty(ddlLocalidad.SelectedValue))
            {
                MostrarMensaje("Error: Todos los campos son obligatorios.", false);
                return;
            }

            Paciente nuevoPaciente = new Paciente();

            nuevoPaciente.Dni = txtDni.Text.Trim();
            nuevoPaciente.Nombre = txtNombre.Text.Trim();
            nuevoPaciente.Apellido = txtApellido.Text.Trim();
            nuevoPaciente.Sexo = Convert.ToChar(ddlSexo.SelectedValue);
            nuevoPaciente.Nacionalidad = txtNacionalidad.Text.Trim();
            nuevoPaciente.FechaNacimiento = Convert.ToDateTime(txtFechaNac.Text);
            nuevoPaciente.Telefono = txtTelefono.Text.Trim();
            nuevoPaciente.Direccion = txtDireccion.Text.Trim();
            nuevoPaciente.IdLocalidad = Convert.ToInt32(ddlLocalidad.SelectedValue);
            nuevoPaciente.CorreoElectronico = txtEmail.Text.Trim();

            NegocioPacientes negocio = new NegocioPacientes();
            bool exito;

            if (string.IsNullOrEmpty(hdnIdPaciente.Value))
            {
                exito = negocio.guardarPaciente(nuevoPaciente);
                if (exito) MostrarMensaje("Se agregó correctamente en la base de datos.", true);
                else MostrarMensaje("Error al guardar. Verifique DNI duplicado.", false);
            }
            else
            {
                nuevoPaciente._IdPaciente = Convert.ToInt32(hdnIdPaciente.Value);
                exito = negocio.modificarPaciente(nuevoPaciente);
                if (exito) MostrarMensaje("Se modificó correctamente en la base de datos.", true);
                else MostrarMensaje("Hubo un error al modificar el paciente.", false);
            }

            fullscreenOverlay.Style["display"] = "none";
            divFormulario.Visible = false;
            if (exito)
            {
                CargarGrillaPacientes();
                LimpiarFormulario();

            }
        }

        private void MostrarMensaje(string texto, bool ok)
        {
            lblMensaje.Text = texto;
            lblMensaje.ForeColor = ok ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
            fullscreenOverlay.Style["display"] = "none";
            divFormulario.Visible = false;
        }

        protected void btnMostrarForm_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            fullscreenOverlay.Style["display"] = "block";
            divFormulario.Visible = true;
        }
        private void CargarPacienteEnFormulario(int idPaciente)
        {
            NegocioPacientes negocio = new NegocioPacientes();
            DataTable dt = negocio.getPacientePorId(idPaciente);
            if (dt == null || dt.Rows.Count == 0) return;

            DataRow r = dt.Rows[0];

            hdnIdPaciente.Value = r["Id_Paciente"].ToString();

            txtDni.Text = r["DNI"].ToString();
            txtNombre.Text = r["Nombre"].ToString();
            txtApellido.Text = r["Apellido"].ToString();
            txtNacionalidad.Text = r["Nacionalidad"].ToString();
            txtFechaNac.Text = Convert.ToDateTime(r["FechaNacimiento"]).ToString("yyyy-MM-dd");
            txtDireccion.Text = r["Direccion"].ToString();
            txtEmail.Text = r["CorreoElectronico"].ToString();
            txtTelefono.Text = r["Telefono"].ToString();

            string sexo = r["Sexo"].ToString().Trim();
            if (ddlSexo.Items.FindByValue(sexo) != null) ddlSexo.SelectedValue = sexo;

            ddlProvincia.SelectedValue = r["Id_Provincia"].ToString();
            CargarLocalidadesFormulario(r["Id_Provincia"].ToString());
            if (ddlLocalidad.Items.FindByValue(r["Id_Localidad"].ToString()) != null)
                ddlLocalidad.SelectedValue = r["Id_Localidad"].ToString();

            txtDni.Enabled = false;
            btnCargar.Text = "Actualizar Paciente";
            hCargarPaciente.InnerText = "Editar Paciente";
        }

        private void CargarLocalidadesFormulario(string idProvincia)
        {
            ddlLocalidad.Items.Clear();
            if (string.IsNullOrEmpty(idProvincia)) return;

            NegocioLocalidades negocioLocalidades = new NegocioLocalidades();
            DataTable dt = negocioLocalidades.getTabla();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Id_Provincia = " + idProvincia;

            ddlLocalidad.DataSource = dv;
            ddlLocalidad.DataTextField = "NombreLocalidad";
            ddlLocalidad.DataValueField = "Id_Localidad";
            ddlLocalidad.DataBind();
        }

        private void LimpiarFormulario()
        {
            hdnIdPaciente.Value = "";
            txtDni.Text = string.Empty; 
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty; 
            txtNacionalidad.Text = string.Empty; 
            txtFechaNac.Text = string.Empty; 
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty; 
            txtEmail.Text = string.Empty;
            ddlSexo.SelectedIndex = 0;
            ddlProvincia.SelectedIndex = 0; 
            ddlLocalidad.Items.Clear();
            txtDni.Enabled = true;
            btnCargar.Text = "Cargar Paciente";
            hCargarPaciente.InnerText = "Agregar Nuevo Paciente";
        }
    }
}

using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3.MenuAdmin
{
    public partial class AdminPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divEliminar.Visible = false;
                CargarGrillaPacientes();
                CargarddlProvincias();
            }
        }


        private void CargarGrillaPacientes()
        {
            DataTable dt = new DataTable();
            NegocioPacientes negocioPacientes = new NegocioPacientes();

            dt = negocioPacientes.getTabla();
            gvGestionPacientes.DataSource = dt;
            gvGestionPacientes.DataBind();
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
            gvGestionPacientes.EditIndex = e.NewEditIndex;
            CargarGrillaPacientes();

            GridViewRow fila = gvGestionPacientes.Rows[e.NewEditIndex];
            DropDownList ddlProv = (DropDownList)fila.FindControl("ddlGridProvincia");
            DropDownList ddlSex = (DropDownList)fila.FindControl("ddlGridSexo");

            if (ddlProv != null)
            {
                NegocioProvincias negProv = new NegocioProvincias();
                ddlProv.DataSource = negProv.getTabla();
                ddlProv.DataTextField = "NombreProvincia";
                ddlProv.DataValueField = "Id_Provincia";
                ddlProv.DataBind();
                ddlProv.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }

            if (ddlSex != null)
            {
                Label lblSexoOriginal = (Label)fila.FindControl("lbl_it_Sexo");
                if (lblSexoOriginal != null)
                {
                    ddlSex.SelectedValue = lblSexoOriginal.Text.Trim();
                }
            }
        }

        protected void gvGestionPacientes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Paciente pacModificado = new Paciente();

            pacModificado.Dni = ((Label)gvGestionPacientes.Rows[e.RowIndex].FindControl("lbl_eit_Dni")).Text.Trim();
            pacModificado.Nombre = ((TextBox)gvGestionPacientes.Rows[e.RowIndex].FindControl("txt_eit_Nombre")).Text.Trim();
            pacModificado.Apellido = ((TextBox)gvGestionPacientes.Rows[e.RowIndex].FindControl("txt_eit_Apellido")).Text.Trim();
            DropDownList ddlS = (DropDownList)gvGestionPacientes.Rows[e.RowIndex].FindControl("ddlGridSexo");
            if (ddlS != null) pacModificado.Sexo = Convert.ToChar(ddlS.SelectedValue);

            pacModificado.Nacionalidad = ((TextBox)gvGestionPacientes.Rows[e.RowIndex].FindControl("txt_eit_Nacionalidad")).Text.Trim();
            pacModificado.FechaNacimiento = Convert.ToDateTime(((TextBox)gvGestionPacientes.Rows[e.RowIndex].FindControl("txt_eit_FechaNac")).Text.Trim());
            pacModificado.Direccion = ((TextBox)gvGestionPacientes.Rows[e.RowIndex].FindControl("txt_eit_Direccion")).Text.Trim();

            DropDownList ddlL = (DropDownList)gvGestionPacientes.Rows[e.RowIndex].FindControl("ddlGridLocalidad");
            if (ddlL != null && !string.IsNullOrEmpty(ddlL.SelectedValue))
            {
                pacModificado.IdLocalidad = Convert.ToInt32(ddlL.SelectedValue);
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: Debe seleccionar una provincia y localidad válidas.";
                return;
            }

            pacModificado.CorreoElectronico = ((TextBox)gvGestionPacientes.Rows[e.RowIndex].FindControl("txt_eit_Email")).Text.Trim();
            pacModificado.Telefono = ((TextBox)gvGestionPacientes.Rows[e.RowIndex].FindControl("txt_eit_Telefono")).Text.Trim();

            NegocioPacientes negocio = new NegocioPacientes();
            bool exito = negocio.modificarPaciente(pacModificado);

            if (exito)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Se modificó correctamente en la base de datos.";
                gvGestionPacientes.EditIndex = -1; 
                CargarGrillaPacientes();
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Hubo un error al intentar modificar el paciente.";
            }
        }
        protected void gvGestionPacientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGestionPacientes.EditIndex = -1;
            CargarGrillaPacientes();
        }

        protected void gvGestionPacientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            divEliminar.Visible = true;
        }

        protected void gvGestionPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            divEliminar.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divEliminar.Visible = false;
        }
        protected void ddlGridProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProv = (DropDownList)sender;
            GridViewRow fila = (GridViewRow)ddlProv.NamingContainer;
            DropDownList ddlLoc = (DropDownList)fila.FindControl("ddlGridLocalidad");

            if (ddlLoc != null && !string.IsNullOrEmpty(ddlProv.SelectedValue))
            {
                NegocioLocalidades negLoc = new NegocioLocalidades();
                DataTable dt = negLoc.getTabla();

                DataView dv = dt.DefaultView;
                dv.RowFilter = "Id_Provincia = " + ddlProv.SelectedValue;

                ddlLoc.DataSource = dv;
                ddlLoc.DataTextField = "NombreLocalidad";
                ddlLoc.DataValueField = "Id_Localidad";
                ddlLoc.DataBind();
            }
        }
        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlProvincia.SelectedValue))
            {
                ddlLocalidad.Items.Clear();
                return;
            }
            NegocioLocalidades negocioLocalidades = new NegocioLocalidades();
            DataTable dt = negocioLocalidades.getTabla();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "Id_Provincia = " + ddlProvincia.SelectedValue;

            ddlLocalidad.DataSource = dv;
            ddlLocalidad.DataTextField = "NombreLocalidad";
            ddlLocalidad.DataValueField = "Id_Localidad";
            ddlLocalidad.DataBind();

        }
        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDni.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) || 
                string.IsNullOrWhiteSpace(txtNacionalidad.Text) || 
                string.IsNullOrWhiteSpace(txtFechaNac.Text) || 
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) || 
                ddlProvincia.SelectedIndex == 0 ||
                string.IsNullOrEmpty(ddlLocalidad.SelectedValue))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: Todos los campos son obligatorios";
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
            bool exito = negocio.guardarPaciente(nuevoPaciente);

            if (exito)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Se agregó correctamente en la base de datos."; 
                CargarGrillaPacientes();
                LimpiarFormulario();
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Hubo un error al guardar el registro. Verifique si el DNI ya existe.";
            }
        }
        private void LimpiarFormulario()
        {
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
        }
    }
}
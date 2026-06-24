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
        public class LocalidadesEventArgs : EventArgs // esta es una clase auxiliar para actualizar las localidades
        {
            public Label LabelLocalidad { get; }

            public LocalidadesEventArgs(Label label)
            {
                LabelLocalidad = label;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divEliminar.Visible = false;
                CargarFiltroProvincias();
                CargarGrillaPacientes();
                CargarddlProvincias();
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
            // (sebastian)
            // aca van algunos comentarios, para el resto que por ahi no entienda nada de lo que esta aca:
            // tenia un problema para que los dropdownlists mostraran los valores que venian seleccionados desde la base de datos,
            // el problema en general yacia en que previamente necesitabamos almacenar los labels de los item templates antes de setear EditIndex

            // lo primero que hice fue crear unos labels vacios (asignados a null)
            Label lblProvinciaOriginal = null;
            Label lblLocalidadOriginal = null;
            Label lblSexoOriginal = null;
            
            var row = gvGestionPacientes.Rows[e.NewEditIndex];


            lblProvinciaOriginal = (Label)row.FindControl("lbl_it_Provincia");
            lblSexoOriginal = (Label)row.FindControl("lbl_it_Sexo");
            lblLocalidadOriginal = (Label)row.FindControl("lbl_it_Localidad");

            LocalidadesEventArgs localidadesEvenArgs = new LocalidadesEventArgs(lblLocalidadOriginal);


            // switcheo al modo editor
            gvGestionPacientes.EditIndex = e.NewEditIndex;
            CargarGrillaPacientes();

            // almaceno los dropdownlists del gridview asi puedo trabajar con ellos
            GridViewRow fila = gvGestionPacientes.Rows[e.NewEditIndex];
            DropDownList ddlProv = (DropDownList)fila.FindControl("ddlGridProvincia");
            DropDownList ddlSex = (DropDownList)fila.FindControl("ddlGridSexo");
            DropDownList ddlLoc = (DropDownList)fila.FindControl("ddlGridLocalidad");


            // bind province dropdown and restore selections
            if (ddlProv != null)
            {
                NegocioProvincias negProv = new NegocioProvincias();
                ddlProv.DataSource = negProv.getTabla();
                ddlProv.DataTextField = "NombreProvincia";
                ddlProv.DataValueField = "Id_Provincia";
                ddlProv.DataBind();

                // y, despues de algunas validaciones, dejo marcada la provincia
                if (lblProvinciaOriginal != null && !string.IsNullOrWhiteSpace(lblProvinciaOriginal.Text))
                {
                    string provText = lblProvinciaOriginal.Text.Trim();
                    string locText = lblLocalidadOriginal?.Text?.Trim();
                    ListItem itemByText = ddlProv.Items.FindByText(provText);
                    if (itemByText != null)
                    {
                        ddlProv.SelectedValue = itemByText.Value;
                        ddlGridProvincia_SelectedIndexChanged(ddlProv, localidadesEvenArgs);

                        ddlLoc = (DropDownList)fila.FindControl("ddlGridLocalidad");
                        if (ddlLoc != null && !string.IsNullOrEmpty(locText))
                        {
                            ListItem locItem = ddlLoc.Items.FindByText(locText) ?? ddlLoc.Items.FindByValue(locText);
                            if (locItem != null)
                            {
                                ddlLoc.SelectedValue = locItem.Value;
                            }
                        }
                    }
                    else
                    {
                        ddlProv.SelectedIndex = 0;
                    }
                }
            }

            if (ddlSex != null && lblSexoOriginal != null && !string.IsNullOrWhiteSpace(lblSexoOriginal.Text))
            {
                string sexo = lblSexoOriginal.Text.Trim();
                ListItem li = ddlSex.Items.FindByValue(sexo) ?? ddlSex.Items.FindByText(sexo);
                if (li != null)
                {
                    ddlSex.SelectedValue = li.Value;

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
                lblMensajeGrid.ForeColor = System.Drawing.Color.Green;
                lblMensajeGrid.Text = "Se modificó correctamente en la base de datos.";
                gvGestionPacientes.EditIndex = -1; 
                CargarGrillaPacientes();
            }
            else
            {
                lblMensajeGrid.ForeColor = System.Drawing.Color.Red;
                lblMensajeGrid.Text = "Hubo un error al intentar modificar el paciente.";
            }
        }
        protected void gvGestionPacientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGestionPacientes.EditIndex = -1;
            CargarGrillaPacientes();
        }

        protected void gvGestionPacientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            hdnIdEliminar.Value = gvGestionPacientes.DataKeys[e.RowIndex].Value.ToString();
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
                lblMensajeGrid.ForeColor = System.Drawing.Color.Green;
                lblMensajeGrid.Text = "Se eliminó correctamente de la base de datos.";
            }
            else
            {
                lblMensajeGrid.ForeColor = System.Drawing.Color.Red;
                lblMensajeGrid.Text = "Hubo un error al eliminar el registro.";
            }

            divEliminar.Visible = false;
            gvGestionPacientes.EditIndex = -1;
            CargarGrillaPacientes();
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

            var custom = e as LocalidadesEventArgs;

            if (custom != null && custom.LabelLocalidad != null && ddlLoc != null)
            {
                var text = custom.LabelLocalidad.Text?.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    var li = ddlLoc.Items.FindByText(text) ?? ddlLoc.Items.FindByValue(text);
                    if (li != null)
                    {
                        ddlLoc.SelectedValue = li.Value;
                    }
                }
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
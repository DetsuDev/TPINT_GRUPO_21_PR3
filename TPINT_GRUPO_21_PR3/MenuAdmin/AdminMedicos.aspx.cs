using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3.MenuAdmin
{
    public partial class GestionMedicos : Culture
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

                CargarFiltroEspecialidadBusqueda();
                CargarGrillaMedicos();
                CargarDdlEspecialidades();
                CargarProvincias();

                ddlHorario.Items.Clear();
                ddlHorario.Items.Add(new ListItem("08:00 - 16:00", "1"));
                ddlHorario.Items.Add(new ListItem("10:00 - 18:00", "2"));
            }
        }

        private void CargarGrillaMedicos()
        {
            NegocioMedicos negocioMedicos = new NegocioMedicos();
            DataTable dt = negocioMedicos.getTablaCompleta();

            List<string> filtros = new List<string>();

            if (!string.IsNullOrWhiteSpace(txtBuscarNombreApellido.Text))
            {
                string b = txtBuscarNombreApellido.Text.Trim().Replace("'", "''");
                filtros.Add($"(Nombre LIKE '%{b}%' OR Apellido LIKE '%{b}%')");
            }

            if (!string.IsNullOrWhiteSpace(txtBuscarLegajo.Text))
            {
                string l = txtBuscarLegajo.Text.Trim().Replace("'", "''");
                filtros.Add($"Legajo_Medico LIKE '%{l}%'");
            }

            if (ddlFiltroEspecialidad.SelectedIndex > 0)
            {
                string esp = ddlFiltroEspecialidad.SelectedItem.Text.Replace("'", "''");
                filtros.Add($"Especialidad = '{esp}'");
            }

            DataView dv = dt.DefaultView;
            if (filtros.Count > 0)
            {
                dv.RowFilter = string.Join(" AND ", filtros);
            }

            gvGestionMedicos.DataSource = dv;
            gvGestionMedicos.DataBind();
        }

        private void CargarDdlEspecialidades()
        {
            NegocioEspecialidades negocioEspecialidades = new NegocioEspecialidades();
            ddlEspecialidad.DataSource = negocioEspecialidades.getTabla();
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Id_Especialidad";
            ddlEspecialidad.DataBind();
        }

        private void CargarFiltroEspecialidadBusqueda()
        {
            NegocioEspecialidades negocioEspecialidades = new NegocioEspecialidades();
            ddlFiltroEspecialidad.DataSource = negocioEspecialidades.getTabla();
            ddlFiltroEspecialidad.DataTextField = "Nombre";
            ddlFiltroEspecialidad.DataValueField = "Id_Especialidad";
            ddlFiltroEspecialidad.DataBind();
            ddlFiltroEspecialidad.Items.Insert(0, new ListItem("-- Todas --", ""));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            gvGestionMedicos.EditIndex = -1;
            gvGestionMedicos.PageIndex = 0;
            CargarGrillaMedicos();
        }

        protected void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarNombreApellido.Text = "";
            txtBuscarLegajo.Text = "";
            ddlFiltroEspecialidad.SelectedIndex = 0;

            gvGestionMedicos.EditIndex = -1;
            gvGestionMedicos.PageIndex = 0;
            CargarGrillaMedicos();
        }

        private void CargarProvincias()
        {
            NegocioProvincias negocioProvincias = new NegocioProvincias();
            ddlProvincia.DataSource = negocioProvincias.getTabla();
            ddlProvincia.DataTextField = "NombreProvincia";
            ddlProvincia.DataValueField = "Id_Provincia";
            ddlProvincia.DataBind();
            ddlProvincia.Items.Insert(0, new ListItem("-- Elija una provincia --", ""));
        }

        private void CargarLocalidades(string idProvincia)
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

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidades(ddlProvincia.SelectedValue);
        }

        protected void gvGestionMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGestionMedicos.PageIndex = e.NewPageIndex;
            CargarGrillaMedicos();
        }

        protected void gvGestionMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int idMedico = Convert.ToInt32(gvGestionMedicos.DataKeys[e.NewEditIndex].Value);
            CargarMedicoEnFormulario(idMedico);
        }

        private void CargarMedicoEnFormulario(int idMedico)
        {
            NegocioMedicos negocio = new NegocioMedicos();
            DataTable dt = negocio.getMedicoPorId(idMedico);
            if (dt == null || dt.Rows.Count == 0) return;

            DataRow r = dt.Rows[0];

            hdnIdMedico.Value = r["Id_Medico"].ToString();
            hdnIdPersona.Value = r["Id_Persona"].ToString();

            txtLegajo.Text = r["Legajo_Medico"].ToString();
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

            if (ddlEspecialidad.Items.FindByValue(r["Id_Especialidad"].ToString()) != null)
                ddlEspecialidad.SelectedValue = r["Id_Especialidad"].ToString();

            ddlProvincia.SelectedValue = r["Id_Provincia"].ToString();
            CargarLocalidades(r["Id_Provincia"].ToString());
            if (ddlLocalidad.Items.FindByValue(r["Id_Localidad"].ToString()) != null)
                ddlLocalidad.SelectedValue = r["Id_Localidad"].ToString();

            txtUsuario.Text = r["Usuario"] == DBNull.Value ? "" : r["Usuario"].ToString();
            txtContrasenia.Text = "";
            txtConfirmarContrasenia.Text = "";

            string dias = r["DiasDisponibles"] == DBNull.Value ? "" : r["DiasDisponibles"].ToString();
            foreach (ListItem it in cblDiasDisponibles.Items)
                it.Selected = dias.Contains(it.Value);

            if (r["HoraInicio"] != DBNull.Value)
            {
                TimeSpan hi = (TimeSpan)r["HoraInicio"];
                ddlHorario.SelectedValue = hi.Hours <= 9 ? "1" : "2";
            }

            txtLegajo.Enabled = false;
            txtDni.Enabled = false;
            btnCargar.Text = "Actualizar Médico";
            hCargarMedico.InnerText = "Editar Médico";
            lblMensaje.Text = "";
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {

            lblErrorContrasenia.Text = "";
            if (!Page.IsValid) return;

            if (string.IsNullOrWhiteSpace(txtLegajo.Text) ||
                string.IsNullOrWhiteSpace(txtDni.Text) ||
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

            bool esAlta = string.IsNullOrEmpty(hdnIdMedico.Value);
            if (esAlta && !string.IsNullOrWhiteSpace(txtUsuario.Text) &&
                (txtContrasenia.Text.Length == 0 || txtConfirmarContrasenia.Text.Length == 0))
            {
                MostrarMensaje("Ingrese la contraseña dos veces para el usuario.", false);
                return;
            }
            if (txtContrasenia.Text != txtConfirmarContrasenia.Text)
            {
                lblErrorContrasenia.Text = " * Las contraseñas no coinciden.";
                MostrarMensaje("", false);
                return;
            }

            Medico m = new Medico();
            m.LegajoMedico = txtLegajo.Text.Trim();
            m.Dni = txtDni.Text.Trim();
            m.Nombre = txtNombre.Text.Trim();
            m.Apellido = txtApellido.Text.Trim();
            m.Sexo = Convert.ToChar(ddlSexo.SelectedValue);
            m.Nacionalidad = txtNacionalidad.Text.Trim();
            m.FechaNacimiento = Convert.ToDateTime(txtFechaNac.Text);
            m.Direccion = txtDireccion.Text.Trim();
            m.Telefono = txtTelefono.Text.Trim();
            m.CorreoElectronico = txtEmail.Text.Trim();
            m.IdLocalidad = Convert.ToInt32(ddlLocalidad.SelectedValue);
            m.IdEspecialidad = Convert.ToInt32(ddlEspecialidad.SelectedValue);
            m.Usuario = txtUsuario.Text.Trim();
            m.Contrasenia = txtContrasenia.Text;
            m.DiasDisponibles = ObtenerDiasSeleccionados();

            string horaInicio, horaFin;
            ObtenerHorario(out horaInicio, out horaFin);
            m.HoraInicio = horaInicio;
            m.HoraFin = horaFin;

            NegocioMedicos negocio = new NegocioMedicos();
            bool exito;

            if (string.IsNullOrEmpty(hdnIdMedico.Value))
            {
                exito = negocio.guardarMedico(m);
                if (exito) MostrarMensaje("Se agregó correctamente en la base de datos.", true);
                else MostrarMensaje("Error al guardar. Verifique DNI, legajo o usuario duplicados.", false);

            }
            else
            {
                m.IdMedico = Convert.ToInt32(hdnIdMedico.Value);
                m.IdPersona = Convert.ToInt32(hdnIdPersona.Value);
                exito = negocio.modificarMedico(m);
                if (exito) MostrarMensaje("Se modificó correctamente en la base de datos.", true);
                else MostrarMensaje("Hubo un error al modificar el médico.", false);

            }

            fullscreenOverlay.Style["display"] = "none";
            divFormulario.Visible = false;
            if (exito)
            {

                CargarGrillaMedicos();
                LimpiarFormulario();

            }
        }
        private string ObtenerDiasSeleccionados()
        {
            string dias = "";
            foreach (ListItem it in cblDiasDisponibles.Items)
                if (it.Selected) dias += it.Value;
            return dias;
        }

        private void ObtenerHorario(out string horaInicio, out string horaFin)
        {
            if (ddlHorario.SelectedValue == "2") { horaInicio = "10:00"; horaFin = "18:00"; }
            else { horaInicio = "08:00"; horaFin = "16:00"; }
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

        private void LimpiarFormulario()
        {
            hdnIdMedico.Value = "";
            hdnIdPersona.Value = "";
            txtLegajo.Text = "";
            txtDni.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNacionalidad.Text = "";
            txtFechaNac.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtUsuario.Text = "";
            txtContrasenia.Text = "";
            txtConfirmarContrasenia.Text = "";
            ddlSexo.SelectedIndex = 0;
            if (ddlEspecialidad.Items.Count > 0) ddlEspecialidad.SelectedIndex = 0;
            ddlProvincia.SelectedIndex = 0;
            ddlLocalidad.Items.Clear();
            ddlHorario.SelectedIndex = 0;
            foreach (ListItem it in cblDiasDisponibles.Items) it.Selected = false;

            txtLegajo.Enabled = true;
            txtDni.Enabled = true;
            btnCargar.Text = "Cargar Médico";
            hCargarMedico.InnerText = "Cargar Nuevo Médico";
        }


        protected void gvGestionMedicos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            hdnIdMedico.Value = gvGestionMedicos.DataKeys[e.RowIndex].Value.ToString();
            fullscreenOverlay.Style["display"] = "block";
            divEliminar.Visible = true;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            divEliminar.Visible = false;
            int id = Convert.ToInt32(hdnIdMedico.Value);
            Medico med = new Medico();
            NegocioMedicos negMed = new NegocioMedicos();

            med.IdMedico = id;

            if (negMed.eliminarMedico(med))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Se eliminó correctamente de la base de datos.";
            }
            else
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Hubo un error al eliminar el registro.";
            }
            fullscreenOverlay.Style["display"] = "none";
            gvGestionMedicos.EditIndex = -1;
            CargarGrillaMedicos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divEliminar.Visible = false;
            fullscreenOverlay.Style["display"] = "none";
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            fullscreenOverlay.Style["display"] = "block";
            int idMedico = Convert.ToInt32(((Button)sender).CommandArgument);
            CargarMedicoEnFormulario(idMedico);
            divFormulario.Visible = true;

        }

        protected void btnMostrarForm_Click(object sender, EventArgs e)
        { 
            LimpiarFormulario();
            fullscreenOverlay.Style["display"] = "block";
            divFormulario.Visible = true;
        }
        protected void cvDiasDisponibles_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool alMenosUno = false;

            foreach (ListItem item in cblDiasDisponibles.Items)
            {
                if (item.Selected)
                {
                    alMenosUno = true;
                    break;
                }
            }
            args.IsValid = alMenosUno;
        }
    }
}

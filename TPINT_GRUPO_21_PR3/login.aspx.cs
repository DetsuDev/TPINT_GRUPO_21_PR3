using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3
{
    public partial class Login : System.Web.UI.Page
    {

        public string usuario = "";
        public string contrasenia = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                Session["UsuarioLogeado"] = null;
            }
        }

        protected void btnLogearse_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = ""; 

            if (Page.IsValid)
            {
                string usuario = txtUsuario.Text.Trim();
                string contrasenia = txtContrasena.Text.Trim();

                NegocioUsuarios negocioUsuarios = new NegocioUsuarios();
                DataTable dtUsuario = negocioUsuarios.verificarCredenciales(usuario, contrasenia);

                if (dtUsuario != null && dtUsuario.Rows.Count > 0)
                {
                    DataRow row = dtUsuario.Rows[0];

                    bool estadoActivo = Convert.ToBoolean(row["Estado"]);
                    if (!estadoActivo)
                    {
                        lblMensaje.Text = "Usuario inactivo. Baja logica";
                        return;
                    }

                    Usuario user = new Usuario();

                    user.persona.Nombre = (string)row["Nombre"];
                    user.persona.Apellido = (string)row["Apellido"];
                    user.Rol = (string)row["Rol"];

                    Session["UsuarioLogueado"] = user;

                    string rol = row["Rol"].ToString();

                    if (rol == "A") /// Admin
                    {
                        Response.Redirect("~/MenuAdmin/AdminInformes.aspx");
                    }
                    else if (rol == "M") /// Medico
                    {
                        Response.Redirect("~/MenuMedico/MedicoTurnos.aspx");
                    }
                }
                else
                {
                    lblMensaje.Text = "Usuario o contraseña incorrectos.";
                }
            }
        }
    }
}
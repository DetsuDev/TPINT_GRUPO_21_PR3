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

        }

        protected void btnTestLoginMedico_Click(object sender, EventArgs e)
        {
            
            if (Page.IsValid)
            {
                Response.Redirect("~/MenuMedico/MedicoTurnos.aspx");
            }
        }

        protected void btnTestLoginAdmin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Redirect("~/MenuAdmin/AdminInformes.aspx");
            }
        }

        protected void btnTestLogearse_Click(object sender, EventArgs e)
        {



        string usuario = txtUsuario.Text;
            string contrasenia = txtContrasena.Text;

            if (Page.IsValid)
            {
                NegocioUsuarios usuarios = new NegocioUsuarios();
                if (usuarios.buscarUsuario(usuario, contrasenia))
                {

                    Response.Redirect("~/MenuAdmin/AdminInformes.aspx");
                    Session["UsuarioLogeado"] = usuario;

                }
                else
                {
                    lblMensaje.Visible = true;
                }
            }

        }
    }
}
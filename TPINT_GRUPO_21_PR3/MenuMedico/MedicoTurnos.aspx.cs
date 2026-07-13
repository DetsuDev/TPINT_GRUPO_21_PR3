using Entidades;
using System;
using Negocio;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPINT_GRUPO_21_PR3.MenuAdmin;

namespace TPINT_GRUPO_21_PR3.MenuMedico
{
    public partial class MedicoTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["UsuarioLogueado"];

            if (user == null || user.Rol != "M")
            {
                Response.Redirect("~/SesionInvalida.html");
            }

            if (!IsPostBack)
            {
                lblNombreUsuario.Text = user.persona.Nombre + " " + user.persona.Apellido;
                CargarGrillaTurnos();
            }

        }

        private void CargarGrillaTurnos()
        {
            Usuario user = (Usuario)Session["UsuarioLogueado"];

            NegocioMedicos negocioMedicos = new NegocioMedicos();

            int idMedico = negocioMedicos.obtenerIdMedicoPorIdPersona(user.IdPersona);

            NegocioTurnos negocioTurnos = new NegocioTurnos();

            DataTable dt = negocioTurnos.getTabla(idMedico);

            List<string> filtros = new List<string>();
            if (!string.IsNullOrWhiteSpace(txtBuscarDni.Text))
                filtros.Add("DNI LIKE '%" + txtBuscarDni.Text.Trim().Replace("'", "''") + "%'");
            if (!string.IsNullOrWhiteSpace(txtBuscarPaciente.Text))
                filtros.Add("Paciente LIKE '%" + txtBuscarPaciente.Text.Trim().Replace("'", "''") + "%'");
            if (!string.IsNullOrWhiteSpace(txtBuscarFecha.Text))
                filtros.Add("Fecha LIKE '%" + txtBuscarFecha.Text.Trim().Replace("'", "''") + "%'");

            DataView dv = dt.DefaultView;
            if (filtros.Count > 0)
            {
                dv.RowFilter = string.Join(" AND ", filtros);
            }


            gvMedicoTurnos.DataSource = dv;
            gvMedicoTurnos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaTurnos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarDni.Text = "";
            txtBuscarPaciente.Text = "";
            txtBuscarFecha.Text = "";
            CargarGrillaTurnos();
        }

        protected void btnConfirmarPresentismo_Click(object sender, EventArgs e)
        {
            GridViewRow fila = (GridViewRow)((Button)sender).NamingContainer;
            int idTurno = Convert.ToInt32(gvMedicoTurnos.DataKeys[fila.RowIndex].Value);

            RadioButtonList rbl = (RadioButtonList)fila.FindControl("rblPresentismo");
            TextBox txtObs = (TextBox)fila.FindControl("txtObsPresentismo");

            if (string.IsNullOrEmpty(rbl.SelectedValue))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Seleccione Presente o Ausente.";
                return;
            }

            Negocio.NegocioTurnos negocioTurnos = new Negocio.NegocioTurnos();
            bool ok = negocioTurnos.marcarPresentismo(idTurno, rbl.SelectedValue, txtObs.Text.Trim());

            lblMensaje.ForeColor = ok ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            lblMensaje.Text = ok ? "Presentismo registrado correctamente." : "Hubo un error al registrar el presentismo.";
            CargarGrillaTurnos();
        }
    }
}
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
            {
                // El calendario (input type=date) envía yyyy-MM-dd; la grilla muestra dd/MM/yyyy → convertir para que matchee
                if (DateTime.TryParse(txtBuscarFecha.Text.Trim(), out DateTime fechaFiltro))
                    filtros.Add("Fecha = '" + fechaFiltro.ToString("dd/MM/yyyy") + "'");
            }

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

            TextBox txtObs = (TextBox)fila.FindControl("txtObservacion");

            if (string.IsNullOrEmpty(rbl.SelectedValue))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Seleccione Presente o Ausente antes de guardar.";
                return;
            }

            Negocio.NegocioTurnos negocioTurnos = new Negocio.NegocioTurnos();
            bool ok = negocioTurnos.marcarPresentismo(idTurno, rbl.SelectedValue, txtObs.Text.Trim());

            lblMensaje.ForeColor = ok ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            lblMensaje.Text = ok ? "Cambios guardados correctamente." : "Hubo un error al guardar.";
            CargarGrillaTurnos();
        }
        protected void gvMedicoTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEstado = (Label)e.Row.FindControl("lblEstadoActual");

                if (lblEstado != null)
                {
                    string color;
                    switch (lblEstado.Text.Trim().ToUpper())
                    {
                        case "PENDIENTE": color = "#FFC107"; break;
                        case "PRESENTE": color = "#198754"; break;
                        case "AUSENTE": color = "#DC3545"; break;
                        default: return;
                    }

                    string borde = "2px solid " + color;
                    foreach (TableCell celda in e.Row.Cells)
                    {
                        celda.Style["border-top"] = borde;
                        celda.Style["border-bottom"] = borde;
                    }
                    e.Row.Cells[0].Style["border-left"] = borde;
                    e.Row.Cells[e.Row.Cells.Count - 1].Style["border-right"] = borde;
                }
            }
        }
    }
}
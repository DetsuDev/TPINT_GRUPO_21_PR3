using Negocio;
using System;
using System.Data;

namespace TPINT_GRUPO_21_PR3.MenuAdmin
{
    public partial class Informes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogeado"] == null)
            {
                Response.Redirect("~/login.aspx");
                return;
            }

            lblNombreUsuario.Text = Session["UsuarioLogeado"].ToString();

            if (!IsPostBack)
            {
                CargarRanking();
                CargarPresentismo();
            }
        }

        private string FechaODefecto(string valor, string porDefecto)
        {
            DateTime f;
            if (DateTime.TryParse(valor, out f)) return f.ToString("yyyy-MM-dd");
            return porDefecto;
        }

        private void CargarRanking()
        {
            string desde = FechaODefecto(txtFechaInicio.Text, "1900-01-01");
            string hasta = FechaODefecto(txtFechaFin.Text, "2999-12-31");

            NegocioTurnos neg = new NegocioTurnos();
            gvRankingEspecialidades.DataSource = neg.getRanking(desde, hasta);
            gvRankingEspecialidades.DataBind();
        }

        private void CargarPresentismo()
        {
            string desde = FechaODefecto(TextBox1.Text, "1900-01-01");
            string hasta = FechaODefecto(TextBox2.Text, "2999-12-31");

            NegocioTurnos neg = new NegocioTurnos();
            DataTable dt = neg.getPresentismo(desde, hasta);

            int presentes = dt.Rows[0]["Presentes"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Presentes"]);
            int ausentes = dt.Rows[0]["Ausentes"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Ausentes"]);
            int total = presentes + ausentes;

            int porcPresentes = total == 0 ? 0 : (int)Math.Round(presentes * 100.0 / total);
            int porcAusentes = total == 0 ? 0 : 100 - porcPresentes;

            barraPresentes.Attributes["style"] = "width: " + porcPresentes + "%";
            barraPresentes.InnerText = porcPresentes + "%";
            barraAusentes.Attributes["style"] = "width: " + porcAusentes + "%";
            barraAusentes.InnerText = porcAusentes + "%";
        }

        protected void btnFiltrarRanking_Click(object sender, EventArgs e)
        {
            CargarRanking();
        }

        protected void btnPresentismo_Click(object sender, EventArgs e)
        {
            CargarPresentismo();
        }
    }
}

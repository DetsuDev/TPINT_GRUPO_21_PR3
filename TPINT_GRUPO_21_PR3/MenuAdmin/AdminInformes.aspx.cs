using Entidades;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3.MenuAdmin
{
    public partial class Informes : Culture
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Usuario user = (Usuario)Session["UsuarioLogueado"];

            if (user == null || user.Rol != "A")
            {
                Response.Redirect("~/SesionInvalida.html");
            }


            lblNombreUsuario.Text = user.persona.Nombre + " " + user.persona.Apellido;

            CargarRankingMock();
            

        }
        private void CargarRankingMock()
        {
            NegocioTurnos negTurnos = new NegocioTurnos();
            DataTable dt = negTurnos.filtrarRanking("2001-00-00", "2050-12-31");

            gvRankingEspecialidades.DataSource = dt;
            gvRankingEspecialidades.DataBind();
            return;

        }
        

        protected void btnInformeFechas_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = DateTime.Parse(txtFechaInicioPresentismo.Text);
            DateTime fechaFin = DateTime.Parse(txtFechaFinPresentismo.Text);

            NegocioTurnos negTurnos = new NegocioTurnos();

            int[] presentismo = negTurnos.calcularPresentismoPorFecha(fechaInicio, fechaFin);


            calcularBarra(presentismo[0], presentismo[1], presentismo[2], presentismo[3]);


            txtFechaInicioProductividad.Text = "dd/mm/aaaa";
            txtFechaFinProductividad.Text = "dd/mm/aaaa";

        }

        protected void ocultarBarras()
        {
            barraVerde.Visible=false;
            barraAmarilla.Visible = false;
            barraRoja.Visible=false;
        }

        protected void btnFiltrarRanking_Click(object sender, EventArgs e)
        {

            string minFechaString = txtFechaInicioProductividad.Text;
            string maxFechaString = txtFechaFinProductividad.Text;

            NegocioTurnos neg = new NegocioTurnos();

            DataTable dt2 = neg.filtrarRanking(minFechaString, maxFechaString);
            
           
            gvRankingEspecialidades.DataSource = dt2;
            gvRankingEspecialidades.DataBind();

            txtFechaInicioPresentismo.Text = "dd/mm/aaaa";
            txtFechaFinPresentismo.Text = "dd/mm/aaaa";
        }

        protected void btnLimpiarRanking_Click(object sender, EventArgs e)
        {

            CargarRankingMock();

            txtFechaInicioProductividad.Text = "dd/mm/aaaa";
            txtFechaFinProductividad.Text = "dd/mm/aaaa";

        }

        protected void btnLimpiarInforme_Click(object sender, EventArgs e)
        {
            ocultarBarras();
            txtFechaInicioPresentismo.Text = "dd/mm/aaaa";
            txtFechaFinPresentismo.Text = "dd/mm/aaaa";
        }
        private void calcularBarra(int cPresentes, int cPendientes, int cAusentes, float cTotal)
        {
            if (cTotal > 0)
            {
                float pPresentes = (cPresentes / cTotal) * 100;
                float pPendientes = (cPendientes / cTotal) * 100;
                float pAusentes = (cAusentes / cTotal) * 100;

                barraVerde.Style["width"] = $"{pPresentes.ToString("F2", CultureInfo.InvariantCulture)}%";
                barraVerde.InnerText = $"{pPresentes:F2}% ({cPresentes})";

                barraAmarilla.Style["width"] = $"{pPendientes.ToString("F2", CultureInfo.InvariantCulture)}%";
                barraAmarilla.InnerText = $"{pPendientes:F2}% ({cPendientes})";

                barraRoja.Style["width"] = $"{pAusentes.ToString("F2", CultureInfo.InvariantCulture)}%";
                barraRoja.InnerText = $"{pAusentes:F2}% ({cAusentes})";
            }
            else
            {
                barraVerde.Style["width"] = "0%";
                barraAmarilla.Style["width"] = "0%";

                barraRoja.Style["width"] = "100%";
                barraRoja.InnerText =
                    "NO HAY DATOS CON LOS TÉRMINOS INDICADOS.";
            }
        }

    }
}
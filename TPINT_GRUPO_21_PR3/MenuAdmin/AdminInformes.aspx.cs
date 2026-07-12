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
    public partial class Informes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ocultarInformes();
            informeSegunFecha.Visible = true;
            //ocultarBarras();

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

            float[] presentismo = negTurnos.calcularPresentismo(fechaInicio, fechaFin);
            float pPresentes = presentismo[0];
            int cConfirmados = (int)presentismo[1];
            int tTurnos = (int)presentismo[2];

            if (cConfirmados > 0)
            {
                barraVerde.Style["width"] = $"{pPresentes.ToString("F2", CultureInfo.InvariantCulture)}%;";
                barraVerde.InnerText = $"{pPresentes:F2}% ({cConfirmados})";

                barraRoja.Style["width"] = $"{(100 - pPresentes).ToString("F2", CultureInfo.InvariantCulture)}%;";
                barraRoja.InnerText = $"{100 - pPresentes:F2}% ({tTurnos - cConfirmados})";

                barraVerde.Visible = true;
            }
            else
            {
                barraRoja.Style["width"] = $"100%";
                barraRoja.InnerText = $"NO HAY DATOS EN EL INTERVALO INDICADO.";

            }


            barraRoja.Visible = true;

            txtFechaInicioProductividad.Text = "dd/mm/aaaa";
            txtFechaFinProductividad.Text = "dd/mm/aaaa";

        }

        protected void ocultarBarras()
        {
            barraVerde.Visible=false;
            barraRoja.Visible=false;
        }
        protected void ocultarInformes()
        {
            informeSegunEspecialidad.Visible = false;
            informeSegunFecha.Visible = false;
            informeSegunMedico.Visible = false;
        }
        protected void dpInformes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (dpInformes.SelectedIndex)
            {
                case 0:
                    ocultarInformes();
                    informeSegunFecha.Visible = true;
                    break;
                case 1:
                    ocultarInformes();
                    cargarEspecialidad();
                    informeSegunEspecialidad.Visible = true;
                    break;
                case 2:
                    ocultarInformes();
                    cargarMedicos();
                    informeSegunMedico.Visible = true;
                    break;
                default:
                    break;
            }

        }

        protected void cargarEspecialidad()
        {
            NegocioEspecialidades neg = new NegocioEspecialidades();
            DataTable dt = neg.getTabla();

            ddlEspecialidad.DataSource = dt;
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Id_Especialidad";
            ddlEspecialidad.DataBind();

        }

        protected void cargarMedicos()
        {
            NegocioMedicos Neg = new NegocioMedicos();

            ddlMedicos.DataSource = Neg.getTablaINA();
            ddlMedicos.DataTextField = "NombreApellido";
            ddlMedicos.DataValueField = "Id_Medico";
            ddlMedicos.DataBind();
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

            txtFechaInicioPresentismo.Text = "dd/mm/aaaa";
            txtFechaFinPresentismo.Text = "dd/mm/aaaa";
        }
    }
}
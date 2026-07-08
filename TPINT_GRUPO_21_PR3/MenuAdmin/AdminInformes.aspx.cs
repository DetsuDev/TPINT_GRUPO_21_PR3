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
            presentesFecha.Visible = false;
            ausentesFecha.Visible = false;
            /*
            barraPresentismoFechas.Visible = false;
            barraEspecialidadMedico.Visible = false;*/

            Usuario user = (Usuario)Session["UsuarioLogueado"];

            if (user == null || user.Rol != "A")
            {
                Response.Redirect("~/SesionInvalida.html");
            }


            lblNombreUsuario.Text = user.persona.Nombre + " " + user.persona.Apellido;

            if (!IsPostBack)
            {
                CargarRankingMock();
            }

        }

        private void CargarRankingMock()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Especialidad");
            dt.Columns.Add("CantidadTurnos");

            dt.Rows.Add("Pediatria", "30");
            dt.Rows.Add("Traumatologia", "22");
            dt.Rows.Add("Odontologia", "19");
            dt.Rows.Add("Cardiologia", "15");
            dt.Rows.Add("Demartología", "14");
            dt.Rows.Add("Clinica Médica", "9");

            gvRankingEspecialidades.DataSource = dt;
            gvRankingEspecialidades.DataBind(); 
        }

        protected void btnInformeFechas_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = DateTime.Parse(txtFechaInicioPresentismo.Text);
            DateTime fechaFin = DateTime.Parse(txtFechaFinPresentismo.Text);

            NegocioTurnos negTurnos = new NegocioTurnos();

            float pPresentes = negTurnos.calcularPresentismo(fechaInicio, fechaFin);
            float pAusentes = 100 - pPresentes;

            int[] sumatoria = negTurnos.sumatoriaPresentismo(fechaInicio, fechaFin);
            int totalTurnos = sumatoria[0];
            int totalPresentes = sumatoria[1];
            int totalAusentes = totalTurnos - totalPresentes;

            presentesFecha.Style["width"] = $"{pPresentes.ToString("F2", CultureInfo.InvariantCulture)}%;";
            presentesFecha.InnerText = $"{pPresentes:F2}% ({totalPresentes})";

            ausentesFecha.Style["width"] = $"{pAusentes.ToString("F2", CultureInfo.InvariantCulture)}%;";
            ausentesFecha.InnerText = $"{pAusentes:F2}% ({totalAusentes})";

            presentesFecha.Visible = true;
            ausentesFecha.Visible = true;


        }
    }
}
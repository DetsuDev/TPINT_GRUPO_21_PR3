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

            float[] presentismo = negTurnos.calcularPresentismo(fechaInicio, fechaFin);
            float pPresentes = presentismo[0];
            int cConfirmados = (int)presentismo[1];
            int tTurnos = (int)presentismo[2];


            presentesFecha.Style["width"] = $"{pPresentes.ToString("F2", CultureInfo.InvariantCulture)}%;";
            presentesFecha.InnerText = $"{pPresentes:F2}% ({cConfirmados})";

            ausentesFecha.Style["width"] = $"{(100 - pPresentes).ToString("F2", CultureInfo.InvariantCulture)}%;";
            ausentesFecha.InnerText = $"{100 - pPresentes:F2}% ({tTurnos - cConfirmados})";

            presentesFecha.Visible = true;
            ausentesFecha.Visible = true;


        }
    }
}
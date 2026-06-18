using System;
using System.Collections.Generic;
using System.Data;
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
            if (!IsPostBack)
            {

                CargarRankingMock();
            }

            CargarPresentismo();
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

        private void CargarPresentismo()
        {

                int success = 70;
                int danger = 30;

                barraPresentes.Attributes["style"] = $"width: {success}%";
                barraPresentes.InnerText = $"{success}%";

                barraAusentes.Attributes["style"] = $"width: {danger}%";
                barraAusentes.InnerText = $"{danger}%";
            
        }

    }
}
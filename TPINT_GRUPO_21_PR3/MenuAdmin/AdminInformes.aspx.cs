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
        DataTable informeTotal = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            ocultarInformes();
            informeSegunFecha.Visible = true;
            ocultarBarras();

            Usuario user = (Usuario)Session["UsuarioLogueado"];

            if (user == null || user.Rol != "A")
            {
                Response.Redirect("~/SesionInvalida.html");
            }


            lblNombreUsuario.Text = user.persona.Nombre + " " + user.persona.Apellido;


                informeTotal = CargarRankingMock();
            

        }

        private DataTable CargarRankingMock()
        {
            /*
            dt.Columns.Add("Especialidad");
            dt.Columns.Add("CantidadTurnos");

            dt.Rows.Add("Pediatria", "30");
            dt.Rows.Add("Traumatologia", "22");
            dt.Rows.Add("Odontologia", "19");
            dt.Rows.Add("Cardiologia", "15");
            dt.Rows.Add("Demartología", "14");
            dt.Rows.Add("Clinica Médica", "9");
            */

            DataTable dt = new DataTable();
            
            NegocioTurnos negTurno = new NegocioTurnos();

            dt = negTurno.getTabla();

            int Pediatria = 0;
            int Traumatologia = 0;
            int Cardiologia = 0;
            int Dermatologia = 0;

            foreach (DataRow dr in dt.Rows) {
                switch ((string)dr["Especialidad"])
                {
                    case "Cardiología":
                        Cardiologia++;
                        break;

                    case "Pediatría":
                        Pediatria++;
                        break;

                    case "Traumatología":
                        Traumatologia++;
                        break;

                    case "Dermatología":
                        Dermatologia++;
                        break;
                }
            
            }

            DataTable dt2 = new DataTable();

            dt2.Columns.Add("Especialidad");
            dt2.Columns.Add("CantidadTurnos");
            dt2.Rows.Add("Pediatria", $"{Pediatria}");
            dt2.Rows.Add("Traumatologia", $"{Traumatologia}");
            dt2.Rows.Add("Cardiologia", $"{Cardiologia}");
            dt2.Rows.Add("Demartología", $"{Dermatologia}");

            gvRankingEspecialidades.DataSource = dt2;
            gvRankingEspecialidades.DataBind();

            return dt;
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
            DataTable dt = informeTotal;

            string minFechaString = txtFechaInicioProductividad.Text;
            string maxFechaString = txtFechaFinProductividad.Text;

            string formato = "yyyy-MM-dd";

            DateTime.TryParseExact(minFechaString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime minFecha);
            DateTime.TryParseExact(maxFechaString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime maxFecha);

            DataTable dt2 = new DataTable();

            int Pediatria = 0;
            int Traumatologia = 0;
            int Cardiologia = 0;
            int Dermatologia = 0;

            foreach (DataRow dr in dt.Rows)
            {

                    if (((DateTime)dr["FechaDateTime"] >= minFecha) && ((DateTime)dr["FechaDateTime"] <= maxFecha))
                    {
                        switch ((string)dr["Especialidad"])
                        {
                            case "Cardiología":
                                Cardiologia++;
                                break;

                            case "Pediatría":
                                Pediatria++;
                                break;

                            case "Traumatología":
                                Traumatologia++;
                                break;

                            case "Dermatología":
                                Dermatologia++;
                                break;
                        }
                    }
                
            }

            dt2.Columns.Add("Especialidad");
            dt2.Columns.Add("CantidadTurnos");
            dt2.Rows.Add("Pediatria", $"{Pediatria}");
            dt2.Rows.Add("Traumatologia", $"{Traumatologia}");
            dt2.Rows.Add("Cardiologia", $"{Cardiologia}");
            dt2.Rows.Add("Demartología", $"{Dermatologia}");

            gvRankingEspecialidades.DataSource = dt2;
            gvRankingEspecialidades.DataBind();
        }
    }
}
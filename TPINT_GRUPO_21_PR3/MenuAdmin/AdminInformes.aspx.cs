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
            ocultarBarras();

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
            DataTable dt = Neg.getTabla();

            DataTable tablaINA = new DataTable();
            tablaINA.Columns.Add("id");
            tablaINA.Columns.Add("nombreApellido");

            foreach (DataRow row in dt.Rows)
            {
                tablaINA.Rows.Add(row["Id_Medico"], row["Nombre"].ToString() + " " + row["Apellido"].ToString());
            }

            ddlMedicos.DataSource = tablaINA;
            ddlMedicos.DataTextField = "nombreApellido";
            ddlMedicos.DataValueField = "id";
            ddlMedicos.DataBind();
        }



    }
}
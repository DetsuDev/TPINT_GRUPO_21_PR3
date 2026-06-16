using System;
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
            if (!IsPostBack)
            {
                CargarGrillaTurnos();
            }

        }

        private void CargarGrillaTurnos()
        {
            DataTable dt = ObtenerTurnos();

            List<string> filtros = new List<string>();
            if (!string.IsNullOrWhiteSpace(txtBuscarDni.Text))
                filtros.Add("DNI LIKE '%" + txtBuscarDni.Text.Trim().Replace("'", "''") + "%'");
            if (!string.IsNullOrWhiteSpace(txtBuscarPaciente.Text))
                filtros.Add("Paciente LIKE '%" + txtBuscarPaciente.Text.Trim().Replace("'", "''") + "%'");
            if (!string.IsNullOrWhiteSpace(txtBuscarFecha.Text))
                filtros.Add("Fecha LIKE '%" + txtBuscarFecha.Text.Trim().Replace("'", "''") + "%'");

            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Join(" AND ", filtros);

            gvMedicoTurnos.DataSource = dv;
            gvMedicoTurnos.DataBind();
        }

        private DataTable ObtenerTurnos()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("DNI");
            dt.Columns.Add("Paciente");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Hora");
            dt.Columns.Add("Observacion");

            dt.Rows.Add("1","12345465", "Juan Pérez", "15/06/2026", "09:00", "Control general");
            dt.Rows.Add("2", "12345465", "María Gómez", "15/06/2026", "09:30", "Dolor de cabeza");
            dt.Rows.Add("3", "12345215", "Carlos López", "15/06/2026", "10:00", "Análisis clínicos");
            dt.Rows.Add("4", "12345435", "Ana Rodríguez", "15/06/2026", "10:30", "Control anual");
            dt.Rows.Add("5", "12345235", "Pedro Martínez", "15/06/2026", "11:00", "Vacunación");
            dt.Rows.Add("6", "12342355", "Laura Fernández", "15/06/2026", "11:30", "Consulta cardiológica");
            dt.Rows.Add("7", "12345345", "Diego Sánchez", "15/06/2026", "12:00", "Dolor lumbar");
            dt.Rows.Add("8", "12345443", "Sofía Torres", "15/06/2026", "12:30", "Control pediátrico");

            return dt;
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
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3.MenuAdmin
{
    public partial class GestionMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrillaMedicos();
            }

            if (!IsPostBack)
            {
                ddlEspecialidad.Items.Add(new ListItem("Pediatría", "1"));
                ddlEspecialidad.Items.Add(new ListItem("Traumatología", "2"));
                ddlHorario.Items.Add(new ListItem("08:00 - 16:00", "1"));
                ddlHorario.Items.Add(new ListItem("10:00 - 18:00", "2"));
                ddlProvincia.Items.Add(new ListItem("Buenos Aires", "1"));
                ddlLocalidad.Items.Add(new ListItem("Tigre", "1"));
            }
        }

        private void CargarGrillaMedicos()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Legajo");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellido");
            dt.Columns.Add("Especialidad");
            dt.Columns.Add("Horario");
            dt.Columns.Add("Sexo");
            dt.Columns.Add("Nacionalidad");
            dt.Columns.Add("FechaNac");
            dt.Columns.Add("Direccion");
            dt.Columns.Add("Localidad");
            dt.Columns.Add("Provincia");
            dt.Columns.Add("Email");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Usuario");
            dt.Columns.Add("Contrasenia");

            dt.Rows.Add("1", "MED-101", "Juan", "Pérez", "Pediatría", "08:00 - 16:00", "M", "Argentina", "10/05/1980", "Av. Siempre Viva 123", "Tigre", "Buenos Aires", "juan@mail.com", "1122334455", "jperez", "jperez1234");
            dt.Rows.Add("2", "MED-102", "Ana", "Gómez", "Traumatología", "10:00 - 18:00", "F", "Argentina", "15/08/1990", "San Martín 456", "San Isidro", "Buenos Aires", "ana@mail.com", "1166778899", "agomez", "agomezcontrasenia2");
            dt.Rows.Add("3", "MED-103", "Luis", "Luis", "Cardiología", "08:00 - 16:00", "M", "Argentina", "22/11/1985", "Belgrano 1200", "San Fernando", "Buenos Aires", "luis@mail.com", "1144556622", "lluis", "clave3");
            dt.Rows.Add("4", "MED-104", "Sofía", "Herrera", "Ginecología", "14:00 - 22:00", "F", "Argentina", "04/03/1993", "Alvear 55", "Martínez", "Buenos Aires", "sofia@mail.com", "1122998877", "sherrera", "clave4");
            dt.Rows.Add("5", "MED-105", "Marcos", "Acuña", "Pediatría", "08:00 - 16:00", "M", "Argentina", "18/10/1988", "Maipú 340", "Vicente López", "Buenos Aires", "marcos@mail.com", "1133884411", "macuna", "clave5");
            dt.Rows.Add("6", "MED-106", "Lucía", "Díaz", "Clínica Médica", "12:00 - 20:00", "F", "Argentina", "30/12/1991", "Dardo Rocha 900", "San Isidro", "Buenos Aires", "lucia@mail.com", "1177665544", "ldiaz", "clave6");
            dt.Rows.Add("7", "MED-107", "Bautista", "Suárez", "Dermatología", "08:00 - 16:00", "M", "Argentina", "14/06/1982", "Santa Fe 2300", "Palermo", "CABA", "bauti@mail.com", "1155443322", "bsuarez", "clave7");
            dt.Rows.Add("8", "MED-108", "Ariel", "Ortega", "Clínica Médica", "10:00 - 18:00", "M", "Argentina", "04/03/1974", "Av. Libertador 4500", "Nuñez", "CABA", "ariel@mail.com", "1100112233", "burrito", "clave8");

            gvGestionMedicos.DataSource = dt;
            gvGestionMedicos.DataBind();
        }
        protected void gvGestionMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGestionMedicos.PageIndex = e.NewPageIndex;
            CargarGrillaMedicos();
        }

        protected void gvGestionMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGestionMedicos.EditIndex = e.NewEditIndex;
            CargarGrillaMedicos(); 
        }
        protected void gvGestionMedicos_RowUpdating(object sender, GridViewUpdateEventArgs e) { }
        protected void gvGestionMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGestionMedicos.EditIndex = -1;
            CargarGrillaMedicos();
        }
        protected void gvGestionMedicos_RowDeleting(object sender, GridViewDeleteEventArgs e) { }
    }
}
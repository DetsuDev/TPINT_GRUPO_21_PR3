using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3.MenuAdmin
{
    public partial class AdminPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                divEliminar.Visible = false;
                CargarGrillaPacientes();
            }
        }

        private void CargarGrillaPacientes()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("DNI");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellido");
            dt.Columns.Add("Sexo");
            dt.Columns.Add("Nacionalidad");
            dt.Columns.Add("FechaNac");
            dt.Columns.Add("Direccion");
            dt.Columns.Add("Localidad");
            dt.Columns.Add("Provincia");
            dt.Columns.Add("Email");
            dt.Columns.Add("Telefono");

            dt.Rows.Add("1", "40123456", "Carlos", "Sánchez", "M", "Argentina", "12/03/1997", "Av. Mitre 450", "Avellaneda", "Buenos Aires", "carlos@mail.com", "1133445566");
            dt.Rows.Add("2", "42987654", "María", "López", "F", "Argentina", "25/07/2000", "Belgrano 789", "San Isidro", "Buenos Aires", "maria@mail.com", "1155667788");
            dt.Rows.Add("3", "38111222", "Jorge", "Rodríguez", "M", "Argentina", "05/11/1994", "Lavalle 123", "Tigre", "Buenos Aires", "jorge@mail.com", "1122334455");
            dt.Rows.Add("4", "41555666", "Florencia", "Fernández", "F", "Argentina", "18/01/1999", "Rivadavia 3200", "Ramos Mejía", "Buenos Aires", "flor@mail.com", "1144556677");
            dt.Rows.Add("5", "35444333", "Ricardo", "D Darín", "M", "Argentina", "16/01/1957", "Olazábal 1540", "Belgrano", "CABA", "ricardo@mail.com", "1177889900");
            dt.Rows.Add("6", "43222111", "Agustina", "Martínez", "F", "Argentina", "09/09/2001", "Maipú 600", "Vicente López", "Buenos Aires", "agus@mail.com", "1166778899");
            dt.Rows.Add("7", "39888777", "Claudio", "Caniggia", "M", "Argentina", "09/01/1967", "Pampa 2300", "Nuñez", "CABA", "elpajaro@mail.com", "1188990011");
            dt.Rows.Add("8", "45111000", "Tomas", "Forte", "M", "Argentina", "22/04/2004", "Centenario 1200", "San Isidro", "Buenos Aires", "tomas@mail.com", "1199001122");

            gvGestionPacientes.DataSource = dt;
            gvGestionPacientes.DataBind();
        }
        protected void gvGestionPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGestionPacientes.PageIndex = e.NewPageIndex;
            CargarGrillaPacientes();
        }
        protected void gvGestionPacientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGestionPacientes.EditIndex = e.NewEditIndex;
            CargarGrillaPacientes();
        }

        protected void gvGestionPacientes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }
        protected void gvGestionPacientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGestionPacientes.EditIndex = -1;
            CargarGrillaPacientes();
        }

        protected void gvGestionPacientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            divEliminar.Visible = true;
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
        }

        protected void gvGestionPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            divEliminar.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divEliminar.Visible = false;
        }
    }
}
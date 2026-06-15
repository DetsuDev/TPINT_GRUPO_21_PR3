using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3.GestionAdmin
{
    public partial class GestionMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
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
            dt.Columns.Add("Horario");
            dt.Columns.Add("Usuario");
            dt.Columns.Add("Contrasenia");

            dt.Rows.Add("1", "Juan", "Pérez", "M", "Argentina",
                        "10/05/1980", "Av. Siempre Viva 123",
                        "Tigre", "Buenos Aires",
                        "juan@mail.com", "1122334455",
                        "08:00 - 16:00", "jperez", "jperez1234");

            dt.Rows.Add("2", "Ana", "Gómez", "F", "Argentina",
                        "15/08/1990", "San Martín 456",
                        "San Isidro", "Buenos Aires",
                        "ana@mail.com", "1166778899",
                        "10:00 - 18:00", "agomez", "agomezcontrasenia2");

            gvGestionMedicos.DataSource = dt;
            gvGestionMedicos.DataBind();

        }

        protected void gvGestionMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }


        protected void gvGestionMedicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvGestionMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvGestionMedicos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}
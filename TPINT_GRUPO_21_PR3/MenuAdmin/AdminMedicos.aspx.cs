using Negocio;
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
                divEliminar.Visible = false;
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
            NegocioMedicos negocioMedicos = new NegocioMedicos();
            dt = negocioMedicos.getTabla();
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
        protected void gvGestionMedicos_RowDeleting(object sender, GridViewDeleteEventArgs e) {

            divEliminar.Visible = true;
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
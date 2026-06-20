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
    public partial class AdminPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                divEliminar.Visible = false;
                CargarGrillaPacientes();
                CargarddlProvincias();
            }
        }


        private void CargarGrillaPacientes()
        {
            DataTable dt = new DataTable();
            NegocioPacientes negocioPacientes = new NegocioPacientes();

            dt = negocioPacientes.getTabla();
            gvGestionPacientes.DataSource = dt;
            gvGestionPacientes.DataBind();
        }

        private void CargarddlProvincias()
        {
            DataTable dt = new DataTable();
            NegocioProvincias negocioProvincias= new NegocioProvincias();
            dt = negocioProvincias.getTabla();

            ddlProvincia.DataSource = dt;
            ddlProvincia.DataTextField = "NombreProvincia";
            ddlProvincia.DataValueField = "Id_Provincia";
            ddlProvincia.DataBind();

            ddlProvincia.Items.Insert(0, new ListItem("-- Elija una provincia --", ""));
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

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            NegocioLocalidades negocioLocalidades = new NegocioLocalidades();

            DataTable dt = negocioLocalidades.getTabla();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "Id_Provincia = " + ddlProvincia.SelectedValue;

            ddlLocalidad.DataSource = dv;
            ddlLocalidad.DataTextField = "NombreLocalidad";
            ddlLocalidad.DataValueField = "Id_Localidad";
            ddlLocalidad.DataBind();

        }
    }
}
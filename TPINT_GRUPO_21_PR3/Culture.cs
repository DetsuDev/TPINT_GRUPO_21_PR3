using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPINT_GRUPO_21_PR3
{
    public class Culture:System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            string culture = Session["Culture"]?.ToString() ?? "en"; // en resumidas cuentas: si la cultura no es nula, la convierte a string, y sino, la convierte a EN

            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            base.InitializeCulture();
        }

        protected override void OnInit(EventArgs e) // se encarga de la persistencia de la seleccion del ES / EN
        {
            base.OnInit(e);

            try
            {
                var sel = Session["Culture"]?.ToString() ?? "en";
                var form = this.Form;
                if (form != null)
                {
                    var rbtnEn = form.FindControl("rbtnEn") as RadioButton;
                    var rbtnEs = form.FindControl("rbtnEs") as RadioButton;
                    if (rbtnEn != null && rbtnEs != null)
                    {
                        rbtnEn.Checked = sel == "en";
                        rbtnEs.Checked = sel == "es";
                    }
                }
            }
            catch
            {
                
            }
        }

        protected void rblLanguage_SelectedIndexChanged(object sender, EventArgs e) // handlea el radiobutton cuando hay un nuevo checked
        {
            var rb = sender as RadioButton;
            if (rb == null) return;

            if (rb.ID == "rbtnEn" && rb.Checked) Session["Culture"] = "en";
            else if (rb.ID == "rbtnEs" && rb.Checked) Session["Culture"] = "es";

            Response.Redirect(Request.RawUrl);
        }
    }
}

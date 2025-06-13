using System;

namespace UI.ClaseMaster
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (Session["Usuario"] != null)
            {
                this.MasterPageFile = "~/Usuario/UsuarioMaster.Master";
            }
            else
            {
                this.MasterPageFile = "~/MasterPage.Master";
            }
        }
    }
}
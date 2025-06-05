using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class Contrasena : System.Web.UI.Page
    {
        public string mail { get; set; } = "dd@mail.com";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            if (txtMail.Text == mail)
                Response.Redirect("/Productos.aspx", false);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio; 

namespace UI
{
    public partial class Perfil : System.Web.UI.Page
    {
        public Usuarios UsuarioLogeado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/Inicio.aspx", false);
            }

            UsuarioLogeado = new Usuarios();
            UsuarioLogeado = (Usuarios)Session["Usuario"];

        }
    }
}

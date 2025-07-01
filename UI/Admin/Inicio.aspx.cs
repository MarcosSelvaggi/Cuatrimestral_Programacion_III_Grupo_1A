using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace UI.Admin
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/Usuario/Logearse.aspx", false);
                return;
            }

            Usuarios usuarioLogeado = (Usuarios)Session["Usuario"];

            if (usuarioLogeado.Rol.Id != 1)
            {
                Response.Redirect("/Perfil.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                cargarVentasEntregadas();
            }
        }
        private void cargarVentasEntregadas()
        {
            PedidoManager manager = new PedidoManager();
            var ventas = manager.listarVentasEntregadas();
            rptVentasEntregadas.DataSource = ventas;
            rptVentasEntregadas.DataBind();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace UI.Admin
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
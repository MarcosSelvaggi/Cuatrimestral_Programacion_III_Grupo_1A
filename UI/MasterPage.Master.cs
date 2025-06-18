using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RealizarBusquedaProducto_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBusqueda.Text))
            {
                Response.Redirect("/Productos.aspx", false);
                return; 
            }
            else
            {
                Response.Redirect("/Productos.aspx?busqueda=" + txtBusqueda.Text, false);
                return;
            }
        }
    }
}
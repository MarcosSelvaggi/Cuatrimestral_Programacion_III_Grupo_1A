using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Usuario
{
    public partial class PedidoDetalle : UI.ClaseMaster.BasePage
    {
        public Pedido PedidoSeleccionado { get; set; }
        public Usuarios UsuarioPedido { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                int idPedido = int.Parse(Request.QueryString["id"]);

                PedidoManager pedidoManager = new PedidoManager();
                PedidoSeleccionado = pedidoManager.obtenerPedidoFactura(idPedido);

                UsuarioPedido = PedidoSeleccionado.Usuario;
                Session["PedidoSeleccionado"] = PedidoSeleccionado;
            }
            else
            {
                PedidoSeleccionado = (Pedido)Session["PedidoSeleccionado"];
                UsuarioPedido = PedidoSeleccionado.Usuario;
            }
        }

    }
}
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
        protected Pedido PedidoSeleccionado { get; set; }
        protected Usuarios UsuarioPedido { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("Logearse.aspx");

            if (!IsPostBack)
            {
                int idPedido;

                if (!int.TryParse(Request.QueryString["id"], out idPedido))
                {
                    Response.Redirect("Pedidos.aspx");
                    return;
                }

                PedidoManager pedidoManager = new PedidoManager();
                Pedido pedidoValido = pedidoManager.obtenerPedidoFactura(idPedido);

                if (pedidoValido == null)
                {
                    Response.Redirect("Pedidos.aspx");
                    return;
                }

                int idUsuarioLogueado = ((Usuarios)Session["Usuario"]).Id;

                if (pedidoValido.Usuario.Id != idUsuarioLogueado)
                {
                    Response.Redirect("Pedidos.aspx");
                    return;
                }

                PedidoSeleccionado = pedidoValido;
                UsuarioPedido = pedidoValido.Usuario;
                Session["PedidoSeleccionado"] = pedidoValido;
            }
            else
            {
                PedidoSeleccionado = (Pedido)Session["PedidoSeleccionado"];
                UsuarioPedido = PedidoSeleccionado.Usuario;
            }
        }

    }
}
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace UI.Usuario
{
    public partial class Pedidos : UI.ClaseMaster.BasePage
    {
        public List<Pedido> listaPedidos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idUsuario = ((Usuarios)Session["Usuario"]).Id;
                PedidoManager pedidoManager = new PedidoManager();
                listaPedidos = pedidoManager.listarPedidosPorUsuario(idUsuario);

                rptPedidos.DataSource = listaPedidos;
                rptPedidos.DataBind();
            }
        }
    }
}
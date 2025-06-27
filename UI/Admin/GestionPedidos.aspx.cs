using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace UI.Admin
{
    public partial class GestionPedidos : System.Web.UI.Page
    {
        private PedidoManager managerPedido = new PedidoManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarPedidos();
                cargarEstadosPedidos();
                cargarEstadosEnvio();
                cargarEstadosPago();
            }
        }

        protected void rptPedidos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idPedido = Convert.ToInt32(e.CommandArgument);
            var pedido = managerPedido.obtenerPedidoPorId(idPedido);
            if (pedido != null)
            {
                if (e.CommandName == "Ver")
                {
                    lblDetallePedido.Text = $"Pedido N° {pedido.IdPedido}<br/>Cliente: {pedido.Cliente}<br/>Fecha: {pedido.FechaPedido:dd/MM/yyyy}<br/>Estado: {pedido.EstadoPedido.Descripcion}<br/>Total: ${pedido.PrecioTotal:N2}";
                    lblDetallePago.Text = $"Método: {pedido.DetallePago.Metodo}<br/>" +
                      $"Fecha: {pedido.DetallePago.Fecha:dd/MM/yyyy HH:mm}<br/>";

                    var detalles = managerPedido.obtenerDetallesPorPedido(idPedido);
                    rptDetallePedido.DataSource = detalles;
                    rptDetallePedido.DataBind();


                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirDetalleModal", "var modal = new bootstrap.Modal(document.getElementById('modalDetallePedido')); modal.show();", true);
                }
                else if (e.CommandName == "Editar")
                {
                    lblIdPedidoEditar.Text = pedido.IdPedido.ToString();
                    ddlEstadoEnvio.SelectedValue = pedido.EstadoEnvio.IdEstadoEnvio.ToString();
                    ddlEstadoPedido.SelectedValue = pedido.EstadoPedido.IdEstadoPedido.ToString();
                    ddlEstadoPago.SelectedValue = pedido.EstadoPago.IdEstadoPago.ToString();
                    ddlMetodoPago.Items.Clear();
                    ddlMetodoPago.Items.Add(new ListItem(pedido.DetallePago.Metodo, pedido.DetallePago.Metodo));
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirEditarModal", "var modal = new bootstrap.Modal(document.getElementById('modalEditarEstado')); modal.show();", true);
                }
                else if (e.CommandName == "Eliminar")
                {
                    lblIdEliminarPedido.Text = pedido.IdPedido.ToString();
                    lblDescripcionEliminarPedido.Text = $"¿Estás seguro que querés eliminar el pedido N° <strong>{pedido.IdPedido}</strong>?";
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirEliminarModal", "var modal = new bootstrap.Modal(document.getElementById('modalEliminarPedido')); modal.show();", true);
                }
            }
        }

        protected void btnGuardarEstado_Click(object sender, EventArgs e)
        {
            int idPedido = Convert.ToInt32(lblIdPedidoEditar.Text);
            int nuevoEstadoPedido = Convert.ToInt32(ddlEstadoPedido.SelectedValue);
            int nuevoEstadoEnvio = Convert.ToInt32(ddlEstadoEnvio.SelectedValue);
            int nuevoEstadoPago = Convert.ToInt32(ddlEstadoPago.SelectedValue);
            managerPedido.modificarEstadoPedidoYEnvio(idPedido, nuevoEstadoPedido, nuevoEstadoEnvio, nuevoEstadoPago);
            cargarPedidos();
        }

        protected void btnConfirmarEliminarPedido_Click(object sender, EventArgs e)
        {
            int idPedido = Convert.ToInt32(lblIdEliminarPedido.Text);
            managerPedido.eliminar(idPedido);
            cargarPedidos();
        }

        private void cargarPedidos()
        {
            var lista = managerPedido.listar();
            rptPedidos.DataSource = lista;
            rptPedidos.DataBind();
        }

        private void cargarEstadosPedidos()
        {
            var estados = managerPedido.listarEstados();
            ddlEstadoPedido.DataSource = estados;
            ddlEstadoPedido.DataValueField = "IdEstadoPedido";
            ddlEstadoPedido.DataTextField = "Descripcion";
            ddlEstadoPedido.DataBind();
        }

        private void cargarEstadosEnvio()
        {
            var estadosEnvio = managerPedido.listarEstadosEnvio();
            ddlEstadoEnvio.DataSource = estadosEnvio;
            ddlEstadoEnvio.DataValueField = "IdEstadoEnvio";
            ddlEstadoEnvio.DataTextField = "Descripcion";
            ddlEstadoEnvio.DataBind();
        }

        private void cargarEstadosPago()
        {
            var estadosPago = managerPedido.listarEstadosPago();
            ddlEstadoPago.DataSource = estadosPago;
            ddlEstadoPago.DataValueField = "IdEstadoPago";
            ddlEstadoPago.DataTextField = "Descripcion";
            ddlEstadoPago.DataBind();
        }
    }
}

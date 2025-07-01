using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace UI.Admin
{
    public partial class GestionPedidos : System.Web.UI.Page
    {
        private PedidoManager managerPedido = new PedidoManager();

        private readonly Dictionary<int, List<int>> estadosPagoPermitidosPorEstadoPedido = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2 } },                  // Creado -> Pendiente
            { 2, new List<int> { 1, 2 } },               // Confirmado -> Aprobado, Pendiente
            { 3, new List<int> { 1 } },                  // Procesando -> Aprobado
            { 4, new List<int> { 1, 4 } },               // Completado -> Aprobado, Reembolsado
            { 5, new List<int> { 3, 4 } },               // Cancelado -> Fallido, Reembolsado
            { 6, new List<int> { 4 } }                   // Devuelto -> Reembolsado
        };
        private readonly Dictionary<int, List<int>> estadosEnvioPermitidosPorEstadoPedido = new Dictionary<int, List<int>>
        {

            { 1, new List<int> { 1 } },           // Creado -> No enviado
            { 2, new List<int> { 1, 2 } },        // Confirmado -> No enviado, En preparación
            { 3, new List<int> { 2, 3, 4 } },     // Procesando -> En preparación, Enviado, En tránsito
            { 4, new List<int> { 5 } },           // Completado -> Entregado
            { 5, new List<int> { 6, 7 } },        // Cancelado -> Fallido, Devuelto
            { 6, new List<int> { 7 } },           // Devuelto -> Devuelto
        };
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
                /*else if (e.CommandName == "Editar")
                {
                    lblIdPedidoEditar.Text = pedido.IdPedido.ToString();
                    ddlEstadoEnvio.SelectedValue = pedido.EstadoEnvio.IdEstadoEnvio.ToString();
                    ddlEstadoPedido.SelectedValue = pedido.EstadoPedido.IdEstadoPedido.ToString();
                    ddlEstadoPago.SelectedValue = pedido.EstadoPago.IdEstadoPago.ToString();
                    ddlMetodoPago.Items.Clear();
                    ddlMetodoPago.Items.Add(new ListItem(pedido.DetallePago.Metodo, pedido.DetallePago.Metodo));
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirEditarModal", "var modal = new bootstrap.Modal(document.getElementById('modalEditarEstado')); modal.show();", true);
                }*/
                else if (e.CommandName == "Editar")
                {
                    lblIdPedidoEditar.Text = pedido.IdPedido.ToString();
                    ddlEstadoPedido.SelectedValue = pedido.EstadoPedido.IdEstadoPedido.ToString();

                    var todosEstadosEnvio = managerPedido.listarEstadosEnvio();

                    // Filtro estados de envío permitidos según el estado de pedido
                    List<int> idsPermitidos;
                    if (!estadosEnvioPermitidosPorEstadoPedido.TryGetValue(pedido.EstadoPedido.IdEstadoPedido, out idsPermitidos))
                    {
                        idsPermitidos = todosEstadosEnvio.Select(eEnvio => eEnvio.IdEstadoEnvio).ToList();
                    }

                    var estadosFiltrados = todosEstadosEnvio.Where(eEnvio => idsPermitidos.Contains(eEnvio.IdEstadoEnvio)).ToList();

                    ddlEstadoEnvio.DataSource = estadosFiltrados;
                    ddlEstadoEnvio.DataValueField = "IdEstadoEnvio";
                    ddlEstadoEnvio.DataTextField = "Descripcion";
                    ddlEstadoEnvio.DataBind();

                    // Verifico si el estado actual del pedido está en la lista filtrada
                    if (idsPermitidos.Contains(pedido.EstadoEnvio.IdEstadoEnvio))
                        ddlEstadoEnvio.SelectedValue = pedido.EstadoEnvio.IdEstadoEnvio.ToString();
                    else
                        ddlEstadoEnvio.SelectedIndex = 0; // Selecciono el primero

                    // Verifico si el valor de EstadoPago existe en el DropDownList
                    if (ddlEstadoPago.Items.FindByValue(pedido.EstadoPago.IdEstadoPago.ToString()) != null)
                    {
                        ddlEstadoPago.SelectedValue = pedido.EstadoPago.IdEstadoPago.ToString();
                    }
                    else
                    {
                        ddlEstadoPago.SelectedIndex = 0;
                    }

                    //ddlEstadoPago.SelectedValue = pedido.EstadoPago.IdEstadoPago.ToString();

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

        protected void ddlEstadoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            int estadoPedidoSeleccionado = Convert.ToInt32(ddlEstadoPedido.SelectedValue);

            var todosEstadosEnvio = managerPedido.listarEstadosEnvio();

            List<int> idsPermitidos;
            if (!estadosEnvioPermitidosPorEstadoPedido.TryGetValue(estadoPedidoSeleccionado, out idsPermitidos))
            {
                idsPermitidos = todosEstadosEnvio.Select(envio => envio.IdEstadoEnvio).ToList();
            }

            var estadosFiltrados = todosEstadosEnvio.Where(envio => idsPermitidos.Contains(envio.IdEstadoEnvio)).ToList();

            ddlEstadoEnvio.DataSource = estadosFiltrados;
            ddlEstadoEnvio.DataValueField = "IdEstadoEnvio";
            ddlEstadoEnvio.DataTextField = "Descripcion";
            ddlEstadoEnvio.DataBind();


            if (ddlEstadoEnvio.Items.Count > 0)
                ddlEstadoEnvio.SelectedIndex = 0;

            var todosEstadosPago = managerPedido.listarEstadosPago();
            List<int> idsPagosPermitidos;
            if (!estadosPagoPermitidosPorEstadoPedido.TryGetValue(estadoPedidoSeleccionado, out idsPagosPermitidos))
            {
                idsPagosPermitidos = todosEstadosPago.Select(pago => (int)pago.IdEstadoPago).ToList();
            }

            var estadosFiltradosPago = todosEstadosPago
                .Where(pago => idsPagosPermitidos.Contains(pago.IdEstadoPago))
                .ToList();

            ddlEstadoPago.DataSource = estadosFiltradosPago;
            ddlEstadoPago.DataValueField = "IdEstadoPago";
            ddlEstadoPago.DataTextField = "Descripcion";
            ddlEstadoPago.DataBind();

            if (ddlEstadoPago.Items.Count > 0)
                ddlEstadoPago.SelectedIndex = 0;

            // Reabrir modal porque se cierra con el postback
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirEditarModal", "var modal = new bootstrap.Modal(document.getElementById('modalEditarEstado')); modal.show();", true);
        }
    }
}
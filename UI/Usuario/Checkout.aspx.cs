using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace UI.Usuario
{
    public partial class Checkout : UI.ClaseMaster.BasePage
    {
        public Usuarios UsuarioLogeado { get; set; }
        public List<Detalle> listaDetalles;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
                Response.Redirect("Logearse.aspx");

            if (Request.QueryString.Count > 0)
                Response.Redirect("Inicio.aspx");

            UsuarioLogeado = (Usuarios)Session["Usuario"];

            if (!IsPostBack)
            {
                cargarMetodosPago();
                CarritoManager carritoManager = new CarritoManager();
                DetalleManager detalleManager = new DetalleManager();

                int idCarrito = carritoManager.carritoDisponible(UsuarioLogeado.Id);
                listaDetalles = detalleManager.listarDetallesCarrito(idCarrito);

                if (listaDetalles == null || listaDetalles.Count == 0)
                    Response.Redirect("Inicio.aspx");

                Session["DetallesCarrito"] = listaDetalles;
            }
            else
            {
                listaDetalles = (List<Detalle>)Session["DetallesCarrito"];
            }

        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            if (listaDetalles == null || listaDetalles.Count == 0)
                Response.Redirect("/Inicio.aspx");

            int idUsuario = UsuarioLogeado.Id;

            try
            {
                PedidoManager pedidoManager = new PedidoManager();
                CarritoManager carritoManager = new CarritoManager();
                DetalleManager detalleManager = new DetalleManager();

                Pedido nuevoPedido = new Pedido();

                nuevoPedido.MetodoPago = new MetodoPago();
                nuevoPedido.DetallePago = new DetallePago();
                nuevoPedido.EstadoPago = new EstadoPago();
                nuevoPedido.EstadoPedido = new EstadoPedido();
                nuevoPedido.EstadoEnvio = new EstadoEnvio();

                nuevoPedido.IdUsuario = idUsuario;
                nuevoPedido.MetodoPago.Id = int.Parse(ddlMetodoPago.SelectedValue);
                nuevoPedido.MetodoPago.Descripcion = ddlMetodoPago.SelectedItem.Text;

                nuevoPedido.DetallePago.Metodo = ddlMetodoPago.SelectedItem.Text;
                nuevoPedido.DetallePago.Fecha = DateTime.Now;

                switch (nuevoPedido.MetodoPago.Id)
                {
                    // Tarjeta y Mp
                    case 1:
                    case 2:
                        nuevoPedido.DetallePago.Descripcion = "Pagado";

                        nuevoPedido.EstadoPedido = pedidoManager.obtenerEstadoPedidoPorId(1);
                        nuevoPedido.EstadoPago = pedidoManager.obtenerEstadoPagoPorId(1);
                        nuevoPedido.EstadoEnvio = pedidoManager.obtenerEstadoEnvioPorId(1);
                        break;
                    // Efectivo y Transferencia
                    case 3:
                    case 4:
                        nuevoPedido.DetallePago.Descripcion = "En proceso";

                        nuevoPedido.EstadoPedido = pedidoManager.obtenerEstadoPedidoPorId(3);
                        nuevoPedido.EstadoPago = pedidoManager.obtenerEstadoPagoPorId(2);
                        nuevoPedido.EstadoEnvio = pedidoManager.obtenerEstadoEnvioPorId(1);
                        break;
                    default:
                        nuevoPedido.DetallePago.Descripcion = "Error de transacción";

                        nuevoPedido.EstadoPedido = pedidoManager.obtenerEstadoPedidoPorId(5);
                        nuevoPedido.EstadoPago = pedidoManager.obtenerEstadoPagoPorId(3);
                        nuevoPedido.EstadoEnvio = pedidoManager.obtenerEstadoEnvioPorId(6);
                        break;
                }

                nuevoPedido.FechaPedido = DateTime.Now;
                nuevoPedido.ListaDetalles = listaDetalles;
                nuevoPedido.Activo = true;
                nuevoPedido.PrecioTotal = listaDetalles.Sum(d => d.Subtotal);

                pedidoManager.crearPedidoCompleto(nuevoPedido);

                int idCarrito = carritoManager.carritoDisponible(idUsuario);
                detalleManager.limpiarCarrito(idCarrito);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModal",
                    "var myModal = new bootstrap.Modal(document.getElementById('compraExitosa')); myModal.show();", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cargarMetodosPago()
        {
            MetodoPagoManager metodoPagoManager = new MetodoPagoManager();
            List<MetodoPago> listaMetodos = metodoPagoManager.listarMetodosPago();

            ddlMetodoPago.DataSource = listaMetodos;
            ddlMetodoPago.DataValueField = "Id";
            ddlMetodoPago.DataTextField = "Descripcion";
            ddlMetodoPago.DataBind();
        }
    }
}
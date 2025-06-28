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
                Response.Redirect("Login.aspx");

            UsuarioLogeado = (Usuarios)Session["Usuario"];

            if (!IsPostBack)
            {
                cargarMetodosPago();
                CarritoManager carritoManager = new CarritoManager();
                DetalleManager detalleManager = new DetalleManager();

                int idCarrito = carritoManager.carritoDisponible(UsuarioLogeado.Id);
                listaDetalles = detalleManager.listarDetallesCarrito(idCarrito);

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
            {
                Response.Redirect("/Inicio.aspx");
            }

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

                // Momentaneamente queda asi hasta siguiente push
                nuevoPedido.DetallePago.IdDetallePago = 0;
                nuevoPedido.DetallePago.Metodo = ddlMetodoPago.SelectedItem.Text;
                nuevoPedido.DetallePago.Fecha = DateTime.Now;
                nuevoPedido.DetallePago.Descripcion = "Pagado";

                nuevoPedido.EstadoPago.IdEstadoPago = 1;
                nuevoPedido.EstadoPago.Descripcion = "Aprobado";

                nuevoPedido.EstadoPedido.IdEstadoPedido = 1;
                nuevoPedido.EstadoPedido.Descripcion = "Creado";

                nuevoPedido.EstadoEnvio.IdEstadoEnvio = 1;
                nuevoPedido.EstadoEnvio.Descripcion = "No enviado";

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
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace UI.Usuario
{
    public partial class Carrito : UI.ClaseMaster.BasePage
    {
        public List<Detalle> listaDetalles;
        public List<ImagenesProducto> listaImagenes;
        public decimal total;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Inicio.aspx");
                return;
            }

            int idUsuario = ((Usuarios)Session["Usuario"]).Id;

            CarritoManager carritoManager = new CarritoManager();
            DetalleManager detalleManager = new DetalleManager();
            ImagenManager imagenManager = new ImagenManager();

            int idCarrito = carritoManager.carritoDisponible(idUsuario);

            if (!IsPostBack && Request.QueryString["quitar"] != null)
            {
                string quitarStr = Request.QueryString["quitar"];
                if (int.TryParse(quitarStr, out int idProductoQuitar))
                {
                    detalleManager.eliminarDetalle(idCarrito, idProductoQuitar);
                    Response.Redirect("Carrito.aspx");
                    return;
                }
            }

            listaDetalles = detalleManager.listarDetallesCarrito(idCarrito);
            listaImagenes = imagenManager.listarImagenes();

            total = 0;
            foreach (Detalle d in listaDetalles)
            {
                total += d.Subtotal;
            }
        }
    }
}
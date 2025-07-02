using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace UI
{
	public partial class ProductoDetalle : UI.ClaseMaster.BasePage
    {
        public List<ImagenesProducto> listaImagenes;
        public Producto producto = new Producto();
        public bool favoritoRepetido = false;
        public bool modalCarrito = false;
        public int cantidadAgregada = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImagenManager imagenManager = new ImagenManager();
            listaImagenes = imagenManager.listarImagenes();

            if (Request.QueryString["id"] == null)
                Response.Redirect("Inicio.aspx");

            int idProducto;
            if (!int.TryParse(Request.QueryString["id"], out idProducto))
                Response.Redirect("Inicio.aspx");

            CargarProducto(idProducto);
            if (producto.Id == 0)
                Response.Redirect("Inicio.aspx");

            if (Session["Usuario"] != null)
            {
                int idUsuario = ((Usuarios)Session["Usuario"]).Id;
                FavoritoManager favoritoManager = new FavoritoManager();

                favoritoRepetido = favoritoManager.favoritoRepetido(idProducto, idUsuario);

                if (Request.QueryString["fav"] != null)
                {
                    string accion = Request.QueryString["fav"];
                    if (accion == "agregar" && !favoritoRepetido)
                    {
                        favoritoManager.agregarFavorito(idProducto, idUsuario);
                    }
                    else if (accion == "quitar" && favoritoRepetido)
                    {
                        favoritoManager.eliminarFavorito(idProducto, idUsuario);
                    }

                    Response.Redirect($"ProductoDetalle.aspx?id={idProducto}");
                    return;
                }
            }
        }

        private void CargarProducto(int idProducto)
        {
            ProductoManager productoManager = new ProductoManager();

            producto = productoManager.BuscarProductoPorId(idProducto);

            if (producto != null)
            {
                lblNombre.Text = producto.Nombre;
                lblPrecio.Text = "$" + producto.Precio.ToString("N2");
                lblCategoria.Text = producto.Categoria.Descripcion;
                lblMarca.Text = producto.Marca.Descripcion;
            }
            else
            {
                producto = new Producto();
                producto.Id = 0;

                Response.Redirect("Inicio.aspx");
            }
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                int idUsuario = ((Usuarios)Session["Usuario"]).Id;
                int idProducto = producto.Id;
                int cantidad = 1;

                if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad < 1)
                {
                    cantidad = 1;
                }

                CarritoManager carritoManager = new CarritoManager();
                DetalleManager detalleManager = new DetalleManager();

                int idCarrito = carritoManager.carritoDisponible(idUsuario);

                detalleManager.agregarProductoAlCarrito(idCarrito, idProducto, cantidad);

                modalCarrito = true;
                cantidadAgregada = cantidad;
            }
            else
            {
                Response.Redirect("Logearse.aspx");
            }
        }
    }
}
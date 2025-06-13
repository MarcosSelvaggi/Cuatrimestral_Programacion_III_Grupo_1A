using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI
{
	public partial class ProductoDetalle : UI.ClaseMaster.BasePage
    {
        public List<ImagenesProducto> listaImagenes;
        public Producto producto = new Producto();
        public bool favoritoRepetido = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImagenManager imagenManager = new ImagenManager();
            listaImagenes = imagenManager.listarImagenes();

            if (Request.QueryString["id"] != null)
            {
                int idProducto;
                if (int.TryParse(Request.QueryString["id"], out idProducto))
                {
                    CargarProducto(idProducto);

                    if (Session["Usuario"] != null)
                    {
                        int idUsuario = ((Usuarios)Session["Usuario"]).Id;
                        FavoritoManager favoritoManager = new FavoritoManager();

                        favoritoRepetido = favoritoManager.favoritoRepetido(idProducto, idUsuario);

                        if (Request.QueryString["fav"] != null)
                        {
                            string accion = Request.QueryString["fav"];
                            int idFavorito;

                            if (int.TryParse(Request.QueryString["id"], out idFavorito))
                            {
                                if (accion == "agregar" && !favoritoRepetido)
                                {
                                    favoritoManager.agregarFavorito(idFavorito, idUsuario);
                                }
                                else if (accion == "quitar" && favoritoRepetido)
                                {
                                    favoritoManager.eliminarFavorito(idFavorito, idUsuario);
                                }

                                Response.Redirect($"ProductoDetalle.aspx?id={idProducto}");
                                return;
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            else
            {
                Response.Redirect("Inicio.aspx");
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
                Response.Redirect("Inicio.aspx");
            }
        }
    }
}
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
	public partial class ProductoDetalle : System.Web.UI.Page
	{
        public List<ImagenesProducto> listaImagenes;
        public Producto producto = new Producto();

        protected void Page_Load(object sender, EventArgs e)
		{
            ImagenManager imagenManager = new ImagenManager();
            listaImagenes = imagenManager.listarImagenes();

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int idProducto;
                    if (int.TryParse(Request.QueryString["id"], out idProducto))
                    {
                        CargarProducto(idProducto);
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
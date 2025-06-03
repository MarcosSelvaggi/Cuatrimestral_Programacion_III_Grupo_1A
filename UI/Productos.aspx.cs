using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace UI
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Categoria> listaCategorias;
        public List<Producto> listaProductos;
        public List<ImagenesProducto> listaImagenes;
        public int categoriaSeleccionada { get; set; } = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaManager categoriaManager = new CategoriaManager();
            listaCategorias = categoriaManager.listar();

            ProductoManager productoManager = new ProductoManager();
            listaProductos = productoManager.ListarProductos();

            ImagenManager imagenManager = new ImagenManager();
            listaImagenes = imagenManager.listarImagenes();

            if (Request.QueryString["categoria"] != null)
            {
                if (Int32.TryParse(Request.QueryString["categoria"].ToString(), out int auxNum))
                {
                    categoriaSeleccionada = Int32.Parse(Request.QueryString["categoria"]);
                    //Por si el usuario pasa una Id de categoria mayor al que hay en la bd 
                    if (categoriaSeleccionada > listaCategorias.Count)
                        categoriaSeleccionada = 1;
                }
                else
                {
                    categoriaSeleccionada = 1;
                }
            }
        }
    }
}
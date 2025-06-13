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
    public partial class Inicio : UI.ClaseMaster.BasePage
    {
        public List<Categoria> listaCategorias; 
        public List<Producto> listaProductos;
        public List<ImagenesProducto> listaImagenes; 
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaManager categoriaManager = new CategoriaManager();
            listaCategorias = categoriaManager.listar();
            ImagenManager imagenManager = new ImagenManager();
            listaImagenes = imagenManager.listarImagenes();
            ProductoManager productoManager = new ProductoManager();
            listaProductos = productoManager.ListarProductos();
        }
    }
}
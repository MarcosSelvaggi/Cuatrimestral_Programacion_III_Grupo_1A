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
    public partial class Productos : UI.ClaseMaster.BasePage
    {

        public object __o;
        public List<Categoria> listaCategorias;
        public List<Marca> listaMarcas;

        public List<Producto> listaProductos;
        //Lista auxiliar que sirve para la búsqueda de productos
        public List<Producto> listaProductosBuscados; 

        public List<ImagenesProducto> listaImagenes;
        public int CategoriaSeleccionada { get; set; } = -1;
        public int MarcaSeleccionada { get; set; } = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaManager categoriaManager = new CategoriaManager();
            listaCategorias = categoriaManager.listar();

            MarcaManager marcaManager = new MarcaManager();
            listaMarcas = marcaManager.listar();

            ProductoManager productoManager = new ProductoManager();
            listaProductos = productoManager.ListarProductos();

            ImagenManager imagenManager = new ImagenManager();
            listaImagenes = imagenManager.listarImagenes();

            if (!String.IsNullOrEmpty(Request.QueryString["categoria"]))
            {
                if (Int32.TryParse(Request.QueryString["categoria"].ToString(), out int auxNumCategoria))
                {
                    CategoriaSeleccionada = auxNumCategoria;
                    //Por si el usuario pasa una Id de categoria mayor al que hay en la bd 
                    if (CategoriaSeleccionada > listaCategorias.Count)
                        CategoriaSeleccionada = 1;
                }
            }
            else if (!String.IsNullOrEmpty(Request.QueryString["marca"]))
            {
                if (Int32.TryParse(Request.QueryString["marca"].ToString(), out int auxNumMarca))
                {
                    MarcaSeleccionada = auxNumMarca;
                    if (MarcaSeleccionada > listaMarcas.Count)
                        MarcaSeleccionada = 1;
                }
            }
            else if (!String.IsNullOrEmpty(Request.QueryString["busqueda"]))
            {
                MarcaSeleccionada = -1;
                CategoriaSeleccionada = -1;
                listaProductosBuscados = productoManager.buscarProductoPorNombre(Request.QueryString["busqueda"]);
            }
            else
            {
                CategoriaSeleccionada = 1;
            }

        }

        protected void RealizarBusquedaProducto_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBusqueda.Text))
            {
                Response.Redirect("/Productos.aspx", false);
                return;
            }
            else
            {
                Response.Redirect("/Productos.aspx?busqueda=" + txtBusqueda.Text, false);
                return;
            }
        }
    }
}
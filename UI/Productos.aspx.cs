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
        //Este objeto está declarado para evitar que el IntelliSense arroje errores porque no está declarado este objeto 
        //Solución sacada de https://stackoverflow.com/questions/31886413/the-name-o-does-not-exist-in-the-current-context  
        public object __o;
        public List<Categoria> listaCategorias;
        public List<Marca> listaMarcas;

        public List<Producto> listaProductos;

        public List<ImagenesProducto> listaImagenes;

        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaManager categoriaManager = new CategoriaManager();
            listaCategorias = categoriaManager.listar();

            MarcaManager marcaManager = new MarcaManager();
            listaMarcas = marcaManager.listar();

            ProductoManager productoManager = new ProductoManager();

            ImagenManager imagenManager = new ImagenManager();
            listaImagenes = imagenManager.listarImagenes();

            //Se fija si el request es categoria y si no está vacío o con espacios
            if (!String.IsNullOrEmpty(Request.QueryString["categoria"]))
            {
                /*Deprecado 
                Por si el usuario pasa una Id de categoria que no hay en la bd 
                if (!listaCategorias.Exists(X => X.Id == auxNumCategoria))
                {
                    auxNumCategoria = listaCategorias[0].Id;
                }
                Deprecado */

                listaProductos = productoManager.ListarProductosSegunCategoria(Request.QueryString["categoria"]);
                Session.Remove("ListaProductos");
            }

            //Se fija si el request es marca y si no está vacío o con espacios
            else if (!String.IsNullOrEmpty(Request.QueryString["marca"]))
            {
                /* Deprecado
                if (Int32.TryParse(Request.QueryString["marca"].ToString(), out int auxNumMarca))
                {
                    //Por si el usuario pasa una Id de marca que no hay en la bd
                    if (!listaMarcas.Exists(X => X.Id == auxNumMarca))
                    {
                        auxNumMarca = listaMarcas[0].Id;
                    }

                    
                }
                Deprecado */
                Session.Remove("ListaProductos");
                listaProductos = productoManager.ListarProductosSegunMarca(Request.QueryString["marca"]);
            }
            //Se fija si el request es una búsqueda y si no está vacia o con espacios
            else if (!String.IsNullOrEmpty(Request.QueryString["busqueda"]))
            {
                listaProductos = productoManager.busquedaProductosActivosPorNombre(Request.QueryString["busqueda"]);
                Session.Remove("ListaProductos");
            }
            //Se fija si el request es un filtro de precios y si no está vacío o con espacios
            else if (!String.IsNullOrEmpty(Request.QueryString["precio"]))
            {
                List<Producto> listaProductosAux = new List<Producto>();
                listaProductosAux = (List<Producto>)Session["ListaProductos"];

                listaProductos = new List<Producto>();

                decimal valorMinimo = Decimal.Parse(Session["valorMínimo"].ToString());
                decimal valorMaximo = Decimal.Parse(Session["valorMáximo"].ToString());

                foreach (var producto in listaProductosAux)
                {
                    if (producto.Precio > valorMinimo && producto.Precio < valorMaximo)
                    {
                        listaProductos.Add(producto);
                    }
                }
            }
            else
            {
                listaProductos = productoManager.listar();
                Session.Remove("ListaProductos");
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

        protected void btnRangoPrecios_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPrecioMinimo.Text) && !string.IsNullOrWhiteSpace(txtPrecioMaximo.Text))
            {
                Session.Add("valorMínimo", txtPrecioMinimo.Text);
                Session.Add("valorMáximo", txtPrecioMaximo.Text);

                //Si es la primera vez buscando precios, agrega la lista a session
                if (Session["ListaProductos"] == null)
                {
                    Session.Add("ListaProductos", listaProductos);
                }
                Response.Redirect("/Productos.aspx?precio=1");
            }
        }
    }
}
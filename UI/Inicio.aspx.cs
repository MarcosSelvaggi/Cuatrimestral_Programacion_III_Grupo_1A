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
        public List<Producto> listaProductosAux;

        public List<ImagenesProducto> listaImagenes; 
        public List<ImagenesProducto> listaImagesAux;
        protected void Page_Load(object sender, EventArgs e)
        {
            ImagenManager imagenManager = new ImagenManager();
            listaImagenes = imagenManager.listarImagenes();

            ProductoManager productoManager = new ProductoManager();
            listaProductos = productoManager.ListarProductosActivos();

            listaProductosAux = new List<Producto>();
            listaImagesAux = new List<ImagenesProducto>();


            //Usando random para agregar de forma aleatoria productos a la lista que se va a usar para mostrar los productos destacados
            for (int i = 0; i < 3; i++)
            {
                Random random = new Random();
                int aux = random.Next(0, listaProductos.Count);

                //Con el número aleatorio generado arriba agregamos el producto a la lista auxiliar 
                listaProductosAux.Add(listaProductos[aux]); 
                listaImagesAux.Add(listaImagenes.Find(X => X.IdProducto == listaProductos[aux].Id));

                //Elimina el producto de la lista así baja la cantidad de productos y evita que pueda salir repetido
                listaProductos.RemoveAt(aux);
            }
            
            




        }
    }
}
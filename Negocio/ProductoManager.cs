using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ProductoManager
    {
        private List<Categoria> listaCategorias;
        private List<Marca> listaMarcas;

        public List<Producto> ListarProductos()
        {
            CategoriaManager categoriaManager = new CategoriaManager();
            MarcaManager marcaManager = new MarcaManager();

            listaCategorias = categoriaManager.listar();
            listaMarcas = marcaManager.listar();

            List<Producto> listaProductos = new List<Producto>();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Select IdProducto, Nombre, Descripcion, Precio, Estado, IdMarca, IdCategoria from Productos";
                conexion.setearConsulta(query);
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.Id = (int)conexion.Lector["IdProducto"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Descripcion = (string)conexion.Lector["Descripcion"];
                    aux.Precio = Decimal.Parse(conexion.Lector["Precio"].ToString());
                    aux.Estado = (bool)conexion.Lector["Estado"];

                    int idCategoria = (int)conexion.Lector["IdCategoria"];
                    aux.Categoria.Id = idCategoria;

                    foreach (Categoria cat in listaCategorias)
                    {
                        if (cat.Id == idCategoria)
                        {
                            aux.Categoria.Descripcion = cat.Descripcion;
                            break;
                        }
                    }

                    int idMarca = (int)conexion.Lector["IdMarca"];
                    aux.Marca.Id = idMarca;

                    foreach (Marca m in listaMarcas)
                    {
                        if (m.Id == idMarca)
                        {
                            aux.Marca.Descripcion = m.Descripcion;
                            break;
                        }
                    }

                    listaProductos.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return listaProductos;
        }
    }
}

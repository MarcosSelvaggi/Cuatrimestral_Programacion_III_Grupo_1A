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
                string query = "Select IdProducto, Nombre, Precio, Activo, IdMarca, IdCategoria from Productos";
                conexion.setearConsulta(query);
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.Id = (int)conexion.Lector["IdProducto"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Precio = Decimal.Parse(conexion.Lector["Precio"].ToString());
                    aux.Activo = (bool)conexion.Lector["Activo"];

                    //int idCategoria = (int)conexion.Lector["IdCategoria"];
                    int idCategoria = (byte)conexion.Lector["IdCategoria"];
                    aux.Categoria.Id = idCategoria;

                    foreach (Categoria cat in listaCategorias)
                    {
                        if (cat.Id == idCategoria)
                        {
                            aux.Categoria.Descripcion = cat.Descripcion;
                            break;
                        }
                    }
                    //int idMarca = (int)conexion.Lector["IdMarca"];
                    int idMarca = (byte)conexion.Lector["IdMarca"];
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

        public Producto BuscarProductoPorId(int id)
        {
            CategoriaManager categoriaManager = new CategoriaManager();
            MarcaManager marcaManager = new MarcaManager();

            listaCategorias = categoriaManager.listar();
            listaMarcas = marcaManager.listar();

            AccesoADatos conexion = new AccesoADatos();
            Producto producto = new Producto();

            try
            {
                string query = "Select IdProducto, Nombre, Precio, Activo, IdCategoria, IdMarca from Productos Where IdProducto = @id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@id", id);
                conexion.ejecutarQuery();

                if (conexion.Lector.Read())
                {
                    producto = new Producto();
                    producto.Id = (int)conexion.Lector["IdProducto"];
                    producto.Nombre = conexion.Lector["Nombre"].ToString();
                    producto.Precio = Decimal.Parse(conexion.Lector["Precio"].ToString());
                    producto.Activo = (bool)conexion.Lector["Activo"];

                    int idCategoria = (byte)conexion.Lector["IdCategoria"];
                    producto.Categoria.Id = idCategoria;

                    foreach (Categoria cat in listaCategorias)
                    {
                        if (cat.Id == idCategoria)
                        {
                            producto.Categoria.Descripcion = cat.Descripcion;
                            break;
                        }
                    }

                    int idMarca = (byte)conexion.Lector["IdMarca"];
                    producto.Marca.Id = idMarca;

                    foreach (Marca m in listaMarcas)
                    {
                        if (m.Id == idMarca)
                        {
                            producto.Marca.Descripcion = m.Descripcion;
                            break;
                        }
                    }
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

            return producto;
        }
    }
}

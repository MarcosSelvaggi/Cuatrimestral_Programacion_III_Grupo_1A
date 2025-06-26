using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class ProductoManager
    {
        private List<Categoria> listaCategorias;
        private List<Marca> listaMarcas;

        public ProductoManager()
        {
            CategoriaManager categoriaManager = new CategoriaManager();
            MarcaManager marcaManager = new MarcaManager();

            listaCategorias = categoriaManager.listar();
            listaMarcas = marcaManager.listar();
        }

        public List<Producto> ListarProductos()
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Select IdProducto, Nombre, Precio, Activo, IdMarca, IdCategoria from Productos";
                conexion.setearConsulta(query);
                conexion.ejecutarQuery();

                /*while (conexion.Lector.Read())
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
                */

                //Simplificado lo de arriba creando un método reutilizable que hace lo mismo
                listaProductos = leerDatosDesdeBD(conexion);
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

        public List<Producto> ListarProductosActivos()
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Select IdProducto, Nombre, Precio, Activo, IdMarca, IdCategoria from Productos where Stock > 0 and Activo = 1";
                conexion.setearConsulta(query);
                conexion.ejecutarQuery();

                listaProductos = leerDatosDesdeBD(conexion);
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


        public List<Producto> ListarProductosSegunCategoria(string NombreCategoria)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Select IdProducto, Nombre, Precio, P.Activo, IdMarca, P.IdCategoria " +
                               "from Productos P Inner join Categorias CA on P.IdCategoria = CA.IdCategoria " +
                               "where Ca.Descripcion Like @Descripción and Stock > 0 and P.Activo = 1";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Descripción", "%" + NombreCategoria + "%");
                conexion.ejecutarQuery();

                listaProductos = leerDatosDesdeBD(conexion);
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
        public List<Producto> ListarProductosSegunMarca(string NombreMarca)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Select IdProducto, Nombre, Precio, P.Activo, P.IdMarca, P.IdCategoria " +
                               "from Productos P Inner join Marcas MA on P.IdMarca = MA.IdMarca " +
                               "where MA.Descripcion Like @Descripción and Stock > 0 and P.Activo = 1";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Descripción", NombreMarca);
                conexion.ejecutarQuery();

                listaProductos = leerDatosDesdeBD(conexion);
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

        public List<Producto> ListarProductosPorPrecioBuscado(string precioMinimo, string precioMaximo)
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Select IdProducto, Nombre, Precio, Activo, IdMarca, IdCategoria from Productos where precio < @PrecioMinimo and precio > @PrecioMaximo and Stock > 0 and Activo = 1";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@PrecioMinimo", precioMinimo);
                conexion.agregarParametros("@PrecioMaximo", precioMaximo);
                conexion.ejecutarQuery();

                listaProductos = leerDatosDesdeBD(conexion);
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


        public List<Producto> listar()
        {
            List<Producto> listaProductos = new List<Producto>();
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = @"SELECT p.IdProducto, p.Nombre, p.Precio, p.Stock, p.Activo, 
                                        c.IdCategoria, c.Descripcion as CategoriaDescripcion,
                                        m.IdMarca, m.Descripcion as MarcaDescripcion
                                 FROM Productos p
                                 INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
                                 INNER JOIN Marcas m ON p.IdMarca = m.IdMarca";

                conexion.setearConsulta(query);
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.Id = Convert.ToInt32(conexion.Lector["IdProducto"]);
                    aux.Nombre = conexion.Lector["Nombre"].ToString();
                    aux.Precio = Convert.ToDecimal(conexion.Lector["Precio"]);
                    aux.Stock = Convert.ToInt32(conexion.Lector["Stock"]);
                    aux.Activo = Convert.ToBoolean(conexion.Lector["Activo"]);

                    aux.Categoria = new Categoria
                    {
                        Id = Convert.ToInt32(conexion.Lector["IdCategoria"]),
                        Descripcion = conexion.Lector["CategoriaDescripcion"].ToString()
                    };

                    aux.Marca = new Marca
                    {
                        Id = Convert.ToInt32(conexion.Lector["IdMarca"]),
                        Descripcion = conexion.Lector["MarcaDescripcion"].ToString()
                    };

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
                string query = "Select IdProducto, Nombre, Precio, Activo, IdCategoria, IdMarca, Stock from Productos Where IdProducto = @id";
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
                    producto.Stock = (int)conexion.Lector["Stock"];

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

        public List<Producto> busquedaProductosActivosPorNombre(string busqueda)
        {
            List<Producto> ListaProducto = new List<Producto>();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                String Query = "Select IdProducto, Nombre, Precio, Activo, IdCategoria, IdMarca from Productos Where Nombre LIKE @Nombre and stock > 0 and Activo = 1";
                conexion.setearConsulta(Query);
                conexion.agregarParametros("@Nombre", "%" + busqueda + "%");
                conexion.ejecutarQuery();

                ListaProducto = leerDatosDesdeBD(conexion);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }

            return ListaProducto;
        }

        private List<Producto> leerDatosDesdeBD(AccesoADatos conexion)
        {
            List<Producto> listaProductos = new List<Producto>();
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
            return listaProductos;
        }

        public void agregar(Producto nuevoProducto)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = @"INSERT INTO Productos (Nombre,  Precio, Stock, IdCategoria, IdMarca, Activo)
                                 VALUES (@Nombre, @Precio, @Stock, @IdCategoria, @IdMarca, @Activo)";

                conexion.setearConsulta(query);
                conexion.limpiarParametros();

                conexion.agregarParametros("@Nombre", nuevoProducto.Nombre);
                conexion.agregarParametros("@Precio", nuevoProducto.Precio);
                conexion.agregarParametros("@Stock", nuevoProducto.Stock);
                conexion.agregarParametros("@IdCategoria", nuevoProducto.Categoria.Id);
                conexion.agregarParametros("@IdMarca", nuevoProducto.Marca.Id);
                conexion.agregarParametros("@Activo", nuevoProducto.Activo);

                conexion.ejecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }
        public int agregarYDevolverId(Producto producto)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                conexion.setearConsulta("Insert Into Productos (Nombre, Precio, Stock, IdCategoria, IdMarca, Activo) " +
                                        "Values (@Nombre, @Precio, @Stock, @IdCategoria, @IdMarca, @Activo); " +
                                        "SELECT Scope_Identity();");
                conexion.agregarParametros("@Nombre", producto.Nombre);
                conexion.agregarParametros("@Precio", producto.Precio);
                conexion.agregarParametros("@Stock", producto.Stock);
                conexion.agregarParametros("@IdCategoria", producto.Categoria.Id);
                conexion.agregarParametros("@IdMarca", producto.Marca.Id);
                conexion.agregarParametros("@Activo", producto.Activo);

                object result = conexion.EjecutarScalar();
                return Convert.ToInt32(result);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }
        public void modificar(Producto productoModificado)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = @"Update Productos 
                                 Set Nombre = @Nombre,  
                                     Precio = @Precio, 
                                     Stock = @Stock, 
                                     IdCategoria = @IdCategoria, 
                                     IdMarca = @IdMarca, 
                                     Activo = @Activo
                                 Where IdProducto = @Id";

                conexion.setearConsulta(query);
                conexion.limpiarParametros();

                conexion.agregarParametros("@Id", productoModificado.Id);
                conexion.agregarParametros("@Nombre", productoModificado.Nombre);
                conexion.agregarParametros("@Precio", productoModificado.Precio);
                conexion.agregarParametros("@Stock", productoModificado.Stock);
                conexion.agregarParametros("@IdCategoria", productoModificado.Categoria.Id);
                conexion.agregarParametros("@IdMarca", productoModificado.Marca.Id);
                conexion.agregarParametros("@Activo", productoModificado.Activo);

                conexion.ejecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public void eliminar(int idProducto)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "UPDATE Productos SET Activo = 0 WHERE IdProducto = @Id";

                conexion.setearConsulta(query);
                conexion.limpiarParametros();
                conexion.agregarParametros("@Id", idProducto);

                conexion.ejecutarNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }
        public Producto obtenerProductoPorId(int idProducto)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = @"SELECT p.IdProducto, p.Nombre, p.Precio, p.Stock, p.Activo, 
                                        c.IdCategoria, c.Descripcion as CategoriaDescripcion,
                                        m.IdMarca, m.Descripcion as MarcaDescripcion
                                 FROM Productos p
                                 INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria
                                 INNER JOIN Marcas m ON p.IdMarca = m.IdMarca
                                 WHERE p.IdProducto = @Id";

                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", idProducto);
                conexion.ejecutarQuery();

                if (conexion.Lector.Read())
                {
                    Producto producto = new Producto();

                    producto.Id = Convert.ToInt32(conexion.Lector["IdProducto"]);
                    producto.Nombre = conexion.Lector["Nombre"].ToString();
                    producto.Precio = Convert.ToDecimal(conexion.Lector["Precio"]);
                    producto.Stock = Convert.ToInt32(conexion.Lector["Stock"]);
                    producto.Activo = Convert.ToBoolean(conexion.Lector["Activo"]);

                    producto.Categoria = new Categoria
                    {
                        Id = Convert.ToInt32(conexion.Lector["IdCategoria"]),
                        Descripcion = conexion.Lector["CategoriaDescripcion"].ToString()
                    };

                    producto.Marca = new Marca
                    {
                        Id = Convert.ToInt32(conexion.Lector["IdMarca"]),
                        Descripcion = conexion.Lector["MarcaDescripcion"].ToString()
                    };

                    return producto;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }
    }
}

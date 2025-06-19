using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class DetalleManager
    {
        public List<Detalle> listarDetallesCarrito(int idCarrito)
        {
            List<Detalle> listaDetalles = new List<Detalle>();
            ProductoManager productoManager = new ProductoManager();
            List<Producto> listaProductos = productoManager.ListarProductos();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Select IdDetalle, IdProducto, Cantidad, PrecioUnitario from Detalles where IdCarrito = @IdCarrito";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@IdCarrito", idCarrito);
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    Detalle aux = new Detalle();
                    aux.Id = (int)conexion.Lector["IdDetalle"];
                    aux.IdProducto = (int)conexion.Lector["IdProducto"];
                    aux.Cantidad = (int)conexion.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)conexion.Lector["PrecioUnitario"];

                    Producto producto = null;

                    foreach (Producto p in listaProductos)
                    {
                        if (p.Id == aux.IdProducto)
                        {
                            producto = p;
                            break;
                        }
                    }

                    aux.Producto = producto;

                    listaDetalles.Add(aux);
                }

                return listaDetalles;
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

        public void agregarProductoAlCarrito(int idCarrito, int idProducto, int cantidad)
        {
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string queryCant = "Select Cantidad from Detalles where IdCarrito = @IdCarrito and IdProducto = @IdProducto";
                conexion.setearConsulta(queryCant);
                conexion.agregarParametros("@IdCarrito", idCarrito);
                conexion.agregarParametros("@IdProducto", idProducto);
                conexion.ejecutarQuery();

                if (conexion.Lector.Read())
                {
                    int cantidadActual = (int)conexion.Lector["Cantidad"];
                    int cantidadTotal = cantidadActual + cantidad;

                    conexion.cerrarConexion();

                    string query = "Update Detalles set Cantidad = @NuevaCantidad where IdCarrito = @IdCarrito and IdProducto = @IdProducto";
                    conexion.limpiarParametros();
                    conexion.setearConsulta(query);
                    conexion.agregarParametros("@NuevaCantidad", cantidadTotal);
                    conexion.agregarParametros("@IdCarrito", idCarrito);
                    conexion.agregarParametros("@IdProducto", idProducto);
                    conexion.ejecutarNonQuery();
                }
                else
                {
                    conexion.cerrarConexion();

                    decimal precioUnitario = obtenerPrecioProducto(idProducto);

                    string queryInsert = "Insert Into Detalles (IdCarrito, IdProducto, Cantidad, PrecioUnitario) values (@IdCarrito, @IdProducto, @Cantidad, @PrecioUnitario)";
                    conexion.limpiarParametros();
                    conexion.setearConsulta(queryInsert);
                    conexion.agregarParametros("@IdCarrito", idCarrito);
                    conexion.agregarParametros("@IdProducto", idProducto);
                    conexion.agregarParametros("@Cantidad", cantidad);
                    conexion.agregarParametros("@PrecioUnitario", precioUnitario);
                    conexion.ejecutarNonQuery();
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
        }

        public void eliminarDetalle(int idCarrito, int idProducto)
        {
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Delete from Detalles where IdCarrito = @IdCarrito and IdProducto = @IdProducto";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@IdCarrito", idCarrito);
                conexion.agregarParametros("@IdProducto", idProducto);
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

        private decimal obtenerPrecioProducto(int idProducto)
        {
            ProductoManager productoManager = new ProductoManager();
            List<Producto> listaProductos = productoManager.ListarProductos();
            Producto producto = new Producto();

            foreach (Producto p in listaProductos)
            {
                if (p.Id == idProducto)
                {
                    producto = p;
                    break;
                }
            }

            if (producto != null)
            {
                return producto.Precio;
            }
            else
            {
                return 0;
            }
        }
    }
}

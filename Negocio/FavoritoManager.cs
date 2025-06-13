using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class FavoritoManager
    {
        public List<Favorito> listarFavoritoUsuario(int idUsuario)
        {
            List<Favorito> listaFavoritos = new List<Favorito>();
            ProductoManager productoManager = new ProductoManager();

            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Select IdFavorito, IdProducto, IdUsuario from Favoritos where IdUsuario = @IdUsuario";

                conexion.setearConsulta(query);
                conexion.agregarParametros("@IdUsuario", idUsuario);
                conexion.ejecutarQuery();

                List<Producto> listaProductos = productoManager.ListarProductos();

                while (conexion.Lector.Read())
                {
                    Favorito aux = new Favorito();

                    aux.Id = (int)conexion.Lector["IdFavorito"];
                    aux.IdUsuario = (int)conexion.Lector["IdUsuario"];
                    aux.IdProducto = (int)conexion.Lector["IdProducto"];

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

                    listaFavoritos.Add(aux);
                }

                return listaFavoritos;
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

        public void agregarFavorito(int idProducto, int idUsuario)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Insert Into Favoritos (IdProducto, IdUsuario) Values (@IdProducto, @IdUsuario)";
                conexion.setearConsulta(query);
                conexion.limpiarParametros();
                conexion.agregarParametros("@IdProducto", idProducto);
                conexion.agregarParametros("@IdUsuario", idUsuario);
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

        public void eliminarFavorito(int idProducto, int idUsuario)
        {
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Delete from Favoritos where IdProducto = @IdProducto and IdUsuario = @IdUsuario";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@IdProducto", idProducto);
                conexion.agregarParametros("@IdUsuario", idUsuario);
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

        public bool favoritoRepetido(int idProducto, int idUsuario)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Select Count(*) from Favoritos where IdProducto = @IdProducto and IdUsuario = @IdUsuario";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@IdProducto", idProducto);
                conexion.agregarParametros("@IdUsuario", idUsuario);

                int cantidad = (int)conexion.EjecutarScalar();

                return cantidad > 0;
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

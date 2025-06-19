using System;
using System.Collections.Generic;
using Dominio; 

namespace Negocio
{
    public class ImagenManager
    {
        public List<ImagenesProducto> listarImagenes()
        {
            List<ImagenesProducto> listaImagenes = new List<ImagenesProducto>();
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                conexion.setearConsulta("select IdImagen, IdProducto, UrlImagen from ImagenesDeProductos");
                conexion.ejecutarQuery();
                while (conexion.Lector.Read())
                {
                    ImagenesProducto aux = new ImagenesProducto();
                    aux.Id = (int)conexion.Lector["IdImagen"];
                    aux.IdProducto = (int)conexion.Lector["IdProducto"];
                    aux.UrlProducto = (string)conexion.Lector["UrlImagen"];
                    listaImagenes.Add(aux);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return listaImagenes;
        }

        public List<ImagenesProducto> listarPorProducto(int idProducto)
        {
            List<ImagenesProducto> listaImagenes = new List<ImagenesProducto>();
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                conexion.setearConsulta("Select IdImagen, IdProducto, UrlImagen From ImagenesDeProductos WHERE IdProducto = @IdProducto");
                conexion.agregarParametros("@IdProducto", idProducto);
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    ImagenesProducto aux = new ImagenesProducto();
                    aux.Id = (int)conexion.Lector["IdImagen"];
                    aux.IdProducto = (int)conexion.Lector["IdProducto"];
                    aux.UrlProducto = (string)conexion.Lector["UrlImagen"];
                    listaImagenes.Add(aux);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.cerrarConexion();
            }
            return listaImagenes;
        }

        public void eliminar(int idImagen)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                conexion.setearConsulta("Delete From ImagenesDeProductos Where IdImagen = @IdImagen");
                conexion.agregarParametros("@IdImagen", idImagen);
                conexion.ejecutarNonQuery();
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

        public void agregar(ImagenesProducto imagen)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                conexion.setearConsulta("INSERT INTO ImagenesDeProductos (IdProducto, UrlImagen) VALUES (@IdProducto, @UrlImagen)");
                conexion.agregarParametros("@IdProducto", imagen.IdProducto);
                conexion.agregarParametros("@UrlImagen", imagen.UrlProducto);
                conexion.ejecutarNonQuery();
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
    }
}

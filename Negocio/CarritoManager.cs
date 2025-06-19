using Dominio;
using System;

namespace Negocio
{
    public class CarritoManager
    {
        public Carrito buscarCarritoUsuario(int idUsuario)
        {
            AccesoADatos conexion = new AccesoADatos();
            Carrito carrito = new Carrito();

            try
            {
                string query = "Select IdCarrito, IdUsuario from Carrito where IdUsuario = @IdUsuario";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@IdUsuario", idUsuario);
                conexion.ejecutarQuery();

                if (conexion.Lector.Read())
                {
                    carrito = new Carrito();
                    carrito.Id = (int)conexion.Lector["IdCarrito"];
                    carrito.IdUsuario = (int)conexion.Lector["IdUsuario"];
                }

                return carrito;
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

        public int crearCarrito(int idUsuario)
        {
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "Insert Into Carrito (IdUsuario) Values (@IdUsuario); Select Scope_Identity() as 'UltimoIngresado'";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@IdUsuario", idUsuario);
                conexion.ejecutarQuery();

                conexion.Lector.Read(); 

                int carritoId = Int32.Parse(conexion.Lector["UltimoIngresado"].ToString());
                return carritoId;
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

        // Devuelve un Id Carrito dispoible del usuario para agregar el producto o crea un carrito nuevo.
        public int carritoDisponible(int idUsuario)
        {
            Carrito carrito = new Carrito();

            carrito = buscarCarritoUsuario(idUsuario);

            //if (carrito != null)
            if (carrito.Id != 0)
            {
                return carrito.Id;
            }
            else
            {
                return crearCarrito(idUsuario);
            }
        }
    }
}

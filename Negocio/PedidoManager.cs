using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class PedidoManager
    {
        AccesoADatos conexion = new AccesoADatos();

        public List<Pedido> listar()
        {
            List<Pedido> listaPedidos = new List<Pedido>();

            try
            {
                string query = @"Select P.IDPedido, 
                                        U.Nombre + ' ' + U.Apellido AS Cliente, 
                                        P.FechaDePedido, 
                                        EP.Descripcion AS Estado,
                                        EE.Descripcion AS EstadoEnvio,
                                        P.PrecioTotal 
                                 From Pedidos P
                                 Inner Join Usuarios U ON P.IDCliente = U.IdUsuario
                                 Inner Join EstadoDePedidos EP ON P.IDEstadoPedido = EP.IDEstadoPedido
                                 Inner Join EstadoDeEnvios EE ON P.IDEnvio = EE.IDEnvio";
                conexion.setearConsulta(query);
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    Pedido aux = new Pedido();
                    aux.IdPedido = (int)conexion.Lector["IDPedido"];
                    aux.Cliente = conexion.Lector["Cliente"].ToString();
                    aux.FechaPedido = (DateTime)conexion.Lector["FechaDePedido"];
                    aux.EstadoPedido.Descripcion = conexion.Lector["Estado"].ToString();
                    aux.EstadoEnvio = new EstadoEnvio();
                    aux.EstadoEnvio.Descripcion = conexion.Lector["EstadoEnvio"].ToString();
                    aux.PrecioTotal = conexion.Lector["PrecioTotal"] != DBNull.Value ? (decimal)conexion.Lector["PrecioTotal"] : 0;

                    listaPedidos.Add(aux);
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

            return listaPedidos;
        }

        public Pedido obtenerPedidoPorId(int idPedido)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = @"Select P.IDPedido, 
                                U.Nombre + ' ' + U.Apellido AS Cliente, 
                                P.FechaDePedido, 
                                E.Descripcion AS Estado, 
                                P.PrecioTotal,
                                P.IDEnvio,
                                P.IDEstadoPedido,
                                M.Descripcion AS MetodoPago,
                                DP.FechaDePago, 
                                DP.EstadoPago, 
                                DP.Detalles
                         From Pedidos P
                         Inner Join Usuarios U ON P.IDCliente = U.IdUsuario
                         Inner Join EstadoDePedidos E ON P.IDEstadoPedido = E.IDEstadoPedido
                         Inner Join DetalleDePagos DP ON P.IDPago = DP.IDPago
                         Inner Join MetodosDePago M ON DP.IDMetodoPago = M.IDMetodoPago
                         Where P.IDPedido = @Id";

                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", idPedido);
                conexion.ejecutarQuery();

                if (conexion.Lector.Read())
                {
                    Pedido aux = new Pedido();
                    aux.IdPedido = (int)conexion.Lector["IDPedido"];
                    aux.Cliente = conexion.Lector["Cliente"].ToString();
                    aux.FechaPedido = (DateTime)conexion.Lector["FechaDePedido"];
                    aux.EstadoPedido.Descripcion = conexion.Lector["Estado"].ToString();
                    aux.PrecioTotal = (decimal)conexion.Lector["PrecioTotal"];
                    aux.EstadoEnvio = new EstadoEnvio();
                    aux.EstadoEnvio.IdEstadoEnvio = (int)conexion.Lector["IDEnvio"];
                    aux.EstadoPedido.IdEstadoPedido = (byte)conexion.Lector["IDEstadoPedido"];

                    aux.DetallePago = new DetallePago()
                    {
                        Metodo = conexion.Lector["MetodoPago"].ToString(),
                        Fecha = (DateTime)conexion.Lector["FechaDePago"],
                        Estado = conexion.Lector["EstadoPago"].ToString(),
                        Descripcion = conexion.Lector["Detalles"].ToString()
                    };

                    return aux;
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

        public void eliminar(int idPedido)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            { 
                string queryPedido = "Update Pedidos Set IDEstadoPedido = 4 Where IDPedido = @IdPedido";
                conexion.setearConsulta(queryPedido);
                conexion.agregarParametros("@IdPedido", idPedido);
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

        public void modificarEstadoPedidoYEnvio(int idPedido, int idNuevoEstadoPedido, int idNuevoEstadoEnvio)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Update Pedidos Set IDEstadoPedido = @EstadoPedido, IDEnvio = @EstadoEnvio Where IDPedido = @Id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", idPedido);
                conexion.agregarParametros("@EstadoPedido", idNuevoEstadoPedido);
                conexion.agregarParametros("@EstadoEnvio", idNuevoEstadoEnvio);
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


        public List<EstadoPedido> listarEstados()
        {
            List<EstadoPedido> listaEstados = new List<EstadoPedido>();

            try
            {
                conexion.setearConsulta("Select IDEstadoPedido, Descripcion From EstadoDePedidos");
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    EstadoPedido aux = new EstadoPedido();
                    aux.IdEstadoPedido = (byte)conexion.Lector["IDEstadoPedido"];
                    aux.Descripcion = conexion.Lector["Descripcion"].ToString();
                    listaEstados.Add(aux);
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

            return listaEstados;
        }
        public List<DetallePedido> obtenerDetallesPorPedido(int idPedido)
        {
            AccesoADatos conexion = new AccesoADatos();
            List<DetallePedido> lista = new List<DetallePedido>();

            try
            {
                string query = @"Select dp.IdProducto, p.Nombre, dp.Cantidad, dp.PrecioUnitario
                         From DetalleDePedidos dp
                         Inner Join Productos p ON dp.IdProducto = p.IdProducto
                         Where dp.IdPedido = @IdPedido";

                conexion.setearConsulta(query);
                conexion.agregarParametros("@IdPedido", idPedido);
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    DetallePedido detalle = new DetallePedido();
                    detalle.Producto = new Producto();
                    detalle.Producto.Nombre = conexion.Lector["Nombre"].ToString();
                    detalle.Cantidad = (int)conexion.Lector["Cantidad"];
                    detalle.PrecioUnitario = (decimal)conexion.Lector["PrecioUnitario"];
                    lista.Add(detalle);
                }
                return lista;
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

        public List<EstadoEnvio> listarEstadosEnvio()
        {
            List<EstadoEnvio> lista = new List<EstadoEnvio>();
            try
            {
                conexion.setearConsulta("Select IDEnvio, Descripcion From EstadoDeEnvios");
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    EstadoEnvio estado = new EstadoEnvio();
                    estado.IdEstadoEnvio = (int)conexion.Lector["IDEnvio"];
                    estado.Descripcion = conexion.Lector["Descripcion"].ToString();
                    lista.Add(estado);
                }
                return lista;
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

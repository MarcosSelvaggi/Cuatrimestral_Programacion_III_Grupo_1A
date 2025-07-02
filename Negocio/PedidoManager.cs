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
                       P.IdEstadoPago,
                       EP.Descripcion AS EstadoPago, 
                       DP.Detalles
                       From Pedidos P
                       Inner Join Usuarios U ON P.IDCliente = U.IdUsuario
                       Inner Join EstadoDePedidos E ON P.IDEstadoPedido = E.IDEstadoPedido
                       Inner Join DetalleDePagos DP ON P.IDPago = DP.IDPago
                       Inner Join MetodosDePago M ON DP.IDMetodoPago = M.IDMetodoPago
                       Inner Join EstadoDePagos EP ON P.IdEstadoPago = EP.IdEstadoPago
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
                    aux.EstadoPedido = new EstadoPedido()
                    {
                        IdEstadoPedido = (byte)conexion.Lector["IDEstadoPedido"],
                        Descripcion = conexion.Lector["Estado"].ToString()
                    };
                    aux.EstadoPago = new EstadoPago()
                    {
                        IdEstadoPago = (byte)conexion.Lector["IdEstadoPago"],
                        Descripcion = conexion.Lector["EstadoPago"].ToString()
                    };
                    aux.PrecioTotal = (decimal)conexion.Lector["PrecioTotal"];
                    aux.EstadoEnvio = new EstadoEnvio()
                    {
                        IdEstadoEnvio = (int)conexion.Lector["IDEnvio"]
                    };
                    aux.DetallePago = new DetallePago()
                    {
                        Metodo = conexion.Lector["MetodoPago"].ToString(),
                        Fecha = (DateTime)conexion.Lector["FechaDePago"],
                        Descripcion = conexion.Lector["Detalles"].ToString(),
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
                string queryPedido = "Update Pedidos Set IDEstadoPedido = 5, IDEnvio = 6, IdEstadoPago = 3 Where IDPedido = @IdPedido";
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

        public void modificarEstadoPedidoYEnvio(int idPedido, int idNuevoEstadoPedido, int idNuevoEstadoEnvio, int idNuevoEstadoPago)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                //Actualizo el Pedido
                string query = "Update Pedidos Set IDEstadoPedido = @EstadoPedido, IDEnvio = @EstadoEnvio, IdEstadoPago = @EstadoPago Where IDPedido = @Id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", idPedido);
                conexion.agregarParametros("@EstadoPedido", idNuevoEstadoPedido);
                conexion.agregarParametros("@EstadoEnvio", idNuevoEstadoEnvio);
                conexion.agregarParametros("@EstadoPago", idNuevoEstadoPago);
                conexion.ejecutarNonQuery();

                ////Actualizo Detalle de Pagos
                //string queryPago = @"UPDATE DetalleDePagos SET IdEstadoPago = @EstadoPago WHERE IDPago = (SELECT IDPago FROM Pedidos WHERE IDPedido = @IdPedido)";
                //conexion.setearConsulta(queryPago);
                //conexion.agregarParametros("@EstadoPago", idNuevoEstadoPago);
                //conexion.agregarParametros("@IdPedido", idPedido);
                //conexion.ejecutarNonQuery();
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

        public List<Pedido> listarVentasEntregadas()
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = @"
            Select P.IDPedido, 
                   U.Nombre + ' ' + U.Apellido AS Cliente, 
                   P.FechaDePedido, 
                   EP.Descripcion AS EstadoPedido,
                   EE.Descripcion AS EstadoEnvio,
                   EPG.Descripcion AS EstadoPago,
                   P.PrecioTotal
            From Pedidos P
            Inner Join Usuarios U ON P.IDCliente = U.IdUsuario
            Inner Join EstadoDePedidos EP ON P.IDEstadoPedido = EP.IDEstadoPedido
            Inner Join EstadoDeEnvios EE ON P.IDEnvio = EE.IDEnvio
            Inner Join EstadoDePagos EPG ON P.IdEstadoPago = EPG.IdEstadoPago
            Where EP.Descripcion = 'Completado' 
              AND EE.Descripcion = 'Entregado'
              AND EPG.Descripcion = 'Aprobado'";

                conexion.setearConsulta(query);
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    Pedido aux = new Pedido();
                    aux.IdPedido = (int)conexion.Lector["IDPedido"];
                    aux.Cliente = conexion.Lector["Cliente"].ToString();
                    aux.FechaPedido = (DateTime)conexion.Lector["FechaDePedido"];
                    aux.EstadoPedido.Descripcion = conexion.Lector["EstadoPedido"].ToString();
                    aux.EstadoEnvio = new EstadoEnvio();
                    aux.EstadoEnvio.Descripcion = conexion.Lector["EstadoEnvio"].ToString();
                    aux.PrecioTotal = conexion.Lector["PrecioTotal"] != DBNull.Value ? (decimal)conexion.Lector["PrecioTotal"] : 0;

                    aux.EstadoPago = new EstadoPago()
                    {
                        Descripcion = conexion.Lector["EstadoPago"].ToString()
                    };

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

        public List<EstadoPago> listarEstadosPago()
        {
            List<EstadoPago> lista = new List<EstadoPago>();

            try
            {
                conexion.setearConsulta("Select IdEstadoPago, Descripcion From EstadoDePagos");
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    EstadoPago estado = new EstadoPago();
                    estado.IdEstadoPago = (byte)conexion.Lector["IdEstadoPago"];
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

        public int crearPedidoCompleto(Pedido pedido)
        {
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query1 = @"Insert into DetalleDePagos (IDMetodoPago, FechaDePago, Detalles)
                             values (@idMetodoPago, @fechaPago, @detalles);
                             Select Scope_Identity()";

                conexion.setearConsulta(query1);
                conexion.agregarParametros("@idMetodoPago", pedido.MetodoPago.Id);
                conexion.agregarParametros("@fechaPago", pedido.DetallePago.Fecha);
                conexion.agregarParametros("@detalles", pedido.DetallePago.Descripcion);
                int idPago = Convert.ToInt32(conexion.EjecutarScalar());

                string query2 = @"Insert into Pedidos (IDCliente, IDEnvio, IDEstadoPedido, IdEstadoPago, FechaDePedido, PrecioTotal, IDPago)
                             values (@idCliente, @idEnvio, @idEstadoPedido, @idEstadoPago, @fechaPedido, @precioTotal, @idPago);
                             Select Scope_Identity()";

                conexion.setearConsulta(query2);
                conexion.limpiarParametros();
                conexion.agregarParametros("@idCliente", pedido.IdUsuario);
                conexion.agregarParametros("@idEnvio", pedido.EstadoEnvio.IdEstadoEnvio);
                conexion.agregarParametros("@idEstadoPedido", pedido.EstadoPedido.IdEstadoPedido);
                conexion.agregarParametros("@idEstadoPago", pedido.EstadoPago.IdEstadoPago);
                conexion.agregarParametros("@fechaPedido", pedido.FechaPedido);
                conexion.agregarParametros("@precioTotal", pedido.Total);
                conexion.agregarParametros("@idPago", idPago);
                int idPedido = Convert.ToInt32(conexion.EjecutarScalar());

                foreach (var detalle in pedido.ListaDetalles)
                {
                    string query3 = @"Insert into DetalleDePedidos (IDPedido, IDProducto, Cantidad, PrecioUnitario, Subtotal, Impuestos)
                                    values (@idPedido, @idProducto, @cantidad, @precioUnitario, @subtotal, 21)";

                    conexion.setearConsulta(query3);
                    conexion.limpiarParametros();
                    conexion.agregarParametros("@idPedido", idPedido);
                    conexion.agregarParametros("@idProducto", detalle.Producto.Id);
                    conexion.agregarParametros("@cantidad", detalle.Cantidad);
                    conexion.agregarParametros("@precioUnitario", detalle.PrecioUnitario);
                    conexion.agregarParametros("@subtotal", detalle.Subtotal);
                    conexion.ejecutarNonQuery();
                }

                return idPedido;
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

        public List<Pedido> listarPedidosPorUsuario(int idUsuario)
        {
            List<Pedido> listaPedidos = new List<Pedido>();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = @"Select P.IDPedido, P.FechaDePedido, EP.Descripcion as EstadoPedido, EPA.Descripcion as EstadoPago, P.PrecioTotal from Pedidos P
                            inner join EstadoDePedidos EP on P.IDEstadoPedido = EP.IDEstadoPedido 
                            inner join EstadoDePagos EPA on P.IDEstadoPago = EPA.IdEstadoPago
                            where P.IDCliente = @idUsuario order by P.FechaDePedido DESC";

                conexion.setearConsulta(query);
                conexion.agregarParametros("@idUsuario", idUsuario);
                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    Pedido aux = new Pedido();
                    aux.IdPedido = (int)conexion.Lector["IDPedido"];
                    aux.FechaPedido = (DateTime)conexion.Lector["FechaDePedido"];

                    aux.EstadoPedido = new EstadoPedido();
                    aux.EstadoPedido.Descripcion = conexion.Lector["EstadoPedido"].ToString();

                    aux.EstadoPago = new EstadoPago();
                    aux.EstadoPago.Descripcion = conexion.Lector["EstadoPago"].ToString();

                    aux.PrecioTotal = (decimal)conexion.Lector["PrecioTotal"];

                    listaPedidos.Add(aux);
                }

                return listaPedidos;
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

        public Pedido obtenerPedidoFactura(int idPedido)
        {
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = @"Select P.IDPedido, U.IdUsuario, U.Nombre, U.Apellido, U.Documento, U.Direccion, U.Localidad, U.Provincia, U.CodigoPostal,
                            U.Telefono, P.FechaDePedido, E.Descripcion as Estado, P.PrecioTotal, P.IDEnvio, EE.Descripcion as EstadoEnvioDescripcion, 
                            P.IDEstadoPedido, M.Descripcion as MetodoPago, DP.FechaDePago, DP.Detalles, EP.Descripcion as EstadoPagoDescripcion
                            From Pedidos P
                            Inner Join Usuarios U on P.IDCliente = U.IdUsuario
                            Inner Join EstadoDePedidos E on P.IDEstadoPedido = E.IDEstadoPedido
                            Inner Join EstadoDeEnvios EE on P.IDEnvio = EE.IDEnvio
                            Inner Join DetalleDePagos DP on P.IDPago = DP.IDPago
                            Inner Join MetodosDePago M on DP.IDMetodoPago = M.IDMetodoPago
                            Inner Join EstadoDePagos EP on P.IdEstadoPago = EP.IdEstadoPago
                            Where P.IDPedido = @Id";

                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", idPedido);
                conexion.ejecutarQuery();

                if (conexion.Lector.Read())
                {
                    Pedido aux = new Pedido();

                    aux.IdPedido = (int)conexion.Lector["IDPedido"];
                    aux.Cliente = conexion.Lector["Nombre"].ToString() + " " + conexion.Lector["Apellido"].ToString();
                    aux.FechaPedido = (DateTime)conexion.Lector["FechaDePedido"];

                    aux.EstadoPedido = new EstadoPedido()
                    {
                        IdEstadoPedido = (byte)conexion.Lector["IDEstadoPedido"],
                        Descripcion = conexion.Lector["Estado"].ToString()
                    };

                    aux.EstadoEnvio = new EstadoEnvio()
                    {
                        IdEstadoEnvio = (int)conexion.Lector["IDEnvio"],
                        Descripcion = conexion.Lector["EstadoEnvioDescripcion"].ToString()
                    };

                    aux.PrecioTotal = (decimal)conexion.Lector["PrecioTotal"];

                    aux.DetallePago = new DetallePago()
                    {
                        Metodo = conexion.Lector["MetodoPago"].ToString(),
                        Fecha = (DateTime)conexion.Lector["FechaDePago"],
                        Estado = conexion.Lector["EstadoPagoDescripcion"].ToString(),
                        Descripcion = conexion.Lector["Detalles"].ToString()
                    };

                    aux.Usuario = new Usuarios()
                    {
                        Id = (int)conexion.Lector["IdUsuario"],
                        Nombre = conexion.Lector["Nombre"].ToString(),
                        Apellido = conexion.Lector["Apellido"].ToString(),
                        Documento = conexion.Lector["Documento"].ToString(),
                        Direccion = conexion.Lector["Direccion"].ToString(),
                        Localidad = conexion.Lector["Localidad"].ToString(),
                        Provincia = conexion.Lector["Provincia"].ToString(),
                        CodigoPostal = conexion.Lector["CodigoPostal"].ToString(),
                        Telefono = conexion.Lector["Telefono"].ToString()
                    };

                    DetalleManager detalleManager = new DetalleManager();
                    aux.ListaDetalles = detalleManager.listarDetallesPedido(idPedido);

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

        public EstadoPago obtenerEstadoPagoPorId(int id)
        {
            AccesoADatos datos = new AccesoADatos();
            EstadoPago estadoPago = new EstadoPago();

            try
            {
                datos.setearConsulta("Select IdEstadoPago, Descripcion from EstadoDePagos where IdEstadoPago = @id");
                datos.agregarParametros("@id", id);
                datos.ejecutarQuery();

                if (datos.Lector.Read())
                {
                    estadoPago = new EstadoPago();
                    estadoPago.IdEstadoPago = (byte)datos.Lector["IdEstadoPago"];
                    estadoPago.Descripcion = datos.Lector["Descripcion"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
            return estadoPago;
        }

        public EstadoPedido obtenerEstadoPedidoPorId(int id)
        {
            AccesoADatos datos = new AccesoADatos();
            EstadoPedido estadoPedido = new EstadoPedido();

            try
            {
                datos.setearConsulta("Select IDEstadoPedido, Descripcion from EstadoDePedidos where IDEstadoPedido = @id");
                datos.agregarParametros("@id", id);
                datos.ejecutarQuery();

                if (datos.Lector.Read())
                {
                    estadoPedido = new EstadoPedido();
                    estadoPedido.IdEstadoPedido = (byte)datos.Lector["IDEstadoPedido"];
                    estadoPedido.Descripcion = datos.Lector["Descripcion"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
            return estadoPedido;
        }

        public EstadoEnvio obtenerEstadoEnvioPorId(int id)
        {
            AccesoADatos datos = new AccesoADatos();
            EstadoEnvio estadoEnvio = new EstadoEnvio();

            try
            {
                datos.setearConsulta("Select IDEnvio, Descripcion from EstadoDeEnvios where IDEnvio = @id");
                datos.agregarParametros("@id", id);
                datos.ejecutarQuery();

                if (datos.Lector.Read())
                {
                    estadoEnvio = new EstadoEnvio();
                    estadoEnvio.IdEstadoEnvio = (int)datos.Lector["IDEnvio"];
                    estadoEnvio.Descripcion = datos.Lector["Descripcion"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
            return estadoEnvio;
        }
    }
}

using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class MarcaManager
    {
        AccesoADatos conexion = new AccesoADatos();

        public List<Marca> listar()
        {
            List<Marca> listaMarcas = new List<Marca>();
            try
            {
                conexion.setearConsulta("Select IdMarca, Descripcion from Marcas");
                conexion.ejecutarQuery();
                while (conexion.Lector.Read())
                {
                    Marca aux = new Marca();
                    //aux.Id = (int)conexion.Lector["IdMarca"];
                    aux.Id = (byte)conexion.Lector["IdMarca"];
                    try
                    {
                        aux.Descripcion = (string)conexion.Lector["Descripcion"];
                    }
                    catch (Exception)
                    {
                        aux.Descripcion = "Error al leer la marca";
                    }
                    listaMarcas.Add(aux);
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

            return listaMarcas;
        }

        public Marca obtenerMarcaPorId(int idMarca)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Select IdMarca, Descripcion, Activo From Marcas Where IdMarca = @Id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", idMarca);

                conexion.ejecutarQuery();

                if (conexion.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.Id = (byte)conexion.Lector["IdMarca"];
                    marca.Descripcion = conexion.Lector["Descripcion"].ToString();
                    marca.Activo = (bool)conexion.Lector["Activo"];
                    return marca;
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

        public void eliminar(int idMarca)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Update Marcas Set Activo = 0 Where IdMarca = @Id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", idMarca);
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

        public void agregar(Marca marcaNueva)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Insert Into Marcas (Descripcion, Activo) Values (@Descripcion, @Activo)";
                conexion.setearConsulta(query);
                conexion.limpiarParametros();
                conexion.agregarParametros("@Descripcion", marcaNueva.Descripcion);
                conexion.agregarParametros("@Activo", marcaNueva.Activo);
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

        public void modificar(Marca marcaModificada)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Update Marcas Set Descripcion = @Descripcion, Activo = @Activo where IdMarca = @Id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", marcaModificada.Id);
                conexion.agregarParametros("@Activo", marcaModificada.Activo);
                conexion.agregarParametros("@Descripcion", marcaModificada.Descripcion);
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
    }
}


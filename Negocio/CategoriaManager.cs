using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public  class CategoriaManager
    {
        public List<Categoria> listar()
        {

            List<Categoria> listaCategorias = new List<Categoria>();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                conexion.setearConsulta("Select IdCategoria, Descripcion from Categorias");
                conexion.ejecutarQuery();
                while (conexion.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    //aux.Id = (int)conexion.Lector["IdCategoria"];
                    aux.Id = (byte)conexion.Lector["IdCategoria"];
                    try
                    {
                        aux.Descripcion = (string)conexion.Lector["Descripcion"];
                    }
                    catch (Exception)
                    {
                        aux.Descripcion = "Error al leer la categoría";
                    }
                    listaCategorias.Add(aux);
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
            return listaCategorias;
        }

        public void agregar(Categoria categoriaNueva)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Insert Into Categorias (Descripcion, Activo) Values (@Descripcion, @Activo)";
                conexion.setearConsulta(query);
                conexion.limpiarParametros();
                conexion.agregarParametros("@Descripcion", categoriaNueva.Descripcion);
                conexion.agregarParametros("@Activo", categoriaNueva.Activo);
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

        public void modificar(Categoria categoriaModificada)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Update Categorias Set Descripcion = @Descripcion, Activo = @Activo where IdCategoria = @Id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", categoriaModificada.Id);
                conexion.agregarParametros("@Activo", categoriaModificada.Activo);
                conexion.agregarParametros("@Descripcion", categoriaModificada.Descripcion);
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

        
        public Categoria obtenerCategoriaPorId(int idCategoria)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Select IdCategoria, Descripcion, Activo From Categorias Where IdCategoria = @Id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", idCategoria);

                conexion.ejecutarQuery();

                if (conexion.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (byte)conexion.Lector["IdCategoria"];
                    categoria.Descripcion = conexion.Lector["Descripcion"].ToString();
                    categoria.Activo = (bool)conexion.Lector["Activo"];
                    return categoria;
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

        public void eliminar(int idCategoria)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Update Categorias Set Activo = 0 Where IdCategoria = @Id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", idCategoria);
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

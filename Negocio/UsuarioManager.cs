using System;
using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class UsuarioManager
    {

        public Usuarios logearse(string mail, string contraseña)
        {
            Usuarios usuarioLogeado = new Usuarios();
            AccesoADatos conexion = new AccesoADatos();

            try
            {
                string query = "select IdUsuario,Email, Contraseña, IdRol, Activo," +
                               "Documento, Nombre, Apellido, Provincia, Localidad," +
                               "CodigoPostal, Direccion, Telefono from Usuarios " +
                               "where email = @email AND contraseña = @pass";

                conexion.setearConsulta(query);
                conexion.agregarParametros("@email", mail);
                conexion.agregarParametros("@pass", contraseña);
                conexion.ejecutarQuery();

                if (conexion.Lector.Read())
                {
                    usuarioLogeado.Id = (int)conexion.Lector["IdUsuario"];
                    usuarioLogeado.Email = (string)conexion.Lector["email"];
                    usuarioLogeado.Constraseña = (string)conexion.Lector["contraseña"];
                    usuarioLogeado.Rol.Id = (byte)conexion.Lector["IdRol"];
                    usuarioLogeado.Activo = (bool)conexion.Lector["Activo"];
                    usuarioLogeado.Documento = (string)conexion.Lector["Documento"];
                    usuarioLogeado.Nombre = (string)conexion.Lector["Nombre"];
                    usuarioLogeado.Apellido = (string)conexion.Lector["Apellido"];
                    usuarioLogeado.Provincia = (string)conexion.Lector["Provincia"];
                    usuarioLogeado.Localidad = (string)conexion.Lector["Localidad"];
                    usuarioLogeado.CodigoPostal = (string)conexion.Lector["CodigoPostal"];
                    usuarioLogeado.Direccion = (string)conexion.Lector["Direccion"];
                    usuarioLogeado.Telefono = (string)conexion.Lector["Telefono"];
                }
                else
                {
                    usuarioLogeado.Id = -1;
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
            return usuarioLogeado;
        }

        public Usuarios buscarMail(string mail)
        {
            Usuarios usuarioEncontrado = new Usuarios();
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "select IdUsuario from Usuarios where Email = @Email";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Email", mail);
                conexion.ejecutarQuery();

                if (conexion.Lector.HasRows)
                {
                    conexion.Lector.Read();
                    usuarioEncontrado.Id = (int)conexion.Lector["IdUsuario"];
                    usuarioEncontrado.Email = mail;
                }
                else
                {
                    usuarioEncontrado.Id = -1;
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
            return usuarioEncontrado;
        }

        public Usuarios modificarContraseña(Usuarios usuario, string contraseñaNueva)
        {
            Usuarios usuarioModificado = new Usuarios();
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Update Usuarios set Contraseña = @Contraseña WHERE IdUsuario = @IdUsuario";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Contraseña", contraseñaNueva);
                conexion.agregarParametros("@IdUsuario", usuario.Id);
                conexion.ejecutarNonQuery();

                usuarioModificado = logearse(usuario.Email, contraseñaNueva);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return usuarioModificado;
        }

        public List<Usuarios> listar()
        {
            AccesoADatos conexion = new AccesoADatos();
            List<Usuarios> listaUsuarios = new List<Usuarios>();
            try
            {
                string query = "Select IdUsuario, Email, Contraseña, IdRol, Activo, Documento, Nombre, Apellido, Provincia, Localidad, CodigoPostal, Direccion, Telefono from Usuarios";
                conexion.setearConsulta(query);

                conexion.ejecutarQuery();

                while (conexion.Lector.Read())
                {
                    var aux = new Usuarios();
                    aux.Id = (int)conexion.Lector["IdUsuario"];
                    aux.Email = (string)conexion.Lector["Email"];
                    aux.Constraseña = (string)conexion.Lector["Contraseña"];
                    aux.Rol.Id = (byte)conexion.Lector["IdRol"];
                    aux.Activo = (bool)conexion.Lector["Activo"];
                    aux.Documento = (string)conexion.Lector["Documento"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Apellido = (string)conexion.Lector["Apellido"];
                    aux.Provincia = (string)conexion.Lector["Provincia"];
                    aux.Localidad = (string)conexion.Lector["Localidad"];
                    aux.CodigoPostal = (string)conexion.Lector["CodigoPostal"];
                    aux.Direccion = (string)conexion.Lector["Direccion"];
                    aux.Telefono = (string)conexion.Lector["Telefono"];
                    listaUsuarios.Add(aux);
                }
                return listaUsuarios;
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
        public Usuarios obtenerPorID(int id)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Select Email, Contraseña, IdRol, Activo, Documento, Nombre, Apellido, Provincia, Localidad, CodigoPostal, Direccion, Telefono from Usuarios where IdUsuario = @IDUsuario";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@IDUsuario", id);

                conexion.ejecutarQuery();

                if (conexion.Lector.Read())
                {
                    var aux = new Usuarios();
                    aux.Id = id;
                    aux.Email = (string)conexion.Lector["Email"];
                    aux.Constraseña = (string)conexion.Lector["Contraseña"];
                    aux.Rol.Id = (byte)conexion.Lector["IdRol"];
                    aux.Activo = (bool)conexion.Lector["Activo"];
                    aux.Documento = (string)conexion.Lector["Documento"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Apellido = (string)conexion.Lector["Apellido"];
                    aux.Provincia = (string)conexion.Lector["Provincia"];
                    aux.Localidad = (string)conexion.Lector["Localidad"];
                    aux.CodigoPostal = (string)conexion.Lector["CodigoPostal"];
                    aux.Direccion = (string)conexion.Lector["Direccion"];
                    aux.Telefono = (string)conexion.Lector["Telefono"];
                    return aux;
                }
                return null;
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

        public void agregar(Usuarios usuarioNuevo)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Insert Into Usuarios (Email, Activo, IdRol, Contraseña, Documento, Nombre, Apellido, Provincia, Localidad, CodigoPostal, Direccion, Telefono) Values (@Email, @Activo, 2, @Contraseña, @Documento, @Nombre, @Apellido, @Provincia, @Localidad, @CodigoPostal, @Direccion, @Telefono)";
                conexion.setearConsulta(query);
                conexion.limpiarParametros();
                conexion.agregarParametros("@Email", usuarioNuevo.Email);
                conexion.agregarParametros("@Activo", usuarioNuevo.Activo);
                conexion.agregarParametros("@Contraseña", "asd123456");
                conexion.agregarParametros("@Documento", usuarioNuevo.Documento);
                conexion.agregarParametros("@Nombre", usuarioNuevo.Nombre);
                conexion.agregarParametros("@Apellido", usuarioNuevo.Apellido);
                conexion.agregarParametros("@Provincia", usuarioNuevo.Provincia);
                conexion.agregarParametros("@Localidad", usuarioNuevo.Localidad);
                conexion.agregarParametros("@CodigoPostal", usuarioNuevo.CodigoPostal);
                conexion.agregarParametros("@Direccion", usuarioNuevo.Direccion);
                conexion.agregarParametros("@Telefono", usuarioNuevo.Telefono);
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

        public void modificar(Usuarios usuarioModificado)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Update Usuarios Set Email = @Email, Activo = @Activo, Documento = @Documento, Nombre = @Nombre, Apellido = @Apellido, Provincia = @Provincia, Localidad = @Localidad, CodigoPostal = @CodigoPostal, Direccion = @Direccion, Telefono = @Telefono Where IdUsuario = @Id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", usuarioModificado.Id);
                conexion.agregarParametros("@Email", usuarioModificado.Email);
                conexion.agregarParametros("@Activo", usuarioModificado.Activo);
                conexion.agregarParametros("@Documento", usuarioModificado.Documento);
                conexion.agregarParametros("@Nombre", usuarioModificado.Nombre);
                conexion.agregarParametros("@Apellido", usuarioModificado.Apellido);
                conexion.agregarParametros("@Provincia", usuarioModificado.Provincia);
                conexion.agregarParametros("@Localidad", usuarioModificado.Localidad);
                conexion.agregarParametros("@CodigoPostal", usuarioModificado.CodigoPostal);
                conexion.agregarParametros("@Direccion", usuarioModificado.Direccion);
                conexion.agregarParametros("@Telefono", usuarioModificado.Telefono);
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

        public void eliminar(int idUsuario)
        {
            AccesoADatos conexion = new AccesoADatos();
            try
            {
                string query = "Update Usuarios Set Activo = 0 Where IdUsuario = @Id";
                conexion.setearConsulta(query);
                conexion.agregarParametros("@Id", idUsuario);
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

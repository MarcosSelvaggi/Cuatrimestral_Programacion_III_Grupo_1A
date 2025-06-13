using System;
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
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class UsuarioManager
    {
        //Para el hash de contraseñas mirar este tutorial que explica rápido como funciona BCrypt 
        //https://www.youtube.com/watch?v=UNLl4kCpwGo

        public Usuarios logearse(string mail, string contraseña)
        {
            //Método para hashear las contraseñas en la BD, descomentar para usarlo 1 sola vez
            //HashearContraseñas();

            Usuarios usuarioLogeado = new Usuarios()
            {
                Id = -1
            };
            AccesoADatos conexion = new AccesoADatos();
            AccesoADatos verificarContraseña = new AccesoADatos();

            try
            {
                //Nos traemos la contraseña hasheada para comprarla 
                verificarContraseña.setearConsulta("Select Contraseña from Usuarios where Email = @Email");
                verificarContraseña.agregarParametros("@Email", mail);
                verificarContraseña.ejecutarQuery();

                if (verificarContraseña.Lector.Read())
                {
                    //Hasheamos la contraseña que nos pasó el usuario y verificamos que esté correcta 
                    if (BCrypt.Net.BCrypt.EnhancedVerify(contraseña, (string)verificarContraseña.Lector["Contraseña"]))
                    {
                        string query = "select IdUsuario,Email, Contraseña, IdRol, Activo," +
                                       "Documento, Nombre, Apellido, Provincia, Localidad," +
                                       "CodigoPostal, Direccion, Telefono from Usuarios " +
                                       "where email = @email";

                        conexion.setearConsulta(query);
                        conexion.agregarParametros("@email", mail);
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
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.cerrarConexion();
                verificarContraseña.cerrarConexion();
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
                string query = "Select IdUsuario, Email, Contraseña, IdRol, Activo, Documento, Nombre, Apellido, Provincia, Localidad, CodigoPostal, Direccion, Telefono from Usuarios Where IdRol = 2";
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

        //Método para hashear las contraseñas, es necesario ejecutarlo 1 sola vez 
        private void HashearContraseñas()
        {
            UsuarioManager usuarioManager = new UsuarioManager();

            //Cuidado que el listar SÓLO trae los usuarios que no sean administradores
            //Para hashear los usuarios admin se necesita modificar el IdRol a 2 de los admin, hashear y volver a cambiar el idRol
            List<Usuarios> ListaUsuarios = usuarioManager.listar();

            try
            {
                foreach (var Usuario in ListaUsuarios)
                {

                    Usuario.Constraseña = BCrypt.Net.BCrypt.EnhancedHashPassword(Usuario.Constraseña, 14);
                    AccesoADatos conexion = new AccesoADatos();
                    conexion.setearConsulta("Update Usuarios set Contraseña = @Contraseña where Email = @Email");
                    conexion.agregarParametros("@Email", Usuario.Email);
                    conexion.agregarParametros("@Contraseña", Usuario.Constraseña);
                    conexion.ejecutarNonQuery();
                    conexion.cerrarConexion();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

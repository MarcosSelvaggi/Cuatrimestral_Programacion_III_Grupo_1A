using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}

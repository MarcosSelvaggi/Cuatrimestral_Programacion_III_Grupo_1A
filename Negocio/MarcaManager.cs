using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
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
    }
}

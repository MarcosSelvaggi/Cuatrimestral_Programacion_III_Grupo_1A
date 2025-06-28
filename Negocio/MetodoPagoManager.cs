using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MetodoPagoManager
    {
        public List<MetodoPago> listarMetodosPago()
        {
            List<MetodoPago> lista = new List<MetodoPago>();
            AccesoADatos datos = new AccesoADatos();

            try
            {
                datos.setearConsulta("Select IDMetodoPago, Descripcion from MetodosDePago");
                datos.ejecutarQuery();

                while (datos.Lector.Read())
                {
                    MetodoPago metodo = new MetodoPago();
                    metodo.Id = Convert.ToInt32(datos.Lector["IDMetodoPago"]);
                    metodo.Descripcion = datos.Lector["Descripcion"].ToString();
                    lista.Add(metodo);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

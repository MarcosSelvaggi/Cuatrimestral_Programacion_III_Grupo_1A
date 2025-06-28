using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Pedido
    {
        public string Cliente { get; set; }
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public Usuarios Usuario { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public EstadoEnvio EstadoEnvio{ get; set; }
        public DetallePago DetallePago { get; set; }
        public DateTime FechaPedido { get; set; }
        public List<Detalle> ListaDetalles { get; set; }
        public bool Activo { get; set; }
        public decimal PrecioTotal { get; set; }
        public EstadoPago EstadoPago { get; set; }

        public Decimal Total 
        {
            get
            {
                decimal total = 0;
                foreach (Detalle detalle in ListaDetalles)
                {
                    total += detalle.Subtotal;
                }
                return total;
            }
        }

        public Pedido()
        {
            EstadoPedido = new EstadoPedido();
            MetodoPago = new MetodoPago();
            DetallePago = new DetallePago();
            EstadoEnvio = new EstadoEnvio();
            ListaDetalles = new List<Detalle>();
        }
    }
}

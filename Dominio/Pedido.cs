using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Pedido
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public DateTime FechaPedido { get; set; }
        public List<Detalle> ListaDetalles { get; set; }
        public bool Activo { get; set; }
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
            ListaDetalles = new List<Detalle>();
        }
    }
}

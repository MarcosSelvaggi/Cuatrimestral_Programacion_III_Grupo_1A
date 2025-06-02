using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Carrito
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public List<Detalle> ListaProductos { get; set; }
        public Decimal Total
        {
            get
            {
                decimal total = 0;
                foreach (Detalle detalle in ListaProductos)
                {
                    total += detalle.Subtotal;
                }
                return total;
            }
        }

        public Carrito()
        {
            ListaProductos = new List<Detalle>();
        }
    }
}

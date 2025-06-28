namespace Dominio
{
    public class Detalle
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal
        {
            get
            {
                return Cantidad * PrecioUnitario;
            }
        }
        public Producto Producto { get; set; }
    }
}

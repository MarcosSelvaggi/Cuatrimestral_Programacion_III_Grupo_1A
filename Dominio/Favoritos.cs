namespace Dominio
{
    public class Favoritos
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        /// <summary>
        /// Revisar el IDProducto y una lista de productos
        /// </summary>
        public int IdProducto { get; set; }
        public Producto producto { get; set; }
    }
}

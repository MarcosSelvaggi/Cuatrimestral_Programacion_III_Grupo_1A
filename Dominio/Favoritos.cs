namespace Dominio
{
    public class Favoritos
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public Producto producto { get; set; }
    }
}

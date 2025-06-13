namespace Dominio
{
    public class Favorito
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }
    }
}

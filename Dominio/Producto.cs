namespace Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Categoria Categoria { get; set; }
        public Marca Marca { get; set; }
        public bool Activo { get; set; }

        public Producto()
        {
            Categoria = new Categoria();
            Marca = new Marca();
        }
    }
}

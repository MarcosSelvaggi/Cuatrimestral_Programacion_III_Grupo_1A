namespace Dominio
{
    public class ImagenesProducto
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public string UrlProducto { get; set; }

        public ImagenesProducto() { }
        public ImagenesProducto(int idProducto, string urlProducto)
        {
            IdProducto = idProducto;
            UrlProducto = urlProducto;
        }
        public override string ToString()
        {
            return UrlProducto;
        }
    }
}

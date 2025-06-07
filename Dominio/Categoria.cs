namespace Dominio
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public bool Activo { get; set; }
        public Categoria() { }
        public Categoria(string descripcion)
        {
            Descripcion = descripcion;
        }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}

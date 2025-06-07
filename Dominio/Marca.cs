namespace Dominio
{
    public class Marca
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public bool Activo { get; set; }
        public Marca() { }
        public Marca(string descripcion)
        {
            Descripcion = descripcion;
        }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}

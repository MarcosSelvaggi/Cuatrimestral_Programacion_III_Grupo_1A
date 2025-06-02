namespace Dominio
{
    public class Rol
    {
        public byte Id { get; set; }
        public string Descripcion { get; set; }

        public Rol() { }
        public Rol(string descripcion)
        {
            Descripcion = descripcion;
        }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}

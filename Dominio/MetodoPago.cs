namespace Dominio
{
    public class MetodoPago
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        
        public MetodoPago() { }
        public MetodoPago(string descripcion)
        {
            Descripcion = descripcion;
        }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}

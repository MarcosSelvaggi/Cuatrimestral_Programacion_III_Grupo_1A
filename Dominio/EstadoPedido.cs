namespace Dominio
{
    public class EstadoPedido
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public EstadoPedido() { }
        public EstadoPedido(string descripcion)
        {
            Descripcion = descripcion;
        }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}

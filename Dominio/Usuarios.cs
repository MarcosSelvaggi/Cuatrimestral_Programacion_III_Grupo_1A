namespace Dominio
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Constraseña { get; set; }
        public Rol Rol { get; set; }
        public bool Activo { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public Usuarios()
        {
            Rol = new Rol();
        }
    }
}

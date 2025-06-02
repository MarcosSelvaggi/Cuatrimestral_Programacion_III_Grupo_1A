namespace Dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Constraseña { get; set; }
        public Rol Rol { get; set; }
        public bool Estado { get; set; }

        public Usuario()
        {
            Rol = new Rol();
        }
    }
}

using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace UI.Usuario
{
    public partial class Checkout : UI.ClaseMaster.BasePage
    {
        public Usuarios UsuarioLogeado { get; set; }
        public List<Detalle> listaDetalles;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            UsuarioLogeado = (Usuarios)Session["Usuario"];

            CarritoManager carritoManager = new CarritoManager();
            DetalleManager detalleManager = new DetalleManager();

            int idCarrito = carritoManager.carritoDisponible(UsuarioLogeado.Id);
            listaDetalles = detalleManager.listarDetallesCarrito(idCarrito);
        }
    }
}
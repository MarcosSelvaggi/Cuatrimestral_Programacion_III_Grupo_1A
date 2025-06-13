using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.Usuario
{
    public partial class Favoritos : UI.ClaseMaster.BasePage
    {
        public List<Favorito> listaFavoritos;
        public List<ImagenesProducto> listaImagenes;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Inicio.aspx");
                return;
            }

            int idUsuario = ((Usuarios)Session["Usuario"]).Id;

            FavoritoManager favoritoManager = new FavoritoManager();
            ImagenManager imagenManager = new ImagenManager();

            if (!IsPostBack && Request.QueryString["quitar"] != null)
            {
                string quitarStr = Request.QueryString["quitar"];
                if (int.TryParse(quitarStr, out int idProductoQuitar))
                {
                    favoritoManager.eliminarFavorito(idProductoQuitar, idUsuario);
                    Response.Redirect("Favoritos.aspx");
                    return;
                }
            }

            listaFavoritos = favoritoManager.listarFavoritoUsuario(idUsuario);
            listaImagenes = imagenManager.listarImagenes();
        }
    }
}
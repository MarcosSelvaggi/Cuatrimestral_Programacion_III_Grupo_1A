using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace UI.Admin
{
    public partial class GestionMarcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var lista = new MarcaManager().listar();
                rptMarcas.DataSource = lista;
                rptMarcas.DataBind();
            }
        }
    }
}
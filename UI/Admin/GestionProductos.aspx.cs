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
    public partial class GestionProductos : System.Web.UI.Page
    {
        private ProductoManager managerProducto = new ProductoManager();
        private CategoriaManager managerCategoria = new CategoriaManager();
        private MarcaManager managerMarca = new MarcaManager();
        private List<string> ImagenesTemporalesEdicion
        {
            get
            {
                if (Session["ImagenesTemporalesEdicion"] == null)
                    Session["ImagenesTemporalesEdicion"] = new List<string>();
                return (List<string>)Session["ImagenesTemporalesEdicion"];
            }
            set
            {
                Session["ImagenesTemporalesEdicion"] = value;
            }
        }
        public bool EsModoEdicion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProductos();
                cargarCategoriasEnDropdown(ddlCategoria);
                cargarCategoriasEnDropdown(ddlNuevaCategoria);
                cargarMarcasEnDropdown(ddlMarca);
                cargarMarcasEnDropdown(ddlNuevaMarca);
            }
        }

        protected void rptProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idProducto = Convert.ToInt32(e.CommandArgument);
            var producto = managerProducto.obtenerProductoPorId(idProducto);

            if (producto != null)
            {
                if (e.CommandName == "Ver")
                {
                    lblIdProducto.Text = producto.Id.ToString();
                    lblModalTitulo.InnerText = "Detalle Producto";
                    EsModoEdicion = false;
                    ImagenManager imagenManager = new ImagenManager();
                    var imagenes = imagenManager.listarPorProducto(producto.Id);
                    rptImagenes.DataSource = imagenes;
                    rptImagenes.DataBind();
                    ImagenesTemporalesEdicion.Clear();


                    rptImagenesTemporalesEdicion.DataSource = null;
                    rptImagenesTemporalesEdicion.DataBind();
                    rptImagenesTemporales.DataSource = null;
                    rptImagenesTemporales.DataBind();
                    ImagenesTemporales.Clear();

                    divAgregarImagenEdicion.Visible = false;

                    lblNombre.Text = producto.Nombre;
                    lblPrecio.Text = producto.Precio.ToString("C");
                    lblStock.Text = producto.Stock.ToString();
                    lblCategoria.Text = producto.Categoria?.Descripcion ?? "";
                    lblMarca.Text = producto.Marca?.Descripcion ?? "";
                    lblActivo.Text = producto.Activo ? "Sí" : "No";

                    // Oculto los campos de edición
                    alternarCamposEdicion(false);

                    btnGuardar.Visible = false;

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalProducto", "var modal = new bootstrap.Modal(document.getElementById('modalProducto')); modal.show();", true);
                }
                else if (e.CommandName == "Editar")
                {
                    lblIdProducto.Text = producto.Id.ToString();
                    lblModalTitulo.InnerText = "Editar Producto";
                    EsModoEdicion = true;
                    ImagenManager imagenManager = new ImagenManager();
                    var imagenes = imagenManager.listarPorProducto(producto.Id);
                    rptImagenes.DataSource = imagenes;
                    rptImagenes.DataBind();
                    txtNombre.Text = producto.Nombre;
                    txtPrecio.Text = producto.Precio.ToString("N2");
                    txtStock.Text = producto.Stock.ToString();

                    // Cargo las imágenes temporales vacías al iniciar la edición
                    ImagenesTemporalesEdicion = new List<string>();
                    divAgregarImagenEdicion.Visible = true;

                    // Bindeo imágenes existentes
                    rptImagenesTemporalesEdicion.DataSource = ImagenesTemporalesEdicion;
                    rptImagenesTemporalesEdicion.DataBind();



                    // Selecciono categoria y marca en los ddls
                    ddlCategoria.SelectedValue = producto.Categoria?.Id.ToString() ?? "";
                    ddlMarca.SelectedValue = producto.Marca?.Id.ToString() ?? "";

                    ddlActivo.SelectedValue = producto.Activo.ToString().ToLower();

                    // Muestro los controles de edición
                    alternarCamposEdicion(true);

                    btnGuardar.Visible = true;

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalProducto", "var modal = new bootstrap.Modal(document.getElementById('modalProducto')); modal.show();", true);
                }
                else if (e.CommandName == "Eliminar")
                {
                    lblIdEliminar.Text = producto.Id.ToString();
                    lblDescripcionEliminar.Text = $"¿Estás seguro que querés eliminar el producto: <strong>{producto.Nombre}</strong>?";

                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirEliminarModal", "var modal = new bootstrap.Modal(document.getElementById('modalEliminar')); modal.show();", true);
                }
            }
        }
        protected void rptImagenes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EliminarImagen")
            {
                int idImagen = Convert.ToInt32(e.CommandArgument);
                ImagenManager imagenManager = new ImagenManager();
                imagenManager.eliminar(idImagen);

                // Vuelvo a cargar las imágenes actualizadas
                int idProducto = Convert.ToInt32(lblIdProducto.Text);
                var imagenes = imagenManager.listarPorProducto(idProducto);
                rptImagenes.DataSource = imagenes;
                rptImagenes.DataBind();

                EsModoEdicion = true;
                alternarCamposEdicion(true);
                divAgregarImagenEdicion.Visible = true;
                btnGuardar.Visible = true;


                ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "var modal = new bootstrap.Modal(document.getElementById('modalProducto')); modal.show();", true);
            }
        }
        protected void btnAbrirAgregar_Click(object sender, EventArgs e)
        {
            limpiarCamposAgregar();
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirAgregarModal", "var modal = new bootstrap.Modal(document.getElementById('modalAgregar')); modal.show();", true);
        }

        protected void rptImagenesTemporales_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EliminarTemporal")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var imagenes = ImagenesTemporales;

                if (index >= 0 && index < imagenes.Count)
                {
                    imagenes.RemoveAt(index);
                    ImagenesTemporales = imagenes;

                    rptImagenesTemporales.DataSource = ImagenesTemporales;
                    rptImagenesTemporales.DataBind();
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalAgregar", "var myModal = new bootstrap.Modal(document.getElementById('modalAgregar')); myModal.show();", true);
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(lblIdProducto.Text);
            string nombre = txtNombre.Text.Trim();
            decimal precio = 0;
            int stock = 0;
            int categoriaId = 0;
            int marcaId = 0;
            bool activo = Convert.ToBoolean(ddlActivo.SelectedValue);




            decimal.TryParse(txtPrecio.Text.Trim(), out precio);
            int.TryParse(txtStock.Text.Trim(), out stock);
            int.TryParse(ddlCategoria.SelectedValue, out categoriaId);
            int.TryParse(ddlMarca.SelectedValue, out marcaId);


            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtStock.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text)
                || txtNombre.Text.Length > 50 || Convert.ToDecimal(txtPrecio.Text) <= 0 || Convert.ToDecimal(txtPrecio.Text) > 9999999.99m || Convert.ToInt32(txtStock.Text) < 0 || Convert.ToInt32(txtStock.Text) > 9999)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalProducto", "var modal = new bootstrap.Modal(document.getElementById('modalProducto')); modal.show();", true);
                return;
            }

            Producto producto = new Producto
            {
                Id = idProducto,
                Nombre = nombre,
                Precio = precio,
                Stock = stock,
                Categoria = new Categoria { Id = categoriaId },
                Marca = new Marca { Id = marcaId },
                Activo = activo
            };

            managerProducto.modificar(producto);

            // Guardo las imágenes nuevas
            ImagenManager imagenManager = new ImagenManager();
            foreach (var url in ImagenesTemporalesEdicion)
            {
                ImagenesProducto img = new ImagenesProducto(idProducto, url);
                imagenManager.agregar(img);
            }
            ImagenesTemporalesEdicion.Clear();

            cargarProductos();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNuevoNombre.Text) || string.IsNullOrWhiteSpace(txtNuevoStock.Text) || string.IsNullOrWhiteSpace(txtNuevoPrecio.Text) || txtNuevoNombre.Text.Length > 50 || Convert.ToDecimal(txtNuevoPrecio.Text) <= 0 || Convert.ToDecimal(txtNuevoPrecio.Text) > 9999999.99m || Convert.ToInt32(txtNuevoStock.Text) < 0 || Convert.ToInt32(txtNuevoStock.Text) > 9999)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirAgregarModal", "var modal = new bootstrap.Modal(document.getElementById('modalAgregar')); modal.show();", true);
                return;
            }
            string nombre = txtNuevoNombre.Text.Trim();
            decimal precio = Convert.ToDecimal(txtNuevoPrecio.Text);
            int idCategoria = Convert.ToInt32(ddlNuevaCategoria.SelectedValue);
            int idMarca = Convert.ToInt32(ddlNuevaMarca.SelectedValue);
            bool activo = Convert.ToBoolean(ddlNuevoActivo.SelectedValue);
            int stock = Convert.ToInt32(txtNuevoStock.Text);


            if (!string.IsNullOrEmpty(nombre))
            {
                Producto producto = new Producto
                {
                    Nombre = nombre,
                    Precio = precio,
                    Stock = stock,
                    Categoria = new Categoria { Id = idCategoria },
                    Marca = new Marca { Id = idMarca },
                    Activo = activo
                };

                ProductoManager productoManager = new ProductoManager();
                int idNuevoProducto = productoManager.agregarYDevolverId(producto);

                // Agrego todas las imágenes
                ImagenManager imagenManager = new ImagenManager();
                foreach (var url in ImagenesTemporales)
                {
                    ImagenesProducto img = new ImagenesProducto(idNuevoProducto, url);
                    imagenManager.agregar(img);
                }

                // Limpio todo
                ImagenesTemporales.Clear();
                cargarProductos();
                limpiarCamposAgregar();

                ImagenesTemporales = new List<string>();
                rptImagenesTemporales.DataSource = null;
                rptImagenesTemporales.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirAgregarModal", "var modal = new bootstrap.Modal(document.getElementById('modalAgregar')); modal.show();", true);
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(lblIdEliminar.Text);
            managerProducto.eliminar(idProducto);
            cargarProductos();
        }

        private void cargarProductos()
        {
            var lista = managerProducto.listar();
            rptProductos.DataSource = lista;
            rptProductos.DataBind();
        }

        private void cargarCategoriasEnDropdown(DropDownList ddl)
        {
            var listaCategorias = managerCategoria.listar();
            ddl.DataSource = listaCategorias;
            ddl.DataTextField = "Descripcion";
            ddl.DataValueField = "Id";
            ddl.DataBind();
        }

        private void cargarMarcasEnDropdown(DropDownList ddl)
        {
            var listaMarcas = managerMarca.listar();
            ddl.DataSource = listaMarcas;
            ddl.DataTextField = "Descripcion";
            ddl.DataValueField = "Id";
            ddl.DataBind();
        }

        private void limpiarCamposAgregar()
        {
            txtNuevoNombre.Text = "";
            txtNuevoPrecio.Text = "";
            txtNuevoStock.Text = "";
            ddlNuevaCategoria.SelectedIndex = -1;
            ddlNuevaMarca.SelectedIndex = -1;
            ddlNuevoActivo.SelectedValue = "true";
        }
        private void alternarCamposEdicion(bool editar)
        {
            lblNombre.Visible = !editar;
            txtNombre.Visible = editar;

            lblPrecio.Visible = !editar;
            txtPrecio.Visible = editar;

            lblStock.Visible = !editar;
            txtStock.Visible = editar;

            lblCategoria.Visible = !editar;
            ddlCategoria.Visible = editar;

            lblMarca.Visible = !editar;
            ddlMarca.Visible = editar;

            lblActivo.Visible = !editar;
            ddlActivo.Visible = editar;
        }

        private List<string> ImagenesTemporales
        {
            get
            {
                if (Session["ImagenesTemporales"] == null)
                    Session["ImagenesTemporales"] = new List<string>();
                return (List<string>)Session["ImagenesTemporales"];
            }
            set
            {
                Session["ImagenesTemporales"] = value;
            }
        }
        protected void btnAgregarImagenTemporal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNuevaImagen.Text))
            {
                var imagenes = ImagenesTemporales;
                imagenes.Add(txtNuevaImagen.Text);
                ImagenesTemporales = imagenes;

                rptImagenesTemporales.DataSource = ImagenesTemporales;
                rptImagenesTemporales.DataBind();

                txtNuevaImagen.Text = "";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalAgregar", "var myModal = new bootstrap.Modal(document.getElementById('modalAgregar')); myModal.show();", true);
        }


        protected void btnAgregarImagenTemporalEdicion_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNuevaImagenEdicion.Text))
            {
                var imagenes = ImagenesTemporalesEdicion;
                imagenes.Add(txtNuevaImagenEdicion.Text);
                ImagenesTemporalesEdicion = imagenes;

                rptImagenesTemporalesEdicion.DataSource = ImagenesTemporalesEdicion;
                rptImagenesTemporalesEdicion.DataBind();

                txtNuevaImagenEdicion.Text = "";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalProducto", "var myModal = new bootstrap.Modal(document.getElementById('modalProducto')); myModal.show();", true);
        }

        protected void rptImagenesTemporalesEdicion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EliminarTemporalEdicion")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var imagenes = ImagenesTemporalesEdicion;

                if (index >= 0 && index < imagenes.Count)
                {
                    imagenes.RemoveAt(index);
                    ImagenesTemporalesEdicion = imagenes;

                    rptImagenesTemporalesEdicion.DataSource = ImagenesTemporalesEdicion;
                    rptImagenesTemporalesEdicion.DataBind();
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalProducto", "var myModal = new bootstrap.Modal(document.getElementById('modalProducto')); myModal.show();", true);
        }
    }
}
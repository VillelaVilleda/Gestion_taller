using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Repuestos" dentro de la ventana principal (menu lateral).
    /// Lista los repuestos y permite a cualquier usuario crear uno nuevo
    /// (nombre, existencia y precio). Solo el administrador puede editar y eliminar.
    /// </summary>
    public partial class RepuestosControl : UserControl
    {
        private readonly IRepuestoRepository _repuestoRepository;
        private readonly PermisosUsuario _permisos;
        private readonly CrearRepuestoService _crearRepuestoService;
        private readonly ModificarRepuestoService _modificarRepuestoService;
        private readonly EliminarRepuestoService _eliminarRepuestoService;

        public RepuestosControl(IRepuestoRepository repuestoRepository, IOrdenRepository ordenRepository, Empleado usuarioActual)
        {
            InitializeComponent();
            _repuestoRepository = repuestoRepository;
            _permisos = new PermisosUsuario(usuarioActual);
            _crearRepuestoService = new CrearRepuestoService(repuestoRepository);
            _modificarRepuestoService = new ModificarRepuestoService(repuestoRepository);
            _eliminarRepuestoService = new EliminarRepuestoService(repuestoRepository, ordenRepository);
            cmbRepuesto.SelectedIndexChanged += (_, _) => MostrarDatosDelSeleccionado();

            btnGuardarCambios.Visible = _permisos.PuedeEditar;
            btnEliminar.Visible = _permisos.PuedeEliminar;
            panelEdicion.Visible = _permisos.PuedeEditar || _permisos.PuedeEliminar;

            if (!panelEdicion.Visible)
            {
                var alto = panelEdicion.Height;
                lblMensaje.Top -= alto;
                tablaRepuestos.Top -= alto;
                tablaRepuestos.Height += alto;
            }

            Load += RepuestosControl_Load;
        }

        private void RepuestosControl_Load(object? sender, EventArgs e)
        {
            CargarRepuestos();
        }

        private void CargarRepuestos()
        {
            var columnas = new List<ColumnaTabla<Repuesto>>
            {
                new("Nombre", r => r.Nombre),
                new("Existencia", r => r.Existencia),
                new("Precio", r => r.Precio, r => $"Q {r.Precio:N2}")
            };

            var repuestos = _repuestoRepository.ObtenerTodos();
            tablaRepuestos.Cargar(columnas, repuestos, ordenInicial: columnas[0]);
            cmbRepuesto.CargarElementos(repuestos.OrderBy(r => r.Nombre).ToList(), r => r.Nombre);
            MostrarDatosDelSeleccionado();
        }

        private void MostrarDatosDelSeleccionado()
        {
            var repuesto = cmbRepuesto.ElementoSeleccionado as Repuesto;

            txtEditarNombre.Text = repuesto?.Nombre ?? string.Empty;
            numEditarExistencia.Value = repuesto?.Existencia ?? 0;
            numEditarPrecio.Value = repuesto?.Precio ?? 0;
        }

        private void btnGuardarCambios_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            if (cmbRepuesto.ElementoSeleccionado is not Repuesto repuesto)
            {
                lblMensaje.Text = "Selecciona un repuesto de la lista.";
                return;
            }

            try
            {
                _modificarRepuestoService.Ejecutar(_permisos, repuesto, txtEditarNombre.Text, (int)numEditarExistencia.Value, numEditarPrecio.Value);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Repuesto \"{repuesto.Nombre}\" actualizado.";

                CargarRepuestos();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            if (cmbRepuesto.ElementoSeleccionado is not Repuesto repuesto)
            {
                lblMensaje.Text = "Selecciona un repuesto de la lista.";
                return;
            }

            var confirmar = MessageBox.Show(
                $"Se eliminara el repuesto \"{repuesto.Nombre}\". Continuar?",
                "Eliminar repuesto",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _eliminarRepuestoService.Ejecutar(_permisos, repuesto);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Repuesto \"{repuesto.Nombre}\" eliminado.";

                CargarRepuestos();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnAgregarRepuesto_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                var repuesto = _crearRepuestoService.Ejecutar(txtNombre.Text, (int)numExistencia.Value, numPrecio.Value);

                txtNombre.Clear();
                numExistencia.Value = 0;
                numPrecio.Value = 0;
                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Repuesto \"{repuesto.Nombre}\" agregado.";

                CargarRepuestos();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}

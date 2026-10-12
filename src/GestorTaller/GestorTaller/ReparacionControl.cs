using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Formulario de la reparacion realizada. Mientras la orden esta en EnReparacion
    /// permite registrar empleado y detalle, y asignar repuestos (con descuento
    /// automatico del inventario, guardado en el momento de asignar para que el
    /// inventario nunca quede descontado a medias). Al guardar la reparacion, la
    /// orden pasa a Terminado. Una vez que la orden ya avanzo, todo se muestra en
    /// solo lectura.
    /// </summary>
    public partial class ReparacionControl : UserControl
    {
        private readonly RegistrarReparacionOrdenService _registrarReparacionOrdenService;
        private readonly AsignarRepuestoOrdenService _asignarRepuestoOrdenService = new();
        private readonly QuitarRepuestoOrdenService _quitarRepuestoOrdenService = new();
        private readonly IOrdenRepository _ordenRepository;
        private readonly IRepuestoRepository _repuestoRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private Orden _orden = null!;

        public event Action? ReparacionRegistrada;

        public ReparacionControl(IOrdenRepository ordenRepository, IEmpleadoRepository empleadoRepository, IRepuestoRepository repuestoRepository)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _empleadoRepository = empleadoRepository;
            _repuestoRepository = repuestoRepository;
            _registrarReparacionOrdenService = new RegistrarReparacionOrdenService(new AvanzarEstadoOrdenService());

            dgvRepuestosAsignados.Columns.Add("Repuesto", "Repuesto");
            dgvRepuestosAsignados.Columns.Add("Cantidad", "Cantidad");
            cmbRepuesto.SelectedIndexChanged += (_, _) => ActualizarDisponible();
            Load += (_, _) => BeginInvoke(() =>
            {
                if (!IsDisposed && cmbEmpleado.Enabled)
                {
                    cmbEmpleado.Focus();
                }
            });
        }

        public void Mostrar(Orden orden)
        {
            _orden = orden;
            lblMensaje.Text = "";

            var esPendiente = orden.Estado == EstadoOrden.EnReparacion;

            CargarEmpleados();
            cmbEmpleado.Enabled = esPendiente;
            txtDetalle.ReadOnly = !esPendiente;
            btnGuardarReparacion.Visible = esPendiente;

            cmbRepuesto.Enabled = esPendiente;
            numCantidad.Enabled = esPendiente;
            btnAgregarRepuesto.Visible = esPendiente;
            btnQuitarRepuesto.Visible = esPendiente;
            lblDisponible.Visible = esPendiente;

            cmbEmpleado.SeleccionarPorTexto(orden.EmpleadoReparacion);
            txtDetalle.Text = orden.DetalleReparacion;

            if (esPendiente)
            {
                CargarRepuestos();
            }

            MostrarAsignados();
        }

        private void CargarEmpleados()
        {
            var empleados = _empleadoRepository.ObtenerTodos().OrderBy(e => e.Nombre).ToList();
            cmbEmpleado.CargarElementos(empleados, e => e.Nombre);
        }

        private string NombreEmpleadoSeleccionado() => (cmbEmpleado.ElementoSeleccionado as Empleado)?.Nombre ?? string.Empty;

        private void CargarRepuestos()
        {
            var disponibles = _repuestoRepository.ObtenerTodos()
                .Where(r => r.Existencia > 0)
                .OrderBy(r => r.Nombre)
                .ToList();

            cmbRepuesto.CargarElementos(disponibles, r => r.Nombre);
            ActualizarDisponible();
        }

        private void ActualizarDisponible()
        {
            lblDisponible.Text = cmbRepuesto.ElementoSeleccionado is Repuesto repuesto
                ? $"Disponible: {repuesto.Existencia}"
                : "Disponible: -";
        }

        private void MostrarAsignados()
        {
            dgvRepuestosAsignados.Rows.Clear();

            foreach (var asignado in _orden.Repuestos)
            {
                var indice = dgvRepuestosAsignados.Rows.Add(asignado.Repuesto?.Nombre ?? "(sin nombre)", asignado.Cantidad);
                dgvRepuestosAsignados.Rows[indice].Tag = asignado.RepuestoId;
            }

            dgvRepuestosAsignados.ClearSelection();
        }

        private void btnQuitarRepuesto_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            if (dgvRepuestosAsignados.SelectedRows.Count == 0
                || dgvRepuestosAsignados.SelectedRows[0].Tag is not Guid repuestoId)
            {
                lblMensaje.Text = "Selecciona en la tabla el repuesto que quieres quitar.";
                return;
            }

            var asignado = _orden.Repuestos.FirstOrDefault(r => r.RepuestoId == repuestoId);

            if (asignado?.Repuesto is null)
            {
                lblMensaje.Text = "No se encontro el repuesto seleccionado.";
                return;
            }

            try
            {
                var nombre = asignado.Repuesto.Nombre;
                var cantidad = asignado.Cantidad;

                _quitarRepuestoOrdenService.Ejecutar(_orden, asignado.Repuesto, cantidad);
                _ordenRepository.Actualizar(_orden);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Se quito {nombre} (x {cantidad}) y se devolvio al inventario.";

                CargarRepuestos();
                MostrarAsignados();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnAgregarRepuesto_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            if (cmbRepuesto.ElementoSeleccionado is not Repuesto repuesto)
            {
                lblMensaje.Text = "Selecciona un repuesto de la lista.";
                return;
            }

            try
            {
                _asignarRepuestoOrdenService.Ejecutar(_orden, repuesto, (int)numCantidad.Value);
                _ordenRepository.Actualizar(_orden);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = "Repuesto asignado.";
                numCantidad.Value = 1;

                CargarRepuestos();
                MostrarAsignados();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnGuardarReparacion_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                _registrarReparacionOrdenService.Ejecutar(_orden, NombreEmpleadoSeleccionado(), txtDetalle.Text);
                ReparacionRegistrada?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}

using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Formulario del paso Cotizacion dentro del seguimiento. Mientras la
    /// orden esta en estado Cotizacion (el paso activo), permite capturarla,
    /// elegir los repuestos que se usaran (sin descontar el inventario todavia)
    /// y decidir si el cliente la acepto, lo que avanza la orden directo a
    /// Reparacion o a Terminado segun la decision. Al aceptar se descuenta el
    /// inventario. Si la orden ya avanzo mas alla, se muestra en modo solo lectura.
    /// </summary>
    public partial class CotizacionControl : UserControl
    {
        private readonly RegistrarCotizacionOrdenService _registrarCotizacionOrdenService;
        private readonly AsignarRepuestoOrdenService _asignarRepuestoOrdenService = new();
        private readonly QuitarRepuestoOrdenService _quitarRepuestoOrdenService = new();
        private readonly IOrdenRepository _ordenRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IRepuestoRepository _repuestoRepository;
        private Orden _orden = null!;

        public event Action? CotizacionRegistrada;

        public CotizacionControl(IOrdenRepository ordenRepository, IEmpleadoRepository empleadoRepository, IRepuestoRepository repuestoRepository)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _empleadoRepository = empleadoRepository;
            _repuestoRepository = repuestoRepository;
            _registrarCotizacionOrdenService = new RegistrarCotizacionOrdenService(new AvanzarEstadoOrdenService());

            dgvRepuestos.Columns.Add("Repuesto", "Repuesto");
            dgvRepuestos.Columns.Add("Cantidad", "Cant.");
            dgvRepuestos.Columns.Add("Precio", "Precio");
            dgvRepuestos.Columns.Add("Subtotal", "Subtotal");
            cmbRepuesto.SelectedIndexChanged += (_, _) => ActualizarDisponible();
            numMontoCotizado.ValueChanged += (_, _) => ActualizarTotal();

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

            var esPendiente = orden.Estado == EstadoOrden.Cotizacion;

            CargarEmpleados();
            cmbEmpleado.Enabled = esPendiente;
            numMontoCotizado.Enabled = esPendiente;
            rbAceptada.Enabled = esPendiente;
            rbRechazada.Enabled = esPendiente;
            btnGuardarCotizacion.Visible = esPendiente;

            cmbRepuesto.Enabled = esPendiente;
            numCantidad.Enabled = esPendiente;
            btnAgregarRepuesto.Visible = esPendiente;
            btnQuitarRepuesto.Visible = esPendiente;
            lblDisponible.Visible = esPendiente;

            cmbEmpleado.SeleccionarPorTexto(orden.EmpleadoCotizacion?.Nombre);
            numMontoCotizado.Value = orden.MontoCotizado;

            if (esPendiente || orden.CotizacionAceptada)
            {
                rbAceptada.Checked = true;
            }
            else
            {
                rbRechazada.Checked = true;
            }

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

        private Empleado? EmpleadoSeleccionado() => cmbEmpleado.ElementoSeleccionado as Empleado;

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
                ? $"Disponible: {repuesto.Existencia}  |  Precio: Q {repuesto.Precio:N2}"
                : "Disponible: -";
        }

        private void MostrarAsignados()
        {
            dgvRepuestos.Rows.Clear();

            foreach (var asignado in _orden.Repuestos)
            {
                var indice = dgvRepuestos.Rows.Add(
                    asignado.Repuesto?.Nombre ?? "(sin nombre)",
                    asignado.Cantidad,
                    $"Q {asignado.PrecioUnitario:N2}",
                    $"Q {asignado.PrecioUnitario * asignado.Cantidad:N2}");
                dgvRepuestos.Rows[indice].Tag = asignado.RepuestoId;
            }

            dgvRepuestos.ClearSelection();

            var subtotal = _orden.Repuestos.Sum(a => a.PrecioUnitario * a.Cantidad);
            lblSubtotalRepuestos.Text = $"Subtotal de repuestos: Q {subtotal:N2}";
            ActualizarTotal();
        }

        // Suma que se le informa al cliente: costo de mano de obra + repuestos cotizados.
        private void ActualizarTotal()
        {
            if (_orden is null)
            {
                return;
            }

            var repuestos = _orden.Repuestos.Sum(a => a.PrecioUnitario * a.Cantidad);
            lblTotalCotizacion.Text = $"Mano de obra + repuestos: Q {numMontoCotizado.Value + repuestos:N2}";
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
                lblMensaje.Text = "Repuesto agregado a la cotizacion.";
                numCantidad.Value = 1;

                MostrarAsignados();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnQuitarRepuesto_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            if (dgvRepuestos.SelectedRows.Count == 0
                || dgvRepuestos.SelectedRows[0].Tag is not Guid repuestoId)
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

                _quitarRepuestoOrdenService.Ejecutar(_orden, asignado.Repuesto, asignado.Cantidad);
                _ordenRepository.Actualizar(_orden);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Se quito {nombre} de la cotizacion.";

                MostrarAsignados();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnGuardarCotizacion_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                _registrarCotizacionOrdenService.Ejecutar(_orden, EmpleadoSeleccionado()!, numMontoCotizado.Value, rbAceptada.Checked);
                CotizacionRegistrada?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}

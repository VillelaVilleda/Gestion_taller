using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Formulario del paso Terminado dentro del seguimiento. Mientras la
    /// orden esta en estado Terminado (el paso activo), permite registrar
    /// quien entrego el objeto y cuanto pago el cliente, lo que avanza la
    /// orden a Entregado. Se muestra tambien bajo el chip Entregado, en
    /// modo solo lectura, como resumen de la entrega ya realizada.
    /// </summary>
    public partial class TerminadoControl : UserControl
    {
        private readonly RegistrarEntregaOrdenService _registrarEntregaOrdenService;
        private readonly IEmpleadoRepository _empleadoRepository;
        private Orden _orden = null!;

        public event Action? EntregaRegistrada;

        public TerminadoControl(IEmpleadoRepository empleadoRepository)
        {
            InitializeComponent();
            _empleadoRepository = empleadoRepository;
            _registrarEntregaOrdenService = new RegistrarEntregaOrdenService(new AvanzarEstadoOrdenService());
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

            var esPendiente = orden.Estado == EstadoOrden.Terminado;

            CargarEmpleados();
            cmbEmpleado.Enabled = esPendiente;
            btnConfirmarPago.Visible = esPendiente;

            cmbEmpleado.SeleccionarPorTexto(orden.EmpleadoEntrega?.Nombre);

            var totalAPagar = esPendiente ? CalcularMontoTotal(orden) : orden.MontoPagado;
            lblTotalAPagar.Text = $"Q {totalAPagar:N2}";

            lblFechaEntrega.Text = orden.FechaEntrega.HasValue
                ? $"Entregado el {orden.FechaEntrega:dd/MM/yyyy HH:mm}"
                : "";
        }

        private void CargarEmpleados()
        {
            var empleados = _empleadoRepository.ObtenerTodos().OrderBy(e => e.Nombre).ToList();
            cmbEmpleado.CargarElementos(empleados, e => e.Nombre);
        }

        private Empleado? EmpleadoSeleccionado() => cmbEmpleado.ElementoSeleccionado as Empleado;

        private static decimal CalcularMontoTotal(Orden orden)
        {
            var total = orden.CostoDiagnostico;

            if (orden.HistorialEstados.Contains(EstadoOrden.EnReparacion))
            {
                total += orden.MontoCotizado;
            }

            return total;
        }

        private void btnConfirmarPago_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                _registrarEntregaOrdenService.Ejecutar(_orden, EmpleadoSeleccionado()!, CalcularMontoTotal(_orden));
                EntregaRegistrada?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}

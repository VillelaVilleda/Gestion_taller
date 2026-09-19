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
        private Orden _orden = null!;

        public event Action? EntregaRegistrada;

        public TerminadoControl()
        {
            InitializeComponent();
            _registrarEntregaOrdenService = new RegistrarEntregaOrdenService(new AvanzarEstadoOrdenService());
        }

        public void Mostrar(Orden orden)
        {
            _orden = orden;
            lblMensaje.Text = "";

            var esPendiente = orden.Estado == EstadoOrden.Terminado;

            txtEmpleado.ReadOnly = !esPendiente;
            btnConfirmarPago.Visible = esPendiente;

            txtEmpleado.Text = orden.EmpleadoEntrega;

            var totalAPagar = esPendiente ? CalcularMontoTotal(orden) : orden.MontoPagado;
            lblTotalAPagar.Text = $"Q {totalAPagar:N2}";

            lblFechaEntrega.Text = orden.FechaEntrega.HasValue
                ? $"Entregado el {orden.FechaEntrega:dd/MM/yyyy HH:mm}"
                : "";
        }

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
                _registrarEntregaOrdenService.Ejecutar(_orden, txtEmpleado.Text, CalcularMontoTotal(_orden));
                EntregaRegistrada?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}

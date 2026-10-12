using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Formulario del paso Cotizacion dentro del seguimiento. Mientras la
    /// orden esta en estado Cotizacion (el paso activo), permite capturarla
    /// y decidir si el cliente la acepto, lo que avanza la orden directo a
    /// Reparacion o a Terminado segun la decision. Si la orden ya avanzo mas
    /// alla, se muestra en modo solo lectura.
    /// </summary>
    public partial class CotizacionControl : UserControl
    {
        private readonly RegistrarCotizacionOrdenService _registrarCotizacionOrdenService;
        private readonly IEmpleadoRepository _empleadoRepository;
        private Orden _orden = null!;

        public event Action? CotizacionRegistrada;

        public CotizacionControl(IEmpleadoRepository empleadoRepository)
        {
            InitializeComponent();
            _empleadoRepository = empleadoRepository;
            _registrarCotizacionOrdenService = new RegistrarCotizacionOrdenService(new AvanzarEstadoOrdenService());
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

            cmbEmpleado.SeleccionarPorTexto(orden.EmpleadoCotizacion);
            numMontoCotizado.Value = orden.MontoCotizado;

            if (esPendiente || orden.CotizacionAceptada)
            {
                rbAceptada.Checked = true;
            }
            else
            {
                rbRechazada.Checked = true;
            }
        }

        private void CargarEmpleados()
        {
            var empleados = _empleadoRepository.ObtenerTodos().OrderBy(e => e.Nombre).ToList();
            cmbEmpleado.CargarElementos(empleados, e => e.Nombre);
        }

        private string NombreEmpleadoSeleccionado() => (cmbEmpleado.ElementoSeleccionado as Empleado)?.Nombre ?? string.Empty;

        private void btnGuardarCotizacion_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                _registrarCotizacionOrdenService.Ejecutar(_orden, NombreEmpleadoSeleccionado(), numMontoCotizado.Value, rbAceptada.Checked);
                CotizacionRegistrada?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}
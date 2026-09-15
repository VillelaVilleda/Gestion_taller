using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Formulario del paso Cotizacion dentro del seguimiento. Si la orden
    /// todavia esta en Diagnostico (la cotizacion esta pendiente), permite
    /// capturarla y decidir si el cliente la acepto, lo que avanza la orden
    /// directo a Reparacion o a Terminado segun la decision. Si la orden ya
    /// avanzo mas alla, se muestra en modo solo lectura.
    /// </summary>
    public partial class CotizacionControl : UserControl
    {
        private readonly RegistrarCotizacionOrdenService _registrarCotizacionOrdenService;
        private Orden _orden = null!;

        public event Action? CotizacionRegistrada;

        public CotizacionControl()
        {
            InitializeComponent();
            _registrarCotizacionOrdenService = new RegistrarCotizacionOrdenService(new AvanzarEstadoOrdenService());
        }

        public void Mostrar(Orden orden)
        {
            _orden = orden;
            lblMensaje.Text = "";

            var esPendiente = orden.Estado == EstadoOrden.Diagnostico;

            txtEmpleado.ReadOnly = !esPendiente;
            numMontoCotizado.Enabled = esPendiente;
            rbAceptada.Enabled = esPendiente;
            rbRechazada.Enabled = esPendiente;
            btnGuardarCotizacion.Visible = esPendiente;

            txtEmpleado.Text = orden.EmpleadoCotizacion;
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

        private void btnGuardarCotizacion_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                _registrarCotizacionOrdenService.Ejecutar(_orden, txtEmpleado.Text, numMontoCotizado.Value, rbAceptada.Checked);
                CotizacionRegistrada?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}
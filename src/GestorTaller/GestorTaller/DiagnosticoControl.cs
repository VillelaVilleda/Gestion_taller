using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Formulario del paso Diagnostico dentro del seguimiento. Mientras la
    /// orden esta en estado Diagnostico (el paso activo), permite capturarlo
    /// y avanza la orden a Cotizacion al guardar. Si la orden ya avanzo mas
    /// alla (se esta revisando este paso hacia atras), se muestra en modo
    /// solo lectura con los datos ya guardados.
    /// </summary>
    public partial class DiagnosticoControl : UserControl
    {
        private readonly RegistrarDiagnosticoOrdenService _registrarDiagnosticoOrdenService;
        private Orden _orden = null!;

        public event Action? DiagnosticoRegistrado;

        public DiagnosticoControl()
        {
            InitializeComponent();
            _registrarDiagnosticoOrdenService = new RegistrarDiagnosticoOrdenService(new AvanzarEstadoOrdenService());
        }

        public void Mostrar(Orden orden)
        {
            _orden = orden;
            lblMensaje.Text = "";

            var esPendiente = orden.Estado == EstadoOrden.Diagnostico;

            txtEmpleado.ReadOnly = !esPendiente;
            txtDetalle.ReadOnly = !esPendiente;
            btnGuardarDiagnostico.Visible = esPendiente;

            txtEmpleado.Text = orden.EmpleadoDiagnostico;
            txtDetalle.Text = orden.DetalleDiagnostico;
        }

        private void btnGuardarDiagnostico_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                _registrarDiagnosticoOrdenService.Ejecutar(_orden, txtEmpleado.Text, txtDetalle.Text);
                DiagnosticoRegistrado?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}
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
        private readonly IEmpleadoRepository _empleadoRepository;
        private Orden _orden = null!;

        public event Action? DiagnosticoRegistrado;

        public DiagnosticoControl(IEmpleadoRepository empleadoRepository)
        {
            InitializeComponent();
            _empleadoRepository = empleadoRepository;
            _registrarDiagnosticoOrdenService = new RegistrarDiagnosticoOrdenService(new AvanzarEstadoOrdenService());
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

            var esPendiente = orden.Estado == EstadoOrden.Diagnostico;

            CargarEmpleados();
            cmbEmpleado.Enabled = esPendiente;
            txtDetalle.ReadOnly = !esPendiente;
            btnGuardarDiagnostico.Visible = esPendiente;

            cmbEmpleado.SeleccionarPorTexto(orden.EmpleadoDiagnostico?.Nombre);
            txtDetalle.Text = orden.DetalleDiagnostico;
        }

        private void CargarEmpleados()
        {
            var empleados = _empleadoRepository.ObtenerTodos().OrderBy(e => e.Nombre).ToList();
            cmbEmpleado.CargarElementos(empleados, e => e.Nombre);
        }

        private Empleado? EmpleadoSeleccionado() => cmbEmpleado.ElementoSeleccionado as Empleado;

        private void btnGuardarDiagnostico_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                _registrarDiagnosticoOrdenService.Ejecutar(_orden, EmpleadoSeleccionado()!, txtDetalle.Text);
                DiagnosticoRegistrado?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}
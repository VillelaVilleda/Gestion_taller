using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Formulario de la reparacion realizada. Mientras la orden esta en EnReparacion
    /// permite registrar empleado y detalle; al guardar, la orden pasa a Terminado.
    /// Los repuestos se eligieron en la Cotizacion y aqui solo se muestran (solo
    /// lectura), de modo que el total nunca difiere de lo que el cliente acepto.
    /// Una vez que la orden ya avanzo, todo se muestra en solo lectura.
    /// </summary>
    public partial class ReparacionControl : UserControl
    {
        private readonly RegistrarReparacionOrdenService _registrarReparacionOrdenService;
        private readonly IEmpleadoRepository _empleadoRepository;
        private Orden _orden = null!;

        public event Action? ReparacionRegistrada;

        public ReparacionControl(IEmpleadoRepository empleadoRepository)
        {
            InitializeComponent();
            _empleadoRepository = empleadoRepository;
            _registrarReparacionOrdenService = new RegistrarReparacionOrdenService(new AvanzarEstadoOrdenService());

            dgvRepuestosAsignados.Columns.Add("Repuesto", "Repuesto");
            dgvRepuestosAsignados.Columns.Add("Cantidad", "Cantidad");
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

            cmbEmpleado.SeleccionarPorTexto(orden.EmpleadoReparacion);
            txtDetalle.Text = orden.DetalleReparacion;

            MostrarAsignados();
        }

        private void CargarEmpleados()
        {
            var empleados = _empleadoRepository.ObtenerTodos().OrderBy(e => e.Nombre).ToList();
            cmbEmpleado.CargarElementos(empleados, e => e.Nombre);
        }

        private string NombreEmpleadoSeleccionado() => (cmbEmpleado.ElementoSeleccionado as Empleado)?.Nombre ?? string.Empty;

        private void MostrarAsignados()
        {
            dgvRepuestosAsignados.Rows.Clear();

            foreach (var asignado in _orden.Repuestos)
            {
                dgvRepuestosAsignados.Rows.Add(asignado.Repuesto?.Nombre ?? "(sin nombre)", asignado.Cantidad);
            }

            dgvRepuestosAsignados.ClearSelection();
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

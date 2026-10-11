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
        private readonly IOrdenRepository _ordenRepository;
        private readonly IRepuestoRepository _repuestoRepository;
        private Orden _orden = null!;

        public event Action? ReparacionRegistrada;

        public ReparacionControl(IOrdenRepository ordenRepository, IRepuestoRepository repuestoRepository)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _repuestoRepository = repuestoRepository;
            _registrarReparacionOrdenService = new RegistrarReparacionOrdenService(new AvanzarEstadoOrdenService());

            dgvRepuestosAsignados.Columns.Add("Repuesto", "Repuesto");
            dgvRepuestosAsignados.Columns.Add("Cantidad", "Cantidad");
            cmbRepuesto.SelectedIndexChanged += (_, _) => ActualizarDisponible();
        }

        public void Mostrar(Orden orden)
        {
            _orden = orden;
            lblMensaje.Text = "";

            var esPendiente = orden.Estado == EstadoOrden.EnReparacion;

            txtEmpleado.ReadOnly = !esPendiente;
            txtDetalle.ReadOnly = !esPendiente;
            btnGuardarReparacion.Visible = esPendiente;

            cmbRepuesto.Enabled = esPendiente;
            numCantidad.Enabled = esPendiente;
            btnAgregarRepuesto.Visible = esPendiente;
            lblDisponible.Visible = esPendiente;

            txtEmpleado.Text = orden.EmpleadoReparacion;
            txtDetalle.Text = orden.DetalleReparacion;

            if (esPendiente)
            {
                CargarRepuestos();
            }

            MostrarAsignados();
        }

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
                dgvRepuestosAsignados.Rows.Add(asignado.Repuesto?.Nombre ?? "(sin nombre)", asignado.Cantidad);
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
                _registrarReparacionOrdenService.Ejecutar(_orden, txtEmpleado.Text, txtDetalle.Text);
                ReparacionRegistrada?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}

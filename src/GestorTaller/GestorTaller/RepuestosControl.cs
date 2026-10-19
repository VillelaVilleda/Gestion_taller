using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Repuestos" dentro de la ventana principal (menu lateral).
    /// Lista los repuestos y permite a cualquier usuario crear uno nuevo
    /// (nombre, existencia y precio). Editar y eliminar quedan para una issue futura.
    /// </summary>
    public partial class RepuestosControl : UserControl
    {
        private readonly IRepuestoRepository _repuestoRepository;
        private readonly CrearRepuestoService _crearRepuestoService;

        public RepuestosControl(IRepuestoRepository repuestoRepository)
        {
            InitializeComponent();
            _repuestoRepository = repuestoRepository;
            _crearRepuestoService = new CrearRepuestoService(repuestoRepository);
            Load += RepuestosControl_Load;
        }

        private void RepuestosControl_Load(object? sender, EventArgs e)
        {
            CargarRepuestos();
        }

        private void CargarRepuestos()
        {
            var columnas = new List<ColumnaTabla<Repuesto>>
            {
                new("Nombre", r => r.Nombre),
                new("Existencia", r => r.Existencia),
                new("Precio", r => r.Precio, r => $"Q {r.Precio:N2}")
            };

            tablaRepuestos.Cargar(columnas, _repuestoRepository.ObtenerTodos(), ordenInicial: columnas[0]);
        }

        private void btnAgregarRepuesto_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                var repuesto = _crearRepuestoService.Ejecutar(txtNombre.Text, (int)numExistencia.Value, numPrecio.Value);

                txtNombre.Clear();
                numExistencia.Value = 0;
                numPrecio.Value = 0;
                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Repuesto \"{repuesto.Nombre}\" agregado.";

                CargarRepuestos();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}

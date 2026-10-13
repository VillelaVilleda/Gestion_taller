using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Repuestos" dentro de la ventana principal (menu lateral).
    /// Solo lectura por ahora: crear/editar/eliminar repuestos, y la
    /// asignacion a ordenes con descuento de inventario, quedan para una
    /// issue futura.
    /// </summary>
    public partial class RepuestosControl : UserControl
    {
        private readonly IRepuestoRepository _repuestoRepository;

        public RepuestosControl(IRepuestoRepository repuestoRepository)
        {
            InitializeComponent();
            _repuestoRepository = repuestoRepository;
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
    }
}

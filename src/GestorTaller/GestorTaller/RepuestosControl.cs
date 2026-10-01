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
            dgvRepuestos.Columns.Clear();
            dgvRepuestos.Columns.Add("Nombre", "Nombre");
            dgvRepuestos.Columns.Add("Existencia", "Existencia");

            dgvRepuestos.Rows.Clear();

            foreach (var repuesto in _repuestoRepository.ObtenerTodos())
            {
                dgvRepuestos.Rows.Add(repuesto.Nombre, repuesto.Existencia);
            }
        }
    }
}

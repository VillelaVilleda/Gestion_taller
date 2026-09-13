using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Ordenes registradas" dentro de la ventana principal (menu lateral).
    /// Es el mismo contenido que antes vivia en FormListadoOrdenes, movido a un
    /// UserControl para poder mostrarse dentro del panel de contenido.
    /// </summary>
    public partial class ListadoOrdenesControl : UserControl
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly Action<Guid> _abrirSeguimiento;

        public ListadoOrdenesControl(IOrdenRepository ordenRepository, Action<Guid> abrirSeguimiento)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _abrirSeguimiento = abrirSeguimiento;
            Load += ListadoOrdenesControl_Load;
        }

        private void dgvOrdenes_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var ordenId = (Guid)dgvOrdenes.Rows[e.RowIndex].Tag;
            _abrirSeguimiento(ordenId);
        }

        private void ListadoOrdenesControl_Load(object? sender, EventArgs e)
        {
            CargarOrdenes();
        }

        private void CargarOrdenes()
        {
            dgvOrdenes.Columns.Clear();
            dgvOrdenes.Columns.Add("Cliente", "Cliente");
            dgvOrdenes.Columns.Add("Descripcion", "Descripcion del problema");
            dgvOrdenes.Columns.Add("Estado", "Estado");
            dgvOrdenes.Columns.Add("Fecha", "Fecha de recepcion");

            dgvOrdenes.Rows.Clear();

            foreach (var orden in _ordenRepository.ObtenerTodas())
            {
                var indiceFila = dgvOrdenes.Rows.Add(
                    orden.Cliente.Nombre,
                    orden.DescripcionProblema,
                    orden.Estado,
                    orden.FechaRecepcion);

                dgvOrdenes.Rows[indiceFila].Tag = orden.Id;
            }
        }
    }
}

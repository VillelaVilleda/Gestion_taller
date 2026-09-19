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
            dgvOrdenes.Columns.Add("Codigo", "Codigo");
            dgvOrdenes.Columns.Add("Cliente", "Cliente");
            dgvOrdenes.Columns.Add("Estado", "Estado");
            dgvOrdenes.Columns.Add("Fecha", "Fecha de modificacion");

            dgvOrdenes.Rows.Clear();

            // El codigo es solo la posicion de la orden en la lista (1, 2, 3...).
            // Como OrdenRepositoryEnMemoria se reinicia vacio en cada ejecucion
            // de la app, este contador tambien arranca de nuevo cada vez.
            var codigo = 1;
            foreach (var orden in _ordenRepository.ObtenerTodas())
            {
                var indiceFila = dgvOrdenes.Rows.Add(
                    codigo,
                    orden.Cliente.Nombre,
                    orden.Estado,
                    orden.FechaRecepcion);

                dgvOrdenes.Rows[indiceFila].Tag = orden.Id;
                codigo++;
            }

            // TODO: cuando cada estado guarde su propia fecha (sprint futuro),
            // la columna "Fecha de modificacion" debe mostrar esa fecha en vez
            // de FechaRecepcion, que es fija desde la creacion de la orden.
        }
    }
}

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

        public ListadoOrdenesControl(IOrdenRepository ordenRepository)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            Load += ListadoOrdenesControl_Load;
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
                dgvOrdenes.Rows.Add(
                    orden.Cliente.Nombre,
                    orden.DescripcionProblema,
                    orden.Estado,
                    orden.FechaRecepcion);
            }
        }
    }
}

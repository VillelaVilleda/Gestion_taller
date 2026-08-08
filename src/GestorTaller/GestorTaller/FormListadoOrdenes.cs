using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Pantalla de listado de las ordenes registradas (issue #14).
    /// </summary>
    public partial class FormListadoOrdenes : Form
    {
        private readonly IOrdenRepository _ordenRepository;

        public FormListadoOrdenes(IOrdenRepository ordenRepository)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            Load += FormListadoOrdenes_Load;
        }

        private void FormListadoOrdenes_Load(object? sender, EventArgs e)
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

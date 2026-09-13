using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de seguimiento de una orden especifica. Por ahora solo muestra
    /// datos basicos; el wizard con los pasos del ciclo de vida se construye
    /// en las issues siguientes.
    /// </summary>
    public partial class SeguimientoOrdenControl : UserControl
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly Guid _ordenId;

        public SeguimientoOrdenControl(IOrdenRepository ordenRepository, Guid ordenId)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _ordenId = ordenId;
            Load += SeguimientoOrdenControl_Load;
        }

        private void SeguimientoOrdenControl_Load(object? sender, EventArgs e)
        {
            var orden = _ordenRepository.ObtenerPorId(_ordenId);

            lblInfo.Text = orden is null
                ? "No se encontro la orden."
                : $"Seguimiento de la orden de {orden.Cliente.Nombre}\nEstado actual: {orden.Estado}";
        }

        private void lblInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de seguimiento de una orden: combina la miga de pan
    /// (SeguimientoBreadcrumbControl) con el formulario del paso que se
    /// este viendo. Por ahora solo Recepcion tiene formulario real; el
    /// resto de los pasos se agregan en issues posteriores.
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
            breadcrumb.PasoSeleccionado += MostrarPaso;
            Load += SeguimientoOrdenControl_Load;
        }

        private void SeguimientoOrdenControl_Load(object? sender, EventArgs e)
        {
            var orden = _ordenRepository.ObtenerPorId(_ordenId);

            if (orden is null)
            {
                MostrarFormulario(new Label
                {
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "No se encontro la orden."
                });
                return;
            }

            breadcrumb.Mostrar(orden.HistorialEstados);
            MostrarPaso(orden.HistorialEstados[^1]);
        }

        private void MostrarPaso(EstadoOrden paso)
        {
            var orden = _ordenRepository.ObtenerPorId(_ordenId);
            if (orden is null)
            {
                return;
            }

            Control formulario = paso switch
            {
                EstadoOrden.Recepcion => CrearFormularioRecepcion(orden),
                _ => new Label
                {
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 12F),
                    Text = $"Formulario de {paso} - Proximamente"
                }
            };

            MostrarFormulario(formulario);
        }

        private static Control CrearFormularioRecepcion(Orden orden)
        {
            var control = new RecepcionSoloLecturaControl();
            control.Mostrar(orden);
            return control;
        }

        private void MostrarFormulario(Control control)
        {
            panelFormulario.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelFormulario.Controls.Add(control);
        }
    }
}
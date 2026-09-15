using GestorTaller.Negocio;

namespace GestorTaller
{
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
            ActualizarBreadcrumbYMostrarPasoPendiente();
        }

        private void ActualizarBreadcrumbYMostrarPasoPendiente()
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
            MostrarPaso(PasoPendienteOActual(orden));
        }

        private static EstadoOrden PasoPendienteOActual(Orden orden)
        {
            var indiceActual = Array.IndexOf(SeguimientoBreadcrumbControl.TodosLosPasos, orden.Estado);
            var hayPendiente = indiceActual >= 0 && indiceActual < SeguimientoBreadcrumbControl.TodosLosPasos.Length - 1;
            return hayPendiente ? SeguimientoBreadcrumbControl.TodosLosPasos[indiceActual + 1] : orden.Estado;
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
                EstadoOrden.Diagnostico => CrearFormularioDiagnostico(orden),
                EstadoOrden.Cotizacion => CrearFormularioCotizacion(orden),
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

        private Control CrearFormularioCotizacion(Orden orden)
        {
            var control = new CotizacionControl();
            control.Mostrar(orden);
            control.CotizacionRegistrada += ActualizarBreadcrumbYMostrarPasoPendiente;
            return control;
        }

        private Control CrearFormularioDiagnostico(Orden orden)
        {
            var control = new DiagnosticoControl();
            control.Mostrar(orden);
            control.DiagnosticoRegistrado += ActualizarBreadcrumbYMostrarPasoPendiente;
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
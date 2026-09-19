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
            var orden = _ordenRepository.ObtenerPorId(_ordenId);

            // Primera vez que se abre el seguimiento de una orden recien
            // recibida: la avanzamos a Diagnostico, que pasa a ser su paso
            // activo (Orden.Estado representa el paso activo, no el ultimo
            // ya completado).
            if (orden is not null && orden.Estado == EstadoOrden.Recepcion)
            {
                new AvanzarEstadoOrdenService().Ejecutar(orden);
            }

            ActualizarBreadcrumbYMostrarPasoActual();
        }

        private void ActualizarBreadcrumbYMostrarPasoActual()
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
            MostrarPaso(orden.Estado);
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
                EstadoOrden.EnReparacion => CrearFormularioReparacion(orden),
                EstadoOrden.Terminado => CrearFormularioTerminado(orden),
                EstadoOrden.Entregado => CrearFormularioTerminado(orden),
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
            control.CotizacionRegistrada += ActualizarBreadcrumbYMostrarPasoActual;
            return control;
        }

        private Control CrearFormularioDiagnostico(Orden orden)
        {
            var control = new DiagnosticoControl();
            control.Mostrar(orden);
            control.DiagnosticoRegistrado += ActualizarBreadcrumbYMostrarPasoActual;
            return control;
        }

        private Control CrearFormularioReparacion(Orden orden)
        {
            var control = new ReparacionControl();
            control.Mostrar(orden);
            control.ReparacionRegistrada += ActualizarBreadcrumbYMostrarPasoActual;
            return control;
        }

        private Control CrearFormularioTerminado(Orden orden)
        {
            var control = new TerminadoControl();
            control.Mostrar(orden);
            control.EntregaRegistrada += ActualizarBreadcrumbYMostrarPasoActual;
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
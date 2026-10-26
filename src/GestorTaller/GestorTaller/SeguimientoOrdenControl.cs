using GestorTaller.Negocio;

namespace GestorTaller
{
    public partial class SeguimientoOrdenControl : UserControl
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IRepuestoRepository _repuestoRepository;
        private readonly PermisosUsuario _permisos;
        private readonly Guid _ordenId;

        public SeguimientoOrdenControl(IOrdenRepository ordenRepository, IEmpleadoRepository empleadoRepository, IRepuestoRepository repuestoRepository, Empleado usuarioActual, Guid ordenId)
        {
            InitializeComponent();
            _permisos = new PermisosUsuario(usuarioActual);
            _ordenRepository = ordenRepository;
            _empleadoRepository = empleadoRepository;
            _repuestoRepository = repuestoRepository;
            _ordenId = ordenId;
            breadcrumb.PasoSeleccionado += MostrarPaso;
            Load += SeguimientoOrdenControl_Load;
        }

        private void SeguimientoOrdenControl_Load(object? sender, EventArgs e)
        {
            var orden = _ordenRepository.ObtenerPorId(_ordenId);

            if (orden is not null && orden.Estado == EstadoOrden.Recepcion)
            {
                new AvanzarEstadoOrdenService().Ejecutar(orden);
                _ordenRepository.Actualizar(orden);
            }

            ActualizarBreadcrumbYMostrarPasoActual();
        }

        private void GuardarYActualizar()
        {
            var orden = _ordenRepository.ObtenerPorId(_ordenId);

            if (orden is not null)
            {
                _ordenRepository.Actualizar(orden);
            }

            ActualizarBreadcrumbYMostrarPasoActual();
        }

        private void ActualizarBreadcrumbYMostrarPasoActual()
        {
            var orden = _ordenRepository.ObtenerPorId(_ordenId);

            if (orden is null)
            {
                btnEditarOrden.Visible = false;
                MostrarFormulario(new Label
                {
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "No se encontro la orden."
                });
                return;
            }

            btnEditarOrden.Visible = _permisos.PuedeEditar && orden.Estado != EstadoOrden.Entregado;
            lblEncabezado.Text = $"Cliente: {orden.Cliente.Nombre}  |  {orden.DescripcionObjeto}";
            breadcrumb.Mostrar(orden.HistorialEstados);
            MostrarPaso(orden.Estado);
        }

        private void btnEditarOrden_Click(object? sender, EventArgs e)
        {
            var orden = _ordenRepository.ObtenerPorId(_ordenId);

            if (orden is null)
            {
                return;
            }

            using var dialogo = new FormEditarOrden(_ordenRepository, _permisos, orden);

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                ActualizarBreadcrumbYMostrarPasoActual();
            }
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
                EstadoOrden.Entregado => CrearFormularioEntregaResumen(orden),
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
            var control = new CotizacionControl(_ordenRepository, _empleadoRepository, _repuestoRepository);
            control.Mostrar(orden);
            control.CotizacionRegistrada += GuardarYActualizar;
            return control;
        }

        private Control CrearFormularioDiagnostico(Orden orden)
        {
            var control = new DiagnosticoControl(_empleadoRepository);
            control.Mostrar(orden);
            control.DiagnosticoRegistrado += GuardarYActualizar;
            return control;
        }

        private Control CrearFormularioReparacion(Orden orden)
        {
            var control = new ReparacionControl(_empleadoRepository);
            control.Mostrar(orden);
            control.ReparacionRegistrada += GuardarYActualizar;
            return control;
        }

        private Control CrearFormularioTerminado(Orden orden)
        {
            var control = new TerminadoControl(_empleadoRepository);
            control.Mostrar(orden);
            control.EntregaRegistrada += GuardarYActualizar;
            return control;
        }

        private static Control CrearFormularioEntregaResumen(Orden orden)
        {
            var control = new EntregaResumenControl();
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
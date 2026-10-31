using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana para que el administrador corrija el empleado encargado y el detalle del diagnóstico o de
    /// la reparación de una orden. Si no se cambia nada, no se guarda: <see cref="HuboCambios"/> indica
    /// si hubo algo que guardar.
    /// </summary>
    public class FormEditarEtapa : FormDialogo
    {
        private readonly ModificarEtapaOrdenService _servicio;
        private readonly PermisosUsuario _permisos;
        private readonly Orden _orden;
        private readonly EstadoOrden _etapa;
        private readonly ComboBoxFiltrable _cmbEmpleado = new();
        private readonly TextBox _txtDetalle = new() { MaxLength = 500, Multiline = true, Height = 100 };

        public FormEditarEtapa(IOrdenRepository ordenRepository, IEmpleadoRepository empleadoRepository, PermisosUsuario permisos, Orden orden, EstadoOrden etapa)
            : base(etapa == EstadoOrden.Diagnostico ? "Editar diagnóstico" : "Editar reparación")
        {
            _servicio = new ModificarEtapaOrdenService(ordenRepository);
            _permisos = permisos;
            _orden = orden;
            _etapa = etapa;

            var esDiagnostico = etapa == EstadoOrden.Diagnostico;

            AgregarCampo("Empleado encargado", _cmbEmpleado);
            AgregarCampo(esDiagnostico ? "Detalle del diagnóstico" : "Detalle de la reparación", _txtDetalle);

            _cmbEmpleado.CargarElementos(empleadoRepository.ObtenerTodos().OrderBy(e => e.Nombre).ToList(), e => e.Nombre);

            var encargado = esDiagnostico ? orden.EmpleadoDiagnostico : orden.EmpleadoReparacion;
            if (encargado is not null)
            {
                _cmbEmpleado.Seleccionar<Empleado>(e => e.Id == encargado.Id);
            }

            _txtDetalle.Text = esDiagnostico ? orden.DetalleDiagnostico : orden.DetalleReparacion;
        }

        /// <summary>True si al guardar cambió algún dato; false si todo quedó igual.</summary>
        public bool HuboCambios { get; private set; }

        protected override bool AlGuardar()
        {
            if (_cmbEmpleado.ElementoSeleccionado is not Empleado empleado)
            {
                throw new ArgumentException("Selecciona un empleado de la lista.");
            }

            HuboCambios = _servicio.Ejecutar(_permisos, _orden, _etapa, empleado, _txtDetalle.Text);

            return true;
        }
    }
}

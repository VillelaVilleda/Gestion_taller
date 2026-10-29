using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana para registrar un repuesto nuevo (nombre, existencia inicial y precio) o modificar uno
    /// existente (nombre, existencia y precio; solo el administrador puede modificar).
    /// Al cerrar con OK, <see cref="Resultado"/> tiene el repuesto guardado.
    /// </summary>
    public class FormRepuesto : FormDialogo
    {
        private readonly CrearRepuestoService _crearRepuestoService;
        private readonly ModificarRepuestoService _modificarRepuestoService;
        private readonly PermisosUsuario _permisos;
        private readonly Repuesto? _repuesto;
        private readonly TextBox _txtNombre = new() { MaxLength = CrearRepuestoService.LongitudMaximaNombre };
        private readonly NumericUpDown _numExistencia = new() { Maximum = AjustarExistenciaService.LimiteExistencia };
        private readonly NumericUpDown _numPrecio = new() { DecimalPlaces = 2, Maximum = 1_000_000, ThousandsSeparator = true };

        /// <summary>Si es null, la ventana registra un repuesto nuevo; si no, modifica ese repuesto.</summary>
        public FormRepuesto(IRepuestoRepository repuestoRepository, PermisosUsuario permisos, Repuesto? repuesto = null)
            : base(repuesto is null ? "Nuevo repuesto" : "Modificar repuesto")
        {
            _crearRepuestoService = new CrearRepuestoService(repuestoRepository);
            _modificarRepuestoService = new ModificarRepuestoService(repuestoRepository);
            _permisos = permisos;
            _repuesto = repuesto;

            AgregarCampo("Nombre", _txtNombre);

            AgregarCampo(repuesto is null ? "Existencia inicial" : "Existencia", _numExistencia);
            AgregarCampo("Precio (Q)", _numPrecio);

            if (repuesto is not null)
            {
                _txtNombre.Text = repuesto.Nombre;
                _numExistencia.Value = repuesto.Existencia;
                _numPrecio.Value = repuesto.Precio;
            }
        }

        /// <summary>El repuesto registrado o modificado; es null mientras no se haya guardado.</summary>
        public Repuesto? Resultado { get; private set; }

        protected override bool AlGuardar()
        {
            if (_repuesto is null)
            {
                Resultado = _crearRepuestoService.Ejecutar(_txtNombre.Text, (int)_numExistencia.Value, _numPrecio.Value);
            }
            else
            {
                _modificarRepuestoService.Ejecutar(_permisos, _repuesto, _txtNombre.Text, (int)_numExistencia.Value, _numPrecio.Value);
                Resultado = _repuesto;
            }

            return true;
        }
    }
}

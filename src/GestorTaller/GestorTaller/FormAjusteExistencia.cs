using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana para sumar (incrementar) o restar (decrementar) existencia a un repuesto.
    /// Muestra la existencia actual y pide la cantidad.
    /// </summary>
    public class FormAjusteExistencia : FormDialogo
    {
        private readonly AjustarExistenciaService _servicio;
        private readonly PermisosUsuario _permisos;
        private readonly Repuesto _repuesto;
        private readonly bool _incrementar;
        private readonly NumericUpDown _numCantidad = new()
        {
            Minimum = 1,
            Maximum = AjustarExistenciaService.LimiteExistencia,
            Value = 1
        };

        public FormAjusteExistencia(IRepuestoRepository repuestoRepository, PermisosUsuario permisos, Repuesto repuesto, bool incrementar)
            : base(incrementar ? "Incrementar existencia" : "Decrementar existencia")
        {
            _servicio = new AjustarExistenciaService(repuestoRepository);
            _permisos = permisos;
            _repuesto = repuesto;
            _incrementar = incrementar;

            AgregarCampo(null, new Label
            {
                Text = $"{repuesto.Nombre}: {repuesto.Existencia} en existencia.",
                AutoSize = true,
                MaximumSize = new Size(380, 0)
            });
            AgregarCampo(incrementar ? "Cantidad a sumar" : "Cantidad a restar", _numCantidad);
        }

        protected override bool AlGuardar()
        {
            var cantidad = (int)_numCantidad.Value;

            if (_incrementar)
            {
                _servicio.Incrementar(_permisos, _repuesto, cantidad);
            }
            else
            {
                _servicio.Decrementar(_permisos, _repuesto, cantidad);
            }

            return true;
        }
    }
}

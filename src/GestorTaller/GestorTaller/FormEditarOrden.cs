using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana para que el administrador edite los datos de recepción de una orden (descripción del
    /// objeto, descripción del problema y costo del diagnóstico). Si no se cambia nada, no se guarda:
    /// <see cref="HuboCambios"/> indica si hubo algo que guardar.
    /// </summary>
    public class FormEditarOrden : FormDialogo
    {
        private readonly ModificarOrdenService _modificarOrdenService;
        private readonly PermisosUsuario _permisos;
        private readonly Orden _orden;
        private readonly TextBox _txtObjeto = new() { MaxLength = 300 };
        private readonly TextBox _txtProblema = new() { MaxLength = 300, Multiline = true, Height = 80 };
        private readonly NumericUpDown _numCosto = new() { DecimalPlaces = 2, Maximum = 1000000, ThousandsSeparator = true };

        public FormEditarOrden(IOrdenRepository ordenRepository, PermisosUsuario permisos, Orden orden)
            : base("Editar recepción")
        {
            _modificarOrdenService = new ModificarOrdenService(ordenRepository);
            _permisos = permisos;
            _orden = orden;

            AgregarCampo("Descripción del objeto", _txtObjeto);
            AgregarCampo("Descripción del problema", _txtProblema);
            AgregarCampo("Costo de diagnóstico (Q)", _numCosto);

            _txtObjeto.Text = orden.DescripcionObjeto;
            _txtProblema.Text = orden.DescripcionProblema;
            _numCosto.Value = orden.CostoDiagnostico;
        }

        /// <summary>True si al guardar cambió algún dato de la orden; false si todo quedó igual.</summary>
        public bool HuboCambios { get; private set; }

        protected override bool AlGuardar()
        {
            HuboCambios = _modificarOrdenService.Ejecutar(_permisos, _orden, _txtObjeto.Text, _txtProblema.Text, _numCosto.Value);

            return true;
        }
    }
}

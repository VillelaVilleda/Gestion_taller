using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana para registrar un cliente nuevo o modificar uno existente (con sus datos actuales).
    /// Al cerrar con OK, <see cref="Resultado"/> tiene el cliente guardado.
    /// </summary>
    public class FormCliente : FormDialogo
    {
        private readonly RegistrarClienteService _registrarClienteService;
        private readonly ModificarClienteService _modificarClienteService;
        private readonly PermisosUsuario _permisos;
        private readonly Cliente? _cliente;
        private readonly TextBox _txtNombre = new() { MaxLength = ValidadorDatosCliente.LongitudMaximaNombre };
        private readonly TextBox _txtTelefono = new() { MaxLength = ValidadorDatosCliente.LongitudMaximaTelefono, PlaceholderText = "Ej. 5555-1234" };
        private readonly TextBox _txtCorreo = new() { MaxLength = ValidadorDatosCliente.LongitudMaximaCorreo, PlaceholderText = "Ej. nombre@correo.com" };
        private readonly TextBox _txtDireccion = new() { MaxLength = ValidadorDatosCliente.LongitudMaximaDireccion, Multiline = true, Height = 64 };

        /// <summary>Si es null, la ventana registra un cliente nuevo; si no, modifica ese cliente.</summary>
        public FormCliente(IClienteRepository clienteRepository, PermisosUsuario permisos, Cliente? cliente = null)
            : base(cliente is null ? "Nuevo cliente" : "Modificar cliente")
        {
            _registrarClienteService = new RegistrarClienteService(clienteRepository);
            _modificarClienteService = new ModificarClienteService(clienteRepository);
            _permisos = permisos;
            _cliente = cliente;

            AgregarCampo("Nombre", _txtNombre);
            AgregarCampo("Teléfono", _txtTelefono);
            AgregarCampo("Correo electrónico", _txtCorreo);
            AgregarCampo("Dirección (opcional)", _txtDireccion);

            if (cliente is not null)
            {
                _txtNombre.Text = cliente.Nombre;
                _txtTelefono.Text = cliente.Telefono;
                _txtCorreo.Text = cliente.Correo;
                _txtDireccion.Text = cliente.Direccion;
            }
        }

        /// <summary>El cliente registrado o modificado; es null mientras no se haya guardado.</summary>
        public Cliente? Resultado { get; private set; }

        protected override bool AlGuardar()
        {
            if (_cliente is null)
            {
                Resultado = _registrarClienteService.Ejecutar(_txtNombre.Text, _txtTelefono.Text, _txtCorreo.Text, _txtDireccion.Text);
            }
            else
            {
                _modificarClienteService.Ejecutar(_permisos, _cliente, _txtNombre.Text, _txtTelefono.Text, _txtCorreo.Text, _txtDireccion.Text);
                Resultado = _cliente;
            }

            return true;
        }
    }
}

using GestorTaller.Negocio;

namespace GestorTaller
{
    public enum ModoFormEmpleado
    {
        /// <summary>Registrar un empleado nuevo: el sistema genera usuario y contraseña temporal.</summary>
        Nuevo,
        /// <summary>Editar los datos del empleado: nombre y rol de administrador.</summary>
        EditarEmpleado,
        /// <summary>Editar solo el usuario (nombre de inicio de sesión).</summary>
        EditarUsuario
    }

    /// <summary>
    /// Ventana para registrar un empleado, editar sus datos o editar su usuario (solo administrador).
    /// Al cerrar con OK, <see cref="Creado"/> tiene el empleado nuevo y su contraseña temporal
    /// (solo en modo Nuevo).
    /// </summary>
    public class FormEmpleado : FormDialogo
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly Empleado _usuarioActual;
        private readonly Empleado? _empleado;
        private readonly ModoFormEmpleado _modo;
        private readonly TextBox _txtNombre = new() { MaxLength = CrearEmpleadoService.LongitudMaximaNombre };
        private readonly TextBox _txtUsuario = new() { MaxLength = ModificarEmpleadoService.LongitudMaximaUsuario };
        private readonly CheckBox _chkAdministrador = new() { Text = "Administrador", AutoSize = true };

        public FormEmpleado(IEmpleadoRepository empleadoRepository, Empleado usuarioActual, ModoFormEmpleado modo, Empleado? empleado = null)
            : base(Titulo(modo))
        {
            if (modo != ModoFormEmpleado.Nuevo && empleado is null)
            {
                throw new ArgumentException("Se requiere el empleado a editar.", nameof(empleado));
            }

            _empleadoRepository = empleadoRepository;
            _usuarioActual = usuarioActual;
            _empleado = empleado;
            _modo = modo;

            if (modo != ModoFormEmpleado.EditarUsuario)
            {
                AgregarCampo("Nombre", _txtNombre);
                AgregarCampo(null, _chkAdministrador);
            }

            if (modo == ModoFormEmpleado.EditarUsuario)
            {
                AgregarCampo("Usuario", _txtUsuario);
            }

            if (modo == ModoFormEmpleado.Nuevo)
            {
                AgregarCampo(null, new Label
                {
                    Text = "El usuario y la contraseña temporal los genera el sistema.",
                    AutoSize = true,
                    ForeColor = Tema.TextoSecundario,
                    MaximumSize = new Size(380, 0)
                });
            }

            if (empleado is not null)
            {
                _txtNombre.Text = empleado.Nombre;
                _txtUsuario.Text = empleado.NombreUsuario;
                _chkAdministrador.Checked = empleado.EsAdministrador;
            }
        }

        /// <summary>El empleado registrado con su contraseña temporal; es null si no es modo Nuevo o no se guardó.</summary>
        public EmpleadoCreado? Creado { get; private set; }

        private static string Titulo(ModoFormEmpleado modo) => modo switch
        {
            ModoFormEmpleado.Nuevo => "Nuevo empleado",
            ModoFormEmpleado.EditarEmpleado => "Editar empleado",
            _ => "Editar usuario"
        };

        protected override bool AlGuardar()
        {
            if (_modo == ModoFormEmpleado.Nuevo)
            {
                Creado = new CrearEmpleadoService(_empleadoRepository).Ejecutar(_txtNombre.Text, _chkAdministrador.Checked);
                return true;
            }

            var empleado = _empleado!;
            var modificar = new ModificarEmpleadoService(_empleadoRepository);

            if (_modo == ModoFormEmpleado.EditarEmpleado)
            {
                modificar.Ejecutar(_usuarioActual, empleado, _txtNombre.Text, empleado.NombreUsuario, _chkAdministrador.Checked);
            }
            else
            {
                modificar.Ejecutar(_usuarioActual, empleado, empleado.Nombre, _txtUsuario.Text, empleado.EsAdministrador);
            }

            return true;
        }
    }
}

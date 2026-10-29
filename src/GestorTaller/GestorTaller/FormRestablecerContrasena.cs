using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana para restablecer la contraseña de un empleado (solo administrador): el sistema genera
    /// una temporal aleatoria o el administrador elige una (con confirmación). Al cerrar con OK,
    /// <see cref="Contrasena"/> tiene la contraseña nueva en texto y <see cref="FueGenerada"/> indica
    /// si la generó el sistema (en ese caso hay que mostrársela al administrador una sola vez).
    /// </summary>
    public class FormRestablecerContrasena : FormDialogo
    {
        private readonly RestablecerContrasenaService _servicio;
        private readonly PermisosUsuario _permisos;
        private readonly Empleado _empleado;
        private readonly RadioButton _rbGenerar = new() { Text = "Generar una contraseña temporal aleatoria", AutoSize = true, Checked = true };
        private readonly RadioButton _rbElegir = new() { Text = "Elegir la contraseña", AutoSize = true };
        private readonly TextBox _txtNueva = new() { UseSystemPasswordChar = true, MaxLength = ValidadorContrasena.LongitudMaxima, Enabled = false };
        private readonly TextBox _txtConfirmacion = new() { UseSystemPasswordChar = true, MaxLength = ValidadorContrasena.LongitudMaxima, Enabled = false };

        public FormRestablecerContrasena(IEmpleadoRepository empleadoRepository, PermisosUsuario permisos, Empleado empleado)
            : base("Restablecer contraseña", "Restablecer")
        {
            _servicio = new RestablecerContrasenaService(empleadoRepository);
            _permisos = permisos;
            _empleado = empleado;

            AgregarCampo(null, new Label
            {
                Text = $"{empleado.Nombre} ({empleado.NombreUsuario}). La contraseña actual dejará de funcionar.",
                AutoSize = true,
                MaximumSize = new Size(380, 0)
            });
            AgregarCampo(null, _rbGenerar);
            AgregarCampo(null, _rbElegir);
            AgregarCampo($"Contraseña nueva ({ValidadorContrasena.LongitudMinima} a {ValidadorContrasena.LongitudMaxima} caracteres)", _txtNueva);
            AgregarCampo("Confirmar contraseña nueva", _txtConfirmacion);

            _rbElegir.CheckedChanged += (_, _) =>
            {
                _txtNueva.Enabled = _rbElegir.Checked;
                _txtConfirmacion.Enabled = _rbElegir.Checked;

                if (_rbElegir.Checked)
                {
                    _txtNueva.Focus();
                }
            };
        }

        /// <summary>La contraseña nueva en texto; es null mientras no se haya guardado.</summary>
        public string? Contrasena { get; private set; }

        /// <summary>True si la contraseña la generó el sistema; false si la eligió el administrador.</summary>
        public bool FueGenerada => _rbGenerar.Checked;

        protected override bool AlGuardar()
        {
            if (_rbElegir.Checked)
            {
                if (_txtNueva.Text != _txtConfirmacion.Text)
                {
                    throw new ArgumentException("La confirmación no coincide con la contraseña nueva.");
                }

                Contrasena = _servicio.Ejecutar(_permisos, _empleado, _txtNueva.Text);
            }
            else
            {
                Contrasena = _servicio.Ejecutar(_permisos, _empleado);
            }

            return true;
        }
    }
}

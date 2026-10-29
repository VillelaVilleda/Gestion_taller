using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana para que el empleado actual cambie su propia contrasena
    /// (contrasena actual, nueva y confirmacion de la nueva), sobre el dialogo base.
    /// </summary>
    public class FormCambiarContrasena : FormDialogo
    {
        private readonly CambiarContrasenaService _cambiarContrasenaService;
        private readonly Empleado _empleado;
        private readonly TextBox _txtActual = CrearCampoContrasena();
        private readonly TextBox _txtNueva = CrearCampoContrasena();
        private readonly TextBox _txtConfirmacion = CrearCampoContrasena();

        public FormCambiarContrasena(IEmpleadoRepository empleadoRepository, Empleado empleado)
            : base("Cambiar contraseña")
        {
            _cambiarContrasenaService = new CambiarContrasenaService(empleadoRepository);
            _empleado = empleado;

            AgregarCampo("Contraseña actual", _txtActual);
            AgregarCampo("Contraseña nueva", _txtNueva);
            AgregarCampo("Confirmar contraseña nueva", _txtConfirmacion);
        }

        private static TextBox CrearCampoContrasena() => new() { UseSystemPasswordChar = true, MaxLength = ValidadorContrasena.LongitudMaxima };

        protected override bool AlGuardar()
        {
            if (_txtNueva.Text != _txtConfirmacion.Text)
            {
                throw new ArgumentException("La confirmación no coincide con la contraseña nueva.");
            }

            _cambiarContrasenaService.Ejecutar(_empleado, _txtActual.Text, _txtNueva.Text);

            MessageBox.Show("Tu contraseña fue cambiada.", "Cambiar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;
        }
    }
}

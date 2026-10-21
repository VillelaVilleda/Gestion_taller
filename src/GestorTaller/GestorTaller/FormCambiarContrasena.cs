using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana para que el empleado actual cambie su propia contrasena
    /// (contrasena actual, nueva y confirmacion de la nueva). Los controles se
    /// crean en codigo porque es un dialogo pequeno.
    /// </summary>
    public class FormCambiarContrasena : Form
    {
        private readonly CambiarContrasenaService _cambiarContrasenaService;
        private readonly Empleado _empleado;
        private readonly TextBox _txtActual = CrearCampoContrasena(40);
        private readonly TextBox _txtNueva = CrearCampoContrasena(100);
        private readonly TextBox _txtConfirmacion = CrearCampoContrasena(160);
        private readonly Label _lblMensaje = new() { AutoSize = true, ForeColor = Color.DarkRed, Location = new Point(20, 195), MaximumSize = new Size(320, 0) };

        public FormCambiarContrasena(IEmpleadoRepository empleadoRepository, Empleado empleado)
        {
            _cambiarContrasenaService = new CambiarContrasenaService(empleadoRepository);
            _empleado = empleado;

            Text = "Cambiar contrasena";
            ClientSize = new Size(360, 290);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            var btnGuardar = new Button { Text = "Guardar", Location = new Point(20, 240), Size = new Size(150, 32) };
            var btnCancelar = new Button { Text = "Cancelar", Location = new Point(190, 240), Size = new Size(150, 32), DialogResult = DialogResult.Cancel };
            btnGuardar.Click += btnGuardar_Click;
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;

            Controls.Add(CrearEtiqueta("Contrasena actual", 15));
            Controls.Add(_txtActual);
            Controls.Add(CrearEtiqueta("Contrasena nueva", 75));
            Controls.Add(_txtNueva);
            Controls.Add(CrearEtiqueta("Confirmar contrasena nueva", 135));
            Controls.Add(_txtConfirmacion);
            Controls.Add(_lblMensaje);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
        }

        private static TextBox CrearCampoContrasena(int y) => new() { Location = new Point(20, y), Size = new Size(320, 27), UseSystemPasswordChar = true };

        private static Label CrearEtiqueta(string texto, int y) => new() { Text = texto, AutoSize = true, Location = new Point(20, y) };

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            _lblMensaje.Text = "";

            if (_txtNueva.Text != _txtConfirmacion.Text)
            {
                _lblMensaje.Text = "La confirmacion no coincide con la contrasena nueva.";
                return;
            }

            try
            {
                _cambiarContrasenaService.Ejecutar(_empleado, _txtActual.Text, _txtNueva.Text);

                MessageBox.Show("Tu contrasena fue cambiada.", "Cambiar contrasena", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                _lblMensaje.Text = ex.Message;
            }
        }
    }
}

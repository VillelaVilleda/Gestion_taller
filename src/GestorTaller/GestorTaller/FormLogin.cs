using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Pantalla de inicio de sesion (issue: login con usuarios de prueba).
    /// Valida contra IniciarSesionService, que por ahora usa una lista fija
    /// de usuarios (sin base de datos).
    /// </summary>
    public partial class FormLogin : Form
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IniciarSesionService _iniciarSesionService = new();

        public FormLogin(IOrdenRepository ordenRepository)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
        }

        private void btnEntrar_Click(object? sender, EventArgs e)
        {
            lblMensaje.Text = "";

            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                lblMensaje.Text = "Ingresa usuario y contrasena.";
                return;
            }

            var usuario = _iniciarSesionService.Autenticar(txtUsuario.Text, txtContrasena.Text);

            if (usuario is null)
            {
                lblMensaje.Text = "Usuario o contrasena incorrectos.";
                return;
            }

            AbrirVentanaPrincipal();
        }

        private void AbrirVentanaPrincipal()
        {
            Hide();

            var formPrincipal = new FormPrincipal(_ordenRepository, MostrarLogin);
            formPrincipal.Show();
        }

        private void MostrarLogin()
        {
            txtUsuario.Clear();
            txtContrasena.Clear();
            lblMensaje.Text = "";
            Show();
        }

        private void btnSalir_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkAyuda_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "Contacto de servicio tecnico:\nalfonsovillela02@gmail.com.com\nTel. 000-0000",
                "Ayuda",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}

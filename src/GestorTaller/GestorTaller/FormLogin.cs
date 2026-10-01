using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Pantalla de inicio de sesion. Valida contra IniciarSesionService,
    /// que consulta los empleados reales a traves de IEmpleadoRepository.
    /// </summary>
    public partial class FormLogin : Form
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IniciarSesionService _iniciarSesionService;

        public FormLogin(IOrdenRepository ordenRepository, IClienteRepository clienteRepository, IEmpleadoRepository empleadoRepository)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _clienteRepository = clienteRepository;
            _iniciarSesionService = new IniciarSesionService(empleadoRepository);
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

            var formPrincipal = new FormPrincipal(_ordenRepository, _clienteRepository, MostrarLogin);
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
                "Contacto de servicio tecnico:\nalfonsovillela02@gmail.com\nTel. +502 0000-0000",
                "Ayuda",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}

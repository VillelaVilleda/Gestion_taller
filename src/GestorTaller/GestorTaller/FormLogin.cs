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
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IRepuestoRepository _repuestoRepository;
        private readonly IniciarSesionService _iniciarSesionService;

        public FormLogin(IOrdenRepository ordenRepository, IClienteRepository clienteRepository, IEmpleadoRepository empleadoRepository, IRepuestoRepository repuestoRepository)
        {
            InitializeComponent();
            Tema.Aplicar(this);
            BackColor = Tema.Blanco; // el inicio de sesion va sobre fondo blanco
            avisoMensaje.Limpiar();
            _ordenRepository = ordenRepository;
            _clienteRepository = clienteRepository;
            _empleadoRepository = empleadoRepository;
            _repuestoRepository = repuestoRepository;
            _iniciarSesionService = new IniciarSesionService(empleadoRepository);
        }

        private void btnEntrar_Click(object? sender, EventArgs e)
        {
            avisoMensaje.Limpiar();

            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                avisoMensaje.Mostrar(TipoAviso.Error, "Ingresa usuario y contrasena.");
                return;
            }

            Empleado? usuario;

            try
            {
                UseWaitCursor = true;
                usuario = _iniciarSesionService.Autenticar(txtUsuario.Text, txtContrasena.Text);
            }
            catch (Exception ex)
            {
                RegistroErrores.Registrar(ex);
                avisoMensaje.Mostrar(TipoAviso.Error, "No se pudo conectar con la base de datos. Verifica que esté encendida e inténtalo de nuevo.");
                return;
            }
            finally
            {
                UseWaitCursor = false;
            }

            if (usuario is null)
            {
                avisoMensaje.Mostrar(TipoAviso.Error, "Usuario o contrasena incorrectos.");
                return;
            }

            AbrirVentanaPrincipal(usuario);
        }

        private void AbrirVentanaPrincipal(Empleado usuario)
        {
            Hide();

            var formPrincipal = new FormPrincipal(_ordenRepository, _clienteRepository, _empleadoRepository, _repuestoRepository, usuario, MostrarLogin);
            formPrincipal.Show();
        }

        private void MostrarLogin()
        {
            txtUsuario.Clear();
            txtContrasena.Clear();
            avisoMensaje.Limpiar();
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

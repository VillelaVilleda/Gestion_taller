using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Pantalla de inicio de sesión: panel de marca a la izquierda y tarjeta de acceso a la derecha.
    /// Valida contra IniciarSesionService, que consulta los empleados reales a través de IEmpleadoRepository.
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
            AplicarEstilo();
            avisoMensaje.Limpiar();
            _ordenRepository = ordenRepository;
            _clienteRepository = clienteRepository;
            _empleadoRepository = empleadoRepository;
            _repuestoRepository = repuestoRepository;
            _iniciarSesionService = new IniciarSesionService(empleadoRepository);
        }

        // Usuario inexistente: solo sirve para comprobar que la base de datos responde.
        private const string UsuarioDePrueba = "__conexion__";

        // Los colores del tema se ponen aquí y no en el Designer: el diseñador no entiende las llamadas a Tema.
        private void AplicarEstilo()
        {
            panelMarca.BackColor = Tema.Pizarra;
            lblTituloMarca.ForeColor = Tema.Blanco;
            insigniaLogo.ColorFondo = Tema.Cobre;
            insigniaLogo.ColorIcono = Tema.Titanio;
            lblTitulo.ForeColor = Tema.Titanio;

            foreach (var etiqueta in new[] { lblUsuario, lblContrasena })
            {
                etiqueta.ForeColor = Tema.TextoSecundario;
            }

            foreach (var enlace in new[] { lnkAyuda, lnkSalir })
            {
                enlace.LinkColor = Tema.Pizarra;
                enlace.ActiveLinkColor = Tema.CobreOscuro;
            }
        }

        private void btnEntrar_Click(object? sender, EventArgs e)
        {
            avisoMensaje.Limpiar();

            if (string.IsNullOrWhiteSpace(campoUsuario.Texto) || string.IsNullOrWhiteSpace(campoContrasena.Texto))
            {
                avisoMensaje.Mostrar(TipoAviso.Error, "Ingresa usuario y contraseña.");
                return;
            }

            Empleado? usuario;

            try
            {
                UseWaitCursor = true;

                // Se comprueba la conexión antes de autenticar: el administrador oculto no consulta la
                // base de datos, pero igual la necesita para trabajar.
                _empleadoRepository.ObtenerPorNombreUsuario(UsuarioDePrueba);

                usuario = _iniciarSesionService.Autenticar(campoUsuario.Texto, campoContrasena.Texto);
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
                avisoMensaje.Mostrar(TipoAviso.Error, "Usuario o contraseña incorrectos.");
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
            campoUsuario.Texto = string.Empty;
            campoContrasena.Texto = string.Empty;
            avisoMensaje.Limpiar();
            Show();
            campoUsuario.Focus();
        }

        private void lnkSalir_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
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

        private void FormLogin_Load(object? sender, EventArgs e)
        {
            campoUsuario.Focus();
        }
    }
}

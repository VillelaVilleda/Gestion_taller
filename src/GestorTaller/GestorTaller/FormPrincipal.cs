using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana principal de la aplicacion: menu lateral a la izquierda y un
    /// panel de contenido a la derecha, donde se muestran las distintas
    /// secciones (Nueva orden, Ordenes registradas, etc.). Conserva el empleado
    /// autenticado y sus permisos mientras dura la sesion.
    /// </summary>
    public partial class FormPrincipal : Form
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IRepuestoRepository _repuestoRepository;
        private readonly Action _cerrarSesion;
        private bool _cerrandoSesion;
        private Empleado? _usuarioActual;
        private PermisosUsuario? _permisos;

        internal Empleado? UsuarioActual => _usuarioActual;

        internal PermisosUsuario? Permisos => _permisos;

        public FormPrincipal(IOrdenRepository ordenRepository, IClienteRepository clienteRepository, IEmpleadoRepository empleadoRepository, IRepuestoRepository repuestoRepository, Empleado usuarioActual, Action cerrarSesion)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _clienteRepository = clienteRepository;
            _empleadoRepository = empleadoRepository;
            _repuestoRepository = repuestoRepository;
            _usuarioActual = usuarioActual;
            RegistroErrores.UsuarioActual = usuarioActual.Nombre;
            _permisos = new PermisosUsuario(usuarioActual);

            Text = $"GestorTaller - {usuarioActual.Nombre} ({(usuarioActual.EsAdministrador ? "Administrador" : "Usuario")})";
            btnEmpleados.Visible = _permisos.PuedeAdministrarEmpleados;
            _cerrarSesion = cerrarSesion;
        }

        private void FormPrincipal_Load(object? sender, EventArgs e)
        {
            MostrarSeccionCrearOrden();
        }

        private void FormPrincipal_FormClosed(object? sender, FormClosedEventArgs e)
        {
            // Si la ventana se cierra por cualquier via que no sea el boton
            // "Cerrar sesion" (la X de la ventana, Alt+F4, etc.), se entiende
            // como que el usuario quiere salir de la aplicacion por completo.
            if (!_cerrandoSesion)
            {
                Application.Exit();
            }
        }

        private void btnCerrarSesion_Click(object? sender, EventArgs e)
        {
            _cerrandoSesion = true;
            _usuarioActual = null;
            RegistroErrores.UsuarioActual = null;
            _permisos = null;
            _cerrarSesion();
            Close();
        }

        private void btnCambiarContrasena_Click(object? sender, EventArgs e)
        {
            using var dialogo = new FormCambiarContrasena(_empleadoRepository, _usuarioActual!);
            dialogo.ShowDialog(this);
        }

        private void btnSalir_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCrearOrden_Click(object? sender, EventArgs e) => MostrarSeccionCrearOrden();

        private void btnOrdenesRegistradas_Click(object? sender, EventArgs e) => MostrarSeccionOrdenesRegistradas();

        private void btnRepuestos_Click(object? sender, EventArgs e) => MostrarSeccionRepuestos();

        private void btnClientes_Click(object? sender, EventArgs e) => MostrarSeccionClientes();

        private void btnEmpleados_Click(object? sender, EventArgs e) => MostrarSeccionEmpleados();

        private void MostrarSeccionCrearOrden() => MostrarControl(new CrearOrdenControl(_ordenRepository, _clienteRepository, MostrarSeccionOrdenesRegistradas));

        private void MostrarSeccionOrdenesRegistradas() => MostrarControl(new ListadoOrdenesControl(_ordenRepository, AbrirSeguimientoOrden));

        private void AbrirSeguimientoOrden(Guid ordenId) => MostrarControl(new SeguimientoOrdenControl(_ordenRepository, _empleadoRepository, _repuestoRepository, ordenId));

        private void MostrarSeccionClientes() => MostrarControl(new ClientesControl(_clienteRepository, _ordenRepository, _usuarioActual!));

        private void MostrarSeccionEmpleados() => MostrarControl(new EmpleadosControl(_empleadoRepository, _usuarioActual!));

        private void MostrarSeccionRepuestos() => MostrarControl(new RepuestosControl(_repuestoRepository, _ordenRepository, _usuarioActual!));

        private void MostrarControl(Control control)
        {
            var anteriores = panelContenido.Controls.Cast<Control>().ToList();

            panelContenido.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(control);

            // Se liberan despues de terminar el evento actual: a veces el cambio de seccion
            // lo provoca un boton del propio control que se esta reemplazando.
            BeginInvoke(() => anteriores.ForEach(anterior => anterior.Dispose()));
        }
    }
}

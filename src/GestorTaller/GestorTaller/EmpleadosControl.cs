using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Empleados" dentro de la ventana principal (menu lateral).
    /// Permite al administrador crear empleados (usuario y contrasena temporal
    /// generados por el sistema), editar su nombre y rol, y restablecer contrasenas. La contrasena temporal
    /// se muestra una sola vez; nunca se muestra el hash. Eliminar queda
    /// para una issue futura.
    /// </summary>
    public partial class EmpleadosControl : UserControl
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly Empleado _usuarioActual;
        private readonly PermisosUsuario _permisos;
        private readonly ModificarEmpleadoService _modificarEmpleadoService;
        private readonly CrearEmpleadoService _crearEmpleadoService;
        private readonly RestablecerContrasenaService _restablecerContrasenaService;

        public EmpleadosControl(IEmpleadoRepository empleadoRepository, Empleado usuarioActual)
        {
            InitializeComponent();
            _empleadoRepository = empleadoRepository;
            _usuarioActual = usuarioActual;
            _permisos = new PermisosUsuario(usuarioActual);
            _modificarEmpleadoService = new ModificarEmpleadoService(empleadoRepository);
            cmbEmpleado.SelectedIndexChanged += (_, _) => MostrarDatosDelSeleccionado();
            _crearEmpleadoService = new CrearEmpleadoService(empleadoRepository);
            _restablecerContrasenaService = new RestablecerContrasenaService(empleadoRepository);
            Load += EmpleadosControl_Load;
        }

        private void EmpleadosControl_Load(object? sender, EventArgs e)
        {
            CargarEmpleados();
        }

        private void CargarEmpleados()
        {
            var columnas = new List<ColumnaTabla<Empleado>>
            {
                new("Nombre", e => e.Nombre),
                new("Usuario", e => e.NombreUsuario),
                new("Administrador", e => e.EsAdministrador, e => e.EsAdministrador ? "Si" : "No")
            };

            var empleados = _empleadoRepository.ObtenerTodos();
            tablaEmpleados.Cargar(columnas, empleados, ordenInicial: columnas[0]);
            cmbEmpleado.CargarElementos(empleados.OrderBy(e => e.Nombre).ToList(), e => e.Nombre);
        }

        private void MostrarDatosDelSeleccionado()
        {
            var empleado = cmbEmpleado.ElementoSeleccionado as Empleado;

            txtEditarNombre.Text = empleado?.Nombre ?? string.Empty;
            chkEditarAdministrador.Checked = empleado?.EsAdministrador ?? false;
        }

        private void btnGuardarCambios_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            if (cmbEmpleado.ElementoSeleccionado is not Empleado empleado)
            {
                lblMensaje.Text = "Selecciona un empleado de la lista.";
                return;
            }

            try
            {
                _modificarEmpleadoService.Ejecutar(_usuarioActual, empleado, txtEditarNombre.Text, empleado.NombreUsuario, chkEditarAdministrador.Checked);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Empleado \"{empleado.Nombre}\" actualizado.";

                CargarEmpleados();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnCrearEmpleado_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                var creado = _crearEmpleadoService.Ejecutar(txtNombre.Text, chkAdministrador.Checked);

                txtNombre.Clear();
                chkAdministrador.Checked = false;
                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Empleado \"{creado.Empleado.Nombre}\" creado.";

                CargarEmpleados();
                MostrarCredenciales(creado.Empleado.NombreUsuario, creado.PasswordTemporal);
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnRestablecer_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            if (cmbEmpleado.ElementoSeleccionado is not Empleado empleado)
            {
                lblMensaje.Text = "Selecciona un empleado de la lista.";
                return;
            }

            var confirmar = MessageBox.Show(
                $"Se generara una contrasena temporal nueva para {empleado.Nombre}. La actual dejara de funcionar. Continuar?",
                "Restablecer contrasena",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var password = _restablecerContrasenaService.Ejecutar(_permisos, empleado);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Contrasena de \"{empleado.Nombre}\" restablecida.";
                MostrarCredenciales(empleado.NombreUsuario, password);
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        // La contrasena temporal solo se muestra aqui; despues ya no se puede consultar.
        private static void MostrarCredenciales(string usuario, string password)
        {
            MessageBox.Show(
                $"Usuario: {usuario}\nContrasena temporal: {password}\n\nAnota la contrasena ahora: no se volvera a mostrar.",
                "Credenciales del empleado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}

using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Clientes" dentro de la ventana principal (menu lateral).
    /// Lista los clientes; los clientes nuevos se crean al registrar una orden.
    /// Solo el administrador puede editar sus datos o eliminarlos (si no tienen ordenes).
    /// </summary>
    public partial class ClientesControl : UserControl
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly PermisosUsuario _permisos;
        private readonly ModificarClienteService _modificarClienteService;
        private readonly EliminarClienteService _eliminarClienteService;

        public ClientesControl(IClienteRepository clienteRepository, IOrdenRepository ordenRepository, Empleado usuarioActual)
        {
            InitializeComponent();
            _clienteRepository = clienteRepository;
            _permisos = new PermisosUsuario(usuarioActual);
            _modificarClienteService = new ModificarClienteService(clienteRepository);
            _eliminarClienteService = new EliminarClienteService(clienteRepository, ordenRepository);
            cmbCliente.SelectedIndexChanged += (_, _) => MostrarDatosDelSeleccionado();

            btnGuardarCambios.Visible = _permisos.PuedeEditar;
            btnEliminar.Visible = _permisos.PuedeEliminar;
            panelEdicion.Visible = _permisos.PuedeEditar || _permisos.PuedeEliminar;

            if (!panelEdicion.Visible)
            {
                var alto = panelEdicion.Height;
                lblMensaje.Top -= alto;
                tablaClientes.Top -= alto;
                tablaClientes.Height += alto;
            }

            Load += ClientesControl_Load;
        }

        private void ClientesControl_Load(object? sender, EventArgs e)
        {
            CargarClientes();
        }

        private void CargarClientes()
        {
            var columnas = new List<ColumnaTabla<Cliente>>
            {
                new("Nombre", c => c.Nombre),
                new("Telefono", c => c.Telefono),
                new("Correo", c => c.Correo),
                new("Direccion", c => c.Direccion)
            };

            var clientes = _clienteRepository.ObtenerTodos();
            tablaClientes.Cargar(columnas, clientes, ordenInicial: columnas[0]);
            cmbCliente.CargarElementos(clientes.OrderBy(c => c.Nombre).ToList(), c => c.Nombre);
            MostrarDatosDelSeleccionado();
        }

        private void MostrarDatosDelSeleccionado()
        {
            var cliente = cmbCliente.ElementoSeleccionado as Cliente;

            txtEditarNombre.Text = cliente?.Nombre ?? string.Empty;
            txtEditarTelefono.Text = cliente?.Telefono ?? string.Empty;
            txtEditarCorreo.Text = cliente?.Correo ?? string.Empty;
            txtEditarDireccion.Text = cliente?.Direccion ?? string.Empty;
        }

        private void btnGuardarCambios_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            if (cmbCliente.ElementoSeleccionado is not Cliente cliente)
            {
                lblMensaje.Text = "Selecciona un cliente de la lista.";
                return;
            }

            try
            {
                _modificarClienteService.Ejecutar(_permisos, cliente, txtEditarNombre.Text, txtEditarTelefono.Text, txtEditarCorreo.Text, txtEditarDireccion.Text);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Cliente \"{cliente.Nombre}\" actualizado.";

                CargarClientes();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            if (cmbCliente.ElementoSeleccionado is not Cliente cliente)
            {
                lblMensaje.Text = "Selecciona un cliente de la lista.";
                return;
            }

            var confirmar = MessageBox.Show(
                $"Se eliminara el cliente \"{cliente.Nombre}\". Continuar?",
                "Eliminar cliente",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _eliminarClienteService.Ejecutar(_permisos, cliente);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = $"Cliente \"{cliente.Nombre}\" eliminado.";

                CargarClientes();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}

using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Clientes" dentro de la ventana principal (menu lateral).
    /// Tabla con una columna de acciones segun el rol (ver AccionesPermitidas): todos pueden modificar
    /// y registrar clientes; solo el administrador puede eliminarlos (si no tienen ordenes).
    /// Registrar y modificar abren el dialogo FormCliente; eliminar pide confirmacion.
    /// </summary>
    public partial class ClientesControl : UserControl
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly PermisosUsuario _permisos;
        private readonly AccionesPermitidas _accionesPermitidas;
        private readonly EliminarClienteService _eliminarClienteService;

        public ClientesControl(IClienteRepository clienteRepository, IOrdenRepository ordenRepository, Empleado usuarioActual)
        {
            InitializeComponent();
            _clienteRepository = clienteRepository;
            _permisos = new PermisosUsuario(usuarioActual);
            _accionesPermitidas = new AccionesPermitidas(_permisos);
            _eliminarClienteService = new EliminarClienteService(clienteRepository, ordenRepository);

            btnNuevo.Visible = _accionesPermitidas.Permite(ModuloTabla.Clientes, AccionTabla.Insertar);
            avisoMensaje.Limpiar();
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
                new("Teléfono", c => c.Telefono),
                new("Correo", c => c.Correo),
                new("Dirección", c => c.Direccion)
            };

            var acciones = new List<AccionFila<Cliente>>();
            foreach (var accion in _accionesPermitidas.Para(ModuloTabla.Clientes))
            {
                switch (accion)
                {
                    case AccionTabla.Modificar:
                        acciones.Add(new AccionFila<Cliente>(accion, ModificarCliente));
                        break;
                    case AccionTabla.Eliminar:
                        acciones.Add(new AccionFila<Cliente>(accion, EliminarCliente));
                        break;
                }
            }

            tablaClientes.Cargar(columnas, _clienteRepository.ObtenerTodos(), ordenInicial: columnas[0], acciones: acciones);
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            avisoMensaje.Limpiar();

            using var dialogo = new FormCliente(_clienteRepository, _permisos);

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                avisoMensaje.Mostrar(TipoAviso.Exito, $"Cliente \"{dialogo.Resultado!.Nombre}\" registrado.");
                CargarClientes();
            }
        }

        private void ModificarCliente(Cliente cliente)
        {
            avisoMensaje.Limpiar();

            using var dialogo = new FormCliente(_clienteRepository, _permisos, cliente);

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                avisoMensaje.Mostrar(TipoAviso.Exito, $"Cliente \"{cliente.Nombre}\" actualizado.");
                CargarClientes();
            }
        }

        private void EliminarCliente(Cliente cliente)
        {
            avisoMensaje.Limpiar();

            var confirmar = DialogoConfirmacion.Confirmar(
                this,
                "Eliminar cliente",
                $"Se eliminará el cliente \"{cliente.Nombre}\". ¿Continuar?");

            if (!confirmar)
            {
                return;
            }

            try
            {
                _eliminarClienteService.Ejecutar(_permisos, cliente);

                avisoMensaje.Mostrar(TipoAviso.Exito, $"Cliente \"{cliente.Nombre}\" eliminado.");
                CargarClientes();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                DialogoConfirmacion.Informar(this, "No se pudo eliminar el cliente", ex.Message);
            }
        }
    }
}

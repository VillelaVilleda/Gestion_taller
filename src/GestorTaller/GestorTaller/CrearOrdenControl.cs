using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Sección de "Nueva orden" dentro de la ventana principal (menú lateral).
    /// El cliente se elige de la lista (con búsqueda) o se registra con el botón "Nuevo cliente", que
    /// abre el diálogo de cliente; al guardarlo queda seleccionado. Un cliente nunca se duplica: la
    /// orden se asocia al cliente elegido.
    /// </summary>
    public partial class CrearOrdenControl : UserControl
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly PermisosUsuario _permisos;
        private readonly CrearOrdenService _crearOrdenService;
        private readonly Action _ordenRegistrada;

        public CrearOrdenControl(IOrdenRepository ordenRepository, IClienteRepository clienteRepository, Empleado usuarioActual, Action ordenRegistrada)
        {
            InitializeComponent();
            _clienteRepository = clienteRepository;
            _permisos = new PermisosUsuario(usuarioActual);
            _crearOrdenService = new CrearOrdenService(ordenRepository);
            _ordenRegistrada = ordenRegistrada;
            avisoMensaje.Limpiar();
        }

        private void CrearOrdenControl_Load(object? sender, EventArgs e)
        {
            CargarClientes();
        }

        private void CargarClientes()
        {
            var clientes = _clienteRepository.ObtenerTodos().OrderBy(c => c.Nombre).ToList();
            cmbCliente.CargarElementos(clientes, c => c.Nombre);
        }

        private void btnNuevoCliente_Click(object? sender, EventArgs e)
        {
            avisoMensaje.Limpiar();

            using var dialogo = new FormCliente(_clienteRepository, _permisos);

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                var cliente = dialogo.Resultado!;

                CargarClientes();
                cmbCliente.Seleccionar<Cliente>(c => c.Id == cliente.Id);
                avisoMensaje.Mostrar(TipoAviso.Exito, $"Cliente \"{cliente.Nombre}\" registrado y seleccionado.");
            }
        }

        private void btnRegistrar_Click(object? sender, EventArgs e)
        {
            avisoMensaje.Limpiar();

            try
            {
                if (cmbCliente.ElementoSeleccionado is not Cliente cliente)
                {
                    avisoMensaje.Mostrar(TipoAviso.Error, "Selecciona un cliente de la lista o registra uno con \"Nuevo cliente\".");
                    return;
                }

                _crearOrdenService.Ejecutar(cliente, txtObjeto.Text, txtDescripcion.Text, numCostoDiagnostico.Value);

                txtObjeto.Clear();
                txtDescripcion.Clear();
                numCostoDiagnostico.Value = 0;
                CargarClientes();
                cmbCliente.Focus();
                avisoMensaje.Mostrar(TipoAviso.Exito, "Orden registrada correctamente.");

                _ordenRegistrada();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                avisoMensaje.Mostrar(TipoAviso.Error, ex.Message);
            }
        }
    }
}

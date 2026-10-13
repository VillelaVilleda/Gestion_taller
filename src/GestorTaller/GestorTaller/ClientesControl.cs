using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Clientes" dentro de la ventana principal (menu lateral).
    /// Solo lectura por ahora: crear/editar/eliminar clientes queda para una
    /// issue futura.
    /// </summary>
    public partial class ClientesControl : UserControl
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesControl(IClienteRepository clienteRepository)
        {
            InitializeComponent();
            _clienteRepository = clienteRepository;
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

            tablaClientes.Cargar(columnas, _clienteRepository.ObtenerTodos(), ordenInicial: columnas[0]);
        }
    }
}
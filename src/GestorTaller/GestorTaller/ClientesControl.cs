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
            dgvClientes.Columns.Clear();
            dgvClientes.Columns.Add("Nombre", "Nombre");
            dgvClientes.Columns.Add("Telefono", "Telefono");
            dgvClientes.Columns.Add("Correo", "Correo");
            dgvClientes.Columns.Add("Direccion", "Direccion");

            dgvClientes.Rows.Clear();

            foreach (var cliente in _clienteRepository.ObtenerTodos())
            {
                dgvClientes.Rows.Add(
                    cliente.Nombre,
                    cliente.Telefono,
                    cliente.Correo,
                    cliente.Direccion);
            }
        }
    }
}
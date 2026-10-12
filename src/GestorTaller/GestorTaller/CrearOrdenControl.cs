using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Nueva orden" dentro de la ventana principal (menu lateral).
    /// El cliente se elige de la lista (con busqueda) o se marca "Cliente nuevo"
    /// para escribir su nombre. Un cliente existente nunca se duplica: la orden
    /// se asocia al ya registrado, y un cliente nuevo se guarda junto con la orden.
    /// </summary>
    public partial class CrearOrdenControl : UserControl
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly CrearOrdenService _crearOrdenService;
        private readonly CrearClienteService _crearClienteService;
        private readonly Action _ordenRegistrada;

        public CrearOrdenControl(IOrdenRepository ordenRepository, IClienteRepository clienteRepository, Action ordenRegistrada)
        {
            InitializeComponent();
            _clienteRepository = clienteRepository;
            _crearOrdenService = new CrearOrdenService(ordenRepository);
            _crearClienteService = new CrearClienteService(clienteRepository);
            _ordenRegistrada = ordenRegistrada;
        }

        private void CrearOrdenControl_Load(object? sender, EventArgs e)
        {
            CargarClientes();
            AlternarClienteNuevo();
        }

        private void CargarClientes()
        {
            var clientes = _clienteRepository.ObtenerTodos().OrderBy(c => c.Nombre).ToList();
            cmbCliente.CargarElementos(clientes, c => c.Nombre);
        }

        private void chkClienteNuevo_CheckedChanged(object? sender, EventArgs e) => AlternarClienteNuevo();

        private void AlternarClienteNuevo()
        {
            txtCliente.Visible = chkClienteNuevo.Checked;
            cmbCliente.Visible = !chkClienteNuevo.Checked;
        }

        private Cliente? ObtenerCliente()
        {
            if (chkClienteNuevo.Checked)
            {
                return _crearClienteService.Ejecutar(txtCliente.Text);
            }

            return cmbCliente.ElementoSeleccionado as Cliente;
        }

        private void btnRegistrar_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                var cliente = ObtenerCliente();

                if (cliente is null)
                {
                    lblMensaje.Text = "Selecciona un cliente de la lista o marca \"Cliente nuevo\".";
                    return;
                }

                _crearOrdenService.Ejecutar(cliente, txtObjeto.Text, txtDescripcion.Text, numCostoDiagnostico.Value);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = "Orden registrada correctamente.";

                chkClienteNuevo.Checked = false;
                txtCliente.Clear();
                txtObjeto.Clear();
                txtDescripcion.Clear();
                numCostoDiagnostico.Value = 0;
                CargarClientes();
                cmbCliente.Focus();

                _ordenRegistrada();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}

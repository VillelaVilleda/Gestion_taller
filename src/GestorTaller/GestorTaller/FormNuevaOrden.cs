using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Pantalla para registrar una orden de trabajo nueva (issue #13).
    /// </summary>
    public partial class FormNuevaOrden : Form
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly CrearOrdenService _crearOrdenService;

        public FormNuevaOrden(IOrdenRepository ordenRepository)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _crearOrdenService = new CrearOrdenService(ordenRepository);
        }

        private void btnVerOrdenes_Click(object? sender, EventArgs e)
        {
            using var formListado = new FormListadoOrdenes(_ordenRepository);
            formListado.ShowDialog(this);
        }

        private void btnRegistrar_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            if (string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                lblMensaje.Text = "Ingresa el nombre del cliente.";
                return;
            }

            try
            {
                var cliente = new Cliente
                {
                    Id = Guid.NewGuid(),
                    Nombre = txtCliente.Text
                };

                _crearOrdenService.Ejecutar(cliente, txtDescripcion.Text, numCostoDiagnostico.Value);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = "Orden registrada correctamente.";

                txtCliente.Clear();
                txtDescripcion.Clear();
                numCostoDiagnostico.Value = 0;
                txtCliente.Focus();
            }
            catch (ArgumentException ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}

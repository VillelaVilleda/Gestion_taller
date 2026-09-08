using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Nueva orden" dentro de la ventana principal (menu lateral).
    /// Es el mismo contenido que antes vivia en FormNuevaOrden, movido a un
    /// UserControl para poder mostrarse dentro del panel de contenido.
    /// </summary>
    public partial class CrearOrdenControl : UserControl
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly CrearOrdenService _crearOrdenService;
        private readonly Action _ordenRegistrada;

        public CrearOrdenControl(IOrdenRepository ordenRepository, Action ordenRegistrada)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _crearOrdenService = new CrearOrdenService(ordenRepository);
            _ordenRegistrada = ordenRegistrada;
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

                _crearOrdenService.Ejecutar(cliente, txtObjeto.Text, txtDescripcion.Text, numCostoDiagnostico.Value);

                lblMensaje.ForeColor = Color.DarkGreen;
                lblMensaje.Text = "Orden registrada correctamente.";

                txtCliente.Clear();
                txtObjeto.Clear();
                txtDescripcion.Clear();
                numCostoDiagnostico.Value = 0;
                txtCliente.Focus();

                _ordenRegistrada();
            }
            catch (ArgumentException ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

        private void CrearOrdenControl_Load(object sender, EventArgs e)
        {

        }
    }
}

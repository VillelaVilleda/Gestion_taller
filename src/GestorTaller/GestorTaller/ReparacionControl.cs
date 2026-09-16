using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Formulario de la reparacion realizada. Se muestra bajo el paso
    /// "Terminado" del breadcrumb (no bajo "Reparacion"): completar la
    /// reparacion es justo lo que produce el estado Terminado, mismo
    /// patron que Diagnostico (se llena en Recepcion, se muestra bajo el
    /// chip "Diagnostico"). Editable mientras la orden esta en
    /// EnReparacion; solo lectura una vez que ya avanzo.
    /// </summary>
    public partial class ReparacionControl : UserControl
    {
        private readonly RegistrarReparacionOrdenService _registrarReparacionOrdenService;
        private Orden _orden = null!;

        public event Action? ReparacionRegistrada;

        public ReparacionControl()
        {
            InitializeComponent();
            _registrarReparacionOrdenService = new RegistrarReparacionOrdenService(new AvanzarEstadoOrdenService());
        }

        public void Mostrar(Orden orden)
        {
            _orden = orden;
            lblMensaje.Text = "";

            var esPendiente = orden.Estado == EstadoOrden.EnReparacion;

            txtEmpleado.ReadOnly = !esPendiente;
            txtDetalle.ReadOnly = !esPendiente;
            btnGuardarReparacion.Visible = esPendiente;

            txtEmpleado.Text = orden.EmpleadoReparacion;
            txtDetalle.Text = orden.DetalleReparacion;
        }

        private void btnGuardarReparacion_Click(object? sender, EventArgs e)
        {
            lblMensaje.ForeColor = Color.DarkRed;

            try
            {
                _registrarReparacionOrdenService.Ejecutar(_orden, txtEmpleado.Text, txtDetalle.Text);
                ReparacionRegistrada?.Invoke();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}
using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Muestra los datos de Recepcion de una orden en modo solo lectura,
    /// dentro de la pantalla de seguimiento. Estos datos ya se capturaron
    /// al crear la orden (CrearOrdenControl); aqui no se editan.
    /// </summary>
    public partial class RecepcionSoloLecturaControl : UserControl
    {
        public RecepcionSoloLecturaControl()
        {
            InitializeComponent();
        }

        public void Mostrar(Orden orden)
        {
            txtCliente.Text = orden.Cliente.Nombre;
            txtObjeto.Text = orden.DescripcionObjeto;
            txtDescripcion.Text = orden.DescripcionProblema;
            txtCosto.Text = orden.CostoDiagnostico.ToString("N2");
            txtFecha.Text = orden.FechaRecepcion.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
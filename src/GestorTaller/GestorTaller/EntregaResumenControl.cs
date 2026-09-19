using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Resumen de solo lectura de una orden ya Entregada: junta en una sola
    /// pantalla los datos de Recepcion, Diagnostico, Cotizacion, Reparacion
    /// (si existio) y Entrega, para no tener que navegar chip por chip.
    /// </summary>
    public partial class EntregaResumenControl : UserControl
    {
        public EntregaResumenControl()
        {
            InitializeComponent();
        }

        public void Mostrar(Orden orden)
        {
            flowResumen.Controls.Clear();

            AgregarTitulo("Recepcion");
            AgregarDato("Cliente", orden.Cliente.Nombre);
            AgregarDato("Objeto", orden.DescripcionObjeto);
            AgregarDato("Problema reportado", orden.DescripcionProblema);
            AgregarDato("Costo de diagnostico", $"Q {orden.CostoDiagnostico:N2}");
            AgregarDato("Fecha de recepcion", orden.FechaRecepcion.ToString("dd/MM/yyyy HH:mm"));

            AgregarTitulo("Diagnostico");
            AgregarDato("Empleado", orden.EmpleadoDiagnostico);
            AgregarDato("Detalle", orden.DetalleDiagnostico);

            AgregarTitulo("Cotizacion");
            AgregarDato("Empleado", orden.EmpleadoCotizacion);
            AgregarDato("Monto cotizado", $"Q {orden.MontoCotizado:N2}");
            AgregarDato("Decision del cliente", orden.CotizacionAceptada ? "Aceptada" : "Rechazada");

            var huboReparacion = orden.HistorialEstados.Contains(EstadoOrden.EnReparacion);
            AgregarTitulo("Reparacion");
            if (huboReparacion)
            {
                AgregarDato("Empleado", orden.EmpleadoReparacion);
                AgregarDato("Detalle", orden.DetalleReparacion);
            }
            else
            {
                AgregarDato("", "No aplica: el cliente rechazo la cotizacion.");
            }

            AgregarTitulo("Entrega");
            AgregarDato("Empleado", orden.EmpleadoEntrega);
            AgregarDato("Monto pagado", $"Q {orden.MontoPagado:N2}");
            AgregarDato("Fecha de entrega", orden.FechaEntrega.HasValue
                ? orden.FechaEntrega.Value.ToString("dd/MM/yyyy HH:mm")
                : "");
        }

        private void AgregarTitulo(string texto)
        {
            flowResumen.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Margin = new Padding(0, 12, 0, 4),
                Text = texto
            });
        }

        private void AgregarDato(string etiqueta, string valor)
        {
            var texto = string.IsNullOrEmpty(etiqueta) ? valor : $"{etiqueta}: {valor}";
            flowResumen.Controls.Add(new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Margin = new Padding(10, 2, 0, 2),
                Text = texto
            });
        }
    }
}

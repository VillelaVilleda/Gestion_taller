using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Resumen de solo lectura de una orden ya Entregada: junta en una sola
    /// pantalla los datos de Recepción, Diagnóstico, Cotización, Reparación
    /// (si existio) y Entrega, para no tener que navegar chip por chip.
    /// </summary>
    public partial class EntregaResumenControl : UserControl
    {
        private readonly List<Label> _datos = new();

        public EntregaResumenControl()
        {
            InitializeComponent();
            panelScroll.SizeChanged += (_, _) => AjustarAnchoDeTextos();
        }

        public void Mostrar(Orden orden)
        {
            flowResumen.Controls.Clear();
            _datos.Clear();

            AgregarTitulo("Recepción");
            AgregarDato("Cliente", orden.Cliente.Nombre);
            AgregarDato("Objeto", orden.DescripcionObjeto);
            AgregarDato("Problema reportado", orden.DescripcionProblema);
            AgregarDato("Costo de diagnóstico", $"Q {orden.CostoDiagnostico:N2}");
            AgregarDato("Fecha de recepción", orden.FechaRecepcion.ToString("dd/MM/yyyy HH:mm"));

            AgregarTitulo("Diagnóstico");
            AgregarDato("Empleado", orden.EmpleadoDiagnostico?.Nombre ?? string.Empty);
            AgregarDato("Detalle", orden.DetalleDiagnostico);

            AgregarTitulo("Cotización");
            AgregarDato("Empleado", orden.EmpleadoCotizacion?.Nombre ?? string.Empty);
            AgregarDato("Costo de mano de obra", $"Q {orden.MontoCotizado:N2}");
            AgregarDato("Decisión del cliente", orden.CotizacionAceptada ? "Aceptada" : "Rechazada");

            var huboReparacion = orden.HistorialEstados.Contains(EstadoOrden.EnReparacion);
            AgregarTitulo("Reparación");
            if (huboReparacion)
            {
                AgregarDato("Empleado", orden.EmpleadoReparacion?.Nombre ?? string.Empty);
                AgregarDato("Detalle", orden.DetalleReparacion);

                AgregarTitulo("Repuestos utilizados");
                if (orden.Repuestos.Count == 0)
                {
                    AgregarDato("", "Sin repuestos.");
                }
                else
                {
                    foreach (var asignado in orden.Repuestos)
                    {
                        AgregarDato(
                            asignado.Repuesto?.Nombre ?? "(sin nombre)",
                            $"x {asignado.Cantidad}  |  Q {asignado.PrecioUnitario:N2} c/u  |  Subtotal Q {asignado.PrecioUnitario * asignado.Cantidad:N2}");
                    }
                }
            }
            else
            {
                AgregarDato("", "No aplica: el cliente rechazó la cotización.");
            }

            var desglose = new CalcularTotalOrdenService().Calcular(orden);

            AgregarTitulo("Entrega");
            AgregarDato("Empleado", orden.EmpleadoEntrega?.Nombre ?? string.Empty);
            AgregarDato("Costo de diagnóstico", $"Q {desglose.CostoDiagnostico:N2}");
            AgregarDato("Costo de mano de obra", $"Q {desglose.ManoDeObra:N2}");
            AgregarDato("Repuestos", $"Q {desglose.Repuestos:N2}");
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
                Font = Tema.Fuente(11F, FontStyle.Bold),
                Margin = new Padding(0, 12, 0, 4),
                Text = texto
            });
        }

        private void AgregarDato(string etiqueta, string valor)
        {
            var texto = string.IsNullOrEmpty(etiqueta) ? valor : $"{etiqueta}: {valor}";
            var dato = new Label
            {
                AutoSize = true,
                Margin = new Padding(10, 2, 0, 2),
                Text = texto
            };

            _datos.Add(dato);
            flowResumen.Controls.Add(dato);
            AjustarAnchoDeTextos();
        }

        // Los textos largos se parten en líneas según el ancho disponible, así se adaptan al cambiar la ventana.
        private void AjustarAnchoDeTextos()
        {
            var ancho = Math.Max(200, panelScroll.ClientSize.Width - flowResumen.Padding.Horizontal - 14);

            foreach (var dato in _datos)
            {
                dato.MaximumSize = new Size(ancho, 0);
            }
        }
    }
}

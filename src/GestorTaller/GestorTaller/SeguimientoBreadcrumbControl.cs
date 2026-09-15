using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Miga de pan del seguimiento de una orden: dibuja los 6 pasos del ciclo
    /// de vida, marca cuales ya se visitaron (segun Orden.HistorialEstados),
    /// cual es el paso pendiente (el que todavia no se completa) y cual se
    /// esta viendo actualmente. Atras/Siguiente solo mueven el "cursor" de
    /// que paso se muestra, incluyendo el pendiente; nunca modifican el
    /// estado real de la orden.
    /// </summary>
    public partial class SeguimientoBreadcrumbControl : UserControl
    {
        public static readonly EstadoOrden[] TodosLosPasos =
        {
            EstadoOrden.Recepcion,
            EstadoOrden.Diagnostico,
            EstadoOrden.Cotizacion,
            EstadoOrden.EnReparacion,
            EstadoOrden.Terminado,
            EstadoOrden.Entregado
        };

        private IReadOnlyList<EstadoOrden> _historial = Array.Empty<EstadoOrden>();
        private int _indiceVisto;

        public event Action<EstadoOrden>? PasoSeleccionado;

        public SeguimientoBreadcrumbControl()
        {
            InitializeComponent();
        }

        public void Mostrar(IReadOnlyList<EstadoOrden> historial)
        {
            _historial = historial;
            _indiceVisto = LimiteSuperior();
            Redibujar();
        }

        private bool HayPasoPendiente() =>
            _historial.Count > 0 && _historial[^1] != EstadoOrden.Entregado;

        private EstadoOrden? PasoPendiente() =>
            HayPasoPendiente() ? TodosLosPasos[Array.IndexOf(TodosLosPasos, _historial[^1]) + 1] : null;

        private int LimiteSuperior() =>
            HayPasoPendiente() ? _historial.Count : _historial.Count - 1;

        private EstadoOrden? PasoEnIndice(int indice)
        {
            if (indice < 0 || _historial.Count == 0)
            {
                return null;
            }

            return indice < _historial.Count ? _historial[indice] : PasoPendiente();
        }

        private void btnAtras_Click(object? sender, EventArgs e)
        {
            if (_indiceVisto > 0)
            {
                _indiceVisto--;
                Redibujar();
                var paso = PasoEnIndice(_indiceVisto);
                if (paso is not null)
                {
                    PasoSeleccionado?.Invoke(paso.Value);
                }
            }
        }

        private void btnSiguiente_Click(object? sender, EventArgs e)
        {
            if (_indiceVisto < LimiteSuperior())
            {
                _indiceVisto++;
                Redibujar();
                var paso = PasoEnIndice(_indiceVisto);
                if (paso is not null)
                {
                    PasoSeleccionado?.Invoke(paso.Value);
                }
            }
        }

        private void Redibujar()
        {
            flowPasos.Controls.Clear();

            var pasoPendiente = PasoPendiente();
            var pasoQueSeVe = PasoEnIndice(_indiceVisto);

            for (var i = 0; i < TodosLosPasos.Length; i++)
            {
                var paso = TodosLosPasos[i];

                if (i > 0)
                {
                    flowPasos.Controls.Add(new Label
                    {
                        AutoSize = true,
                        Margin = new Padding(2, 10, 2, 4),
                        ForeColor = Color.Gray,
                        Text = ">"
                    });
                }

                var chip = new Label
                {
                    AutoSize = true,
                    Padding = new Padding(8, 4, 8, 4),
                    Margin = new Padding(2, 4, 2, 4),
                    Text = NombrePaso(paso)
                };

                var esElQueSeVe = paso == pasoQueSeVe;
                var esPendiente = paso == pasoPendiente;
                var yaVisitado = _historial.Contains(paso);

                if (esElQueSeVe && esPendiente)
                {
                    chip.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    chip.ForeColor = Color.White;
                    chip.BackColor = Color.DarkOrange;
                }
                else if (esElQueSeVe)
                {
                    chip.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    chip.ForeColor = Color.White;
                    chip.BackColor = Color.SeaGreen;
                }
                else if (esPendiente)
                {
                    chip.ForeColor = Color.DarkOrange;
                    chip.BackColor = Color.Cornsilk;
                }
                else if (yaVisitado)
                {
                    chip.ForeColor = Color.SeaGreen;
                    chip.BackColor = Color.Honeydew;
                }
                else
                {
                    chip.ForeColor = Color.Gray;
                    chip.BackColor = Color.WhiteSmoke;
                }

                flowPasos.Controls.Add(chip);
            }

            btnAtras.Enabled = _indiceVisto > 0;
            btnSiguiente.Enabled = _indiceVisto < LimiteSuperior();
        }

        private static string NombrePaso(EstadoOrden paso) => paso switch
        {
            EstadoOrden.Recepcion => "Recepcion",
            EstadoOrden.Diagnostico => "Diagnostico",
            EstadoOrden.Cotizacion => "Cotizacion",
            EstadoOrden.EnReparacion => "Reparacion",
            EstadoOrden.Terminado => "Terminado",
            EstadoOrden.Entregado => "Entregado",
            _ => paso.ToString()
        };
    }
}
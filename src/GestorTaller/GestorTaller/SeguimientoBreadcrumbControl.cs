using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Miga de pan del seguimiento de una orden: dibuja los 6 pasos del ciclo
    /// de vida, marca cuales ya se visitaron, cual es el paso pendiente
    /// (el ultimo del historial, porque Orden.Estado representa el paso
    /// activo ahora mismo, no el ultimo ya completado) y cual se esta
    /// viendo. Atras/Siguiente solo mueven el cursor de que paso se
    /// muestra; nunca modifican el estado real de la orden.
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
            _indiceVisto = _historial.Count - 1;
            Redibujar();
        }

        private void btnAtras_Click(object? sender, EventArgs e)
        {
            if (_indiceVisto > 0)
            {
                _indiceVisto--;
                Redibujar();
                PasoSeleccionado?.Invoke(_historial[_indiceVisto]);
            }
        }

        private void btnSiguiente_Click(object? sender, EventArgs e)
        {
            if (_indiceVisto < _historial.Count - 1)
            {
                _indiceVisto++;
                Redibujar();
                PasoSeleccionado?.Invoke(_historial[_indiceVisto]);
            }
        }

        private void Redibujar()
        {
            flowPasos.Controls.Clear();

            EstadoOrden? pasoPendiente = _historial.Count > 0 ? _historial[^1] : null;
            EstadoOrden? pasoQueSeVe = _historial.Count > 0 ? _historial[_indiceVisto] : null;

            for (var i = 0; i < TodosLosPasos.Length; i++)
            {
                var paso = TodosLosPasos[i];

                if (i > 0)
                {
                    flowPasos.Controls.Add(new Label
                    {
                        AutoSize = true,
                        Margin = new Padding(2, 10, 2, 4),
                        ForeColor = Tema.TextoSecundario,
                        Text = ">"
                    });
                }

                var chip = new Label
                {
                    AutoSize = true,
                    Padding = new Padding(10, 5, 10, 5),
                    Margin = new Padding(2, 4, 2, 4),
                    Font = Tema.Fuente(9F),
                    Text = ChipEtapa.Nombre(paso)
                };

                var esElQueSeVe = paso == pasoQueSeVe;
                var esPendiente = paso == pasoPendiente;
                var yaVisitado = _historial.Contains(paso);

                if (esElQueSeVe && esPendiente)
                {
                    chip.Font = Tema.Fuente(9F, FontStyle.Bold);
                    chip.ForeColor = Tema.Titanio;
                    chip.BackColor = Tema.Cobre;
                }
                else if (esElQueSeVe)
                {
                    chip.Font = Tema.Fuente(9F, FontStyle.Bold);
                    chip.ForeColor = Tema.Blanco;
                    chip.BackColor = Tema.Pizarra;
                }
                else if (esPendiente)
                {
                    chip.ForeColor = Tema.Titanio;
                    chip.BackColor = Color.FromArgb(253, 235, 217);
                }
                else if (yaVisitado)
                {
                    chip.ForeColor = Tema.Titanio;
                    chip.BackColor = Color.FromArgb(222, 233, 244);
                }
                else
                {
                    chip.ForeColor = Tema.TextoSecundario;
                    chip.BackColor = Tema.Aluminio;
                }

                flowPasos.Controls.Add(chip);
            }

            btnAtras.Enabled = _indiceVisto > 0;
            btnSiguiente.Enabled = _indiceVisto < _historial.Count - 1;
        }
    }
}
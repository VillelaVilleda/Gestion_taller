using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Ordenes registradas" dentro de la ventana principal (menu lateral).
    /// Usa la tabla generica; la accion "Ver pasos" de cada fila abre el seguimiento de la orden.
    /// </summary>
    public partial class ListadoOrdenesControl : UserControl
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly Action<Guid> _abrirSeguimiento;

        private record FilaOrden(int Codigo, Orden Orden);

        public ListadoOrdenesControl(IOrdenRepository ordenRepository, Action<Guid> abrirSeguimiento)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _abrirSeguimiento = abrirSeguimiento;
            Load += ListadoOrdenesControl_Load;
        }

        private void ListadoOrdenesControl_Load(object? sender, EventArgs e)
        {
            CargarOrdenes();
        }

        private void CargarOrdenes()
        {
            // El codigo se calcula una sola vez sobre todas las ordenes (por fecha de
            // recepcion), asi no cambia al buscar u ordenar la tabla.
            var filas = _ordenRepository.ObtenerTodas()
                .OrderBy(o => o.FechaRecepcion)
                .Select((orden, indice) => new FilaOrden(indice + 1, orden))
                .ToList();

            var columnas = new List<ColumnaTabla<FilaOrden>>
            {
                new("Codigo", f => f.Codigo),
                new("Cliente", f => f.Orden.Cliente.Nombre),
                new("Objeto", f => f.Orden.DescripcionObjeto),
                new("Estado", f => f.Orden.Estado),
                new("Fecha de recepcion", f => f.Orden.FechaRecepcion, f => f.Orden.FechaRecepcion.ToString("dd/MM/yyyy HH:mm"))
            };

            var acciones = new List<AccionFila<FilaOrden>>
            {
                new(AccionTabla.VerPasos, fila => _abrirSeguimiento(fila.Orden.Id))
            };

            tablaOrdenes.Cargar(
                columnas,
                filas,
                ordenInicial: columnas[0],
                direccion: DireccionOrden.Descendente,
                acciones: acciones);
        }
    }
}

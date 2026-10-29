using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Sección de "Órdenes registradas" dentro de la ventana principal (menú lateral). Muestra dos
    /// tablas: las órdenes pendientes (cualquier etapa menos Entregado) y las cerradas (entregadas).
    /// La etapa se ve como chip y la acción "Ver pasos" de cada fila abre el seguimiento de la orden.
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
            // El código se calcula una sola vez sobre todas las órdenes (por fecha de
            // recepción), así no cambia al separar, buscar u ordenar las tablas.
            var filas = _ordenRepository.ObtenerTodas()
                .OrderBy(o => o.FechaRecepcion)
                .Select((orden, indice) => new FilaOrden(indice + 1, orden))
                .ToList();

            var pendientes = filas.Where(f => !ClasificadorOrdenes.EstaCerrada(f.Orden)).ToList();
            var cerradas = filas.Where(f => ClasificadorOrdenes.EstaCerrada(f.Orden)).ToList();

            lblPendientes.Text = $"Pendientes ({pendientes.Count})";
            lblCerradas.Text = $"Cerradas ({cerradas.Count})";

            CargarTabla(tablaPendientes, pendientes);
            CargarTabla(tablaCerradas, cerradas);
        }

        private void CargarTabla(TablaListadoControl tabla, IReadOnlyList<FilaOrden> filas)
        {
            var columnas = new List<ColumnaTabla<FilaOrden>>
            {
                new("Código", f => f.Codigo),
                new("Cliente", f => f.Orden.Cliente.Nombre),
                new("Objeto", f => f.Orden.DescripcionObjeto),
                new("Estado", f => f.Orden.Estado, f => ChipEtapa.Nombre(f.Orden.Estado)),
                new("Fecha de recepción", f => f.Orden.FechaRecepcion, f => f.Orden.FechaRecepcion.ToString("dd/MM/yyyy HH:mm"))
            };

            var acciones = new List<AccionFila<FilaOrden>>
            {
                new(AccionTabla.VerPasos, fila => _abrirSeguimiento(fila.Orden.Id))
            };

            tabla.Cargar(
                columnas,
                filas,
                ordenInicial: columnas[0],
                direccion: DireccionOrden.Descendente,
                acciones: acciones,
                columnaChipEtapa: "Estado");
        }
    }
}

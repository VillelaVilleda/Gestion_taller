namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: crear una nueva orden de trabajo (estado inicial: Recepcion).
/// Reglas (ver CrearOrdenServiceTests):
///   - cliente es obligatorio
///   - descripcionObjeto es obligatoria (no vacia ni en blanco)
///   - descripcionProblema es obligatoria (no vacia ni en blanco)
///   - ambas descripciones se guardan sin espacios en los extremos y con hasta 300 caracteres
///   - costoDiagnostico no puede ser negativo (0 es valido)
///   - la orden nueva queda en estado Recepcion
///   - se guarda en el repositorio antes de devolverla
/// </summary>
public class CrearOrdenService
{
    public const int LongitudMaximaDescripcion = 300;

    private readonly IOrdenRepository _ordenRepository;

    public CrearOrdenService(IOrdenRepository ordenRepository)
    {
        _ordenRepository = ordenRepository;
    }

    public Orden Ejecutar(Cliente cliente, string descripcionObjeto, string descripcionProblema, decimal costoDiagnostico)
    {
        if (cliente is null)
        {
            throw new ArgumentException("La orden requiere un cliente.", nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(descripcionObjeto))
        {
            throw new ArgumentException("La orden requiere una descripción del objeto.", nameof(descripcionObjeto));
        }

        if (string.IsNullOrWhiteSpace(descripcionProblema))
        {
            throw new ArgumentException("La orden requiere una descripción del problema.", nameof(descripcionProblema));
        }

        var objeto = descripcionObjeto.Trim();
        var problema = descripcionProblema.Trim();
        ValidarLongitud(objeto, problema);

        if (costoDiagnostico < 0)
        {
            throw new ArgumentException("El costo de diagnóstico no puede ser negativo.", nameof(costoDiagnostico));
        }

        var orden = new Orden
        {
            Id = Guid.NewGuid(),
            Cliente = cliente,
            DescripcionObjeto = objeto,
            DescripcionProblema = problema,
            CostoDiagnostico = costoDiagnostico,
            Estado = EstadoOrden.Recepcion,
            FechaRecepcion = DateTime.Now,
            HistorialEstados = new List<EstadoOrden> { EstadoOrden.Recepcion }
        };

        _ordenRepository.Agregar(orden);

        return orden;
    }

    // Lo mismo que acepta la base de datos (descripcion_objeto y descripcion_problema).
    public static void ValidarLongitud(string descripcionObjeto, string descripcionProblema)
    {
        if (descripcionObjeto.Length > LongitudMaximaDescripcion)
        {
            throw new ArgumentException($"La descripción del objeto no puede tener más de {LongitudMaximaDescripcion} caracteres.", nameof(descripcionObjeto));
        }

        if (descripcionProblema.Length > LongitudMaximaDescripcion)
        {
            throw new ArgumentException($"La descripción del problema no puede tener más de {LongitudMaximaDescripcion} caracteres.", nameof(descripcionProblema));
        }
    }
}

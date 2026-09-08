namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: crear una nueva orden de trabajo (estado inicial: Recepcion).
/// Reglas (ver CrearOrdenServiceTests):
///   - cliente es obligatorio
///   - descripcionObjeto es obligatoria (no vacia ni en blanco)
///   - descripcionProblema es obligatoria (no vacia ni en blanco)
///   - costoDiagnostico no puede ser negativo (0 es valido)
///   - la orden nueva queda en estado Recepcion
///   - se guarda en el repositorio antes de devolverla
/// </summary>
public class CrearOrdenService
{
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
            throw new ArgumentException("La orden requiere una descripcion del objeto.", nameof(descripcionObjeto));
        }

        if (string.IsNullOrWhiteSpace(descripcionProblema))
        {
            throw new ArgumentException("La orden requiere una descripcion del problema.", nameof(descripcionProblema));
        }

        if (costoDiagnostico < 0)
        {
            throw new ArgumentException("El costo de diagnostico no puede ser negativo.", nameof(costoDiagnostico));
        }

        var orden = new Orden
        {
            Id = Guid.NewGuid(),
            Cliente = cliente,
            DescripcionObjeto = descripcionObjeto,
            DescripcionProblema = descripcionProblema,
            CostoDiagnostico = costoDiagnostico,
            Estado = EstadoOrden.Recepcion,
            FechaRecepcion = DateTime.Now
        };

        _ordenRepository.Agregar(orden);

        return orden;
    }
}

namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: crear una nueva orden de trabajo (estado inicial: Recepcion).
/// TODO (issue #12): implementar para que CrearOrdenServiceTests pase. Reglas:
///   - cliente es obligatorio
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

    public Orden Ejecutar(Cliente cliente, string descripcionProblema, decimal costoDiagnostico)
    {
        throw new NotImplementedException("Implementar en el issue #12 (TDD: Red -> Green -> Refactor).");
    }
}

namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: subir o bajar la existencia de un repuesto en una cantidad. Reglas (ver AjustarExistenciaServiceTests):
///   - cualquier usuario puede incrementar; solo el administrador puede decrementar
///   - la cantidad debe ser mayor que cero
///   - la existencia no puede quedar negativa ni pasar del limite
/// </summary>
public class AjustarExistenciaService
{
    public const int LimiteExistencia = 1_000_000;

    private readonly IRepuestoRepository _repuestoRepository;

    public AjustarExistenciaService(IRepuestoRepository repuestoRepository)
    {
        _repuestoRepository = repuestoRepository;
    }

    public void Incrementar(PermisosUsuario permisos, Repuesto repuesto, int cantidad)
    {
        Validar(permisos, repuesto, cantidad);

        if (!new AccionesPermitidas(permisos).Permite(ModuloTabla.Repuestos, AccionTabla.Incrementar))
        {
            throw new InvalidOperationException("No tienes permiso para incrementar la existencia.");
        }

        if ((long)repuesto.Existencia + cantidad > LimiteExistencia)
        {
            throw new InvalidOperationException($"La existencia no puede pasar de {LimiteExistencia:N0}.");
        }

        repuesto.Existencia += cantidad;
        _repuestoRepository.Actualizar(repuesto);
    }

    public void Decrementar(PermisosUsuario permisos, Repuesto repuesto, int cantidad)
    {
        Validar(permisos, repuesto, cantidad);

        if (!new AccionesPermitidas(permisos).Permite(ModuloTabla.Repuestos, AccionTabla.Decrementar))
        {
            throw new InvalidOperationException("Solo un administrador puede decrementar la existencia.");
        }

        if (cantidad > repuesto.Existencia)
        {
            throw new InvalidOperationException($"No puedes quitar {cantidad}: solo hay {repuesto.Existencia} en existencia.");
        }

        repuesto.Existencia -= cantidad;
        _repuestoRepository.Actualizar(repuesto);
    }

    private static void Validar(PermisosUsuario permisos, Repuesto repuesto, int cantidad)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
        }

        if (repuesto is null)
        {
            throw new ArgumentException("Se requiere el repuesto.", nameof(repuesto));
        }

        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor que cero.", nameof(cantidad));
        }
    }
}

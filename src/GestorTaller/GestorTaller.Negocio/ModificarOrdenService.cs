namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: editar los datos de recepcion de una orden (DERCAS 6.5.1).
/// Reglas (ver ModificarOrdenServiceTests):
///   - solo un usuario con permiso para editar puede hacerlo
///   - las ordenes en estado Entregado no se editan (DERCAS 6.2.5 y CA-06)
///   - mismas validaciones que al crear: descripcion del objeto y del problema obligatorias,
///     costo de diagnostico no negativo (0 es valido)
///   - el cliente y el estado no cambian
///   - el total a cobrar se calcula con el costo de diagnostico, asi que se actualiza solo
/// </summary>
public class ModificarOrdenService
{
    private readonly IOrdenRepository _ordenRepository;

    public ModificarOrdenService(IOrdenRepository ordenRepository)
    {
        _ordenRepository = ordenRepository;
    }

    public void Ejecutar(PermisosUsuario permisos, Orden orden, string descripcionObjeto, string descripcionProblema, decimal costoDiagnostico)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
        }

        if (orden is null)
        {
            throw new ArgumentException("Se requiere la orden.", nameof(orden));
        }

        if (!permisos.PuedeEditar)
        {
            throw new InvalidOperationException("Solo un administrador puede editar ordenes.");
        }

        if (orden.Estado == EstadoOrden.Entregado)
        {
            throw new InvalidOperationException("Una orden entregada no se puede editar.");
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

        orden.DescripcionObjeto = descripcionObjeto.Trim();
        orden.DescripcionProblema = descripcionProblema.Trim();
        orden.CostoDiagnostico = costoDiagnostico;
        _ordenRepository.Actualizar(orden);
    }
}

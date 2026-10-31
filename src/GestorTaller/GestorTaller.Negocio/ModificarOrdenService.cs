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
///   - si no cambia nada, no se guarda y no se toca la fecha de ultima edicion (devuelve false);
///     si cambia algo, se guarda y se registra la fecha de ultima edicion (devuelve true)
/// </summary>
public class ModificarOrdenService
{
    private readonly IOrdenRepository _ordenRepository;

    public ModificarOrdenService(IOrdenRepository ordenRepository)
    {
        _ordenRepository = ordenRepository;
    }

    public bool Ejecutar(PermisosUsuario permisos, Orden orden, string descripcionObjeto, string descripcionProblema, decimal costoDiagnostico)
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
            throw new InvalidOperationException("Solo un administrador puede editar órdenes.");
        }

        if (orden.Estado == EstadoOrden.Entregado)
        {
            throw new InvalidOperationException("Una orden entregada no se puede editar.");
        }

        if (string.IsNullOrWhiteSpace(descripcionObjeto))
        {
            throw new ArgumentException("La orden requiere una descripción del objeto.", nameof(descripcionObjeto));
        }

        if (string.IsNullOrWhiteSpace(descripcionProblema))
        {
            throw new ArgumentException("La orden requiere una descripción del problema.", nameof(descripcionProblema));
        }

        if (costoDiagnostico < 0)
        {
            throw new ArgumentException("El costo de diagnóstico no puede ser negativo.", nameof(costoDiagnostico));
        }

        var objeto = descripcionObjeto.Trim();
        var problema = descripcionProblema.Trim();

        if (objeto == orden.DescripcionObjeto && problema == orden.DescripcionProblema && costoDiagnostico == orden.CostoDiagnostico)
        {
            return false;
        }

        orden.DescripcionObjeto = objeto;
        orden.DescripcionProblema = problema;
        orden.CostoDiagnostico = costoDiagnostico;
        orden.FechaUltimaEdicion = DateTime.Now;
        _ordenRepository.Actualizar(orden);

        return true;
    }
}

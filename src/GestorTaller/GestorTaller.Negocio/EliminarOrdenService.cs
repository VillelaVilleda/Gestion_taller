namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: eliminar una orden (DERCAS 6.5.1). Reglas (ver EliminarOrdenServiceTests):
///   - solo un usuario con permiso para eliminar puede hacerlo
///   - las ordenes en estado Entregado no se eliminan (DERCAS 6.2.5.2 y CA-06)
///   - si el cliente ya acepto la cotizacion, el inventario ya se desconto de los repuestos:
///     se devuelve a cada repuesto la cantidad descontada antes de borrar
///   - si la cotizacion se rechazo o aun no se acepta, el inventario no cambia
///   - la devolucion y el borrado se guardan juntos en IOrdenRepository.Eliminar
/// </summary>
public class EliminarOrdenService
{
    private readonly IOrdenRepository _ordenRepository;

    public EliminarOrdenService(IOrdenRepository ordenRepository)
    {
        _ordenRepository = ordenRepository;
    }

    public void Ejecutar(PermisosUsuario permisos, Orden orden)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
        }

        if (orden is null)
        {
            throw new ArgumentException("Se requiere la orden.", nameof(orden));
        }

        if (!permisos.PuedeEliminar)
        {
            throw new InvalidOperationException("Solo un administrador puede eliminar ordenes.");
        }

        if (orden.Estado == EstadoOrden.Entregado)
        {
            throw new InvalidOperationException("Una orden entregada no se puede eliminar.");
        }

        if (orden.CotizacionAceptada)
        {
            foreach (var asignado in orden.Repuestos)
            {
                asignado.Repuesto.Existencia += asignado.Cantidad;
            }
        }

        _ordenRepository.Eliminar(orden);
    }
}

namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: registrar la reparacion de una orden en estado EnReparacion,
/// avanzandola a Terminado. Reglas (ver RegistrarReparacionOrdenServiceTests):
///   - la orden debe existir
///   - la orden debe estar en estado EnReparacion (solo aplica si la
///     cotizacion fue aceptada)
///   - el empleado encargado es obligatorio
///   - el detalle de la reparacion es obligatorio
///   - al confirmar, se guardan empleado y detalle, y la orden avanza a Terminado
/// </summary>
public class RegistrarReparacionOrdenService
{
    private readonly AvanzarEstadoOrdenService _avanzarEstadoOrdenService;

    public RegistrarReparacionOrdenService(AvanzarEstadoOrdenService avanzarEstadoOrdenService)
    {
        _avanzarEstadoOrdenService = avanzarEstadoOrdenService;
    }

    public void Ejecutar(Orden orden, string empleadoReparacion, string detalleReparacion)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere una orden.", nameof(orden));
        }

        if (orden.Estado != EstadoOrden.EnReparacion)
        {
            throw new InvalidOperationException("Solo se puede registrar la reparacion de una orden en estado EnReparacion.");
        }

        if (string.IsNullOrWhiteSpace(empleadoReparacion))
        {
            throw new ArgumentException("Se requiere el empleado encargado de la reparacion.", nameof(empleadoReparacion));
        }

        if (string.IsNullOrWhiteSpace(detalleReparacion))
        {
            throw new ArgumentException("Se requiere el detalle de la reparacion.", nameof(detalleReparacion));
        }

        orden.EmpleadoReparacion = empleadoReparacion;
        orden.DetalleReparacion = detalleReparacion;

        _avanzarEstadoOrdenService.Ejecutar(orden);
    }
}
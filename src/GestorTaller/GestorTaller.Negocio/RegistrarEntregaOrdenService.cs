namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: registrar la entrega de una orden en estado Terminado,
/// avanzandola a Entregado. Reglas (ver RegistrarEntregaOrdenServiceTests):
///   - la orden debe existir
///   - la orden debe estar en estado Terminado
///   - el empleado encargado de la entrega es obligatorio
///   - el monto pagado no puede ser negativo (0 es valido: puede pasar
///     que el diagnostico sea gratuito y el cliente rechace la reparacion)
///   - al confirmar, se guardan empleado, monto y la fecha/hora actual,
///     y la orden avanza a Entregado
/// </summary>
public class RegistrarEntregaOrdenService
{
    private readonly AvanzarEstadoOrdenService _avanzarEstadoOrdenService;

    public RegistrarEntregaOrdenService(AvanzarEstadoOrdenService avanzarEstadoOrdenService)
    {
        _avanzarEstadoOrdenService = avanzarEstadoOrdenService;
    }

    public void Ejecutar(Orden orden, string empleadoEntrega, decimal montoPagado)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere una orden.", nameof(orden));
        }

        if (string.IsNullOrWhiteSpace(empleadoEntrega))
        {
            throw new ArgumentException("Se requiere el empleado encargado de la entrega.", nameof(empleadoEntrega));
        }

        if (montoPagado < 0)
        {
            throw new ArgumentException("El monto pagado no puede ser negativo.", nameof(montoPagado));
        }

        if (orden.Estado != EstadoOrden.Terminado)
        {
            throw new InvalidOperationException("Solo se puede registrar la entrega de una orden en estado Terminado.");
        }

        orden.EmpleadoEntrega = empleadoEntrega;
        orden.MontoPagado = montoPagado;
        orden.FechaEntrega = DateTime.Now;

        _avanzarEstadoOrdenService.Ejecutar(orden);
    }
}

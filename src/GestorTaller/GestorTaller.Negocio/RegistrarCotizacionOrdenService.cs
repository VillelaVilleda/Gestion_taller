namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: registrar la cotizacion de una orden en estado Diagnostico.
/// Reglas (ver RegistrarCotizacionOrdenServiceTests):
///   - la orden debe existir
///   - la orden debe estar en estado Diagnostico
///   - el empleado encargado es obligatorio
///   - el monto cotizado debe ser mayor a cero
///   - al confirmar, se guardan empleado, monto y la decision del cliente;
///     la orden pasa por Cotizacion y de ahi directo a EnReparacion
///     (si fue aceptada) o a Terminado (si fue rechazada, saltando
///     EnReparacion)
/// </summary>
public class RegistrarCotizacionOrdenService
{
    private readonly AvanzarEstadoOrdenService _avanzarEstadoOrdenService;

    public RegistrarCotizacionOrdenService(AvanzarEstadoOrdenService avanzarEstadoOrdenService)
    {
        _avanzarEstadoOrdenService = avanzarEstadoOrdenService;
    }

    public void Ejecutar(Orden orden, string empleadoCotizacion, decimal montoCotizado, bool aceptada)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere una orden.", nameof(orden));
        }

        if (orden.Estado != EstadoOrden.Diagnostico)
        {
            throw new InvalidOperationException("Solo se puede registrar la cotizacion de una orden en estado Diagnostico.");
        }

        if (string.IsNullOrWhiteSpace(empleadoCotizacion))
        {
            throw new ArgumentException("Se requiere el empleado encargado de la cotizacion.", nameof(empleadoCotizacion));
        }

        if (montoCotizado <= 0)
        {
            throw new ArgumentException("El monto cotizado debe ser mayor a cero.", nameof(montoCotizado));
        }

        orden.EmpleadoCotizacion = empleadoCotizacion;
        orden.MontoCotizado = montoCotizado;
        orden.CotizacionAceptada = aceptada;

        _avanzarEstadoOrdenService.Ejecutar(orden);          // Diagnostico -> Cotizacion
        _avanzarEstadoOrdenService.Ejecutar(orden, aceptada); // Cotizacion -> EnReparacion | Terminado
    }
}
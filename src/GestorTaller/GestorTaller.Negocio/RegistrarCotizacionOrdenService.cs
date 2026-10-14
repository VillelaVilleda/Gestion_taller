namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: registrar la cotizacion de una orden en estado Cotizacion
/// (el paso activo mientras se cotiza). Reglas (ver
/// RegistrarCotizacionOrdenServiceTests):
///   - la orden debe existir
///   - la orden debe estar en estado Cotizacion
///   - el empleado encargado es obligatorio
///   - el monto cotizado debe ser mayor a cero
///   - al confirmar, se guardan empleado, monto y la decision del cliente,
///     y la orden avanza directo a EnReparacion (si fue aceptada) o a
///     Terminado (si fue rechazada, saltando EnReparacion)
///   - si fue aceptada: se vuelve a comprobar la existencia de todos los repuestos
///     cotizados; si alguno no alcanza se lanza InvalidOperationException sin cambiar
///     nada; si alcanzan, se descuenta el inventario de todos juntos
///   - si fue rechazada: se descartan los repuestos cotizados (el inventario no cambia)
/// Guardar los cambios es responsabilidad de quien llama, con IOrdenRepository.Actualizar.
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

        if (orden.Estado != EstadoOrden.Cotizacion)
        {
            throw new InvalidOperationException("Solo se puede registrar la cotizacion de una orden en estado Cotizacion.");
        }

        if (string.IsNullOrWhiteSpace(empleadoCotizacion))
        {
            throw new ArgumentException("Se requiere el empleado encargado de la cotizacion.", nameof(empleadoCotizacion));
        }

        if (montoCotizado <= 0)
        {
            throw new ArgumentException("El monto cotizado debe ser mayor a cero.", nameof(montoCotizado));
        }

        // Si el cliente acepta, se vuelve a comprobar la existencia de TODOS los repuestos
        // antes de cambiar nada (todo o nada).
        if (aceptada)
        {
            ValidarExistencias(orden);
        }

        orden.EmpleadoCotizacion = empleadoCotizacion;
        orden.MontoCotizado = montoCotizado;
        orden.CotizacionAceptada = aceptada;

        if (aceptada)
        {
            foreach (var asignado in orden.Repuestos)
            {
                asignado.Repuesto.Existencia -= asignado.Cantidad;
            }
        }
        else
        {
            orden.Repuestos.Clear(); // el cliente rechazo: los repuestos cotizados se descartan
        }

        _avanzarEstadoOrdenService.Ejecutar(orden, aceptada); // Cotizacion -> EnReparacion | Terminado
    }

    private static void ValidarExistencias(Orden orden)
    {
        var faltantes = orden.Repuestos
            .Where(a => a.Cantidad > a.Repuesto.Existencia)
            .Select(a => $"{a.Repuesto.Nombre} (disponible: {a.Repuesto.Existencia}, necesita: {a.Cantidad})")
            .ToList();

        if (faltantes.Count > 0)
        {
            throw new InvalidOperationException(
                "No hay existencia suficiente para aceptar la cotizacion: " + string.Join("; ", faltantes) + ".");
        }
    }
}
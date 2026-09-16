namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: registrar el diagnostico de una orden en estado Diagnostico
/// (el paso activo mientras se diagnostica), avanzandola a Cotizacion.
/// Reglas (ver RegistrarDiagnosticoOrdenServiceTests):
///   - la orden debe existir
///   - la orden debe estar en estado Diagnostico
///   - el empleado encargado es obligatorio
///   - el detalle del diagnostico es obligatorio
///   - al confirmar, se guardan el empleado y el detalle, y la orden avanza a Cotizacion
/// </summary>
public class RegistrarDiagnosticoOrdenService
{
    private readonly AvanzarEstadoOrdenService _avanzarEstadoOrdenService;

    public RegistrarDiagnosticoOrdenService(AvanzarEstadoOrdenService avanzarEstadoOrdenService)
    {
        _avanzarEstadoOrdenService = avanzarEstadoOrdenService;
    }

    public void Ejecutar(Orden orden, string empleadoDiagnostico, string detalleDiagnostico)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere una orden.", nameof(orden));
        }

        if (orden.Estado != EstadoOrden.Diagnostico)
        {
            throw new InvalidOperationException("Solo se puede registrar el diagnostico de una orden en estado Diagnostico.");
        }

        if (string.IsNullOrWhiteSpace(empleadoDiagnostico))
        {
            throw new ArgumentException("Se requiere el empleado encargado del diagnostico.", nameof(empleadoDiagnostico));
        }

        if (string.IsNullOrWhiteSpace(detalleDiagnostico))
        {
            throw new ArgumentException("Se requiere el detalle del diagnostico.", nameof(detalleDiagnostico));
        }

        orden.EmpleadoDiagnostico = empleadoDiagnostico;
        orden.DetalleDiagnostico = detalleDiagnostico;

        _avanzarEstadoOrdenService.Ejecutar(orden);
    }
}
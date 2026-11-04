namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: registrar el diagnostico de una orden en estado Diagnostico
/// (el paso activo mientras se diagnostica), avanzandola a Cotizacion.
/// Reglas (ver RegistrarDiagnosticoOrdenServiceTests):
///   - la orden debe existir
///   - la orden debe estar en estado Diagnostico
///   - el empleado encargado es obligatorio
///   - el detalle del diagnostico es obligatorio, de hasta 500 caracteres (se recortan los espacios)
///   - al confirmar, se guardan el empleado y el detalle, y la orden avanza a Cotizacion
/// </summary>
public class RegistrarDiagnosticoOrdenService
{
    public const int LongitudMaximaDetalle = 500;

    private readonly AvanzarEstadoOrdenService _avanzarEstadoOrdenService;

    public RegistrarDiagnosticoOrdenService(AvanzarEstadoOrdenService avanzarEstadoOrdenService)
    {
        _avanzarEstadoOrdenService = avanzarEstadoOrdenService;
    }

    public void Ejecutar(Orden orden, Empleado empleadoDiagnostico, string detalleDiagnostico)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere una orden.", nameof(orden));
        }

        if (orden.Estado != EstadoOrden.Diagnostico)
        {
            throw new InvalidOperationException("Solo se puede registrar el diagnostico de una orden en estado Diagnostico.");
        }

        if (empleadoDiagnostico is null)
        {
            throw new ArgumentException("Se requiere el empleado encargado del diagnostico.", nameof(empleadoDiagnostico));
        }

        if (string.IsNullOrWhiteSpace(detalleDiagnostico))
        {
            throw new ArgumentException("Se requiere el detalle del diagnostico.", nameof(detalleDiagnostico));
        }

        var detalle = detalleDiagnostico.Trim();
        ValidarLongitudDetalle(detalle, nameof(detalleDiagnostico));

        orden.EmpleadoDiagnostico = empleadoDiagnostico;
        orden.DetalleDiagnostico = detalle;

        _avanzarEstadoOrdenService.Ejecutar(orden);
    }

    // Lo mismo que acepta la base de datos (detalle_diagnostico y detalle_reparacion).
    public static void ValidarLongitudDetalle(string detalle, string parametro)
    {
        if (detalle.Length > LongitudMaximaDetalle)
        {
            throw new ArgumentException($"El detalle no puede tener más de {LongitudMaximaDetalle} caracteres.", parametro);
        }
    }
}
namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: avanzar una orden al siguiente estado de su ciclo de vida.
/// Reglas (ver AvanzarEstadoOrdenServiceTests):
///   - Recepcion -> Diagnostico -> Cotizacion
///   - Cotizacion aceptada -> EnReparacion; rechazada -> Terminado (salta EnReparacion)
///   - EnReparacion -> Terminado -> Entregado
///   - Entregado es un estado final: no se puede avanzar mas
///   - cada avance queda registrado en Orden.HistorialEstados
/// </summary>
public class AvanzarEstadoOrdenService
{
    public void Ejecutar(Orden orden, bool cotizacionAceptada = true)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere una orden.", nameof(orden));
        }

        var siguienteEstado = orden.Estado switch
        {
            EstadoOrden.Recepcion => EstadoOrden.Diagnostico,
            EstadoOrden.Diagnostico => EstadoOrden.Cotizacion,
            EstadoOrden.Cotizacion => cotizacionAceptada ? EstadoOrden.EnReparacion : EstadoOrden.Terminado,
            EstadoOrden.EnReparacion => EstadoOrden.Terminado,
            EstadoOrden.Terminado => EstadoOrden.Entregado,
            EstadoOrden.Entregado => throw new InvalidOperationException("La orden ya fue entregada; no hay un estado siguiente."),
            _ => throw new InvalidOperationException("Estado de orden no reconocido.")
        };

        orden.Estado = siguienteEstado;
        orden.HistorialEstados.Add(siguienteEstado);
    }
}
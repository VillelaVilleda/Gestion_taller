namespace GestorTaller.Negocio;

/// <summary>
/// Estados por los que transita una orden de trabajo, en orden:
/// Recepcion -> Diagnostico -> Cotizacion -> (Aceptada: EnReparacion | Rechazada: Finalizado)
/// -> EnReparacion -> Terminado -> Finalizado
/// </summary>
public enum EstadoOrden
{
    Recepcion,
    Diagnostico,
    Cotizacion,
    EnReparacion,
    Terminado,
    Finalizado
}

namespace GestorTaller.Negocio;

/// <summary>
/// Dice si una etapa de la orden se puede editar (ver EdicionDeEtapaTests). Solo hay algo que editar
/// cuando la etapa ya se completó: la etapa en curso se llena con su propio formulario, y una etapa a
/// la que la orden no ha llegado no tiene datos. Las órdenes entregadas no se editan.
///   - Recepción: siempre, mientras la orden no esté entregada
///   - Diagnóstico, Cotización y Reparación: si la orden pasó por esa etapa y ya no está en ella
///   - Terminado y Entregado: no se editan
/// </summary>
public static class EdicionDeEtapa
{
    public static bool PuedeEditar(Orden orden, EstadoOrden etapa)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere la orden.", nameof(orden));
        }

        if (orden.Estado == EstadoOrden.Entregado)
        {
            return false;
        }

        return etapa switch
        {
            EstadoOrden.Recepcion => true,
            EstadoOrden.Diagnostico or EstadoOrden.Cotizacion or EstadoOrden.EnReparacion => orden.HistorialEstados.Contains(etapa) && orden.Estado != etapa,
            _ => false
        };
    }
}

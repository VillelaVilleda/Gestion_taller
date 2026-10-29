namespace GestorTaller.Negocio;

/// <summary>
/// Separa las órdenes para el listado (ver ClasificadorOrdenesTests): una orden está cerrada cuando ya
/// fue entregada; cualquier otra etapa, incluida Terminado (lista pero sin entregar), es pendiente.
/// </summary>
public static class ClasificadorOrdenes
{
    public static bool EstaCerrada(Orden orden)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere la orden.", nameof(orden));
        }

        return orden.Estado == EstadoOrden.Entregado;
    }

    public static IReadOnlyList<Orden> Pendientes(IEnumerable<Orden> ordenes) =>
        ordenes.Where(o => !EstaCerrada(o)).ToList();

    public static IReadOnlyList<Orden> Cerradas(IEnumerable<Orden> ordenes) =>
        ordenes.Where(EstaCerrada).ToList();
}

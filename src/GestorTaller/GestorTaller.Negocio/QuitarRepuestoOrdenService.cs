namespace GestorTaller.Negocio;

/// <summary>
/// Quita (total o parcialmente) un repuesto de la cotizacion de una orden que esta en
/// Cotizacion. Es el inverso de <see cref="AsignarRepuestoOrdenService"/>: como el repuesto
/// aun no se habia descontado del inventario, quitarlo no modifica la existencia.
/// Solo modifica la orden; quien lo invoca se encarga de persistirla.
/// </summary>
public class QuitarRepuestoOrdenService
{
    public void Ejecutar(Orden orden, Repuesto repuesto, int cantidad)
    {
        if (orden is null)
        {
            throw new ArgumentException("La orden es obligatoria.");
        }

        if (repuesto is null)
        {
            throw new ArgumentException("El repuesto es obligatorio.");
        }

        if (orden.Estado != EstadoOrden.Cotizacion)
        {
            throw new InvalidOperationException("Solo se pueden quitar repuestos de una orden en cotizacion.");
        }

        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor que cero.");
        }

        var asignado = orden.Repuestos.FirstOrDefault(r => r.RepuestoId == repuesto.Id);

        if (asignado is null)
        {
            throw new InvalidOperationException($"El repuesto {repuesto.Nombre} no esta en la orden.");
        }

        if (cantidad > asignado.Cantidad)
        {
            throw new InvalidOperationException(
                $"Solo hay {asignado.Cantidad} de {repuesto.Nombre} en la orden.");
        }

        asignado.Cantidad -= cantidad;

        if (asignado.Cantidad == 0)
        {
            orden.Repuestos.Remove(asignado);
        }
    }
}

namespace GestorTaller.Negocio;

/// <summary>
/// Quita (total o parcialmente) un repuesto ya asignado a una orden que esta
/// EnReparacion y devuelve la cantidad al inventario. Es el inverso de
/// <see cref="AsignarRepuestoOrdenService"/>. Solo modifica los objetos de
/// dominio; quien lo invoca se encarga de persistirlos.
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

        if (orden.Estado != EstadoOrden.EnReparacion)
        {
            throw new InvalidOperationException("Solo se pueden quitar repuestos de una orden en reparacion.");
        }

        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor que cero.");
        }

        var asignado = orden.Repuestos.FirstOrDefault(r => r.RepuestoId == repuesto.Id);

        if (asignado is null)
        {
            throw new InvalidOperationException($"El repuesto {repuesto.Nombre} no esta asignado a la orden.");
        }

        if (cantidad > asignado.Cantidad)
        {
            throw new InvalidOperationException(
                $"Solo hay {asignado.Cantidad} de {repuesto.Nombre} asignados a la orden.");
        }

        asignado.Cantidad -= cantidad;

        if (asignado.Cantidad == 0)
        {
            orden.Repuestos.Remove(asignado);
        }

        repuesto.Existencia += cantidad;
    }
}

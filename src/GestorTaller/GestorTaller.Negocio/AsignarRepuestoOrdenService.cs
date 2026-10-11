namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: asignar un repuesto (con cantidad) a una orden en reparacion y
/// descontar esa cantidad de la existencia del repuesto (DERCAS 6.2.4.2, 6.2.4.3, 6.3.2).
/// Reglas (ver AsignarRepuestoOrdenServiceTests):
///   - la orden y el repuesto deben existir
///   - la orden debe estar en estado EnReparacion
///   - la cantidad debe ser mayor a cero
///   - la cantidad no puede superar la existencia disponible
///   - si el repuesto ya estaba asignado a la orden, se suma la cantidad
///   - todo se valida antes de modificar: si falla, no cambia nada
/// Guardar los cambios (orden y repuesto) es responsabilidad de quien llama,
/// con IOrdenRepository.Actualizar.
/// </summary>
public class AsignarRepuestoOrdenService
{
    public void Ejecutar(Orden orden, Repuesto repuesto, int cantidad)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere una orden.", nameof(orden));
        }

        if (repuesto is null)
        {
            throw new ArgumentException("Se requiere un repuesto.", nameof(repuesto));
        }

        if (orden.Estado != EstadoOrden.EnReparacion)
        {
            throw new InvalidOperationException("Solo se pueden asignar repuestos a una orden en estado EnReparacion.");
        }

        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor a cero.", nameof(cantidad));
        }

        if (cantidad > repuesto.Existencia)
        {
            throw new InvalidOperationException(
                $"No hay existencia suficiente de {repuesto.Nombre}. Disponible: {repuesto.Existencia}.");
        }

        var asignado = orden.Repuestos.FirstOrDefault(r => r.RepuestoId == repuesto.Id);

        if (asignado is null)
        {
            orden.Repuestos.Add(new OrdenRepuesto
            {
                OrdenId = orden.Id,
                RepuestoId = repuesto.Id,
                Repuesto = repuesto,
                Cantidad = cantidad
            });
        }
        else
        {
            asignado.Cantidad += cantidad;
        }

        repuesto.Existencia -= cantidad;
    }
}

namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: agregar un repuesto (con cantidad) a la cotizacion de una orden, para que
/// el cliente conozca el total antes de aceptar. NO descuenta el inventario: eso ocurre
/// cuando el cliente acepta la cotizacion.
/// Reglas (ver AsignarRepuestoOrdenServiceTests):
///   - la orden y el repuesto deben existir
///   - la orden debe estar en estado Cotizacion
///   - la cantidad debe ser mayor a cero
///   - la cantidad total (lo ya agregado mas lo nuevo) no puede superar la existencia
///   - si el repuesto ya estaba en la orden, se suma la cantidad y se conserva el precio original
///   - al agregarlo por primera vez se guarda el precio unitario de ese momento
///   - todo se valida antes de modificar: si falla, no cambia nada
/// Guardar los cambios es responsabilidad de quien llama, con IOrdenRepository.Actualizar.
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

        if (orden.Estado != EstadoOrden.Cotizacion)
        {
            throw new InvalidOperationException("Solo se pueden agregar repuestos a una orden en estado Cotizacion.");
        }

        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor a cero.", nameof(cantidad));
        }

        var asignado = orden.Repuestos.FirstOrDefault(r => r.RepuestoId == repuesto.Id);
        var cantidadTotal = (asignado?.Cantidad ?? 0) + cantidad;

        if (cantidadTotal > repuesto.Existencia)
        {
            throw new InvalidOperationException(
                $"No hay existencia suficiente de {repuesto.Nombre}. Disponible: {repuesto.Existencia}.");
        }

        if (asignado is null)
        {
            orden.Repuestos.Add(new OrdenRepuesto
            {
                OrdenId = orden.Id,
                RepuestoId = repuesto.Id,
                Repuesto = repuesto,
                Cantidad = cantidad,
                PrecioUnitario = repuesto.Precio
            });
        }
        else
        {
            asignado.Cantidad = cantidadTotal;
        }
    }
}

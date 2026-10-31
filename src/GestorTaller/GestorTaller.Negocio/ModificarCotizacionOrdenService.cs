namespace GestorTaller.Negocio;

/// <summary>Un repuesto de la cotización con la cantidad que debe quedar en la orden.</summary>
public record RepuestoCotizado(Repuesto Repuesto, int Cantidad);

/// <summary>
/// Caso de uso: corregir la cotización ya registrada de una orden (empleado, mano de obra y repuestos)
/// y ajustar el inventario según lo que cambie. Reglas (ver ModificarCotizacionOrdenServiceTests):
///   - solo un usuario con permiso para editar puede hacerlo
///   - la etapa debe poder editarse (ver EdicionDeEtapa): la cotización ya se registró y la orden no está entregada
///   - el empleado es obligatorio y el monto de mano de obra debe ser mayor a cero
///   - la decisión del cliente no cambia; si la cotización fue rechazada, no lleva repuestos
///   - la lista recibida es la que queda en la orden: cada cantidad debe ser mayor a cero y un repuesto no se repite
///   - el inventario se ajusta con la diferencia: si sube la cantidad se descuenta lo que sube, si baja o se
///     quita el repuesto se devuelve lo que sobra; un repuesto nuevo se descuenta completo
///   - los repuestos que ya estaban conservan su precio original; los nuevos usan el precio vigente
///   - todo se valida antes de modificar: si falta existencia para algún repuesto, no cambia nada
///   - si no cambia nada, no se guarda ni se toca la fecha de última edición (devuelve false);
///     si cambia algo, se guarda y se registra la fecha de última edición (devuelve true)
/// </summary>
public class ModificarCotizacionOrdenService
{
    private readonly IOrdenRepository _ordenRepository;

    public ModificarCotizacionOrdenService(IOrdenRepository ordenRepository)
    {
        _ordenRepository = ordenRepository;
    }

    public bool Ejecutar(PermisosUsuario permisos, Orden orden, Empleado empleado, decimal montoCotizado, IReadOnlyList<RepuestoCotizado> repuestos)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
        }

        if (orden is null)
        {
            throw new ArgumentException("Se requiere la orden.", nameof(orden));
        }

        if (!permisos.PuedeEditar)
        {
            throw new InvalidOperationException("Solo un administrador puede editar la cotización.");
        }

        if (!EdicionDeEtapa.PuedeEditar(orden, EstadoOrden.Cotizacion))
        {
            throw new InvalidOperationException(orden.Estado == EstadoOrden.Entregado
                ? "Una orden entregada no se puede editar."
                : "La cotización de esta orden todavía no se puede editar.");
        }

        if (empleado is null)
        {
            throw new ArgumentException("Se requiere el empleado encargado de la cotización.", nameof(empleado));
        }

        if (montoCotizado <= 0)
        {
            throw new ArgumentException("El monto cotizado debe ser mayor a cero.", nameof(montoCotizado));
        }

        if (repuestos is null)
        {
            throw new ArgumentException("Se requiere la lista de repuestos.", nameof(repuestos));
        }

        if (repuestos.Any(r => r.Cantidad <= 0))
        {
            throw new ArgumentException("La cantidad de cada repuesto debe ser mayor a cero.", nameof(repuestos));
        }

        if (repuestos.GroupBy(r => r.Repuesto.Id).Any(g => g.Count() > 1))
        {
            throw new ArgumentException("Un repuesto no puede repetirse en la cotización.", nameof(repuestos));
        }

        if (!orden.CotizacionAceptada && repuestos.Count > 0)
        {
            throw new InvalidOperationException("La cotización fue rechazada: no lleva repuestos.");
        }

        var actuales = orden.Repuestos.ToDictionary(r => r.RepuestoId);
        var nuevos = repuestos.ToDictionary(r => r.Repuesto.Id);

        // Diferencia por repuesto: positiva = hay que descontar más; negativa = se devuelve.
        var diferencias = actuales.Keys.Union(nuevos.Keys).ToDictionary(
            id => id,
            id => (nuevos.TryGetValue(id, out var n) ? n.Cantidad : 0) - (actuales.TryGetValue(id, out var a) ? a.Cantidad : 0));

        var faltantes = diferencias
            .Where(d => d.Value > 0)
            .Select(d => (Repuesto: actuales.TryGetValue(d.Key, out var a) ? a.Repuesto : nuevos[d.Key].Repuesto, Mas: d.Value))
            .Where(x => x.Mas > x.Repuesto.Existencia)
            .Select(x => $"{x.Repuesto.Nombre} (disponible: {x.Repuesto.Existencia}, necesita: {x.Mas} más)")
            .ToList();

        if (faltantes.Count > 0)
        {
            throw new InvalidOperationException("No hay existencia suficiente: " + string.Join("; ", faltantes) + ".");
        }

        var hayCambios = empleado.Id != orden.EmpleadoCotizacion?.Id
            || montoCotizado != orden.MontoCotizado
            || diferencias.Values.Any(d => d != 0);

        if (!hayCambios)
        {
            return false;
        }

        foreach (var (id, diferencia) in diferencias.Where(d => d.Value != 0))
        {
            if (actuales.TryGetValue(id, out var asignado))
            {
                asignado.Repuesto.Existencia -= diferencia;

                if (nuevos.TryGetValue(id, out var nuevo))
                {
                    asignado.Cantidad = nuevo.Cantidad;
                }
                else
                {
                    orden.Repuestos.Remove(asignado);
                }
            }
            else
            {
                var nuevo = nuevos[id];
                nuevo.Repuesto.Existencia -= diferencia;
                orden.Repuestos.Add(new OrdenRepuesto
                {
                    OrdenId = orden.Id,
                    RepuestoId = id,
                    Repuesto = nuevo.Repuesto,
                    Cantidad = nuevo.Cantidad,
                    PrecioUnitario = nuevo.Repuesto.Precio
                });
            }
        }

        orden.EmpleadoCotizacion = empleado;
        orden.MontoCotizado = montoCotizado;
        orden.FechaUltimaEdicion = DateTime.Now;
        _ordenRepository.Actualizar(orden);

        return true;
    }
}

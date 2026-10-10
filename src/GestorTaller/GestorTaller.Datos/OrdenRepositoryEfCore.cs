using GestorTaller.Negocio;
using Microsoft.EntityFrameworkCore;

namespace GestorTaller.Datos;

public class OrdenRepositoryEfCore : IOrdenRepository
{
    private readonly GestorTallerDbContext _dbContext;

    public OrdenRepositoryEfCore(GestorTallerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Agregar(Orden orden)
    {
        _dbContext.Ordenes.Add(orden);
        GuardarHistorial(orden);
        _dbContext.SaveChanges();
    }

    public Orden? ObtenerPorId(Guid id)
    {
        var orden = _dbContext.Ordenes
            .Include(o => o.Cliente)
            .Include(o => o.Repuestos).ThenInclude(r => r.Repuesto)
            .FirstOrDefault(o => o.Id == id);

        if (orden is null)
        {
            return null;
        }

        orden.HistorialEstados = CargarHistorial(id);
        return orden;
    }

    public IReadOnlyList<Orden> ObtenerTodas()
    {
        var ordenes = _dbContext.Ordenes
            .Include(o => o.Cliente)
            .Include(o => o.Repuestos).ThenInclude(r => r.Repuesto)
            .ToList();

        var historialesPorOrden = _dbContext.Set<HistorialEstadoOrden>()
            .OrderBy(h => h.OrdenPosicion)
            .ToList()
            .GroupBy(h => h.OrdenId)
            .ToDictionary(g => g.Key, g => g.Select(h => h.Estado).ToList());

        foreach (var orden in ordenes)
        {
            orden.HistorialEstados = historialesPorOrden.TryGetValue(orden.Id, out var historial)
                ? historial
                : new List<EstadoOrden>();
        }

        return ordenes;
    }

    private void GuardarHistorial(Orden orden)
    {
        for (var posicion = 0; posicion < orden.HistorialEstados.Count; posicion++)
        {
            _dbContext.Set<HistorialEstadoOrden>().Add(new HistorialEstadoOrden
            {
                OrdenId = orden.Id,
                Estado = orden.HistorialEstados[posicion],
                OrdenPosicion = posicion
            });
        }
    }

    private List<EstadoOrden> CargarHistorial(Guid ordenId)
    {
        return _dbContext.Set<HistorialEstadoOrden>()
            .Where(h => h.OrdenId == ordenId)
            .OrderBy(h => h.OrdenPosicion)
            .Select(h => h.Estado)
            .ToList();
    }

    public void Actualizar(Orden orden)
    {
        _dbContext.Ordenes.Update(orden);
        SincronizarHistorial(orden);
        SincronizarRepuestos(orden);
        _dbContext.SaveChanges();
    }

    // Update() marca como "modificados" los hijos que ya traen llave, incluso los nuevos
    // (la llave de OrdenRepuesto la ponemos nosotros), asi que distinguimos cuales son nuevos.
    private void SincronizarRepuestos(Orden orden)
    {
        foreach (var asignado in orden.Repuestos)
        {
            var yaGuardado = _dbContext.Set<OrdenRepuesto>()
                .AsNoTracking()
                .Any(r => r.OrdenId == orden.Id && r.RepuestoId == asignado.RepuestoId);

            _dbContext.Entry(asignado).State = yaGuardado ? EntityState.Modified : EntityState.Added;
        }
    }

    private void SincronizarHistorial(Orden orden)
    {
        var cantidadGuardada = _dbContext.Set<HistorialEstadoOrden>().Count(h => h.OrdenId == orden.Id);

        for (var posicion = cantidadGuardada; posicion < orden.HistorialEstados.Count; posicion++)
        {
            _dbContext.Set<HistorialEstadoOrden>().Add(new HistorialEstadoOrden
            {
                OrdenId = orden.Id,
                Estado = orden.HistorialEstados[posicion],
                OrdenPosicion = posicion
            });
        }
    }
}
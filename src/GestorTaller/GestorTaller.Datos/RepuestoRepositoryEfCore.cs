using GestorTaller.Negocio;

namespace GestorTaller.Datos;

public class RepuestoRepositoryEfCore : IRepuestoRepository
{
    private readonly GestorTallerDbContext _dbContext;

    public RepuestoRepositoryEfCore(GestorTallerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Agregar(Repuesto repuesto)
    {
        _dbContext.Repuestos.Add(repuesto);
        _dbContext.SaveChanges();
    }

    public Repuesto? ObtenerPorId(Guid id)
    {
        return _dbContext.Repuestos.Find(id);
    }

    public IReadOnlyList<Repuesto> ObtenerTodos()
    {
        return _dbContext.Repuestos.ToList();
    }
}
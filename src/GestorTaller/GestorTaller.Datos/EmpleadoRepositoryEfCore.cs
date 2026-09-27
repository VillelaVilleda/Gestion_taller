using GestorTaller.Negocio;

namespace GestorTaller.Datos;

public class EmpleadoRepositoryEfCore : IEmpleadoRepository
{
    private readonly GestorTallerDbContext _dbContext;

    public EmpleadoRepositoryEfCore(GestorTallerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Agregar(Empleado empleado)
    {
        _dbContext.Empleados.Add(empleado);
        _dbContext.SaveChanges();
    }

    public Empleado? ObtenerPorId(Guid id)
    {
        return _dbContext.Empleados.Find(id);
    }

    public Empleado? ObtenerPorNombreUsuario(string nombreUsuario)
    {
        return _dbContext.Empleados.FirstOrDefault(e => e.NombreUsuario == nombreUsuario);
    }

    public IReadOnlyList<Empleado> ObtenerTodos()
    {
        return _dbContext.Empleados.ToList();
    }
}

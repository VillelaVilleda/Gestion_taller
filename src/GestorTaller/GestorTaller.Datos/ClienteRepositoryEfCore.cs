using GestorTaller.Negocio;

namespace GestorTaller.Datos;

public class ClienteRepositoryEfCore : IClienteRepository
{
    private readonly GestorTallerDbContext _dbContext;

    public ClienteRepositoryEfCore(GestorTallerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Agregar(Cliente cliente)
    {
        _dbContext.Clientes.Add(cliente);
        _dbContext.SaveChanges();
    }

    public Cliente? ObtenerPorId(Guid id)
    {
        return _dbContext.Clientes.Find(id);
    }

    public IReadOnlyList<Cliente> ObtenerTodos()
    {
        return _dbContext.Clientes.ToList();
    }
}

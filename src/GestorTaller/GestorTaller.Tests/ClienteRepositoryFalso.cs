using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Repositorio de clientes en memoria para probar los servicios sin base de datos.
internal class ClienteRepositoryFalso : IClienteRepository
{
    public List<Cliente> Guardados { get; } = new();

    public int Actualizaciones { get; private set; }

    public void Agregar(Cliente cliente) => Guardados.Add(cliente);

    public void Actualizar(Cliente cliente) => Actualizaciones++;

    public void Eliminar(Cliente cliente) => Guardados.Remove(cliente);

    public Cliente? ObtenerPorId(Guid id) => Guardados.FirstOrDefault(c => c.Id == id);

    public IReadOnlyList<Cliente> ObtenerTodos() => Guardados;
}

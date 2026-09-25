namespace GestorTaller.Negocio;

/// <summary>
/// Contrato para guardar y consultar clientes, sin atarlo a una tecnologia
/// de almacenamiento especifica (mismo patron que IOrdenRepository).
/// </summary>
public interface IClienteRepository
{
    void Agregar(Cliente cliente);
    Cliente? ObtenerPorId(Guid id);
    IReadOnlyList<Cliente> ObtenerTodos();
}

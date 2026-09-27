namespace GestorTaller.Negocio;

/// <summary>
/// Contrato para guardar y consultar repuestos, sin atarlo a una tecnologia
/// de almacenamiento especifica (mismo patron que IOrdenRepository/IClienteRepository).
/// </summary>
public interface IRepuestoRepository
{
    void Agregar(Repuesto repuesto);
    Repuesto? ObtenerPorId(Guid id);
    IReadOnlyList<Repuesto> ObtenerTodos();
}
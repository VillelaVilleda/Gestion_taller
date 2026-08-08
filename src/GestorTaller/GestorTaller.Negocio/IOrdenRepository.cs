namespace GestorTaller.Negocio;

/// <summary>
/// Contrato para guardar y consultar ordenes, sin atarlo a una tecnologia
/// de almacenamiento especifica. Permite simularlo en pruebas automatizadas
/// y sustituir su implementacion (memoria -> PostgreSQL) sin modificar
/// el resto de la logica de negocio.
/// </summary>
public interface IOrdenRepository
{
    void Agregar(Orden orden);
    Orden? ObtenerPorId(Guid id);
    IReadOnlyList<Orden> ObtenerTodas();
}

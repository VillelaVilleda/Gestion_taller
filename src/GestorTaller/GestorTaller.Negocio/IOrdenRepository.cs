namespace GestorTaller.Negocio;

public interface IOrdenRepository
{
    void Agregar(Orden orden);
    void Actualizar(Orden orden);
    Orden? ObtenerPorId(Guid id);
    IReadOnlyList<Orden> ObtenerTodas();
}
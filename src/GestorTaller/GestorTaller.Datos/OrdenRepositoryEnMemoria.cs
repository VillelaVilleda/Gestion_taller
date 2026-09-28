using GestorTaller.Negocio;

namespace GestorTaller.Datos;

/// <summary>
/// Implementacion en memoria de IOrdenRepository. Permite que la aplicacion
/// funcione sin depender de PostgreSQL todavia. Es temporal: los datos se
/// pierden al cerrar el programa. Se reemplaza en un sprint posterior.
/// </summary>
public class OrdenRepositoryEnMemoria : IOrdenRepository
{
    private readonly List<Orden> _ordenes = new();

    public void Agregar(Orden orden)
    {
        _ordenes.Add(orden);
    }

    public void Actualizar(Orden orden)
    {
        // No hace falta nada: la lista en memoria guarda la misma referencia
        // que el llamador ya modifico.
    }

    public Orden? ObtenerPorId(Guid id)
    {
        return _ordenes.FirstOrDefault(o => o.Id == id);
    }

    public IReadOnlyList<Orden> ObtenerTodas()
    {
        return _ordenes.AsReadOnly();
    }
}

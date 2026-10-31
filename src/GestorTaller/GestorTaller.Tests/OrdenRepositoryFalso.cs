using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Repositorio de órdenes en memoria que cuenta cuántas veces se guardó una orden.
public class OrdenRepositoryFalso : IOrdenRepository
{
    public List<Orden> Guardadas { get; } = new();

    public int Actualizaciones { get; private set; }

    public void Agregar(Orden orden) => Guardadas.Add(orden);

    public void Actualizar(Orden orden) => Actualizaciones++;

    public void Eliminar(Orden orden) => Guardadas.Remove(orden);

    public Orden? ObtenerPorId(Guid id) => Guardadas.FirstOrDefault(o => o.Id == id);

    public IReadOnlyList<Orden> ObtenerTodas() => Guardadas;
}

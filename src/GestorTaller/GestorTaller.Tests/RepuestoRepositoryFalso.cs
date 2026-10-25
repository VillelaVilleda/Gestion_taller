using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Repositorio de repuestos en memoria para probar los servicios sin base de datos.
internal class RepuestoRepositoryFalso : IRepuestoRepository
{
    public List<Repuesto> Guardados { get; } = new();

    public int Actualizaciones { get; private set; }

    public void Agregar(Repuesto repuesto) => Guardados.Add(repuesto);

    public void Actualizar(Repuesto repuesto) => Actualizaciones++;

    public void Eliminar(Repuesto repuesto) => Guardados.Remove(repuesto);

    public Repuesto? ObtenerPorId(Guid id) => Guardados.FirstOrDefault(r => r.Id == id);

    public IReadOnlyList<Repuesto> ObtenerTodos() => Guardados;
}

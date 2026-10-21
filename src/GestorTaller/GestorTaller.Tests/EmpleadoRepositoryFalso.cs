using GestorTaller.Negocio;

namespace GestorTaller.Tests;

// Repositorio de empleados en memoria para probar los servicios sin base de datos.
internal class EmpleadoRepositoryFalso : IEmpleadoRepository
{
    public List<Empleado> Guardados { get; } = new();

    public int Actualizaciones { get; private set; }

    public void Agregar(Empleado empleado) => Guardados.Add(empleado);

    public void Actualizar(Empleado empleado) => Actualizaciones++;

    public Empleado? ObtenerPorId(Guid id) => Guardados.FirstOrDefault(e => e.Id == id);

    public Empleado? ObtenerPorNombreUsuario(string nombreUsuario) => Guardados.FirstOrDefault(e => e.NombreUsuario == nombreUsuario);

    public IReadOnlyList<Empleado> ObtenerTodos() => Guardados;
}

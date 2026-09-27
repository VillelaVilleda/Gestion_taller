namespace GestorTaller.Negocio;

/// <summary>
/// Contrato para guardar y consultar empleados, sin atarlo a una tecnologia
/// de almacenamiento especifica (mismo patron que IOrdenRepository/IClienteRepository).
/// </summary>
public interface IEmpleadoRepository
{
    void Agregar(Empleado empleado);
    Empleado? ObtenerPorId(Guid id);
    Empleado? ObtenerPorNombreUsuario(string nombreUsuario);
    IReadOnlyList<Empleado> ObtenerTodos();
}

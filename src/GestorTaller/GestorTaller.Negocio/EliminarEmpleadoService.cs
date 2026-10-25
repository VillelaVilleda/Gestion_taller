namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: eliminar un empleado (DERCAS 6.5.1). Reglas (ver EliminarEmpleadoServiceTests):
///   - solo un usuario con permiso para administrar empleados puede hacerlo
///   - nadie puede eliminarse a si mismo
///   - siempre debe quedar al menos un administrador
///   - no se puede eliminar un empleado que es responsable de alguna orden
/// </summary>
public class EliminarEmpleadoService
{
    private readonly IEmpleadoRepository _empleadoRepository;
    private readonly IOrdenRepository _ordenRepository;

    public EliminarEmpleadoService(IEmpleadoRepository empleadoRepository, IOrdenRepository ordenRepository)
    {
        _empleadoRepository = empleadoRepository;
        _ordenRepository = ordenRepository;
    }

    public void Ejecutar(Empleado usuarioActual, Empleado empleado)
    {
        if (usuarioActual is null)
        {
            throw new ArgumentException("Se requiere el usuario actual.", nameof(usuarioActual));
        }

        if (empleado is null)
        {
            throw new ArgumentException("Se requiere el empleado.", nameof(empleado));
        }

        if (!new PermisosUsuario(usuarioActual).PuedeAdministrarEmpleados)
        {
            throw new InvalidOperationException("Solo un administrador puede eliminar empleados.");
        }

        if (empleado.Id == usuarioActual.Id)
        {
            throw new InvalidOperationException("No puedes eliminarte a ti mismo.");
        }

        if (empleado.EsAdministrador && !_empleadoRepository.ObtenerTodos().Any(e => e.EsAdministrador && e.Id != empleado.Id))
        {
            throw new InvalidOperationException("Debe quedar al menos un administrador.");
        }

        if (_ordenRepository.ObtenerTodas().Any(o => EsResponsable(o, empleado)))
        {
            throw new InvalidOperationException("No se puede eliminar el empleado porque es responsable de alguna orden.");
        }

        _empleadoRepository.Eliminar(empleado);
    }

    private static bool EsResponsable(Orden orden, Empleado empleado) =>
        orden.EmpleadoDiagnostico?.Id == empleado.Id
        || orden.EmpleadoCotizacion?.Id == empleado.Id
        || orden.EmpleadoReparacion?.Id == empleado.Id
        || orden.EmpleadoEntrega?.Id == empleado.Id;
}

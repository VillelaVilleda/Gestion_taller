namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: editar el nombre de un empleado y otorgarle o quitarle el rol de
/// administrador (DERCAS 6.4.2). Reglas (ver ModificarEmpleadoServiceTests):
///   - solo un usuario con permiso para administrar empleados puede hacerlo
///   - el nombre es obligatorio, de hasta 150 caracteres (se recortan los espacios)
///   - nadie puede quitarse el rol de administrador a si mismo
///   - siempre debe quedar al menos un administrador
///   - el usuario (nombre de inicio de sesion) no cambia
/// </summary>
public class ModificarEmpleadoService
{
    private readonly IEmpleadoRepository _empleadoRepository;

    public ModificarEmpleadoService(IEmpleadoRepository empleadoRepository)
    {
        _empleadoRepository = empleadoRepository;
    }

    public void Ejecutar(Empleado usuarioActual, Empleado empleado, string nombre, bool esAdministrador)
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
            throw new InvalidOperationException("Solo un administrador puede modificar empleados.");
        }

        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("Se requiere el nombre del empleado.", nameof(nombre));
        }

        var nombreLimpio = nombre.Trim();

        if (nombreLimpio.Length > CrearEmpleadoService.LongitudMaximaNombre)
        {
            throw new ArgumentException($"El nombre no puede tener mas de {CrearEmpleadoService.LongitudMaximaNombre} caracteres.", nameof(nombre));
        }

        if (empleado.EsAdministrador && !esAdministrador)
        {
            if (empleado.Id == usuarioActual.Id)
            {
                throw new InvalidOperationException("No puedes quitarte a ti mismo el rol de administrador.");
            }

            if (!_empleadoRepository.ObtenerTodos().Any(e => e.EsAdministrador && e.Id != empleado.Id))
            {
                throw new InvalidOperationException("Debe quedar al menos un administrador.");
            }
        }

        empleado.Nombre = nombreLimpio;
        empleado.EsAdministrador = esAdministrador;
        _empleadoRepository.Actualizar(empleado);
    }
}

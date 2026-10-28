namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: editar el nombre y el usuario de un empleado, y otorgarle o quitarle el rol de
/// administrador (DERCAS 6.4.1 y 6.4.2). Reglas (ver ModificarEmpleadoServiceTests):
///   - solo un usuario con permiso para administrar empleados puede hacerlo
///   - el nombre es obligatorio, de hasta 150 caracteres (se recortan los espacios)
///   - el usuario es obligatorio, sin espacios, de hasta 50 caracteres y unico sin importar mayusculas
///   - el usuario se guarda en minusculas
///   - nadie puede quitarse el rol de administrador a si mismo
///   - siempre debe quedar al menos un administrador
/// </summary>
public class ModificarEmpleadoService
{
    public const int LongitudMaximaUsuario = 50;

    private readonly IEmpleadoRepository _empleadoRepository;

    public ModificarEmpleadoService(IEmpleadoRepository empleadoRepository)
    {
        _empleadoRepository = empleadoRepository;
    }

    public void Ejecutar(Empleado usuarioActual, Empleado empleado, string nombre, string nombreUsuario, bool esAdministrador)
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
            throw new ArgumentException($"El nombre no puede tener más de {CrearEmpleadoService.LongitudMaximaNombre} caracteres.", nameof(nombre));
        }

        if (string.IsNullOrWhiteSpace(nombreUsuario))
        {
            throw new ArgumentException("Se requiere el usuario del empleado.", nameof(nombreUsuario));
        }

        var usuarioLimpio = nombreUsuario.Trim().ToLowerInvariant();

        if (usuarioLimpio.Any(char.IsWhiteSpace))
        {
            throw new ArgumentException("El usuario no puede tener espacios.", nameof(nombreUsuario));
        }

        if (usuarioLimpio.Length > LongitudMaximaUsuario)
        {
            throw new ArgumentException($"El usuario no puede tener más de {LongitudMaximaUsuario} caracteres.", nameof(nombreUsuario));
        }

        if (_empleadoRepository.ObtenerTodos().Any(e => e.Id != empleado.Id && e.NombreUsuario.ToLowerInvariant() == usuarioLimpio))
        {
            throw new InvalidOperationException("Ya existe un empleado con ese usuario.");
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
        empleado.NombreUsuario = usuarioLimpio;
        empleado.EsAdministrador = esAdministrador;
        _empleadoRepository.Actualizar(empleado);
    }
}

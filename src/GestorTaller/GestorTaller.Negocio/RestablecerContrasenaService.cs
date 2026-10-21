namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: restablecer la contrasena de un empleado (DERCAS 6.4.1).
/// Reglas (ver RestablecerContrasenaServiceTests):
///   - solo un usuario con permiso para administrar empleados puede hacerlo
///   - se genera una contrasena temporal nueva, que se guarda solo con hash
///   - se devuelve la contrasena temporal en texto, que solo se conoce en este momento
/// </summary>
public class RestablecerContrasenaService
{
    private readonly IEmpleadoRepository _empleadoRepository;

    public RestablecerContrasenaService(IEmpleadoRepository empleadoRepository)
    {
        _empleadoRepository = empleadoRepository;
    }

    public string Ejecutar(PermisosUsuario permisos, Empleado empleado)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario actual.", nameof(permisos));
        }

        if (empleado is null)
        {
            throw new ArgumentException("Se requiere el empleado.", nameof(empleado));
        }

        if (!permisos.PuedeAdministrarEmpleados)
        {
            throw new InvalidOperationException("Solo un administrador puede restablecer contrasenas.");
        }

        var password = GeneradorContrasena.Generar();

        empleado.PasswordUsuario = PasswordHasher.Hash(password);
        _empleadoRepository.Actualizar(empleado);

        return password;
    }
}

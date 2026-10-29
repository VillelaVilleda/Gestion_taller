namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: restablecer la contrasena de un empleado (DERCAS 6.4.1).
/// Reglas (ver RestablecerContrasenaServiceTests):
///   - solo un usuario con permiso para administrar empleados puede hacerlo
///   - se usa la contrasena elegida (de 6 a 50 caracteres) o, si no se elige, una temporal generada
///   - se guarda solo con hash
///   - se devuelve la contrasena en texto, que solo se conoce en este momento
/// </summary>
public class RestablecerContrasenaService
{
    private readonly IEmpleadoRepository _empleadoRepository;

    public RestablecerContrasenaService(IEmpleadoRepository empleadoRepository)
    {
        _empleadoRepository = empleadoRepository;
    }

    public string Ejecutar(PermisosUsuario permisos, Empleado empleado, string? passwordElegida = null)
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
            throw new InvalidOperationException("Solo un administrador puede restablecer contraseñas.");
        }

        // Si no se elige una contraseña, se genera una temporal aleatoria.
        if (passwordElegida is not null)
        {
            ValidadorContrasena.Validar(passwordElegida);
        }

        var password = passwordElegida ?? GeneradorContrasena.Generar();

        empleado.PasswordUsuario = PasswordHasher.Hash(password);
        _empleadoRepository.Actualizar(empleado);

        return password;
    }
}

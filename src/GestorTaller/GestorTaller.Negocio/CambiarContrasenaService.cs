namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: que un empleado cambie su propia contrasena (DERCAS 6.4.1).
/// Reglas (ver CambiarContrasenaServiceTests):
///   - la contrasena actual debe ser correcta
///   - la nueva es obligatoria, de al menos 6 caracteres y distinta de la actual
///   - la nueva se guarda solo con hash
/// </summary>
public class CambiarContrasenaService
{
    public const int LongitudMinima = 6;

    private readonly IEmpleadoRepository _empleadoRepository;

    public CambiarContrasenaService(IEmpleadoRepository empleadoRepository)
    {
        _empleadoRepository = empleadoRepository;
    }

    public void Ejecutar(Empleado empleado, string passwordActual, string passwordNueva)
    {
        if (empleado is null)
        {
            throw new ArgumentException("Se requiere el empleado.", nameof(empleado));
        }

        if (string.IsNullOrEmpty(passwordActual) || !PasswordHasher.Verificar(passwordActual, empleado.PasswordUsuario))
        {
            throw new InvalidOperationException("La contrasena actual no es correcta.");
        }

        if (string.IsNullOrWhiteSpace(passwordNueva) || passwordNueva.Length < LongitudMinima)
        {
            throw new ArgumentException($"La contrasena nueva debe tener al menos {LongitudMinima} caracteres.", nameof(passwordNueva));
        }

        if (passwordNueva == passwordActual)
        {
            throw new ArgumentException("La contrasena nueva debe ser distinta de la actual.", nameof(passwordNueva));
        }

        empleado.PasswordUsuario = PasswordHasher.Hash(passwordNueva);
        _empleadoRepository.Actualizar(empleado);
    }
}

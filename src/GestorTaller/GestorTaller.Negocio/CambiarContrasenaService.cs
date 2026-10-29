namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: que un empleado cambie su propia contrasena (DERCAS 6.4.1).
/// Reglas (ver CambiarContrasenaServiceTests):
///   - la contrasena actual debe ser correcta
///   - la nueva cumple ValidadorContrasena (de 6 a 50 caracteres) y es distinta de la actual
///   - la nueva se guarda solo con hash
/// </summary>
public class CambiarContrasenaService
{
    public const int LongitudMinima = ValidadorContrasena.LongitudMinima;

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

        ValidadorContrasena.Validar(passwordNueva, "La contraseña nueva");

        if (passwordNueva == passwordActual)
        {
            throw new ArgumentException("La contraseña nueva debe ser distinta de la actual.", nameof(passwordNueva));
        }

        empleado.PasswordUsuario = PasswordHasher.Hash(passwordNueva);
        _empleadoRepository.Actualizar(empleado);
    }
}

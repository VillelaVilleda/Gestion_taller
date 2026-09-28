namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: validar el inicio de sesion de un empleado contra la base
/// de datos real, usando PasswordHasher para verificar la contrasena.
/// </summary>
public class IniciarSesionService
{
    private readonly IEmpleadoRepository _empleadoRepository;

    public IniciarSesionService(IEmpleadoRepository empleadoRepository)
    {
        _empleadoRepository = empleadoRepository;
    }

    /// <summary>
    /// Devuelve el empleado si nombreUsuario existe y password coincide con
    /// su hash, o null si no coincide con ninguno.
    /// </summary>
    public Empleado? Autenticar(string nombreUsuario, string password)
    {
        var empleado = _empleadoRepository.ObtenerPorNombreUsuario(nombreUsuario);

        if (empleado is null || !PasswordHasher.Verificar(password, empleado.PasswordUsuario))
        {
            return null;
        }

        return empleado;
    }
}
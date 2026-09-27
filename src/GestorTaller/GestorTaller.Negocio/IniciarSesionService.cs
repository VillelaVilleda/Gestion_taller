namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: validar el inicio de sesion de un empleado.
/// Todavia no hay base de datos conectada aqui (eso es la issue #48), asi
/// que se valida contra una lista fija de empleados de prueba, con sus
/// contrasenas ya hasheadas con PasswordHasher.
/// </summary>
public class IniciarSesionService
{
    private readonly List<Empleado> _empleadosDePrueba = new()
    {
        new Empleado
        {
            Id = Guid.NewGuid(),
            Nombre = "Administrador",
            NombreUsuario = "admin",
            PasswordUsuario = PasswordHasher.Hash("admin123"),
            EsAdministrador = true
        },
        new Empleado
        {
            Id = Guid.NewGuid(),
            Nombre = "Empleado de prueba",
            NombreUsuario = "usuario",
            PasswordUsuario = PasswordHasher.Hash("usuario123"),
            EsAdministrador = false
        }
    };

    /// <summary>
    /// Devuelve el empleado si nombreUsuario existe y password coincide con
    /// su hash, o null si no coincide con ninguno.
    /// </summary>
    public Empleado? Autenticar(string nombreUsuario, string password)
    {
        var empleado = _empleadosDePrueba.FirstOrDefault(e => e.NombreUsuario == nombreUsuario);

        if (empleado is null || !PasswordHasher.Verificar(password, empleado.PasswordUsuario))
        {
            return null;
        }

        return empleado;
    }
}
namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: validar el inicio de sesion de un empleado.
/// Todavia no hay base de datos conectada aqui (eso es la issue #48), asi
/// que se valida contra una lista fija de empleados de prueba.
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
            PasswordUsuario = "admin123",
            EsAdministrador = true
        },
        new Empleado
        {
            Id = Guid.NewGuid(),
            Nombre = "Empleado de prueba",
            NombreUsuario = "usuario",
            PasswordUsuario = "usuario123",
            EsAdministrador = false
        }
    };

    /// <summary>
    /// Devuelve el empleado si nombreUsuario y password coinciden con alguno
    /// de los empleados de prueba, o null si no coinciden con ninguno.
    /// </summary>
    public Empleado? Autenticar(string nombreUsuario, string password)
    {
        return _empleadosDePrueba.FirstOrDefault(e =>
            e.NombreUsuario == nombreUsuario && e.PasswordUsuario == password);
    }
}

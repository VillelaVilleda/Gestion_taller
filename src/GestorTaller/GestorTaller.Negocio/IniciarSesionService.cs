namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: validar el inicio de sesion de un usuario.
/// Todavia no hay base de datos de usuarios, asi que se valida contra una
/// lista fija de usuarios de prueba (uno Administrador, uno Usuario comun),
/// ambos con los mismos privilegios por ahora.
/// </summary>
public class IniciarSesionService
{
    private readonly List<Usuario> _usuariosDePrueba = new()
    {
        new Usuario
        {
            Id = Guid.NewGuid(),
            NombreUsuario = "admin",
            Contrasena = "admin123",
            Rol = RolUsuario.Administrador
        },
        new Usuario
        {
            Id = Guid.NewGuid(),
            NombreUsuario = "usuario",
            Contrasena = "usuario123",
            Rol = RolUsuario.Usuario
        }
    };

    /// <summary>
    /// Devuelve el usuario si nombreUsuario y contrasena coinciden con alguno
    /// de los usuarios de prueba, o null si no coinciden con ninguno.
    /// </summary>
    public Usuario? Autenticar(string nombreUsuario, string contrasena)
    {
        return _usuariosDePrueba.FirstOrDefault(u =>
            u.NombreUsuario == nombreUsuario && u.Contrasena == contrasena);
    }
}

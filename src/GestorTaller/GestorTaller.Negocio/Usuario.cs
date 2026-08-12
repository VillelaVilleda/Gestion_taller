namespace GestorTaller.Negocio;

/// <summary>
/// Usuario del sistema (empleado). Por ahora se valida contra una lista
/// fija de usuarios de prueba (ver IniciarSesionService), sin base de datos.
/// </summary>
public class Usuario
{
    public Guid Id { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public RolUsuario Rol { get; set; }
}

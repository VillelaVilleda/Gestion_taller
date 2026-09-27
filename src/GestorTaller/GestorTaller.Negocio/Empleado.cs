namespace GestorTaller.Negocio;

/// <summary>
/// Empleado del sistema (segun el glosario del DERCAS, Empleado = usuario
/// autenticado). Administrador es un Empleado con EsAdministrador = true,
/// no un rol aparte.
/// </summary>
public class Empleado
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
    public string PasswordUsuario { get; set; } = string.Empty;
    public bool EsAdministrador { get; set; }
}

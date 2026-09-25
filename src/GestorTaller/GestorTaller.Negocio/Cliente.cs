namespace GestorTaller.Negocio;

/// <summary>
/// Cliente propietario del equipo o vehiculo asociado a una orden.
/// Incluye datos de contacto segun DERCAS 6.1.1 (telefono y correo;
/// direccion es opcional). Todavia no hay pantalla que los capture,
/// eso es trabajo futuro; aqui solo se habilita que puedan guardarse.
/// </summary>
public class Cliente
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Direccion { get; set; }
}

namespace GestorTaller.Negocio;

/// <summary>
/// Modelo minimo de cliente, suficiente para asociarlo a una orden.
/// La gestion completa de clientes (contacto, direccion) se aborda en un sprint posterior.
/// </summary>
public class Cliente
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

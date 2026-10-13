namespace GestorTaller.Negocio;

/// <summary>
/// Repuesto asignado a una orden, con su cantidad (tabla orden_repuesto del DER).
/// Un repuesto aparece una sola vez por orden; si se asigna otra vez, se suma la cantidad.
/// </summary>
public class OrdenRepuesto
{
    public Guid OrdenId { get; set; }
    public Guid RepuestoId { get; set; }
    public Repuesto Repuesto { get; set; } = null!;
    public int Cantidad { get; set; }

    /// <summary>
    /// Precio unitario del repuesto al momento de asignarlo (copia del precio del
    /// repuesto), para que cambiar el precio despues no altere ordenes ya hechas.
    /// </summary>
    public decimal PrecioUnitario { get; set; }
}

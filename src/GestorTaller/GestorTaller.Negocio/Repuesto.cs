namespace GestorTaller.Negocio;

/// <summary>
/// Repuesto del catalogo del taller. Segun el DERCAS (6.3), lo unico que el
/// sistema debe controlar es su existencia (cantidad disponible en stock);
/// la asignacion a una orden y el descuento automatico del inventario quedan
/// para un sprint futuro.
/// </summary>
public class Repuesto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Existencia { get; set; }

    /// <summary>Precio unitario en quetzales (mayor o igual a 0).</summary>
    public decimal Precio { get; set; }
}
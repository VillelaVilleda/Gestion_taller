namespace GestorTaller.Negocio;

/// <summary>
/// Orden de trabajo. En este sprint solo se completan los datos de Recepcion;
/// los campos de Diagnostico/Cotizacion/Reparacion se agregan en issues posteriores.
/// </summary>
public class Orden
{
    public Guid Id { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public string DescripcionProblema { get; set; } = string.Empty;
    public decimal CostoDiagnostico { get; set; }
    public EstadoOrden Estado { get; set; }
    public DateTime FechaRecepcion { get; set; }
}

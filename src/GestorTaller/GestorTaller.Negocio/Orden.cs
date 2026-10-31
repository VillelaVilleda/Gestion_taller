namespace GestorTaller.Negocio;

/// <summary>
/// Orden de trabajo. En este sprint solo se completan los datos de Recepcion;
/// los campos de Diagnostico/Cotizacion/Reparacion se agregan en issues posteriores.
/// </summary>
public class Orden
{
    public Guid Id { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public string DescripcionObjeto { get; set; } = string.Empty;
    public string DescripcionProblema { get; set; } = string.Empty;
    public decimal CostoDiagnostico { get; set; }
    public EstadoOrden Estado { get; set; }
    public DateTime FechaRecepcion { get; set; }
    public List<EstadoOrden> HistorialEstados { get; set; } = new();
    public Empleado? EmpleadoDiagnostico { get; set; }
    public string DetalleDiagnostico { get; set; } = string.Empty;
    public Empleado? EmpleadoCotizacion { get; set; }
    public decimal MontoCotizado { get; set; }
    public bool CotizacionAceptada { get; set; }
    public Empleado? EmpleadoReparacion { get; set; }
    public string DetalleReparacion { get; set; } = string.Empty;
    public Empleado? EmpleadoEntrega { get; set; }
    public decimal MontoPagado { get; set; }
    public DateTime? FechaEntrega { get; set; }

    /// <summary>Cuándo un administrador corrigió por última vez datos de la orden; null si nunca se editó.</summary>
    public DateTime? FechaUltimaEdicion { get; set; }
    public List<OrdenRepuesto> Repuestos { get; set; } = new();
}

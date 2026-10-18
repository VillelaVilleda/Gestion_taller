namespace GestorTaller.Negocio;

/// <summary>Desglose de lo que se cobra por una orden.</summary>
public record DesgloseOrden(decimal CostoDiagnostico, decimal ManoDeObra, decimal Repuestos)
{
    public decimal Total => CostoDiagnostico + ManoDeObra + Repuestos;
}

/// <summary>
/// Calcula el total a cobrar: costo de diagnostico + costo de mano de obra + repuestos
/// (cantidad x precio unitario guardado en la asignacion). La mano de obra y los repuestos
/// solo se cobran si el cliente acepto la cotizacion; si la rechazo, solo se cobra el diagnostico.
/// </summary>
public class CalcularTotalOrdenService
{
    public DesgloseOrden Calcular(Orden orden)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere una orden.", nameof(orden));
        }

        return orden.CotizacionAceptada
            ? Calcular(orden.CostoDiagnostico, orden.MontoCotizado, orden.Repuestos)
            : new DesgloseOrden(orden.CostoDiagnostico, 0m, 0m);
    }

    /// <summary>Calculo con valores sueltos (por ejemplo, la vista previa en la Cotizacion).</summary>
    public DesgloseOrden Calcular(decimal costoDiagnostico, decimal manoDeObra, IEnumerable<OrdenRepuesto> repuestos)
    {
        if (repuestos is null)
        {
            throw new ArgumentException("Se requiere la lista de repuestos.", nameof(repuestos));
        }

        var totalRepuestos = repuestos.Sum(r => r.PrecioUnitario * r.Cantidad);
        return new DesgloseOrden(costoDiagnostico, manoDeObra, totalRepuestos);
    }
}

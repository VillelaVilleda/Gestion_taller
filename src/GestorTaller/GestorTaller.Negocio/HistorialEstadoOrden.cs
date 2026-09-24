namespace GestorTaller.Negocio;

/// <summary>
/// Una fila del historial de estados de una orden: representa que la orden
/// paso por "Estado" en la posicion "OrdenPosicion" de su historial.
/// Existe solo para que EF Core pueda guardar Orden.HistorialEstados como
/// su propia tabla (ver DER, issue #41); el resto del codigo sigue
/// trabajando con Orden.HistorialEstados como List&lt;EstadoOrden&gt; normal.
/// </summary>
public class HistorialEstadoOrden
{
    public long Id { get; set; }
    public Guid OrdenId { get; set; }
    public EstadoOrden Estado { get; set; }
    public int OrdenPosicion { get; set; }
}
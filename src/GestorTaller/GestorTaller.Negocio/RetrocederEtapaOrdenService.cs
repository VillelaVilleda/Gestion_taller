namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: que un administrador corrija el estado de una orden devolviéndola a la etapa anterior
/// por la que pasó (ver RetrocederEtapaOrdenServiceTests). Reglas:
///   - solo un usuario con permiso para editar puede hacerlo
///   - la etapa anterior es la que precede a la actual en el historial: si la cotización fue rechazada,
///     Terminado vuelve a Cotización; si fue aceptada, vuelve a En reparación
///   - no se puede retroceder desde Recepción ni Diagnóstico (el sistema pasa de Recepción a Diagnóstico
///     solo, al abrir la orden), ni una orden entregada
///   - la etapa que se abandona deja de figurar en el historial y queda de nuevo por registrar
///   - al salir de En reparación se descartan los datos de la reparación y se devuelven al inventario los
///     repuestos descontados; la cotización queda de nuevo por decidir (los repuestos siguen cotizados)
///   - se registra la fecha de última edición y se guarda
/// </summary>
public class RetrocederEtapaOrdenService
{
    private readonly IOrdenRepository _ordenRepository;

    public RetrocederEtapaOrdenService(IOrdenRepository ordenRepository)
    {
        _ordenRepository = ordenRepository;
    }

    /// <summary>True si la orden tiene una etapa anterior a la que se puede volver.</summary>
    public static bool PuedeRetroceder(Orden orden)
    {
        if (orden is null)
        {
            throw new ArgumentException("Se requiere la orden.", nameof(orden));
        }

        return orden.Estado is EstadoOrden.Cotizacion or EstadoOrden.EnReparacion or EstadoOrden.Terminado
            && orden.HistorialEstados.Count >= 2;
    }

    /// <summary>Devuelve la etapa a la que quedó la orden.</summary>
    public EstadoOrden Ejecutar(PermisosUsuario permisos, Orden orden)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
        }

        if (orden is null)
        {
            throw new ArgumentException("Se requiere la orden.", nameof(orden));
        }

        if (!permisos.PuedeEditar)
        {
            throw new InvalidOperationException("Solo un administrador puede retroceder la etapa de una orden.");
        }

        if (!PuedeRetroceder(orden))
        {
            throw new InvalidOperationException(orden.Estado == EstadoOrden.Entregado
                ? "Una orden entregada no se puede retroceder."
                : "Esta orden no tiene una etapa anterior a la que volver.");
        }

        if (orden.Estado == EstadoOrden.EnReparacion)
        {
            foreach (var asignado in orden.Repuestos)
            {
                asignado.Repuesto.Existencia += asignado.Cantidad;
            }

            orden.CotizacionAceptada = false;
            orden.EmpleadoReparacion = null;
            orden.DetalleReparacion = string.Empty;
        }

        orden.HistorialEstados.RemoveAt(orden.HistorialEstados.Count - 1);
        orden.Estado = orden.HistorialEstados[^1];
        orden.FechaUltimaEdicion = DateTime.Now;
        _ordenRepository.Actualizar(orden);

        return orden.Estado;
    }
}

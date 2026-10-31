namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: corregir el empleado encargado y el detalle ya registrados en el diagnóstico o en la
/// reparación de una orden. Reglas (ver ModificarEtapaOrdenServiceTests):
///   - solo un usuario con permiso para editar puede hacerlo
///   - la etapa debe poder editarse (ver EdicionDeEtapa): la orden ya la completó y no está entregada
///   - el empleado y el detalle son obligatorios (el detalle se guarda sin espacios en los extremos)
///   - si no cambia nada, no se guarda y no se toca la fecha de última edición; devuelve false
///   - si cambia algo, se guarda, se registra la fecha de última edición y devuelve true
/// </summary>
public class ModificarEtapaOrdenService
{
    private readonly IOrdenRepository _ordenRepository;

    public ModificarEtapaOrdenService(IOrdenRepository ordenRepository)
    {
        _ordenRepository = ordenRepository;
    }

    public bool Ejecutar(PermisosUsuario permisos, Orden orden, EstadoOrden etapa, Empleado empleado, string detalle)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
        }

        if (orden is null)
        {
            throw new ArgumentException("Se requiere la orden.", nameof(orden));
        }

        if (etapa is not (EstadoOrden.Diagnostico or EstadoOrden.EnReparacion))
        {
            throw new ArgumentException("Solo se puede editar el diagnóstico o la reparación.", nameof(etapa));
        }

        var nombre = etapa == EstadoOrden.Diagnostico ? "diagnóstico" : "reparación";

        if (!permisos.PuedeEditar)
        {
            throw new InvalidOperationException($"Solo un administrador puede editar el {nombre}.");
        }

        if (!EdicionDeEtapa.PuedeEditar(orden, etapa))
        {
            throw new InvalidOperationException(orden.Estado == EstadoOrden.Entregado
                ? "Una orden entregada no se puede editar."
                : $"El {nombre} de esta orden todavía no se puede editar.");
        }

        if (empleado is null)
        {
            throw new ArgumentException($"Se requiere el empleado encargado del {nombre}.", nameof(empleado));
        }

        if (string.IsNullOrWhiteSpace(detalle))
        {
            throw new ArgumentException($"Se requiere el detalle del {nombre}.", nameof(detalle));
        }

        var detalleLimpio = detalle.Trim();
        var empleadoActual = etapa == EstadoOrden.Diagnostico ? orden.EmpleadoDiagnostico : orden.EmpleadoReparacion;
        var detalleActual = etapa == EstadoOrden.Diagnostico ? orden.DetalleDiagnostico : orden.DetalleReparacion;

        if (empleado.Id == empleadoActual?.Id && detalleLimpio == detalleActual.Trim())
        {
            return false;
        }

        if (etapa == EstadoOrden.Diagnostico)
        {
            orden.EmpleadoDiagnostico = empleado;
            orden.DetalleDiagnostico = detalleLimpio;
        }
        else
        {
            orden.EmpleadoReparacion = empleado;
            orden.DetalleReparacion = detalleLimpio;
        }

        orden.FechaUltimaEdicion = DateTime.Now;
        _ordenRepository.Actualizar(orden);

        return true;
    }
}

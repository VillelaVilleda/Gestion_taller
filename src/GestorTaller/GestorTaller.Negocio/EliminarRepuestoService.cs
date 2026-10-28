namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: eliminar un repuesto del catalogo (DERCAS 6.5.1). Reglas (ver EliminarRepuestoServiceTests):
///   - solo un usuario con permiso para eliminar puede hacerlo
///   - no se puede eliminar un repuesto que esta en alguna orden
/// </summary>
public class EliminarRepuestoService
{
    private readonly IRepuestoRepository _repuestoRepository;
    private readonly IOrdenRepository _ordenRepository;

    public EliminarRepuestoService(IRepuestoRepository repuestoRepository, IOrdenRepository ordenRepository)
    {
        _repuestoRepository = repuestoRepository;
        _ordenRepository = ordenRepository;
    }

    public void Ejecutar(PermisosUsuario permisos, Repuesto repuesto)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
        }

        if (repuesto is null)
        {
            throw new ArgumentException("Se requiere el repuesto.", nameof(repuesto));
        }

        if (!permisos.PuedeEliminar)
        {
            throw new InvalidOperationException("Solo un administrador puede eliminar repuestos.");
        }

        if (_ordenRepository.ObtenerTodas().Any(o => o.Repuestos.Any(r => r.RepuestoId == repuesto.Id)))
        {
            throw new InvalidOperationException("No se puede eliminar el repuesto porque está en una o más órdenes.");
        }

        _repuestoRepository.Eliminar(repuesto);
    }
}

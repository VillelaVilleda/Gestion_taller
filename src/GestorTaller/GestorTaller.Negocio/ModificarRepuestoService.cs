namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: editar un repuesto del catalogo (DERCAS 6.5.1). Reglas (ver ModificarRepuestoServiceTests):
///   - solo un usuario con permiso para editar puede hacerlo
///   - mismas validaciones que al crear; el nombre es unico sin contar al propio repuesto
///   - cambiar el precio no altera las ordenes ya cotizadas: guardan su propio precio unitario
/// </summary>
public class ModificarRepuestoService
{
    private readonly IRepuestoRepository _repuestoRepository;

    public ModificarRepuestoService(IRepuestoRepository repuestoRepository)
    {
        _repuestoRepository = repuestoRepository;
    }

    public void Ejecutar(PermisosUsuario permisos, Repuesto repuesto, string nombre, int existencia, decimal precio)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
        }

        if (repuesto is null)
        {
            throw new ArgumentException("Se requiere el repuesto.", nameof(repuesto));
        }

        if (!permisos.PuedeEditar)
        {
            throw new InvalidOperationException("Solo un administrador puede editar repuestos.");
        }

        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("Se requiere el nombre del repuesto.", nameof(nombre));
        }

        var nombreLimpio = nombre.Trim();

        if (nombreLimpio.Length > CrearRepuestoService.LongitudMaximaNombre)
        {
            throw new ArgumentException($"El nombre no puede tener más de {CrearRepuestoService.LongitudMaximaNombre} caracteres.", nameof(nombre));
        }

        if (existencia < 0)
        {
            throw new ArgumentException("La existencia no puede ser negativa.", nameof(existencia));
        }

        if (precio < 0)
        {
            throw new ArgumentException("El precio no puede ser negativo.", nameof(precio));
        }

        var buscado = FiltroDeBusqueda.Normalizar(nombreLimpio);

        if (_repuestoRepository.ObtenerTodos().Any(r => r.Id != repuesto.Id && FiltroDeBusqueda.Normalizar(r.Nombre) == buscado))
        {
            throw new InvalidOperationException("Ya existe un repuesto con ese nombre.");
        }

        repuesto.Nombre = nombreLimpio;
        repuesto.Existencia = existencia;
        repuesto.Precio = precio;
        _repuestoRepository.Actualizar(repuesto);
    }
}

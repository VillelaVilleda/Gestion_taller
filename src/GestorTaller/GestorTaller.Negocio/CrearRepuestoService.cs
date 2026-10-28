namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: crear un repuesto del catalogo (DERCAS 6.4.3).
/// Reglas (ver CrearRepuestoServiceTests):
///   - el nombre es obligatorio, de hasta 150 caracteres (se recortan los espacios)
///   - no puede existir otro repuesto con el mismo nombre (sin importar mayusculas ni acentos)
///   - la existencia y el precio no pueden ser negativos (0 es valido)
///   - el repuesto se guarda en el repositorio antes de devolverlo
/// </summary>
public class CrearRepuestoService
{
    public const int LongitudMaximaNombre = 150;

    private readonly IRepuestoRepository _repuestoRepository;

    public CrearRepuestoService(IRepuestoRepository repuestoRepository)
    {
        _repuestoRepository = repuestoRepository;
    }

    public Repuesto Ejecutar(string nombre, int existencia, decimal precio)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("Se requiere el nombre del repuesto.", nameof(nombre));
        }

        var nombreLimpio = nombre.Trim();

        if (nombreLimpio.Length > LongitudMaximaNombre)
        {
            throw new ArgumentException($"El nombre no puede tener más de {LongitudMaximaNombre} caracteres.", nameof(nombre));
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

        if (_repuestoRepository.ObtenerTodos().Any(r => FiltroDeBusqueda.Normalizar(r.Nombre) == buscado))
        {
            throw new InvalidOperationException("Ya existe un repuesto con ese nombre.");
        }

        var repuesto = new Repuesto { Id = Guid.NewGuid(), Nombre = nombreLimpio, Existencia = existencia, Precio = precio };

        _repuestoRepository.Agregar(repuesto);

        return repuesto;
    }
}

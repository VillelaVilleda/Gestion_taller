namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: preparar un cliente nuevo para una orden (DERCAS 6.1).
/// Reglas (ver CrearClienteServiceTests):
///   - el nombre es obligatorio (se recortan los espacios)
///   - no puede existir ya un cliente con el mismo nombre (sin importar
///     mayusculas ni acentos): se debe elegir el existente
/// El cliente devuelto NO se guarda aqui; se guarda junto con la orden.
/// </summary>
public class CrearClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public CrearClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public Cliente Ejecutar(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("Se requiere el nombre del cliente.", nameof(nombre));
        }

        var nombreLimpio = nombre.Trim();
        var buscado = FiltroDeBusqueda.Normalizar(nombreLimpio);

        var yaExiste = _clienteRepository.ObtenerTodos()
            .Any(c => FiltroDeBusqueda.Normalizar(c.Nombre) == buscado);

        if (yaExiste)
        {
            throw new InvalidOperationException("Ya existe un cliente con ese nombre. Seleccionalo de la lista.");
        }

        return new Cliente { Id = Guid.NewGuid(), Nombre = nombreLimpio };
    }
}

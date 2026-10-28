namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: preparar un cliente nuevo para una orden (DERCAS 6.1).
/// Reglas (ver CrearClienteServiceTests y ValidadorDatosClienteTests):
///   - nombre, contacto (telefono o correo) y direccion cumplen las reglas de ValidadorDatosCliente
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

    public Cliente Ejecutar(string nombre, string? telefono = null, string? correo = null, string? direccion = null)
    {
        var datos = ValidadorDatosCliente.Validar(nombre, telefono, correo, direccion);
        var buscado = FiltroDeBusqueda.Normalizar(datos.Nombre);

        var yaExiste = _clienteRepository.ObtenerTodos()
            .Any(c => FiltroDeBusqueda.Normalizar(c.Nombre) == buscado);

        if (yaExiste)
        {
            throw new InvalidOperationException("Ya existe un cliente con ese nombre. Selecciónalo de la lista.");
        }

        return new Cliente
        {
            Id = Guid.NewGuid(),
            Nombre = datos.Nombre,
            Telefono = datos.Telefono,
            Correo = datos.Correo,
            Direccion = datos.Direccion
        };
    }
}

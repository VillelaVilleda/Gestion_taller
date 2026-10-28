namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: registrar un cliente desde la pantalla de clientes (ver RegistrarClienteServiceTests).
/// Aplica las mismas reglas que CrearClienteService y, a diferencia de este, lo guarda.
/// </summary>
public class RegistrarClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly CrearClienteService _crearClienteService;

    public RegistrarClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
        _crearClienteService = new CrearClienteService(clienteRepository);
    }

    public Cliente Ejecutar(string nombre, string? telefono = null, string? correo = null, string? direccion = null)
    {
        var cliente = _crearClienteService.Ejecutar(nombre, telefono, correo, direccion);
        _clienteRepository.Agregar(cliente);

        return cliente;
    }
}

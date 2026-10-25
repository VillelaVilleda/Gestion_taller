namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: eliminar un cliente (DERCAS 6.5.1). Reglas (ver EliminarClienteServiceTests):
///   - solo un usuario con permiso para eliminar puede hacerlo
///   - no se puede eliminar un cliente que tiene ordenes
/// </summary>
public class EliminarClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IOrdenRepository _ordenRepository;

    public EliminarClienteService(IClienteRepository clienteRepository, IOrdenRepository ordenRepository)
    {
        _clienteRepository = clienteRepository;
        _ordenRepository = ordenRepository;
    }

    public void Ejecutar(PermisosUsuario permisos, Cliente cliente)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
        }

        if (cliente is null)
        {
            throw new ArgumentException("Se requiere el cliente.", nameof(cliente));
        }

        if (!permisos.PuedeEliminar)
        {
            throw new InvalidOperationException("Solo un administrador puede eliminar clientes.");
        }

        if (_ordenRepository.ObtenerTodas().Any(o => o.Cliente.Id == cliente.Id))
        {
            throw new InvalidOperationException("No se puede eliminar el cliente porque tiene ordenes.");
        }

        _clienteRepository.Eliminar(cliente);
    }
}

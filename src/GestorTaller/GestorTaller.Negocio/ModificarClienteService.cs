namespace GestorTaller.Negocio;

/// <summary>
/// Caso de uso: editar el nombre y los datos de contacto de un cliente (DERCAS 6.5.1).
/// Reglas (ver ModificarClienteServiceTests):
///   - solo un usuario con permiso para editar puede hacerlo
///   - los datos cumplen las reglas de ValidadorDatosCliente, igual que al crear
///   - el nombre no puede coincidir con el de otro cliente (sin importar mayusculas ni acentos)
/// </summary>
public class ModificarClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ModificarClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public void Ejecutar(PermisosUsuario permisos, Cliente cliente, string nombre, string? telefono, string? correo, string? direccion)
    {
        if (permisos is null)
        {
            throw new ArgumentException("Se requieren los permisos del usuario.", nameof(permisos));
        }

        if (cliente is null)
        {
            throw new ArgumentException("Se requiere el cliente.", nameof(cliente));
        }

        if (!permisos.PuedeEditar)
        {
            throw new InvalidOperationException("Solo un administrador puede editar clientes.");
        }

        var datos = ValidadorDatosCliente.Validar(nombre, telefono, correo, direccion);
        var buscado = FiltroDeBusqueda.Normalizar(datos.Nombre);

        if (_clienteRepository.ObtenerTodos().Any(c => c.Id != cliente.Id && FiltroDeBusqueda.Normalizar(c.Nombre) == buscado))
        {
            throw new InvalidOperationException("Ya existe un cliente con ese nombre.");
        }

        cliente.Nombre = datos.Nombre;
        cliente.Telefono = datos.Telefono;
        cliente.Correo = datos.Correo;
        cliente.Direccion = datos.Direccion;
        _clienteRepository.Actualizar(cliente);
    }
}

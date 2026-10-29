namespace GestorTaller.Negocio;

/// <summary>Empleado recien creado y su contrasena temporal en texto, que solo se conoce en este momento.</summary>
public record EmpleadoCreado(Empleado Empleado, string PasswordTemporal);

/// <summary>
/// Caso de uso: crear un empleado con credenciales generadas por el sistema (DERCAS 6.4.1,
/// Proceso 6 del DFD: validar datos, generar credenciales, guardar registro).
/// Reglas (ver CrearEmpleadoServiceTests):
///   - el nombre es obligatorio, de hasta 150 caracteres (se recortan los espacios)
///   - el usuario se genera a partir del nombre (nombre.apellido) y es unico: si ya existe se agrega un numero
///   - se genera una contrasena temporal, que se guarda solo con hash
///   - se guarda el empleado y se devuelve junto con la contrasena temporal
/// </summary>
public class CrearEmpleadoService
{
    public const int LongitudMaximaNombre = 150;

    private const int LongitudMaximaUsuario = 45;

    private readonly IEmpleadoRepository _empleadoRepository;

    public CrearEmpleadoService(IEmpleadoRepository empleadoRepository)
    {
        _empleadoRepository = empleadoRepository;
    }

    public EmpleadoCreado Ejecutar(string nombre, bool esAdministrador)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("Se requiere el nombre del empleado.", nameof(nombre));
        }

        var nombreLimpio = nombre.Trim();

        if (nombreLimpio.Length > LongitudMaximaNombre)
        {
            throw new ArgumentException($"El nombre no puede tener más de {LongitudMaximaNombre} caracteres.", nameof(nombre));
        }

        var password = GeneradorContrasena.Generar();

        var empleado = new Empleado
        {
            Id = Guid.NewGuid(),
            Nombre = nombreLimpio,
            NombreUsuario = GenerarUsuarioUnico(nombreLimpio),
            PasswordUsuario = PasswordHasher.Hash(password),
            EsAdministrador = esAdministrador
        };

        _empleadoRepository.Agregar(empleado);

        return new EmpleadoCreado(empleado, password);
    }

    private string GenerarUsuarioUnico(string nombre)
    {
        var palabras = FiltroDeBusqueda.Normalizar(nombre)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(p => new string(p.Where(char.IsLetterOrDigit).ToArray()))
            .Where(p => p.Length > 0)
            .ToList();

        var baseUsuario = palabras.Count switch
        {
            0 => "empleado",
            1 => palabras[0],
            _ => palabras[0] + "." + palabras[^1]
        };

        if (baseUsuario.Length > LongitudMaximaUsuario)
        {
            baseUsuario = baseUsuario[..LongitudMaximaUsuario];
        }

        var usuario = baseUsuario;

        for (var numero = 2; AdministradorOculto.EsReservado(usuario) || _empleadoRepository.ObtenerPorNombreUsuario(usuario) is not null; numero++)
        {
            usuario = baseUsuario + numero;
        }

        return usuario;
    }
}

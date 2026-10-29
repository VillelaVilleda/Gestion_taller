namespace GestorTaller.Negocio;

/// <summary>
/// Administrador de respaldo del sistema (ver AdministradorOcultoTests). No existe en la base de datos:
/// su usuario es reservado y su contraseña se verifica contra un hash fijo del código. Puede hacer todo
/// lo que hace un administrador, pero no aparece en la lista de empleados, no se puede editar ni
/// eliminar, y no cambia su contraseña desde la aplicación. Nunca se guarda en ninguna orden: el
/// responsable de cada etapa se elige de la lista de empleados.
/// </summary>
public static class AdministradorOculto
{
    public const string NombreUsuario = "admin";

    public static readonly Guid Id = new("00000000-0000-0000-0000-00000000ad01");

    // BCrypt de la contraseña del administrador oculto; la contraseña en sí no se guarda en ningún lado.
    private const string HashContrasena = "$2b$11$vGMJWNObnq4uYm.cj5tGz.cPTWTLjQhSMo49Yb5XmDgFpUVE8UWpe";

    /// <summary>True si el usuario es el reservado (sin importar mayúsculas ni espacios en los extremos).</summary>
    public static bool EsReservado(string? nombreUsuario) =>
        string.Equals(nombreUsuario?.Trim(), NombreUsuario, StringComparison.OrdinalIgnoreCase);

    /// <summary>True si el empleado es el administrador oculto.</summary>
    public static bool EsEste(Empleado? empleado) => empleado is not null && empleado.Id == Id;

    /// <summary>Devuelve el administrador oculto si la contraseña es correcta, o null si no lo es.</summary>
    public static Empleado? Autenticar(string password)
    {
        if (string.IsNullOrEmpty(password) || !PasswordHasher.Verificar(password, HashContrasena))
        {
            return null;
        }

        return new Empleado
        {
            Id = Id,
            Nombre = "Administrador",
            NombreUsuario = NombreUsuario,
            PasswordUsuario = HashContrasena,
            EsAdministrador = true
        };
    }
}

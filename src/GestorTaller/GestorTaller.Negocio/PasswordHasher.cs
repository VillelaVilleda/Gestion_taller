namespace GestorTaller.Negocio;

/// <summary>
/// Encapsula el algoritmo de hash de contrasenas (BCrypt) para que el resto
/// del sistema no dependa directamente de la libreria externa. Empleado.PasswordUsuario
/// siempre debe contener el resultado de Hash, nunca la contrasena en texto plano.
/// </summary>
public static class PasswordHasher
{
    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool Verificar(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
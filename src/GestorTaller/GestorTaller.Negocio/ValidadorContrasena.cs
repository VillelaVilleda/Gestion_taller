namespace GestorTaller.Negocio;

/// <summary>
/// Reglas de una contraseña que alguien escribe (ver ValidadorContrasenaTests):
///   - mínimo 6 y máximo 50 caracteres
///   - no puede pasar de 72 bytes en UTF-8: BCrypt ignora lo que sigue, y dos contraseñas distintas
///     que solo se diferencian después de ese punto valdrían igual
/// </summary>
public static class ValidadorContrasena
{
    public const int LongitudMinima = 6;
    public const int LongitudMaxima = 50;

    private const int BytesMaximos = 72;

    public static void Validar(string? password, string descripcion = "La contraseña")
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < LongitudMinima)
        {
            throw new ArgumentException($"{descripcion} debe tener al menos {LongitudMinima} caracteres.", nameof(password));
        }

        if (password.Length > LongitudMaxima)
        {
            throw new ArgumentException($"{descripcion} no puede tener más de {LongitudMaxima} caracteres.", nameof(password));
        }

        if (System.Text.Encoding.UTF8.GetByteCount(password) > BytesMaximos)
        {
            throw new ArgumentException($"{descripcion} es demasiado larga: usa menos símbolos o letras especiales.", nameof(password));
        }
    }
}

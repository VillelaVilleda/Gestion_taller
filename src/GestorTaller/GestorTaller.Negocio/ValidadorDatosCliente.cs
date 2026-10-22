using System.Text.RegularExpressions;

namespace GestorTaller.Negocio;

/// <summary>Datos de un cliente ya validados y recortados (los vacios quedan en null).</summary>
public record DatosCliente(string Nombre, string? Telefono, string? Correo, string? Direccion);

/// <summary>
/// Reglas de los datos de un cliente (DERCAS 6.1.1), compartidas por crear y editar
/// (ver ValidadorDatosClienteTests). No revisa si el nombre ya existe: eso depende del repositorio.
///   - el nombre es obligatorio, de hasta 150 caracteres
///   - debe haber al menos un contacto: telefono o correo
///   - telefono: solo digitos, espacios, +, - y parentesis; al menos 7 digitos; hasta 20 caracteres
///   - correo: formato valido; hasta 150 caracteres
///   - direccion: opcional; hasta 300 caracteres
/// </summary>
public static partial class ValidadorDatosCliente
{
    public const int LongitudMaximaNombre = 150;
    public const int LongitudMaximaTelefono = 20;
    public const int LongitudMaximaCorreo = 150;
    public const int LongitudMaximaDireccion = 300;
    public const int MinimoDigitosTelefono = 7;

    public static DatosCliente Validar(string nombre, string? telefono, string? correo, string? direccion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("Se requiere el nombre del cliente.", nameof(nombre));
        }

        var nombreLimpio = nombre.Trim();
        var telefonoLimpio = Limpiar(telefono);
        var correoLimpio = Limpiar(correo);
        var direccionLimpia = Limpiar(direccion);

        if (nombreLimpio.Length > LongitudMaximaNombre)
        {
            throw new ArgumentException($"El nombre no puede tener mas de {LongitudMaximaNombre} caracteres.", nameof(nombre));
        }

        if (telefonoLimpio is null && correoLimpio is null)
        {
            throw new ArgumentException("Se requiere al menos un contacto: telefono o correo.", nameof(telefono));
        }

        if (telefonoLimpio is not null)
        {
            ValidarTelefono(telefonoLimpio);
        }

        if (correoLimpio is not null)
        {
            ValidarCorreo(correoLimpio);
        }

        if (direccionLimpia is not null && direccionLimpia.Length > LongitudMaximaDireccion)
        {
            throw new ArgumentException($"La direccion no puede tener mas de {LongitudMaximaDireccion} caracteres.", nameof(direccion));
        }

        return new DatosCliente(nombreLimpio, telefonoLimpio, correoLimpio, direccionLimpia);
    }

    private static void ValidarTelefono(string telefono)
    {
        if (telefono.Length > LongitudMaximaTelefono)
        {
            throw new ArgumentException($"El telefono no puede tener mas de {LongitudMaximaTelefono} caracteres.", nameof(telefono));
        }

        if (!FormatoTelefono().IsMatch(telefono) || telefono.Count(char.IsDigit) < MinimoDigitosTelefono)
        {
            throw new ArgumentException(
                $"El telefono solo puede tener digitos, espacios, +, - y parentesis, con al menos {MinimoDigitosTelefono} digitos.", nameof(telefono));
        }
    }

    private static void ValidarCorreo(string correo)
    {
        if (correo.Length > LongitudMaximaCorreo)
        {
            throw new ArgumentException($"El correo no puede tener mas de {LongitudMaximaCorreo} caracteres.", nameof(correo));
        }

        if (!FormatoCorreo().IsMatch(correo))
        {
            throw new ArgumentException("El correo no tiene un formato valido (ejemplo: nombre@dominio.com).", nameof(correo));
        }
    }

    private static string? Limpiar(string? texto) => string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();

    [GeneratedRegex(@"^[0-9 +\-()]+$")]
    private static partial Regex FormatoTelefono();

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex FormatoCorreo();
}

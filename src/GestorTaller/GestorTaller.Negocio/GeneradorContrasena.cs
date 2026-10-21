using System.Security.Cryptography;

namespace GestorTaller.Negocio;

/// <summary>
/// Genera contrasenas temporales aleatorias, sin caracteres que se confundan
/// al leerlos (0/O, 1/l/I).
/// </summary>
public static class GeneradorContrasena
{
    public const int Longitud = 10;

    private const string Caracteres = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";

    public static string Generar()
    {
        return string.Create(Longitud, 0, (destino, _) =>
        {
            for (var i = 0; i < destino.Length; i++)
            {
                destino[i] = Caracteres[RandomNumberGenerator.GetInt32(Caracteres.Length)];
            }
        });
    }
}

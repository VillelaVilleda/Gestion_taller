namespace GestorTaller.Negocio;

/// <summary>
/// Contraste entre dos colores segun WCAG 2.1 (ver ContrasteColorTests).
/// Trabaja con colores en texto "#RRGGBB" para no depender de WinForms y poder probarse.
/// </summary>
public static class ContrasteColor
{
    /// <summary>Razon de contraste entre 1 (iguales) y 21 (negro sobre blanco). El orden no importa.</summary>
    public static double Calcular(string colorA, string colorB)
    {
        var luminanciaA = Luminancia(colorA);
        var luminanciaB = Luminancia(colorB);
        var mayor = Math.Max(luminanciaA, luminanciaB);
        var menor = Math.Min(luminanciaA, luminanciaB);

        return (mayor + 0.05) / (menor + 0.05);
    }

    private static double Luminancia(string color)
    {
        if (color is null || color.Length != 7 || color[0] != '#'
            || !int.TryParse(color.AsSpan(1), System.Globalization.NumberStyles.AllowHexSpecifier, null, out var rgb))
        {
            throw new ArgumentException("El color debe tener el formato #RRGGBB.", nameof(color));
        }

        var rojo = Canal((rgb >> 16) & 0xFF);
        var verde = Canal((rgb >> 8) & 0xFF);
        var azul = Canal(rgb & 0xFF);

        return 0.2126 * rojo + 0.7152 * verde + 0.0722 * azul;
    }

    private static double Canal(int valor)
    {
        var c = valor / 255.0;

        return c <= 0.03928 ? c / 12.92 : Math.Pow((c + 0.055) / 1.055, 2.4);
    }
}

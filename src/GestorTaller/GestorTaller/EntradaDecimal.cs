using System.Globalization;

namespace GestorTaller
{
    /// <summary>
    /// Los campos de montos aceptan punto o coma como separador decimal, sin importar la
    /// configuración regional de Windows. Si Windows usa coma, el punto no se aceptaba y el
    /// monto se guardaba en 0 sin avisar (issue #175).
    /// </summary>
    internal static class EntradaDecimal
    {
        public static void AceptarPuntoYComa(params NumericUpDown[] campos)
        {
            foreach (var campo in campos)
            {
                campo.KeyPress += CambiarAlSeparadorDeWindows;
            }
        }

        private static void CambiarAlSeparadorDeWindows(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar is '.' or ',')
            {
                e.KeyChar = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            }
        }
    }
}

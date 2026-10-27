using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Estilo visual comun de la aplicacion: colores, fuente y medidas. Los colores salen de
    /// <see cref="PaletaTema"/> (donde se prueba su contraste); aqui solo se convierten.
    ///
    /// Convencion de pantallas adaptables (para que todo se acomode al cambiar el tamano de la ventana):
    ///   - Una pantalla es un TableLayoutPanel con Dock = Fill y Padding = <see cref="RellenoPantalla"/>.
    ///   - Filas con alto fijo (AutoSize) para encabezados y formularios, y una fila Percent para la tabla.
    ///   - Los campos de un formulario van en columnas Percent y con Dock = Fill; no se usan Location fijos.
    ///   - Las tablas llevan Dock = Fill y columnas en modo Fill.
    ///   - Los dialogos son de tamano fijo, centrados sobre su ventana (FormBorderStyle = FixedDialog).
    ///   - Cada pantalla se revisa con docs/Revision_Diseno_Adaptable.md.
    /// </summary>
    internal static class Tema
    {
        public const int AlturaControl = 36;
        public const int RadioControl = 6;
        public const int RadioTarjeta = 10;
        public const int Espaciado = 8;
        public const int RellenoPantalla = 24;

        public static readonly Color Fondo = Convertir(PaletaTema.Fondo);
        public static readonly Color Blanco = Convertir(PaletaTema.Blanco);
        public static readonly Color Pizarra = Convertir(PaletaTema.Pizarra);
        public static readonly Color PizarraHover = Convertir(PaletaTema.PizarraHover);
        public static readonly Color Titanio = Convertir(PaletaTema.Titanio);
        public static readonly Color TextoSecundario = Convertir(PaletaTema.TextoSecundario);
        public static readonly Color Aluminio = Convertir(PaletaTema.Aluminio);
        public static readonly Color BordeCampo = Convertir(PaletaTema.BordeCampo);
        public static readonly Color Cobre = Convertir(PaletaTema.Cobre);
        public static readonly Color CobreOscuro = Convertir(PaletaTema.CobreOscuro);
        public static readonly Color PizarraPresionado = Convertir(PaletaTema.PizarraPresionado);
        public static readonly Color CobreHover = Convertir(PaletaTema.CobreHover);
        public static readonly Color CobrePresionado = Convertir(PaletaTema.CobrePresionado);
        public static readonly Color ErrorHover = Convertir(PaletaTema.ErrorHover);
        public static readonly Color ErrorPresionado = Convertir(PaletaTema.ErrorPresionado);
        public static readonly Color SecundarioHover = Convertir(PaletaTema.SecundarioHover);
        public static readonly Color SecundarioPresionado = Convertir(PaletaTema.SecundarioPresionado);
        public static readonly Color TextoHover = Convertir(PaletaTema.TextoHover);
        public static readonly Color TextoPresionado = Convertir(PaletaTema.TextoPresionado);
        public static readonly Color Exito = Convertir(PaletaTema.Exito);
        public static readonly Color Error = Convertir(PaletaTema.Error);
        public static readonly Color Advertencia = Convertir(PaletaTema.Advertencia);
        public static readonly Color Informacion = Convertir(PaletaTema.Informacion);

        public static Font Fuente(float puntos = 10F, FontStyle estilo = FontStyle.Regular) =>
            new("Segoe UI", puntos, estilo);

        /// <summary>
        /// Pone el fondo y el color de texto del tema a una ventana o pantalla. Los controles
        /// que no tengan color propio lo heredan; no cambia tamanos ni posiciones. La fuente no
        /// se toca: las pantallas ya usan Segoe UI.
        /// </summary>
        public static void Aplicar(Control control)
        {
            control.BackColor = Fondo;
            control.ForeColor = Titanio;
        }

        public static (Color Texto, Color Fondo) ColoresEtapa(EstadoOrden estado)
        {
            var (texto, fondo) = PaletaTema.ColoresEtapa(estado);

            return (Convertir(texto), Convertir(fondo));
        }

        private static Color Convertir(string color) => ColorTranslator.FromHtml(color);
    }
}

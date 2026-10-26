using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;

namespace GestorTaller
{
    /// <summary>
    /// Iconos Lucide de la interfaz. Cada icono es un PNG de 64 px (solo transparencia) incrustado
    /// en Recursos\Iconos; se pinta del color y tamano pedidos y se guarda en cache.
    /// Las imagenes devueltas son compartidas: no se deben liberar (Dispose).
    /// </summary>
    internal static class Iconos
    {
        private static readonly Dictionary<(string Nombre, int Tamano, int Color), Bitmap> Cache = new();

        /// <summary>Devuelve el icono (por ejemplo "pencil") con el tamano en pixeles y el color indicados.</summary>
        public static Bitmap Obtener(string nombre, int tamano, Color color)
        {
            var clave = (nombre, tamano, color.ToArgb());

            if (Cache.TryGetValue(clave, out var existente))
            {
                return existente;
            }

            using var flujo = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Iconos.{nombre}.png")
                ?? throw new ArgumentException($"No existe el icono '{nombre}'.", nameof(nombre));
            using var original = new Bitmap(flujo);

            var resultado = new Bitmap(tamano, tamano, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(resultado))
            using (var atributos = new ImageAttributes())
            {
                // Reemplaza el color del PNG por el pedido y conserva la transparencia.
                var matriz = new ColorMatrix(new[]
                {
                    new float[] { 0, 0, 0, 0, 0 },
                    new float[] { 0, 0, 0, 0, 0 },
                    new float[] { 0, 0, 0, 0, 0 },
                    new float[] { 0, 0, 0, 1, 0 },
                    new float[] { color.R / 255f, color.G / 255f, color.B / 255f, 0, 1 }
                });
                atributos.SetColorMatrix(matriz);

                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(original, new Rectangle(0, 0, tamano, tamano), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, atributos);
            }

            Cache[clave] = resultado;

            return resultado;
        }
    }
}

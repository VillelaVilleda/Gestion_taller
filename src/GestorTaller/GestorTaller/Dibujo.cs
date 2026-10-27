using System.Drawing.Drawing2D;

namespace GestorTaller
{
    /// <summary>Utilidades de dibujo compartidas por los controles propios.</summary>
    internal static class Dibujo
    {
        /// <summary>Contorno de un rectangulo con las esquinas redondeadas con el radio indicado.</summary>
        public static GraphicsPath RectanguloRedondeado(Rectangle area, int radio)
        {
            var diametro = radio * 2;
            var ruta = new GraphicsPath();
            ruta.AddArc(area.Left, area.Top, diametro, diametro, 180, 90);
            ruta.AddArc(area.Right - diametro, area.Top, diametro, diametro, 270, 90);
            ruta.AddArc(area.Right - diametro, area.Bottom - diametro, diametro, diametro, 0, 90);
            ruta.AddArc(area.Left, area.Bottom - diametro, diametro, diametro, 90, 90);
            ruta.CloseFigure();

            return ruta;
        }
    }
}

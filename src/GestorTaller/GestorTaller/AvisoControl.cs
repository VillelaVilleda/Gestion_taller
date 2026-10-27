using System.Drawing.Drawing2D;
using GestorTaller.Negocio;

namespace GestorTaller
{
    public enum TipoAviso
    {
        Exito,
        Error,
        Advertencia,
        Informacion
    }

    /// <summary>
    /// Aviso con icono y barra de color para mostrar el resultado de una accion (reemplaza a los
    /// Label de mensaje). Mostrar(tipo, texto) lo muestra y ajusta su alto al texto; Limpiar() lo oculta.
    /// </summary>
    public class AvisoControl : Control
    {
        private TipoAviso _tipo = TipoAviso.Informacion;

        public AvisoControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, false);
        }

        protected override Size DefaultSize => new(320, 40);

        public void Mostrar(TipoAviso tipo, string texto)
        {
            _tipo = tipo;
            Text = texto;
            Height = AltoNecesario();
            Visible = true;
            Invalidate();
        }

        public void Limpiar()
        {
            Text = string.Empty;
            Visible = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent is { BackColor.A: > 0 } ? Parent.BackColor : Tema.Fondo);

            var (colorPrincipal, colorFondo, icono) = Estilo();
            var area = new Rectangle(0, 0, Width - 1, Height - 1);
            using var ruta = Dibujo.RectanguloRedondeado(area, Escala(8));

            using (var pincel = new SolidBrush(colorFondo))
            {
                g.FillPath(pincel, ruta);
            }

            g.SetClip(ruta);
            using (var pincelBarra = new SolidBrush(colorPrincipal))
            {
                g.FillRectangle(pincelBarra, 0, 0, Escala(4), Height);
            }

            g.ResetClip();

            var tamanoIcono = Escala(18);
            g.DrawImage(Iconos.Obtener(icono, tamanoIcono, colorPrincipal), Escala(14), Escala(10), tamanoIcono, tamanoIcono);

            TextRenderer.DrawText(g, Text, Font, AreaTexto(), Tema.Titanio, FormatoTexto);
        }

        private const TextFormatFlags FormatoTexto = TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak
            | TextFormatFlags.Left | TextFormatFlags.VerticalCenter;

        private Rectangle AreaTexto() => new(Escala(42), 0, Width - Escala(42) - Escala(12), Height);

        private int AltoNecesario()
        {
            var anchoTexto = Math.Max(Width - Escala(42) - Escala(12), 1);
            var tamano = TextRenderer.MeasureText(Text, Font, new Size(anchoTexto, 0), FormatoTexto);

            return Math.Max(Escala(40), tamano.Height + Escala(20));
        }

        private (Color Principal, Color Fondo, string Icono) Estilo()
        {
            var (principal, fondo, icono) = _tipo switch
            {
                TipoAviso.Exito => (PaletaTema.Exito, PaletaTema.ExitoClaro, "circle-check"),
                TipoAviso.Error => (PaletaTema.Error, PaletaTema.ErrorClaro, "circle-alert"),
                TipoAviso.Advertencia => (PaletaTema.Advertencia, PaletaTema.AdvertenciaClaro, "triangle-alert"),
                _ => (PaletaTema.Informacion, PaletaTema.InformacionClaro, "info")
            };

            return (ColorTranslator.FromHtml(principal), ColorTranslator.FromHtml(fondo), icono);
        }

        private int Escala(int valor) => (int)Math.Round(valor * DeviceDpi / 96.0);
    }
}

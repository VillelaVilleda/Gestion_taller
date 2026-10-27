using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GestorTaller
{
    public enum VarianteBoton
    {
        Secundario,
        Primario,
        Acento,
        Peligro,
        Texto
    }

    /// <summary>
    /// Boton con el estilo del tema: esquinas redondeadas, variantes de color, icono opcional y
    /// estados (hover, presionado, foco y deshabilitado). Se usa igual que un Button normal.
    /// </summary>
    public class BotonTema : Button
    {
        private static readonly Color FondoDeshabilitado = ColorTranslator.FromHtml("#E2E8F0");
        private static readonly Color TextoDeshabilitado = ColorTranslator.FromHtml("#94A3B8");

        private VarianteBoton _variante = VarianteBoton.Secundario;
        private string _nombreIcono = string.Empty;
        private bool _hover;
        private bool _presionado;

        public BotonTema()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        [DefaultValue(VarianteBoton.Secundario)]
        public VarianteBoton Variante
        {
            get => _variante;
            set { _variante = value; Invalidate(); }
        }

        /// <summary>Nombre de un icono de Recursos\Iconos (por ejemplo "plus"); vacio = sin icono.</summary>
        [DefaultValue("")]
        public string NombreIcono
        {
            get => _nombreIcono;
            set { _nombreIcono = value ?? string.Empty; Invalidate(); }
        }

        protected override Size DefaultSize => new(150, Tema.AlturaControl);

        protected override Cursor DefaultCursor => Cursors.Hand;

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }

        protected override void OnMouseLeave(EventArgs e) { _hover = false; _presionado = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { _presionado = true; Invalidate(); }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) { _presionado = false; Invalidate(); base.OnMouseUp(e); }

        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }

        protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }

        protected override void OnLostFocus(EventArgs e) { Invalidate(); base.OnLostFocus(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent is { BackColor.A: > 0 } ? Parent.BackColor : Tema.Fondo);

            var (fondo, texto, borde) = Colores();
            var area = new Rectangle(0, 0, Width - 1, Height - 1);
            using var ruta = RectanguloRedondeado(area, Escala(Tema.RadioControl));

            if (fondo != Color.Transparent)
            {
                using var pincel = new SolidBrush(fondo);
                g.FillPath(pincel, ruta);
            }

            if (borde is { } colorBorde)
            {
                using var lapiz = new Pen(colorBorde);
                g.DrawPath(lapiz, ruta);
            }

            if (Focused && ShowFocusCues)
            {
                var anillo = _variante is VarianteBoton.Primario or VarianteBoton.Peligro ? Tema.Cobre : Tema.Pizarra;
                var interior = Rectangle.Inflate(area, -Escala(3), -Escala(3));
                using var rutaAnillo = RectanguloRedondeado(interior, Escala(Tema.RadioControl - 2));
                using var lapizAnillo = new Pen(anillo, 2);
                g.DrawPath(lapizAnillo, rutaAnillo);
            }

            DibujarContenido(g, texto);
        }

        private void DibujarContenido(Graphics g, Color texto)
        {
            const TextFormatFlags formato = TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding
                | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter | TextFormatFlags.Left;

            using var fuente = new Font("Segoe UI Semibold", Font.SizeInPoints);
            var tamanoTexto = TextRenderer.MeasureText(g, Text, fuente, Size.Empty, formato);
            var tamanoIcono = Escala(18);
            var separacion = Escala(8);
            var conIcono = _nombreIcono.Length > 0;
            var ancho = tamanoTexto.Width + (conIcono ? tamanoIcono + separacion : 0);
            var x = (Width - ancho) / 2;

            if (conIcono)
            {
                g.DrawImage(Iconos.Obtener(_nombreIcono, tamanoIcono, texto), x, (Height - tamanoIcono) / 2, tamanoIcono, tamanoIcono);
                x += tamanoIcono + separacion;
            }

            TextRenderer.DrawText(g, Text, fuente, new Rectangle(x, 0, tamanoTexto.Width + 2, Height), texto, formato);
        }

        private (Color Fondo, Color Texto, Color? Borde) Colores()
        {
            if (!Enabled)
            {
                return (_variante == VarianteBoton.Texto ? Color.Transparent : FondoDeshabilitado,
                        TextoDeshabilitado,
                        _variante == VarianteBoton.Secundario ? Tema.Aluminio : null);
            }

            return _variante switch
            {
                VarianteBoton.Primario => (Elegir(Tema.Pizarra, Tema.PizarraHover, Tema.PizarraPresionado), Tema.Blanco, null),
                VarianteBoton.Acento => (Elegir(Tema.Cobre, Tema.CobreHover, Tema.CobrePresionado), Tema.Titanio, null),
                VarianteBoton.Peligro => (Elegir(Tema.Error, Tema.ErrorHover, Tema.ErrorPresionado), Tema.Blanco, null),
                VarianteBoton.Texto => (Elegir(Color.Transparent, Tema.TextoHover, Tema.TextoPresionado), Tema.Pizarra, null),
                _ => (Elegir(Tema.Blanco, Tema.SecundarioHover, Tema.SecundarioPresionado), Tema.Pizarra, Tema.BordeCampo)
            };
        }

        private Color Elegir(Color normal, Color hover, Color presionado) =>
            _presionado ? presionado : _hover ? hover : normal;

        private int Escala(int valor) => (int)Math.Round(valor * DeviceDpi / 96.0);

        private static GraphicsPath RectanguloRedondeado(Rectangle area, int radio)
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

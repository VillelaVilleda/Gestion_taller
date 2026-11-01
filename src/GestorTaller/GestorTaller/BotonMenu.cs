using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GestorTaller
{
    /// <summary>
    /// Opción del menú lateral oscuro: icono a la izquierda y texto alineado a la izquierda. La opción
    /// activa lleva fondo más claro y una barra cobre; las demás cambian al pasar el mouse o presionar.
    /// Se usa igual que un Button normal (Click, Text, Visible).
    /// </summary>
    public class BotonMenu : Button
    {
        private string _nombreIcono = string.Empty;
        private bool _activo;
        private bool _hover;
        private bool _presionado;

        public BotonMenu()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        /// <summary>Nombre de un icono de Recursos\Iconos (por ejemplo "clipboard-list").</summary>
        [DefaultValue("")]
        public string NombreIcono
        {
            get => _nombreIcono;
            set { _nombreIcono = value ?? string.Empty; Invalidate(); }
        }

        /// <summary>True si es la sección que se está viendo.</summary>
        [DefaultValue(false)]
        public bool Activo
        {
            get => _activo;
            set { _activo = value; Invalidate(); }
        }

        protected override Size DefaultSize => new(212, 44);

        protected override Cursor DefaultCursor => Cursors.Hand;

        protected override void OnMouseEnter(EventArgs e) { _hover = true; Invalidate(); base.OnMouseEnter(e); }

        protected override void OnMouseLeave(EventArgs e) { _hover = false; _presionado = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { _presionado = true; Invalidate(); }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) { _presionado = false; Invalidate(); base.OnMouseUp(e); }

        protected override void OnGotFocus(EventArgs e) { Invalidate(); base.OnGotFocus(e); }

        protected override void OnLostFocus(EventArgs e) { Invalidate(); base.OnLostFocus(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent is { BackColor.A: > 0 } ? Parent.BackColor : Tema.Pizarra);

            var area = new Rectangle(0, 0, Width - 1, Height - 1);
            var fondo = _presionado ? Tema.PizarraPresionado
                : _activo ? Tema.MenuActivo
                : _hover ? Tema.MenuHover
                : Color.Transparent;

            using var ruta = Dibujo.RectanguloRedondeado(area, Escala(Tema.RadioControl));

            if (fondo != Color.Transparent)
            {
                using var pincel = new SolidBrush(fondo);
                g.FillPath(pincel, ruta);
            }

            if (_activo)
            {
                var alto = Escala(24);
                using var barra = new SolidBrush(Tema.Cobre);
                using var rutaBarra = Dibujo.RectanguloRedondeado(new Rectangle(0, (Height - alto) / 2, Escala(4), alto), Escala(2));
                g.FillPath(barra, rutaBarra);
            }

            if (Focused && ShowFocusCues)
            {
                using var lapiz = new Pen(Tema.Cobre, 2);
                using var rutaFoco = Dibujo.RectanguloRedondeado(Rectangle.Inflate(area, -Escala(2), -Escala(2)), Escala(Tema.RadioControl - 2));
                g.DrawPath(lapiz, rutaFoco);
            }

            var colorTexto = _activo ? Tema.Blanco : Tema.Aluminio;
            var x = Escala(16);

            if (_nombreIcono.Length > 0)
            {
                var tamanoIcono = Escala(20);
                g.DrawImage(Iconos.Obtener(_nombreIcono, tamanoIcono, colorTexto), x, (Height - tamanoIcono) / 2, tamanoIcono, tamanoIcono);
                x += tamanoIcono + Escala(12);
            }

            using var fuente = new Font(Font.FontFamily, Font.SizeInPoints, _activo ? FontStyle.Bold : FontStyle.Regular);
            TextRenderer.DrawText(g, Text, fuente, new Rectangle(x, 0, Math.Max(Width - x - Escala(8), 1), Height), colorTexto,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
                | TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
        }

        private int Escala(int valor) => Tema.Escalar(valor, DeviceDpi);
    }
}

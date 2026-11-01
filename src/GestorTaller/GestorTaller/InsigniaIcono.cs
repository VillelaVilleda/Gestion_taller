using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GestorTaller
{
    /// <summary>
    /// Icono centrado sobre un fondo de color con esquinas redondeadas o circular. Se usa como logo
    /// de la marca y como avatar del usuario. Los colores se asignan en código (ColorFondo, ColorIcono).
    /// </summary>
    public class InsigniaIcono : Control
    {
        private string _nombreIcono = string.Empty;
        private Color _colorFondo = Tema.Cobre;
        private Color _colorIcono = Tema.Titanio;
        private bool _circular;

        public InsigniaIcono()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, false);
        }

        [DefaultValue("")]
        public string NombreIcono
        {
            get => _nombreIcono;
            set { _nombreIcono = value ?? string.Empty; Invalidate(); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ColorFondo
        {
            get => _colorFondo;
            set { _colorFondo = value; Invalidate(); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ColorIcono
        {
            get => _colorIcono;
            set { _colorIcono = value; Invalidate(); }
        }

        /// <summary>True para un círculo (avatar); false para un cuadrado redondeado (logo).</summary>
        [DefaultValue(false)]
        public bool Circular
        {
            get => _circular;
            set { _circular = value; Invalidate(); }
        }

        protected override Size DefaultSize => new(48, 48);

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent is { BackColor.A: > 0 } ? Parent.BackColor : Tema.Fondo);

            var area = new Rectangle(0, 0, Width - 1, Height - 1);
            using var ruta = _circular
                ? Dibujo.RectanguloRedondeado(area, Math.Min(Width, Height) / 2)
                : Dibujo.RectanguloRedondeado(area, Tema.Escalar(12, DeviceDpi));
            using (var pincel = new SolidBrush(_colorFondo))
            {
                g.FillPath(pincel, ruta);
            }

            if (_nombreIcono.Length == 0)
            {
                return;
            }

            var tamano = Math.Min(Width, Height) / 2;
            g.DrawImage(Iconos.Obtener(_nombreIcono, tamano, _colorIcono), (Width - tamano) / 2, (Height - tamano) / 2, tamano, tamano);
        }
    }
}

using System.Drawing.Drawing2D;

namespace GestorTaller
{
    /// <summary>
    /// Panel blanco con esquinas redondeadas y un borde suave, para agrupar un formulario (por ejemplo,
    /// la tarjeta de inicio de sesión). Se usa igual que un Panel.
    /// </summary>
    public class TarjetaPanel : Panel
    {
        public TarjetaPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Tema.Blanco;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent is { BackColor.A: > 0 } ? Parent.BackColor : Tema.Fondo);

            var area = new Rectangle(0, 0, Width - 1, Height - 1);
            using var ruta = Dibujo.RectanguloRedondeado(area, Tema.Escalar(Tema.RadioTarjeta, DeviceDpi));
            using var pincel = new SolidBrush(BackColor);
            using var lapiz = new Pen(Tema.Aluminio);
            g.FillPath(pincel, ruta);
            g.DrawPath(lapiz, ruta);
        }
    }
}

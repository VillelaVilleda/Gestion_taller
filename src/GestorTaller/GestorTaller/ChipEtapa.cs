using System.ComponentModel;
using System.Drawing.Drawing2D;
using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Etiqueta redondeada con el color, el icono y el nombre de una etapa de la orden. Se usa como
    /// control (propiedad Etapa) y, con Medir y Dibujar, para pintarla dentro de la celda de una tabla.
    /// </summary>
    public class ChipEtapa : Control
    {
        private const TextFormatFlags Formato = TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding
            | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter | TextFormatFlags.Left;

        private EstadoOrden _etapa = EstadoOrden.Recepcion;

        public ChipEtapa()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, false);
            Actualizar();
        }

        [DefaultValue(EstadoOrden.Recepcion)]
        public EstadoOrden Etapa
        {
            get => _etapa;
            set { _etapa = value; Actualizar(); }
        }

        protected override Size DefaultSize => new(110, 26);

        public static string Nombre(EstadoOrden etapa) => etapa switch
        {
            EstadoOrden.Recepcion => "Recepción",
            EstadoOrden.Diagnostico => "Diagnóstico",
            EstadoOrden.Cotizacion => "Cotización",
            EstadoOrden.EnReparacion => "Reparación",
            EstadoOrden.Terminado => "Terminado",
            _ => "Entregado"
        };

        public static string Icono(EstadoOrden etapa) => etapa switch
        {
            EstadoOrden.Recepcion => "clipboard-list",
            EstadoOrden.Diagnostico => "search",
            EstadoOrden.Cotizacion => "badge-dollar-sign",
            EstadoOrden.EnReparacion => "wrench",
            EstadoOrden.Terminado => "clipboard-check",
            _ => "truck"
        };

        /// <summary>Tamaño del chip de la etapa, en pixeles, para la escala de pantalla (dpi) indicada.</summary>
        public static Size Medir(EstadoOrden etapa, int dpi)
        {
            using var fuente = CrearFuente();
            var texto = TextRenderer.MeasureText(Nombre(etapa), fuente, Size.Empty, Formato);

            return new Size(Escala(28, dpi) + texto.Width + Escala(11, dpi), Escala(26, dpi));
        }

        /// <summary>Dibuja el chip con su esquina superior izquierda en <paramref name="origen"/>.</summary>
        public static void Dibujar(Graphics g, EstadoOrden etapa, Point origen, int dpi)
        {
            var (colorTexto, colorFondo) = Tema.ColoresEtapa(etapa);
            var tamano = Medir(etapa, dpi);
            var area = new Rectangle(origen, new Size(tamano.Width - 1, tamano.Height - 1));
            var estadoAnterior = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var ruta = Capsula(area))
            using (var pincel = new SolidBrush(colorFondo))
            {
                g.FillPath(pincel, ruta);
            }

            g.SmoothingMode = estadoAnterior;

            var tamanoIcono = Escala(14, dpi);
            g.DrawImage(Iconos.Obtener(Icono(etapa), tamanoIcono, colorTexto),
                origen.X + Escala(9, dpi), origen.Y + (tamano.Height - tamanoIcono) / 2, tamanoIcono, tamanoIcono);

            using var fuente = CrearFuente();
            var areaTexto = new Rectangle(origen.X + Escala(28, dpi), origen.Y, tamano.Width - Escala(28, dpi), tamano.Height);
            TextRenderer.DrawText(g, Nombre(etapa), fuente, areaTexto, colorTexto, Formato);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Parent is { BackColor.A: > 0 } ? Parent.BackColor : Tema.Fondo);
            Dibujar(e.Graphics, _etapa, Point.Empty, DeviceDpi);
        }

        private void Actualizar()
        {
            Text = Nombre(_etapa);
            Size = Medir(_etapa, DeviceDpi);
            Invalidate();
        }

        private static Font CrearFuente() => new("Segoe UI Semibold", 9F);

        private static int Escala(int valor, int dpi) => (int)Math.Round(valor * dpi / 96.0);

        private static GraphicsPath Capsula(Rectangle area)
        {
            var diametro = area.Height;
            var ruta = new GraphicsPath();
            ruta.AddArc(area.Left, area.Top, diametro, diametro, 90, 180);
            ruta.AddArc(area.Right - diametro, area.Top, diametro, diametro, 270, 180);
            ruta.CloseFigure();

            return ruta;
        }
    }
}

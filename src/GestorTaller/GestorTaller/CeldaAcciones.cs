using System.Drawing.Drawing2D;

namespace GestorTaller
{
    /// <summary>
    /// Celda de la columna Acciones: se ve como un combobox ("Acciones" con una flecha). El menu con las
    /// acciones lo muestra la tabla al hacer clic. Solo se dibuja si el texto de la celda no esta vacio;
    /// la tabla lo deja vacio cuando la fila no tiene ninguna accion disponible.
    /// </summary>
    internal class CeldaAcciones : DataGridViewTextBoxCell
    {
        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object? value,
            object? formattedValue,
            string? errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // Fondo, selecciones y bordes de la celda; el contenido lo dibujamos aqui.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, string.Empty, errorText,
                cellStyle, advancedBorderStyle, paintParts & ~DataGridViewPaintParts.ContentForeground);

            var texto = formattedValue as string;
            if (string.IsNullOrEmpty(texto))
            {
                return;
            }

            var dpi = DataGridView?.DeviceDpi ?? 96;
            int Escala(int valor) => (int)Math.Round(valor * dpi / 96.0);

            var alto = Escala(28);
            var ancho = Math.Min(cellBounds.Width - Escala(16), Escala(140));
            var caja = new Rectangle(cellBounds.X + Escala(8), cellBounds.Y + (cellBounds.Height - alto) / 2, ancho, alto);

            var estadoAnterior = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var ruta = Dibujo.RectanguloRedondeado(caja, Escala(Tema.RadioControl)))
            using (var pincel = new SolidBrush(Tema.Blanco))
            using (var lapiz = new Pen(Tema.BordeCampo))
            {
                graphics.FillPath(pincel, ruta);
                graphics.DrawPath(lapiz, ruta);
            }

            graphics.SmoothingMode = estadoAnterior;

            var tamanoFlecha = Escala(16);
            graphics.DrawImage(Iconos.Obtener("chevron-down", tamanoFlecha, Tema.TextoSecundario),
                caja.Right - tamanoFlecha - Escala(6), caja.Y + (caja.Height - tamanoFlecha) / 2, tamanoFlecha, tamanoFlecha);

            var areaTexto = new Rectangle(caja.X + Escala(10), caja.Y, caja.Width - Escala(10) - tamanoFlecha - Escala(8), caja.Height);
            TextRenderer.DrawText(graphics, texto, Tema.Fuente(9F), areaTexto, Tema.Titanio,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }
    }
}

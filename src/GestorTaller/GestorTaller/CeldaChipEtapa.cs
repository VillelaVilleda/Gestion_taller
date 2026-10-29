using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Celda de tabla que muestra la etapa de una orden como chip (color, icono y nombre). El texto de
    /// la celda debe ser el nombre de la etapa (<see cref="ChipEtapa.Nombre"/>); si no coincide con
    /// ninguna, se muestra como texto normal.
    /// </summary>
    internal class CeldaChipEtapa : DataGridViewTextBoxCell
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
            var etapa = Enum.GetValues<EstadoOrden>()
                .Cast<EstadoOrden?>()
                .FirstOrDefault(e => ChipEtapa.Nombre(e!.Value) == formattedValue as string);

            // Con chip, la base solo pinta el fondo: el texto lo dibuja el propio chip.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value,
                etapa is null ? formattedValue : null, errorText, cellStyle, advancedBorderStyle, paintParts);

            if (etapa is null)
            {
                return;
            }

            var dpi = DataGridView?.DeviceDpi ?? 96;
            var tamano = ChipEtapa.Medir(etapa.Value, dpi);
            var margen = (int)Math.Round(8 * dpi / 96.0);
            var origen = new Point(cellBounds.X + margen, cellBounds.Y + (cellBounds.Height - tamano.Height) / 2);

            ChipEtapa.Dibujar(graphics, etapa.Value, origen, dpi);
        }
    }
}

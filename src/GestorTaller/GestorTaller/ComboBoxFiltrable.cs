using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// ComboBox que filtra sus opciones mientras se escribe (coincidencia parcial,
    /// sin importar mayusculas ni acentos) y solo permite quedarse con una opcion
    /// de la lista. Se usa para clientes, empleados y repuestos.
    /// Uso: cmb.CargarElementos(lista, x => x.Nombre); luego cmb.ElementoSeleccionado.
    /// </summary>
    public class ComboBoxFiltrable : ComboBox
    {
        private List<object> _todos = new();
        private Func<object, string> _selectorTexto = o => o.ToString() ?? string.Empty;
        private bool _filtrando;

        public ComboBoxFiltrable()
        {
            DropDownStyle = ComboBoxStyle.DropDown;
            AutoCompleteMode = AutoCompleteMode.None;
            FormattingEnabled = true;
            FlatStyle = FlatStyle.Flat;
            DrawMode = DrawMode.OwnerDrawFixed;
        }

        /// <summary>Elemento elegido de la lista, o null si no hay seleccion valida.</summary>
        public object? ElementoSeleccionado => SelectedItem;

        public void CargarElementos<T>(IEnumerable<T> elementos, Func<T, string> selectorTexto) where T : class
        {
            _todos = elementos.Cast<object>().ToList();
            _selectorTexto = o => selectorTexto((T)o);

            Rellenar(_todos);
            SelectedIndex = -1;
            Text = string.Empty;
        }

        /// <summary>Selecciona el elemento cuyo texto coincide; si no existe, solo muestra el texto.</summary>
        public void SeleccionarPorTexto(string? texto)
        {
            var coincidencia = _todos.FirstOrDefault(x =>
                FiltroDeBusqueda.Normalizar(_selectorTexto(x)) == FiltroDeBusqueda.Normalizar(texto));

            if (coincidencia is not null)
            {
                SelectedItem = coincidencia;
            }
            else
            {
                Text = texto ?? string.Empty;
            }
        }

        private void Rellenar(IEnumerable<object> elementos)
        {
            // Cambiar la lista con el desplegable abierto hace que WinForms pida el elemento 0
            // de una lista que acaba de vaciarse (ArgumentOutOfRangeException), asi que
            // primero se cierra.
            if (IsHandleCreated && DroppedDown)
            {
                DroppedDown = false;
            }

            BeginUpdate();
            Items.Clear();
            Items.AddRange(elementos.ToArray());
            EndUpdate();
        }

        // Fila de la lista con el estilo del tema: la opcion activa lleva fondo gris azulado y barra cobre.
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }

            var activa = (e.State & DrawItemState.Selected) != 0 && (e.State & DrawItemState.ComboBoxEdit) == 0;
            var g = e.Graphics;

            using (var fondo = new SolidBrush(activa ? Tema.SecundarioPresionado : Tema.Blanco))
            {
                g.FillRectangle(fondo, e.Bounds);
            }

            if (activa)
            {
                using var barra = new SolidBrush(Tema.Cobre);
                g.FillRectangle(barra, e.Bounds.X, e.Bounds.Y + 3, 3, e.Bounds.Height - 6);
            }

            var margen = (int)Math.Round(10 * DeviceDpi / 96.0);
            var area = new Rectangle(e.Bounds.X + margen, e.Bounds.Y, e.Bounds.Width - margen, e.Bounds.Height);
            TextRenderer.DrawText(g, GetItemText(Items[e.Index]), Font, area, Tema.Titanio,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        protected override void OnFormat(ListControlConvertEventArgs e)
        {
            base.OnFormat(e);
            e.Value = e.ListItem is null ? string.Empty : _selectorTexto(e.ListItem);
        }

        protected override void OnTextUpdate(EventArgs e)
        {
            base.OnTextUpdate(e);

            if (_filtrando)
            {
                return;
            }

            _filtrando = true;
            try
            {
                var texto = Text;
                var posicion = SelectionStart;
                var filtrados = FiltroDeBusqueda.Filtrar(_todos, texto, _selectorTexto);

                Rellenar(filtrados);

                Text = texto;
                SelectionStart = posicion;
                SelectionLength = 0;

                if (filtrados.Count > 0)
                {
                    DroppedDown = true;
                }

                Cursor.Current = Cursors.Default;
            }
            catch (ArgumentOutOfRangeException)
            {
                // Red de seguridad: si WinForms falla justo mientras se reconstruye la lista,
                // no se rompe la aplicacion; la siguiente tecla vuelve a filtrar.
            }
            finally
            {
                _filtrando = false;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (SelectedItem is not null)
            {
                return;
            }

            var coincidencia = _todos.FirstOrDefault(x =>
                FiltroDeBusqueda.Normalizar(_selectorTexto(x)) == FiltroDeBusqueda.Normalizar(Text));

            Rellenar(_todos);

            if (coincidencia is not null)
            {
                SelectedItem = coincidencia;
            }
            else
            {
                SelectedIndex = -1;
                Text = string.Empty;
            }
        }
    }
}

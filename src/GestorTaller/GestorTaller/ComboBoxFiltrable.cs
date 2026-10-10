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
            BeginUpdate();
            Items.Clear();
            Items.AddRange(elementos.ToArray());
            EndUpdate();
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
                DroppedDown = filtrados.Count > 0;
                Cursor.Current = Cursors.Default;
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

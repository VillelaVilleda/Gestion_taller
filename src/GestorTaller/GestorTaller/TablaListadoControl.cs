using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Tabla reutilizable: buscador con combobox de columnas, encabezados ordenables,
    /// columna Acciones con boton Ver y paginacion Atras/Sig. Las pantallas solo le
    /// pasan sus columnas y datos con <see cref="Cargar{T}"/>; la logica de buscar,
    /// ordenar y paginar vive en <see cref="ConsultaTabla{T}"/>.
    /// </summary>
    public partial class TablaListadoControl : UserControl
    {
        private const string ColumnaAcciones = "Acciones";

        private IVistaTabla? _vista;

        public TablaListadoControl()
        {
            InitializeComponent();
            AplicarEstilo();
        }

        // Se aplica aqui y no en el Designer: el diseñador no entiende las llamadas a Tema.
        private void AplicarEstilo()
        {
            dgvTabla.BackgroundColor = Tema.Blanco;
            dgvTabla.BorderStyle = BorderStyle.None;
            dgvTabla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTabla.GridColor = Tema.Aluminio;
            dgvTabla.EnableHeadersVisualStyles = false;
            dgvTabla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvTabla.ColumnHeadersHeight = 36;
            dgvTabla.RowTemplate.Height = 34;

            dgvTabla.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Tema.Pizarra,
                ForeColor = Tema.Blanco,
                SelectionBackColor = Tema.Pizarra,
                SelectionForeColor = Tema.Blanco,
                Font = new Font("Segoe UI Semibold", 9F),
                Padding = new Padding(6, 0, 6, 0)
            };

            dgvTabla.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Tema.Blanco,
                ForeColor = Tema.Titanio,
                SelectionBackColor = Tema.SecundarioPresionado,
                SelectionForeColor = Tema.Titanio,
                Padding = new Padding(6, 0, 6, 0)
            };

            dgvTabla.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 250, 251),
                SelectionBackColor = Tema.SecundarioPresionado,
                SelectionForeColor = Tema.Titanio
            };
        }

        /// <summary>
        /// Muestra los datos. Si se indica <paramref name="ver"/>, se agrega la columna
        /// Acciones con un boton Ver que la invoca con la fila. <paramref name="ordenInicial"/>
        /// permite arrancar ordenada (por ejemplo, codigo descendente).
        /// </summary>
        public void Cargar<T>(
            IReadOnlyList<ColumnaTabla<T>> columnas,
            IEnumerable<T> datos,
            Action<T>? ver = null,
            ColumnaTabla<T>? ordenInicial = null,
            DireccionOrden direccion = DireccionOrden.Ascendente)
        {
            var consulta = new ConsultaTabla<T>(columnas, datos);

            if (ordenInicial is not null)
            {
                consulta.Ordenar(ordenInicial, direccion);
            }

            _vista = new VistaTabla<T>(consulta, ver);

            cmbColumna.Items.Clear();
            foreach (var columna in columnas)
            {
                cmbColumna.Items.Add(columna.Titulo);
            }

            cmbColumna.SelectedIndex = 0;
            txtBuscar.Clear();

            dgvTabla.Columns.Clear();
            foreach (var columna in columnas)
            {
                var nueva = new DataGridViewTextBoxColumn { Name = columna.Titulo, SortMode = DataGridViewColumnSortMode.Programmatic };
                dgvTabla.Columns.Add(nueva);
            }

            if (ver is not null)
            {
                dgvTabla.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = ColumnaAcciones,
                    HeaderText = ColumnaAcciones,
                    Text = "Ver",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat,
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });
            }

            Refrescar();
        }

        private void Refrescar()
        {
            if (_vista is null)
            {
                return;
            }

            dgvTabla.Rows.Clear();
            foreach (var (fila, celdas) in _vista.FilasDePagina())
            {
                var indice = dgvTabla.Rows.Add(celdas);
                dgvTabla.Rows[indice].Tag = fila;
            }

            for (var i = 0; i < _vista.Titulos.Count; i++)
            {
                var indicador = _vista.IndicadorOrden(i);
                dgvTabla.Columns[i].HeaderText = indicador is null ? _vista.Titulos[i] : $"{_vista.Titulos[i]} {indicador}";
            }

            lblPagina.Text = $"{_vista.PaginaActual}/{_vista.TotalPaginas}";
            btnAtras.Enabled = _vista.HayAnterior;
            btnSiguiente.Enabled = _vista.HaySiguiente;
        }

        private void Buscar()
        {
            _vista?.Buscar(cmbColumna.SelectedIndex, txtBuscar.Text);
            Refrescar();
        }

        private void btnBuscar_Click(object? sender, EventArgs e) => Buscar();

        private void txtBuscar_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Buscar();
            }
        }

        private void dgvTabla_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (_vista is null || e.ColumnIndex >= _vista.Titulos.Count)
            {
                return;
            }

            _vista.Ordenar(e.ColumnIndex);
            Refrescar();
        }

        private void dgvTabla_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (_vista is null || e.RowIndex < 0 || dgvTabla.Columns[e.ColumnIndex].Name != ColumnaAcciones)
            {
                return;
            }

            _vista.Ver(dgvTabla.Rows[e.RowIndex].Tag!);
        }

        private void btnAtras_Click(object? sender, EventArgs e)
        {
            _vista?.PaginaAnterior();
            Refrescar();
        }

        private void btnSiguiente_Click(object? sender, EventArgs e)
        {
            _vista?.PaginaSiguiente();
            Refrescar();
        }

        // El control no es generico (el diseñador no lo permite), asi que habla con la
        // consulta a traves de esta interfaz sin tipo.
        private interface IVistaTabla
        {
            IReadOnlyList<string> Titulos { get; }
            int PaginaActual { get; }
            int TotalPaginas { get; }
            bool HayAnterior { get; }
            bool HaySiguiente { get; }
            IEnumerable<(object Fila, object[] Celdas)> FilasDePagina();
            string? IndicadorOrden(int columna);
            void Buscar(int columna, string texto);
            void Ordenar(int columna);
            void PaginaAnterior();
            void PaginaSiguiente();
            void Ver(object fila);
        }

        private sealed class VistaTabla<T> : IVistaTabla
        {
            private readonly ConsultaTabla<T> _consulta;
            private readonly Action<T>? _ver;

            public VistaTabla(ConsultaTabla<T> consulta, Action<T>? ver)
            {
                _consulta = consulta;
                _ver = ver;
            }

            public IReadOnlyList<string> Titulos => _consulta.Columnas.Select(c => c.Titulo).ToList();
            public int PaginaActual => _consulta.PaginaActual;
            public int TotalPaginas => _consulta.TotalPaginas;
            public bool HayAnterior => _consulta.HayAnterior;
            public bool HaySiguiente => _consulta.HaySiguiente;

            public IEnumerable<(object Fila, object[] Celdas)> FilasDePagina() =>
                _consulta.FilasDePagina.Select(f => ((object)f!, _consulta.Columnas.Select(c => (object)c.Texto(f)).ToArray()));

            public string? IndicadorOrden(int columna)
            {
                if (!ReferenceEquals(_consulta.ColumnaOrden, _consulta.Columnas[columna]))
                {
                    return null;
                }

                return _consulta.Direccion == DireccionOrden.Ascendente ? "▲" : "▼";
            }

            public void Buscar(int columna, string texto) => _consulta.Buscar(_consulta.Columnas[columna], texto);

            public void Ordenar(int columna) => _consulta.Ordenar(_consulta.Columnas[columna]);

            public void PaginaAnterior() => _consulta.PaginaAnterior();

            public void PaginaSiguiente() => _consulta.PaginaSiguiente();

            public void Ver(object fila) => _ver?.Invoke((T)fila);
        }
    }
}

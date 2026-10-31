using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana para que el administrador corrija la cotización de una orden: empleado, mano de obra y
    /// repuestos. El inventario se ajusta solo con lo que cambie. Si la cotización fue rechazada no se
    /// muestran los repuestos. <see cref="HuboCambios"/> indica si hubo algo que guardar.
    /// </summary>
    public class FormEditarCotizacion : FormDialogo
    {
        private readonly ModificarCotizacionOrdenService _servicio;
        private readonly CalcularTotalOrdenService _calcularTotal = new();
        private readonly PermisosUsuario _permisos;
        private readonly Orden _orden;
        private readonly List<OrdenRepuesto> _lineas;
        private readonly ComboBoxFiltrable _cmbEmpleado = new();
        private readonly NumericUpDown _numManoDeObra = new() { DecimalPlaces = 2, Maximum = 1000000, ThousandsSeparator = true };
        private readonly ComboBoxFiltrable _cmbRepuesto = new();
        private readonly NumericUpDown _numCantidad = new() { Minimum = 1, Maximum = 1000000, Value = 1 };
        private readonly Label _lblDisponible = new() { AutoSize = true, ForeColor = Tema.TextoSecundario };
        private readonly DataGridView _tabla = new() { Height = 150 };
        private readonly Label _lblTotal = new() { AutoSize = true, Font = Tema.Fuente(11F, FontStyle.Bold) };

        public FormEditarCotizacion(IOrdenRepository ordenRepository, IEmpleadoRepository empleadoRepository, IRepuestoRepository repuestoRepository, PermisosUsuario permisos, Orden orden)
            : base("Editar cotización")
        {
            _servicio = new ModificarCotizacionOrdenService(ordenRepository);
            _permisos = permisos;
            _orden = orden;
            _lineas = orden.Repuestos
                .Select(r => new OrdenRepuesto { OrdenId = r.OrdenId, RepuestoId = r.RepuestoId, Repuesto = r.Repuesto, Cantidad = r.Cantidad, PrecioUnitario = r.PrecioUnitario })
                .ToList();

            AgregarCampo("Empleado encargado", _cmbEmpleado);
            AgregarCampo("Mano de obra (Q)", _numManoDeObra);

            _cmbEmpleado.CargarElementos(empleadoRepository.ObtenerTodos().OrderBy(e => e.Nombre).ToList(), e => e.Nombre);

            if (orden.EmpleadoCotizacion is not null)
            {
                _cmbEmpleado.Seleccionar<Empleado>(e => e.Id == orden.EmpleadoCotizacion.Id);
            }

            _numManoDeObra.Value = Math.Min(orden.MontoCotizado, _numManoDeObra.Maximum);

            if (orden.CotizacionAceptada)
            {
                ConstruirSeccionRepuestos(repuestoRepository);
            }

            AgregarCampo(null, _lblTotal);
            _numManoDeObra.ValueChanged += (_, _) => ActualizarTabla();
            ActualizarTabla();
        }

        /// <summary>True si al guardar cambió algún dato; false si todo quedó igual.</summary>
        public bool HuboCambios { get; private set; }

        private void ConstruirSeccionRepuestos(IRepuestoRepository repuestoRepository)
        {
            _cmbRepuesto.CargarElementos(repuestoRepository.ObtenerTodos().OrderBy(r => r.Nombre).ToList(), r => r.Nombre);
            _cmbRepuesto.SelectedIndexChanged += (_, _) => ActualizarDisponible();

            var btnAgregar = new BotonTema { Text = "Agregar", NombreIcono = "plus", Size = new Size(120, Tema.AlturaControl), Margin = new Padding(8, 0, 0, 0) };
            var btnQuitar = new BotonTema { Text = "Quitar", NombreIcono = "minus", Size = new Size(120, Tema.AlturaControl), Margin = new Padding(0, 8, 0, 0) };
            btnAgregar.Click += (_, _) => AgregarLinea();
            btnQuitar.Click += (_, _) => QuitarLinea();

            var fila = new TableLayoutPanel { AutoSize = true, ColumnCount = 3, Margin = new Padding(0) };
            fila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            fila.ColumnStyles.Add(new ColumnStyle());
            fila.ColumnStyles.Add(new ColumnStyle());
            _cmbRepuesto.Dock = DockStyle.Fill;
            _cmbRepuesto.Margin = new Padding(0);
            _numCantidad.Width = 80;
            _numCantidad.Margin = new Padding(8, 0, 0, 0);
            fila.Controls.Add(_cmbRepuesto, 0, 0);
            fila.Controls.Add(_numCantidad, 1, 0);
            fila.Controls.Add(btnAgregar, 2, 0);

            _tabla.Columns.Add("repuesto", "Repuesto");
            _tabla.Columns.Add("cantidad", "Cant.");
            _tabla.Columns.Add("precio", "Precio");
            _tabla.Columns.Add("subtotal", "Subtotal");
            _tabla.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Tema.EstilizarTablaSimple(_tabla);

            AgregarCampo("Repuestos (si ya está en la lista, se cambia su cantidad)", fila);
            AgregarCampo(null, _lblDisponible);
            AgregarCampo(null, _tabla);
            AgregarCampo(null, btnQuitar);
            ActualizarDisponible();
        }

        private void ActualizarDisponible()
        {
            if (_cmbRepuesto.ElementoSeleccionado is not Repuesto repuesto)
            {
                _lblDisponible.Text = "Disponible: -";
                return;
            }

            // Lo que ya tiene la orden de este repuesto vuelve a estar disponible para ella.
            var enOrden = _orden.Repuestos.FirstOrDefault(r => r.RepuestoId == repuesto.Id)?.Cantidad ?? 0;
            _lblDisponible.Text = $"Disponible: {repuesto.Existencia + enOrden}  |  Precio: Q {repuesto.Precio:N2}";
        }

        private void AgregarLinea()
        {
            Aviso.Limpiar();

            if (_cmbRepuesto.ElementoSeleccionado is not Repuesto repuesto)
            {
                Aviso.Mostrar(TipoAviso.Error, "Selecciona un repuesto de la lista.");
                return;
            }

            var existente = _lineas.FirstOrDefault(l => l.RepuestoId == repuesto.Id);

            if (existente is null)
            {
                _lineas.Add(new OrdenRepuesto { OrdenId = _orden.Id, RepuestoId = repuesto.Id, Repuesto = repuesto, Cantidad = (int)_numCantidad.Value, PrecioUnitario = repuesto.Precio });
            }
            else
            {
                existente.Cantidad = (int)_numCantidad.Value;
            }

            _numCantidad.Value = 1;
            ActualizarTabla();
        }

        private void QuitarLinea()
        {
            Aviso.Limpiar();

            if (_tabla.SelectedRows.Count == 0 || _tabla.SelectedRows[0].Tag is not Guid repuestoId)
            {
                Aviso.Mostrar(TipoAviso.Error, "Selecciona en la tabla el repuesto que quieres quitar.");
                return;
            }

            _lineas.RemoveAll(l => l.RepuestoId == repuestoId);
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            _tabla.Rows.Clear();

            foreach (var linea in _lineas)
            {
                var indice = _tabla.Rows.Add(
                    linea.Repuesto?.Nombre ?? "(sin nombre)",
                    linea.Cantidad,
                    $"Q {linea.PrecioUnitario:N2}",
                    $"Q {linea.PrecioUnitario * linea.Cantidad:N2}");
                _tabla.Rows[indice].Tag = linea.RepuestoId;
            }

            _tabla.ClearSelection();
            _lblTotal.Text = $"Total a cobrar: Q {_calcularTotal.Calcular(_orden.CostoDiagnostico, _numManoDeObra.Value, _lineas).Total:N2}";
        }

        protected override bool AlGuardar()
        {
            if (_cmbEmpleado.ElementoSeleccionado is not Empleado empleado)
            {
                throw new ArgumentException("Selecciona un empleado de la lista.");
            }

            var repuestos = _lineas.Select(l => new RepuestoCotizado(l.Repuesto!, l.Cantidad)).ToList();
            HuboCambios = _servicio.Ejecutar(_permisos, _orden, empleado, _numManoDeObra.Value, repuestos);

            return true;
        }
    }
}

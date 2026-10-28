using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Sección de "Repuestos" dentro de la ventana principal (menú lateral).
    /// Tabla con una columna de acciones según el rol (ver AccionesPermitidas): todos pueden registrar
    /// repuestos e incrementar la existencia; el administrador también decrementarla, modificar y
    /// eliminar (si el repuesto no está en ninguna orden). Cada acción abre un diálogo o pide confirmación.
    /// </summary>
    public partial class RepuestosControl : UserControl
    {
        private readonly IRepuestoRepository _repuestoRepository;
        private readonly PermisosUsuario _permisos;
        private readonly AccionesPermitidas _accionesPermitidas;
        private readonly EliminarRepuestoService _eliminarRepuestoService;

        public RepuestosControl(IRepuestoRepository repuestoRepository, IOrdenRepository ordenRepository, Empleado usuarioActual)
        {
            InitializeComponent();
            _repuestoRepository = repuestoRepository;
            _permisos = new PermisosUsuario(usuarioActual);
            _accionesPermitidas = new AccionesPermitidas(_permisos);
            _eliminarRepuestoService = new EliminarRepuestoService(repuestoRepository, ordenRepository);

            btnNuevo.Visible = _accionesPermitidas.Permite(ModuloTabla.Repuestos, AccionTabla.Insertar);
            avisoMensaje.Limpiar();
            Load += RepuestosControl_Load;
        }

        private void RepuestosControl_Load(object? sender, EventArgs e)
        {
            CargarRepuestos();
        }

        private void CargarRepuestos()
        {
            var columnas = new List<ColumnaTabla<Repuesto>>
            {
                new("Nombre", r => r.Nombre),
                new("Existencia", r => r.Existencia),
                new("Precio", r => r.Precio, r => $"Q {r.Precio:N2}")
            };

            var acciones = new List<AccionFila<Repuesto>>();
            foreach (var accion in _accionesPermitidas.Para(ModuloTabla.Repuestos))
            {
                switch (accion)
                {
                    case AccionTabla.Incrementar:
                        acciones.Add(new AccionFila<Repuesto>(accion, r => AjustarExistencia(r, incrementar: true)));
                        break;
                    case AccionTabla.Decrementar:
                        acciones.Add(new AccionFila<Repuesto>(accion, r => AjustarExistencia(r, incrementar: false)));
                        break;
                    case AccionTabla.Modificar:
                        acciones.Add(new AccionFila<Repuesto>(accion, ModificarRepuesto));
                        break;
                    case AccionTabla.Eliminar:
                        acciones.Add(new AccionFila<Repuesto>(accion, EliminarRepuesto));
                        break;
                }
            }

            tablaRepuestos.Cargar(columnas, _repuestoRepository.ObtenerTodos(), ordenInicial: columnas[0], acciones: acciones);
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            avisoMensaje.Limpiar();

            using var dialogo = new FormRepuesto(_repuestoRepository, _permisos);

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                avisoMensaje.Mostrar(TipoAviso.Exito, $"Repuesto \"{dialogo.Resultado!.Nombre}\" registrado.");
                CargarRepuestos();
            }
        }

        private void AjustarExistencia(Repuesto repuesto, bool incrementar)
        {
            avisoMensaje.Limpiar();

            using var dialogo = new FormAjusteExistencia(_repuestoRepository, _permisos, repuesto, incrementar);

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                avisoMensaje.Mostrar(TipoAviso.Exito, $"Existencia de \"{repuesto.Nombre}\": {repuesto.Existencia}.");
                CargarRepuestos();
            }
        }

        private void ModificarRepuesto(Repuesto repuesto)
        {
            avisoMensaje.Limpiar();

            using var dialogo = new FormRepuesto(_repuestoRepository, _permisos, repuesto);

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                avisoMensaje.Mostrar(TipoAviso.Exito, $"Repuesto \"{repuesto.Nombre}\" actualizado.");
                CargarRepuestos();
            }
        }

        private void EliminarRepuesto(Repuesto repuesto)
        {
            avisoMensaje.Limpiar();

            var confirmar = DialogoConfirmacion.Confirmar(
                this,
                "Eliminar repuesto",
                $"Se eliminará el repuesto \"{repuesto.Nombre}\". ¿Continuar?");

            if (!confirmar)
            {
                return;
            }

            try
            {
                _eliminarRepuestoService.Ejecutar(_permisos, repuesto);

                avisoMensaje.Mostrar(TipoAviso.Exito, $"Repuesto \"{repuesto.Nombre}\" eliminado.");
                CargarRepuestos();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                DialogoConfirmacion.Informar(this, "No se pudo eliminar el repuesto", ex.Message);
            }
        }
    }
}

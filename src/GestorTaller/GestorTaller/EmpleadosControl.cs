using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Sección de "Empleados" dentro de la ventana principal (solo administrador). Tabla con una columna
    /// de acciones (ver AccionesPermitidas): editar empleado, editar usuario, restablecer contraseña y
    /// eliminar; el botón "Nuevo empleado" abre el diálogo de registro. El usuario y la contraseña
    /// temporal los genera el sistema; la contraseña se muestra una sola vez y nunca se muestra el hash.
    /// </summary>
    public partial class EmpleadosControl : UserControl
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly Empleado _usuarioActual;
        private readonly PermisosUsuario _permisos;
        private readonly AccionesPermitidas _accionesPermitidas;
        private readonly RestablecerContrasenaService _restablecerContrasenaService;
        private readonly EliminarEmpleadoService _eliminarEmpleadoService;

        public EmpleadosControl(IEmpleadoRepository empleadoRepository, IOrdenRepository ordenRepository, Empleado usuarioActual)
        {
            InitializeComponent();
            _empleadoRepository = empleadoRepository;
            _usuarioActual = usuarioActual;
            _permisos = new PermisosUsuario(usuarioActual);
            _accionesPermitidas = new AccionesPermitidas(_permisos);
            _restablecerContrasenaService = new RestablecerContrasenaService(empleadoRepository);
            _eliminarEmpleadoService = new EliminarEmpleadoService(empleadoRepository, ordenRepository);

            btnNuevo.Visible = _accionesPermitidas.Permite(ModuloTabla.Empleados, AccionTabla.Insertar);
            avisoMensaje.Limpiar();
            Load += EmpleadosControl_Load;
        }

        private void EmpleadosControl_Load(object? sender, EventArgs e)
        {
            CargarEmpleados();
        }

        private void CargarEmpleados()
        {
            var columnas = new List<ColumnaTabla<Empleado>>
            {
                new("Nombre", e => e.Nombre),
                new("Usuario", e => e.NombreUsuario),
                new("Administrador", e => e.EsAdministrador, e => e.EsAdministrador ? "Sí" : "No")
            };

            var acciones = new List<AccionFila<Empleado>>();
            foreach (var accion in _accionesPermitidas.Para(ModuloTabla.Empleados))
            {
                switch (accion)
                {
                    case AccionTabla.EditarEmpleado:
                        acciones.Add(new AccionFila<Empleado>(accion, e => Editar(e, ModoFormEmpleado.EditarEmpleado, "actualizado")));
                        break;
                    case AccionTabla.EditarUsuario:
                        acciones.Add(new AccionFila<Empleado>(accion, e => Editar(e, ModoFormEmpleado.EditarUsuario, "actualizado")));
                        break;
                    case AccionTabla.RestablecerContrasena:
                        acciones.Add(new AccionFila<Empleado>(accion, RestablecerContrasena));
                        break;
                    case AccionTabla.Eliminar:
                        acciones.Add(new AccionFila<Empleado>(accion, EliminarEmpleado));
                        break;
                }
            }

            tablaEmpleados.Cargar(columnas, _empleadoRepository.ObtenerTodos(), ordenInicial: columnas[0], acciones: acciones);
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            avisoMensaje.Limpiar();

            using var dialogo = new FormEmpleado(_empleadoRepository, _usuarioActual, ModoFormEmpleado.Nuevo);

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                var creado = dialogo.Creado!;

                avisoMensaje.Mostrar(TipoAviso.Exito, $"Empleado \"{creado.Empleado.Nombre}\" registrado.");
                CargarEmpleados();
                MostrarCredenciales(creado.Empleado.NombreUsuario, creado.PasswordTemporal);
            }
        }

        private void Editar(Empleado empleado, ModoFormEmpleado modo, string accionRealizada)
        {
            avisoMensaje.Limpiar();

            using var dialogo = new FormEmpleado(_empleadoRepository, _usuarioActual, modo, empleado);

            if (dialogo.ShowDialog(this) == DialogResult.OK)
            {
                avisoMensaje.Mostrar(TipoAviso.Exito, $"Empleado \"{empleado.Nombre}\" {accionRealizada}.");
                CargarEmpleados();
            }
        }

        private void RestablecerContrasena(Empleado empleado)
        {
            avisoMensaje.Limpiar();

            var confirmar = DialogoConfirmacion.Confirmar(
                this,
                "Restablecer contraseña",
                $"Se generará una contraseña temporal nueva para {empleado.Nombre}. La actual dejará de funcionar. ¿Continuar?",
                "Restablecer");

            if (!confirmar)
            {
                return;
            }

            try
            {
                var password = _restablecerContrasenaService.Ejecutar(_permisos, empleado);

                avisoMensaje.Mostrar(TipoAviso.Exito, $"Contraseña de \"{empleado.Nombre}\" restablecida.");
                MostrarCredenciales(empleado.NombreUsuario, password);
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                DialogoConfirmacion.Informar(this, "No se pudo restablecer la contraseña", ex.Message);
            }
        }

        private void EliminarEmpleado(Empleado empleado)
        {
            avisoMensaje.Limpiar();

            var confirmar = DialogoConfirmacion.Confirmar(
                this,
                "Eliminar empleado",
                $"Se eliminará al empleado \"{empleado.Nombre}\". ¿Continuar?");

            if (!confirmar)
            {
                return;
            }

            try
            {
                _eliminarEmpleadoService.Ejecutar(_usuarioActual, empleado);

                avisoMensaje.Mostrar(TipoAviso.Exito, $"Empleado \"{empleado.Nombre}\" eliminado.");
                CargarEmpleados();
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                DialogoConfirmacion.Informar(this, "No se pudo eliminar el empleado", ex.Message);
            }
        }

        // La contraseña temporal solo se muestra aquí; después ya no se puede consultar.
        private void MostrarCredenciales(string usuario, string password)
        {
            DialogoConfirmacion.Informar(
                this,
                "Credenciales del empleado",
                $"Usuario: {usuario}\nContraseña temporal: {password}\n\nAnota la contraseña ahora: no se volverá a mostrar.");
        }
    }
}

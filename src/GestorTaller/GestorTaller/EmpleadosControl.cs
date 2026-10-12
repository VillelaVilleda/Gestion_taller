using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Seccion de "Empleados" dentro de la ventana principal (menu lateral).
    /// Solo lectura por ahora: crear/editar/eliminar empleados queda para una
    /// issue futura. No se muestra la contrasena (ni su hash).
    /// </summary>
    public partial class EmpleadosControl : UserControl
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadosControl(IEmpleadoRepository empleadoRepository)
        {
            InitializeComponent();
            _empleadoRepository = empleadoRepository;
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
                new("Administrador", e => e.EsAdministrador, e => e.EsAdministrador ? "Si" : "No")
            };

            tablaEmpleados.Cargar(columnas, _empleadoRepository.ObtenerTodos(), ordenInicial: columnas[0]);
        }
    }
}

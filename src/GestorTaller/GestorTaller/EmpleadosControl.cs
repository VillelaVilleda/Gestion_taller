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
            dgvEmpleados.Columns.Clear();
            dgvEmpleados.Columns.Add("Nombre", "Nombre");
            dgvEmpleados.Columns.Add("NombreUsuario", "Usuario");
            dgvEmpleados.Columns.Add("EsAdministrador", "Administrador");

            dgvEmpleados.Rows.Clear();

            foreach (var empleado in _empleadoRepository.ObtenerTodos())
            {
                dgvEmpleados.Rows.Add(
                    empleado.Nombre,
                    empleado.NombreUsuario,
                    empleado.EsAdministrador ? "Si" : "No");
            }
        }
    }
}

using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana principal de la aplicacion: menu lateral a la izquierda y un
    /// panel de contenido a la derecha, donde se muestran las distintas
    /// secciones (Nueva orden, Ordenes registradas, etc.).
    ///
    /// Por ahora los botones del menu todavia no cambian el contenido; eso
    /// se conecta en el siguiente paso.
    /// </summary>
    public partial class FormPrincipal : Form
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly Action _cerrarSesion;
        private bool _cerrandoSesion;

        public FormPrincipal(IOrdenRepository ordenRepository, Action cerrarSesion)
        {
            InitializeComponent();
            _ordenRepository = ordenRepository;
            _cerrarSesion = cerrarSesion;
        }

        private void FormPrincipal_Load(object? sender, EventArgs e)
        {
            MostrarSeccionCrearOrden();
        }

        private void FormPrincipal_FormClosed(object? sender, FormClosedEventArgs e)
        {
            // Si la ventana se cierra por cualquier via que no sea el boton
            // "Cerrar sesion" (la X de la ventana, Alt+F4, etc.), se entiende
            // como que el usuario quiere salir de la aplicacion por completo.
            if (!_cerrandoSesion)
            {
                Application.Exit();
            }
        }

        private void btnCerrarSesion_Click(object? sender, EventArgs e)
        {
            _cerrandoSesion = true;
            _cerrarSesion();
            Close();
        }

        private void btnSalir_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCrearOrden_Click(object? sender, EventArgs e) => MostrarSeccionCrearOrden();

        private void btnOrdenesRegistradas_Click(object? sender, EventArgs e) => MostrarSeccionOrdenesRegistradas();

        private void btnRepuestos_Click(object? sender, EventArgs e) => MostrarPlaceholder("Repuestos");

        private void btnClientes_Click(object? sender, EventArgs e) => MostrarPlaceholder("Clientes");

        private void btnInventario_Click(object? sender, EventArgs e) => MostrarPlaceholder("Inventario");

        private void btnEmpleados_Click(object? sender, EventArgs e) => MostrarPlaceholder("Empleados");

        private void MostrarSeccionCrearOrden() => MostrarControl(new CrearOrdenControl(_ordenRepository));

        private void MostrarSeccionOrdenesRegistradas() => MostrarControl(new ListadoOrdenesControl(_ordenRepository));

        private void MostrarPlaceholder(string seccion)
        {
            var lblPlaceholder = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12F),
                Text = $"{seccion} - Proximamente"
            };

            MostrarControl(lblPlaceholder);
        }

        private void MostrarControl(Control control)
        {
            panelContenido.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContenido.Controls.Add(control);
        }
    }
}

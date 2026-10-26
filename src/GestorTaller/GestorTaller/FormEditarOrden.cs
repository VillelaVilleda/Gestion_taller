using GestorTaller.Negocio;

namespace GestorTaller
{
    /// <summary>
    /// Ventana para que el administrador edite los datos de recepcion de una orden
    /// (detalles del objeto, detalles del problema y costo del diagnostico).
    /// Los controles se crean en codigo porque es un dialogo pequeno.
    /// </summary>
    public class FormEditarOrden : Form
    {
        private readonly ModificarOrdenService _modificarOrdenService;
        private readonly PermisosUsuario _permisos;
        private readonly Orden _orden;
        private readonly TextBox _txtObjeto = new() { Location = new Point(20, 40), Size = new Size(420, 60), Multiline = true };
        private readonly TextBox _txtProblema = new() { Location = new Point(20, 125), Size = new Size(420, 80), Multiline = true };
        private readonly NumericUpDown _numCosto = new()
        {
            Location = new Point(20, 230),
            Size = new Size(140, 27),
            DecimalPlaces = 2,
            Maximum = 1000000,
            ThousandsSeparator = true
        };
        private readonly Label _lblMensaje = new() { AutoSize = true, ForeColor = Color.DarkRed, Location = new Point(20, 265), MaximumSize = new Size(420, 0) };

        public FormEditarOrden(IOrdenRepository ordenRepository, PermisosUsuario permisos, Orden orden)
        {
            _modificarOrdenService = new ModificarOrdenService(ordenRepository);
            _permisos = permisos;
            _orden = orden;

            Text = "Editar datos de la orden";
            ClientSize = new Size(460, 350);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            _txtObjeto.Text = orden.DescripcionObjeto;
            _txtProblema.Text = orden.DescripcionProblema;
            _numCosto.Value = orden.CostoDiagnostico;

            var btnGuardar = new Button { Text = "Guardar", Location = new Point(20, 305), Size = new Size(150, 32) };
            var btnCancelar = new Button { Text = "Cancelar", Location = new Point(190, 305), Size = new Size(150, 32), DialogResult = DialogResult.Cancel };
            btnGuardar.Click += btnGuardar_Click;
            CancelButton = btnCancelar;

            Controls.Add(CrearEtiqueta("Detalles del objeto", 15));
            Controls.Add(_txtObjeto);
            Controls.Add(CrearEtiqueta("Detalles del problema", 100));
            Controls.Add(_txtProblema);
            Controls.Add(CrearEtiqueta("Costo del diagnostico (Q)", 207));
            Controls.Add(_numCosto);
            Controls.Add(_lblMensaje);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
        }

        private static Label CrearEtiqueta(string texto, int y) => new() { Text = texto, AutoSize = true, Location = new Point(20, y) };

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            _lblMensaje.Text = "";

            try
            {
                _modificarOrdenService.Ejecutar(_permisos, _orden, _txtObjeto.Text, _txtProblema.Text, _numCosto.Value);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                _lblMensaje.Text = ex.Message;
            }
        }
    }
}

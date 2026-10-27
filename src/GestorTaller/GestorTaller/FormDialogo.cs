namespace GestorTaller
{
    /// <summary>
    /// Ventana emergente base para los formularios (insertar, modificar...): titulo, campos con
    /// etiqueta, aviso de error y botones Guardar y Cancelar. Se acomoda sola al contenido, de tamano
    /// fijo y centrada sobre la ventana que la abre. Para usarla se hereda y se llama a
    /// <see cref="AgregarCampo"/> por cada campo; en <see cref="AlGuardar"/> se hace el trabajo y se
    /// devuelve true para cerrar. Si lanza ArgumentException o InvalidOperationException, el mensaje
    /// se muestra como aviso y la ventana sigue abierta.
    /// </summary>
    public class FormDialogo : Form
    {
        private readonly TableLayoutPanel _principal = new()
        {
            AutoSize = true,
            ColumnCount = 1,
            Dock = DockStyle.Fill,
            Padding = new Padding(Tema.RellenoPantalla),
            MinimumSize = new Size(420, 0)
        };

        private readonly TableLayoutPanel _campos = new()
        {
            AutoSize = true,
            ColumnCount = 1,
            Dock = DockStyle.Fill,
            Margin = new Padding(0)
        };

        private int _filaSiguiente;

        public FormDialogo(string titulo, string textoGuardar = "Guardar")
        {
            Text = titulo;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Tema.Aplicar(this);

            var lblTitulo = new Label
            {
                Text = titulo,
                AutoSize = true,
                Font = Tema.Fuente(14F, FontStyle.Bold),
                Margin = new Padding(0, 0, 0, 8)
            };

            Aviso.Dock = DockStyle.Fill;
            Aviso.Margin = new Padding(0, 12, 0, 0);
            Aviso.Limpiar();

            var btnGuardar = new BotonTema
            {
                Text = textoGuardar,
                Variante = VarianteBoton.Primario,
                Size = new Size(110, Tema.AlturaControl),
                Margin = new Padding(8, 0, 0, 0)
            };
            var btnCancelar = new BotonTema
            {
                Text = "Cancelar",
                Size = new Size(110, Tema.AlturaControl),
                Margin = new Padding(0),
                DialogResult = DialogResult.Cancel
            };
            btnGuardar.Click += btnGuardar_Click;
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;

            // De derecha a izquierda: el primero que se agrega queda a la derecha (Guardar).
            var botones = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Margin = new Padding(0, 16, 0, 0)
            };
            botones.Controls.Add(btnGuardar);
            botones.Controls.Add(btnCancelar);

            _campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _principal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _principal.Controls.Add(lblTitulo, 0, 0);
            _principal.Controls.Add(_campos, 0, 1);
            _principal.Controls.Add(Aviso, 0, 2);
            _principal.Controls.Add(botones, 0, 3);
            Controls.Add(_principal);
        }

        /// <summary>Aviso del dialogo, para que las ventanas hijas muestren sus propios mensajes.</summary>
        protected AvisoControl Aviso { get; } = new();

        /// <summary>Agrega un campo con su etiqueta (opcional) debajo de los anteriores; ocupa todo el ancho.</summary>
        protected void AgregarCampo(string? etiqueta, Control campo)
        {
            if (etiqueta is not null)
            {
                _campos.Controls.Add(new Label
                {
                    Text = etiqueta,
                    AutoSize = true,
                    ForeColor = Tema.TextoSecundario,
                    Margin = new Padding(0, 12, 0, 4)
                }, 0, _filaSiguiente++);
            }

            campo.Dock = DockStyle.Fill;
            campo.Margin = new Padding(0);
            _campos.Controls.Add(campo, 0, _filaSiguiente++);
        }

        /// <summary>Se ejecuta al pulsar Guardar. Devuelve true para cerrar la ventana con resultado OK.</summary>
        protected virtual bool AlGuardar() => true;

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            Aviso.Limpiar();

            try
            {
                if (AlGuardar())
                {
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                Aviso.Mostrar(TipoAviso.Error, ex.Message);
            }
        }
    }
}

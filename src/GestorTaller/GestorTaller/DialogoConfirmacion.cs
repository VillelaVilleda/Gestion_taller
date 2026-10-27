namespace GestorTaller
{
    /// <summary>
    /// Ventana para confirmar una accion (por ejemplo, eliminar) o para informar que no se puede hacer
    /// (por ejemplo, un registro en uso). Reemplaza a MessageBox con el estilo del tema:
    ///   if (DialogoConfirmacion.Confirmar(this, "Eliminar cliente", "Se eliminara... ¿Continuar?")) { ... }
    ///   DialogoConfirmacion.Informar(this, "No se pudo eliminar", mensaje);
    /// Al confirmar una accion destructiva, el boton por defecto es Cancelar.
    /// </summary>
    public class DialogoConfirmacion : Form
    {
        private DialogoConfirmacion(string titulo, string mensaje, string textoConfirmar, bool esConfirmacion)
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

            var colorIcono = esConfirmacion ? Tema.Advertencia : Tema.Informacion;
            var icono = new PictureBox
            {
                Image = Iconos.Obtener(esConfirmacion ? "triangle-alert" : "info", 32, colorIcono),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Size = new Size(44, 44),
                Margin = new Padding(0, 0, 12, 0)
            };

            var lblTitulo = new Label
            {
                Text = titulo,
                AutoSize = true,
                Font = Tema.Fuente(12F, FontStyle.Bold),
                MaximumSize = new Size(380, 0),
                Margin = new Padding(0, 4, 0, 6)
            };
            var lblMensaje = new Label
            {
                Text = mensaje,
                AutoSize = true,
                MaximumSize = new Size(380, 0),
                Margin = new Padding(0)
            };

            var textos = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Margin = new Padding(0)
            };
            textos.Controls.Add(lblTitulo);
            textos.Controls.Add(lblMensaje);

            var botones = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Margin = new Padding(0, 20, 0, 0)
            };

            // De derecha a izquierda: el primero que se agrega queda a la derecha.
            var btnPrincipal = new BotonTema
            {
                Text = esConfirmacion ? textoConfirmar : "Aceptar",
                Variante = esConfirmacion ? VarianteBoton.Peligro : VarianteBoton.Primario,
                Size = new Size(120, Tema.AlturaControl),
                Margin = new Padding(8, 0, 0, 0),
                DialogResult = DialogResult.OK
            };
            botones.Controls.Add(btnPrincipal);

            if (esConfirmacion)
            {
                var btnCancelar = new BotonTema
                {
                    Text = "Cancelar",
                    Size = new Size(110, Tema.AlturaControl),
                    Margin = new Padding(0),
                    DialogResult = DialogResult.Cancel
                };
                botones.Controls.Add(btnCancelar);
                AcceptButton = btnCancelar;
                CancelButton = btnCancelar;
            }
            else
            {
                AcceptButton = btnPrincipal;
                CancelButton = btnPrincipal;
            }

            var principal = new TableLayoutPanel
            {
                AutoSize = true,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                Padding = new Padding(Tema.RellenoPantalla),
                MinimumSize = new Size(420, 0)
            };
            principal.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            principal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            principal.Controls.Add(icono, 0, 0);
            principal.Controls.Add(textos, 1, 0);
            principal.Controls.Add(botones, 0, 1);
            principal.SetColumnSpan(botones, 2);
            Controls.Add(principal);
        }

        /// <summary>Pregunta si se debe continuar. Devuelve true si el usuario pulsa el boton de confirmar.</summary>
        public static bool Confirmar(IWin32Window? dueno, string titulo, string mensaje, string textoConfirmar = "Eliminar")
        {
            using var dialogo = new DialogoConfirmacion(titulo, mensaje, textoConfirmar, esConfirmacion: true);

            return dialogo.ShowDialog(dueno) == DialogResult.OK;
        }

        /// <summary>Muestra un mensaje con un solo boton Aceptar.</summary>
        public static void Informar(IWin32Window? dueno, string titulo, string mensaje)
        {
            using var dialogo = new DialogoConfirmacion(titulo, mensaje, "Aceptar", esConfirmacion: false);
            dialogo.ShowDialog(dueno);
        }
    }
}

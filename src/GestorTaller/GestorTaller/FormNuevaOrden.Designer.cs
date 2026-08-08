namespace GestorTaller
{
    partial class FormNuevaOrden
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.NumericUpDown numCostoDiagnostico;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnVerOrdenes;
        private System.Windows.Forms.Label lblMensaje;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblTitulo = new Label();
            lblCliente = new Label();
            txtCliente = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblCosto = new Label();
            numCostoDiagnostico = new NumericUpDown();
            btnRegistrar = new Button();
            btnVerOrdenes = new Button();
            lblMensaje = new Label();

            ((System.ComponentModel.ISupportInitialize)numCostoDiagnostico).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Text = "Nueva orden de trabajo";

            // lblCliente
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(20, 65);
            lblCliente.Text = "Cliente";

            // txtCliente
            txtCliente.Location = new Point(20, 85);
            txtCliente.Size = new Size(340, 23);

            // lblDescripcion
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(20, 120);
            lblDescripcion.Text = "Descripcion del problema";

            // txtDescripcion
            txtDescripcion.Location = new Point(20, 140);
            txtDescripcion.Multiline = true;
            txtDescripcion.Size = new Size(340, 80);

            // lblCosto
            lblCosto.AutoSize = true;
            lblCosto.Location = new Point(20, 235);
            lblCosto.Text = "Costo de diagnostico";

            // numCostoDiagnostico
            numCostoDiagnostico.DecimalPlaces = 2;
            numCostoDiagnostico.Location = new Point(20, 255);
            numCostoDiagnostico.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCostoDiagnostico.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numCostoDiagnostico.Size = new Size(150, 23);
            numCostoDiagnostico.ThousandsSeparator = true;

            // btnRegistrar
            btnRegistrar.Location = new Point(20, 295);
            btnRegistrar.Size = new Size(150, 32);
            btnRegistrar.Text = "Registrar orden";
            btnRegistrar.Click += btnRegistrar_Click;

            // btnVerOrdenes
            btnVerOrdenes.Location = new Point(190, 295);
            btnVerOrdenes.Size = new Size(170, 32);
            btnVerOrdenes.Text = "Ver ordenes registradas";
            btnVerOrdenes.Click += btnVerOrdenes_Click;

            // lblMensaje
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 340);
            lblMensaje.MaximumSize = new Size(340, 0);
            lblMensaje.Text = "";

            // FormNuevaOrden
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 400);
            Controls.Add(lblTitulo);
            Controls.Add(lblCliente);
            Controls.Add(txtCliente);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(lblCosto);
            Controls.Add(numCostoDiagnostico);
            Controls.Add(btnRegistrar);
            Controls.Add(btnVerOrdenes);
            Controls.Add(lblMensaje);
            Text = "GestorTaller - Nueva orden";

            ((System.ComponentModel.ISupportInitialize)numCostoDiagnostico).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

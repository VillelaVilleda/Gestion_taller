namespace GestorTaller
{
    partial class CrearOrdenControl
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
        private System.Windows.Forms.Label lblMensaje;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblCliente = new Label();
            txtCliente = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblCosto = new Label();
            numCostoDiagnostico = new NumericUpDown();
            btnRegistrar = new Button();
            lblMensaje = new Label();
            ((System.ComponentModel.ISupportInitialize)numCostoDiagnostico).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(286, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Nueva orden de trabajo";
            //
            // lblCliente
            //
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(20, 65);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(55, 20);
            lblCliente.TabIndex = 1;
            lblCliente.Text = "Cliente";
            //
            // txtCliente
            //
            txtCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCliente.Location = new Point(20, 85);
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(340, 27);
            txtCliente.TabIndex = 2;
            //
            // lblDescripcion
            //
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(20, 120);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(181, 20);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Descripcion del problema";
            //
            // txtDescripcion
            //
            txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescripcion.Location = new Point(20, 140);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(340, 80);
            txtDescripcion.TabIndex = 4;
            //
            // lblCosto
            //
            lblCosto.AutoSize = true;
            lblCosto.Location = new Point(20, 235);
            lblCosto.Name = "lblCosto";
            lblCosto.Size = new Size(150, 20);
            lblCosto.TabIndex = 5;
            lblCosto.Text = "Costo de diagnostico";
            //
            // numCostoDiagnostico
            //
            numCostoDiagnostico.DecimalPlaces = 2;
            numCostoDiagnostico.Location = new Point(20, 255);
            numCostoDiagnostico.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCostoDiagnostico.Name = "numCostoDiagnostico";
            numCostoDiagnostico.Size = new Size(150, 27);
            numCostoDiagnostico.TabIndex = 6;
            numCostoDiagnostico.ThousandsSeparator = true;
            //
            // btnRegistrar
            //
            btnRegistrar.Location = new Point(20, 295);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(150, 32);
            btnRegistrar.TabIndex = 7;
            btnRegistrar.Text = "Registrar orden";
            btnRegistrar.Click += btnRegistrar_Click;
            //
            // lblMensaje
            //
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 340);
            lblMensaje.MaximumSize = new Size(340, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 8;
            //
            // CrearOrdenControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(lblCliente);
            Controls.Add(txtCliente);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(lblCosto);
            Controls.Add(numCostoDiagnostico);
            Controls.Add(btnRegistrar);
            Controls.Add(lblMensaje);
            Name = "CrearOrdenControl";
            Size = new Size(400, 400);
            ((System.ComponentModel.ISupportInitialize)numCostoDiagnostico).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

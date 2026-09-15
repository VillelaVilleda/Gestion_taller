namespace GestorTaller
{
    partial class CotizacionControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.NumericUpDown numMontoCotizado;
        private System.Windows.Forms.Label lblDecision;
        private System.Windows.Forms.RadioButton rbAceptada;
        private System.Windows.Forms.RadioButton rbRechazada;
        private System.Windows.Forms.Button btnGuardarCotizacion;
        private System.Windows.Forms.Label lblMensaje;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblEmpleado = new Label();
            txtEmpleado = new TextBox();
            lblMonto = new Label();
            numMontoCotizado = new NumericUpDown();
            lblDecision = new Label();
            rbAceptada = new RadioButton();
            rbRechazada = new RadioButton();
            btnGuardarCotizacion = new Button();
            lblMensaje = new Label();
            ((System.ComponentModel.ISupportInitialize)numMontoCotizado).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(150, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cotizacion";
            //
            // lblEmpleado
            //
            lblEmpleado.AutoSize = true;
            lblEmpleado.Location = new Point(20, 55);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(160, 20);
            lblEmpleado.TabIndex = 1;
            lblEmpleado.Text = "Empleado encargado";
            //
            // txtEmpleado
            //
            txtEmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtEmpleado.Location = new Point(20, 75);
            txtEmpleado.Name = "txtEmpleado";
            txtEmpleado.Size = new Size(340, 27);
            txtEmpleado.TabIndex = 2;
            //
            // lblMonto
            //
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(20, 110);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(110, 20);
            lblMonto.TabIndex = 3;
            lblMonto.Text = "Monto cotizado";
            //
            // numMontoCotizado
            //
            numMontoCotizado.DecimalPlaces = 2;
            numMontoCotizado.Location = new Point(20, 130);
            numMontoCotizado.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numMontoCotizado.Name = "numMontoCotizado";
            numMontoCotizado.Size = new Size(150, 27);
            numMontoCotizado.TabIndex = 4;
            numMontoCotizado.ThousandsSeparator = true;
            //
            // lblDecision
            //
            lblDecision.AutoSize = true;
            lblDecision.Location = new Point(20, 170);
            lblDecision.Name = "lblDecision";
            lblDecision.Size = new Size(160, 20);
            lblDecision.TabIndex = 5;
            lblDecision.Text = "Decision del cliente";
            //
            // rbAceptada
            //
            rbAceptada.AutoSize = true;
            rbAceptada.Checked = true;
            rbAceptada.Location = new Point(20, 195);
            rbAceptada.Name = "rbAceptada";
            rbAceptada.Size = new Size(120, 24);
            rbAceptada.TabIndex = 6;
            rbAceptada.TabStop = true;
            rbAceptada.Text = "Aceptada";
            //
            // rbRechazada
            //
            rbRechazada.AutoSize = true;
            rbRechazada.Location = new Point(160, 195);
            rbRechazada.Name = "rbRechazada";
            rbRechazada.Size = new Size(130, 24);
            rbRechazada.TabIndex = 7;
            rbRechazada.Text = "Rechazada";
            //
            // btnGuardarCotizacion
            //
            btnGuardarCotizacion.Location = new Point(20, 235);
            btnGuardarCotizacion.Name = "btnGuardarCotizacion";
            btnGuardarCotizacion.Size = new Size(180, 32);
            btnGuardarCotizacion.TabIndex = 8;
            btnGuardarCotizacion.Text = "Guardar cotizacion";
            btnGuardarCotizacion.Click += btnGuardarCotizacion_Click;
            //
            // lblMensaje
            //
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 280);
            lblMensaje.MaximumSize = new Size(340, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 9;
            //
            // CotizacionControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(lblEmpleado);
            Controls.Add(txtEmpleado);
            Controls.Add(lblMonto);
            Controls.Add(numMontoCotizado);
            Controls.Add(lblDecision);
            Controls.Add(rbAceptada);
            Controls.Add(rbRechazada);
            Controls.Add(btnGuardarCotizacion);
            Controls.Add(lblMensaje);
            Name = "CotizacionControl";
            Size = new Size(400, 320);
            ((System.ComponentModel.ISupportInitialize)numMontoCotizado).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
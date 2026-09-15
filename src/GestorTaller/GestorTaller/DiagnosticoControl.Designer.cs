namespace GestorTaller
{
    partial class DiagnosticoControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.TextBox txtEmpleado;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Button btnGuardarDiagnostico;
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
            lblDetalle = new Label();
            txtDetalle = new TextBox();
            btnGuardarDiagnostico = new Button();
            lblMensaje = new Label();
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
            lblTitulo.Text = "Diagnostico";
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
            // lblDetalle
            //
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(20, 110);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(150, 20);
            lblDetalle.TabIndex = 3;
            lblDetalle.Text = "Detalle del diagnostico";
            //
            // txtDetalle
            //
            txtDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDetalle.Location = new Point(20, 130);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(340, 90);
            txtDetalle.TabIndex = 4;
            //
            // btnGuardarDiagnostico
            //
            btnGuardarDiagnostico.Location = new Point(20, 235);
            btnGuardarDiagnostico.Name = "btnGuardarDiagnostico";
            btnGuardarDiagnostico.Size = new Size(180, 32);
            btnGuardarDiagnostico.TabIndex = 5;
            btnGuardarDiagnostico.Text = "Guardar diagnostico";
            btnGuardarDiagnostico.Click += btnGuardarDiagnostico_Click;
            //
            // lblMensaje
            //
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 280);
            lblMensaje.MaximumSize = new Size(340, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 6;
            //
            // DiagnosticoControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(lblEmpleado);
            Controls.Add(txtEmpleado);
            Controls.Add(lblDetalle);
            Controls.Add(txtDetalle);
            Controls.Add(btnGuardarDiagnostico);
            Controls.Add(lblMensaje);
            Name = "DiagnosticoControl";
            Size = new Size(400, 320);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
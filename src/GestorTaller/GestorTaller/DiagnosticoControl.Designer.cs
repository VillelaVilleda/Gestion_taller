namespace GestorTaller
{
    partial class DiagnosticoControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblEmpleado;
        private GestorTaller.ComboBoxFiltrable cmbEmpleado;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.TextBox txtDetalle;
        private BotonTema btnGuardarDiagnostico;
        private AvisoControl avisoMensaje;

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
            layout = new TableLayoutPanel();
            lblTitulo = new Label();
            lblEmpleado = new Label();
            cmbEmpleado = new ComboBoxFiltrable();
            lblDetalle = new Label();
            txtDetalle = new TextBox();
            btnGuardarDiagnostico = new BotonTema();
            avisoMensaje = new AvisoControl();
            layout.SuspendLayout();
            SuspendLayout();
            //
            // layout
            //
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(lblTitulo, 0, 0);
            layout.Controls.Add(lblEmpleado, 0, 1);
            layout.Controls.Add(cmbEmpleado, 0, 2);
            layout.Controls.Add(lblDetalle, 0, 3);
            layout.Controls.Add(txtDetalle, 0, 4);
            layout.Controls.Add(btnGuardarDiagnostico, 0, 5);
            layout.Controls.Add(avisoMensaje, 0, 6);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.RowCount = 7;
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.Size = new Size(460, 400);
            layout.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Margin = new Padding(0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(150, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Diagnóstico";
            //
            // lblEmpleado
            //
            lblEmpleado.AutoSize = true;
            lblEmpleado.Margin = new Padding(0, 12, 0, 4);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(160, 20);
            lblEmpleado.TabIndex = 1;
            lblEmpleado.Text = "Empleado encargado";
            //
            // cmbEmpleado
            //
            cmbEmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbEmpleado.Margin = new Padding(0);
            cmbEmpleado.Name = "cmbEmpleado";
            cmbEmpleado.Size = new Size(460, 27);
            cmbEmpleado.TabIndex = 2;
            //
            // lblDetalle
            //
            lblDetalle.AutoSize = true;
            lblDetalle.Margin = new Padding(0, 12, 0, 4);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(150, 20);
            lblDetalle.TabIndex = 3;
            lblDetalle.Text = "Detalle del diagnóstico";
            //
            // txtDetalle
            //
            txtDetalle.Dock = DockStyle.Fill;
            txtDetalle.Margin = new Padding(0);
            txtDetalle.MinimumSize = new Size(0, 90);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(460, 90);
            txtDetalle.TabIndex = 4;
            //
            // btnGuardarDiagnostico
            //
            btnGuardarDiagnostico.Anchor = AnchorStyles.Left;
            btnGuardarDiagnostico.Margin = new Padding(0, 16, 0, 0);
            btnGuardarDiagnostico.Name = "btnGuardarDiagnostico";
            btnGuardarDiagnostico.NombreIcono = "save";
            btnGuardarDiagnostico.Size = new Size(200, 36);
            btnGuardarDiagnostico.TabIndex = 5;
            btnGuardarDiagnostico.Text = "Guardar diagnóstico";
            btnGuardarDiagnostico.Variante = VarianteBoton.Primario;
            btnGuardarDiagnostico.Click += btnGuardarDiagnostico_Click;
            //
            // avisoMensaje
            //
            avisoMensaje.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            avisoMensaje.Margin = new Padding(0, 12, 0, 0);
            avisoMensaje.Name = "avisoMensaje";
            avisoMensaje.Size = new Size(460, 40);
            avisoMensaje.TabIndex = 6;
            //
            // DiagnosticoControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "DiagnosticoControl";
            Size = new Size(460, 400);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ResumeLayout(false);
        }
    }
}

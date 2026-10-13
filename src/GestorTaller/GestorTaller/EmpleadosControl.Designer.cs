namespace GestorTaller
{
    partial class EmpleadosControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private GestorTaller.TablaListadoControl tablaEmpleados;

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
            tablaEmpleados = new TablaListadoControl();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(160, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Empleados";
            //
            // tablaEmpleados
            //
            tablaEmpleados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tablaEmpleados.Location = new Point(0, 55);
            tablaEmpleados.Name = "tablaEmpleados";
            tablaEmpleados.Size = new Size(680, 405);
            tablaEmpleados.TabIndex = 1;
            //
            // EmpleadosControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(tablaEmpleados);
            Name = "EmpleadosControl";
            Size = new Size(680, 460);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

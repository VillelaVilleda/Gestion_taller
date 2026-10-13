namespace GestorTaller
{
    partial class RepuestosControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private GestorTaller.TablaListadoControl tablaRepuestos;

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
            tablaRepuestos = new TablaListadoControl();
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
            lblTitulo.Text = "Repuestos";
            //
            // tablaRepuestos
            //
            tablaRepuestos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tablaRepuestos.Location = new Point(0, 55);
            tablaRepuestos.Name = "tablaRepuestos";
            tablaRepuestos.Size = new Size(680, 405);
            tablaRepuestos.TabIndex = 1;
            //
            // RepuestosControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(tablaRepuestos);
            Name = "RepuestosControl";
            Size = new Size(680, 460);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

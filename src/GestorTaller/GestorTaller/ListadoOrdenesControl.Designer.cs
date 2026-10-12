namespace GestorTaller
{
    partial class ListadoOrdenesControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private GestorTaller.TablaListadoControl tablaOrdenes;

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
            tablaOrdenes = new TablaListadoControl();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(243, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Ordenes registradas";
            //
            // tablaOrdenes
            //
            tablaOrdenes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tablaOrdenes.Location = new Point(0, 55);
            tablaOrdenes.Name = "tablaOrdenes";
            tablaOrdenes.Size = new Size(680, 405);
            tablaOrdenes.TabIndex = 1;
            //
            // ListadoOrdenesControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(tablaOrdenes);
            Name = "ListadoOrdenesControl";
            Size = new Size(680, 460);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

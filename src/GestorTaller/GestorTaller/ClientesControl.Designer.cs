namespace GestorTaller
{
    partial class ClientesControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private GestorTaller.TablaListadoControl tablaClientes;

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
            tablaClientes = new TablaListadoControl();
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
            lblTitulo.Text = "Clientes";
            //
            // tablaClientes
            //
            tablaClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tablaClientes.Location = new Point(0, 55);
            tablaClientes.Name = "tablaClientes";
            tablaClientes.Size = new Size(680, 405);
            tablaClientes.TabIndex = 1;
            //
            // ClientesControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(tablaClientes);
            Name = "ClientesControl";
            Size = new Size(680, 460);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

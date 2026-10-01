namespace GestorTaller
{
    partial class RepuestosControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvRepuestos;

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
            dgvRepuestos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvRepuestos).BeginInit();
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
            // dgvRepuestos
            //
            dgvRepuestos.AllowUserToAddRows = false;
            dgvRepuestos.AllowUserToDeleteRows = false;
            dgvRepuestos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvRepuestos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRepuestos.ColumnHeadersHeight = 29;
            dgvRepuestos.Location = new Point(20, 55);
            dgvRepuestos.Name = "dgvRepuestos";
            dgvRepuestos.ReadOnly = true;
            dgvRepuestos.RowHeadersVisible = false;
            dgvRepuestos.RowHeadersWidth = 51;
            dgvRepuestos.Size = new Size(640, 380);
            dgvRepuestos.TabIndex = 1;
            //
            // RepuestosControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(dgvRepuestos);
            Name = "RepuestosControl";
            Size = new Size(680, 460);
            ((System.ComponentModel.ISupportInitialize)dgvRepuestos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

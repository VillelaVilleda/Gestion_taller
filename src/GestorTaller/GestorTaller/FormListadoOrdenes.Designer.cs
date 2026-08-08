namespace GestorTaller
{
    partial class FormListadoOrdenes
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvOrdenes;

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
            dgvOrdenes = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvOrdenes).BeginInit();
            SuspendLayout();

            // lblTitulo
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Text = "Ordenes registradas";

            // dgvOrdenes
            dgvOrdenes.AllowUserToAddRows = false;
            dgvOrdenes.AllowUserToDeleteRows = false;
            dgvOrdenes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrdenes.Location = new Point(20, 55);
            dgvOrdenes.ReadOnly = true;
            dgvOrdenes.RowHeadersVisible = false;
            dgvOrdenes.Size = new Size(640, 380);

            // FormListadoOrdenes
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 460);
            Controls.Add(lblTitulo);
            Controls.Add(dgvOrdenes);
            Text = "GestorTaller - Ordenes registradas";

            ((System.ComponentModel.ISupportInitialize)dgvOrdenes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

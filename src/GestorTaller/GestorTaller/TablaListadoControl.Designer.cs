namespace GestorTaller
{
    partial class TablaListadoControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ComboBox cmbColumna;
        private BotonTema btnBuscar;
        private System.Windows.Forms.DataGridView dgvTabla;
        private BotonTema btnAtras;
        private System.Windows.Forms.Label lblPagina;
        private BotonTema btnSiguiente;

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
            txtBuscar = new TextBox();
            cmbColumna = new ComboBox();
            btnBuscar = new BotonTema();
            dgvTabla = new DataGridView();
            btnAtras = new BotonTema();
            lblPagina = new Label();
            btnSiguiente = new BotonTema();
            ((System.ComponentModel.ISupportInitialize)dgvTabla).BeginInit();
            SuspendLayout();
            //
            // txtBuscar
            //
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Location = new Point(20, 15);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar...";
            txtBuscar.Size = new Size(330, 27);
            txtBuscar.TabIndex = 0;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            //
            // cmbColumna
            //
            cmbColumna.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbColumna.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColumna.Location = new Point(360, 15);
            cmbColumna.Name = "cmbColumna";
            cmbColumna.Size = new Size(180, 28);
            cmbColumna.TabIndex = 1;
            //
            // btnBuscar
            //
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Location = new Point(550, 13);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(110, 30);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.Variante = VarianteBoton.Primario;
            btnBuscar.Click += btnBuscar_Click;
            //
            // dgvTabla
            //
            dgvTabla.AllowUserToAddRows = false;
            dgvTabla.AllowUserToDeleteRows = false;
            dgvTabla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTabla.BackgroundColor = Color.White;
            dgvTabla.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(63, 81, 181),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(63, 81, 181),
                SelectionForeColor = Color.White
            };
            dgvTabla.ColumnHeadersHeight = 32;
            dgvTabla.EnableHeadersVisualStyles = false;
            dgvTabla.Location = new Point(20, 55);
            dgvTabla.MultiSelect = false;
            dgvTabla.Name = "dgvTabla";
            dgvTabla.ReadOnly = true;
            dgvTabla.RowHeadersVisible = false;
            dgvTabla.RowHeadersWidth = 51;
            dgvTabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTabla.Size = new Size(640, 350);
            dgvTabla.TabIndex = 3;
            dgvTabla.CellContentClick += dgvTabla_CellContentClick;
            dgvTabla.ColumnHeaderMouseClick += dgvTabla_ColumnHeaderMouseClick;
            //
            // btnAtras
            //
            btnAtras.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAtras.Location = new Point(440, 415);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(80, 30);
            btnAtras.TabIndex = 4;
            btnAtras.Text = "Atras";
            btnAtras.Click += btnAtras_Click;
            //
            // lblPagina
            //
            lblPagina.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblPagina.Location = new Point(525, 420);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(50, 20);
            lblPagina.TabIndex = 5;
            lblPagina.Text = "0/0";
            lblPagina.TextAlign = ContentAlignment.MiddleCenter;
            //
            // btnSiguiente
            //
            btnSiguiente.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSiguiente.Location = new Point(580, 415);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(80, 30);
            btnSiguiente.TabIndex = 6;
            btnSiguiente.Text = "Sig";
            btnSiguiente.Click += btnSiguiente_Click;
            //
            // TablaListadoControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtBuscar);
            Controls.Add(cmbColumna);
            Controls.Add(btnBuscar);
            Controls.Add(dgvTabla);
            Controls.Add(btnAtras);
            Controls.Add(lblPagina);
            Controls.Add(btnSiguiente);
            Name = "TablaListadoControl";
            Size = new Size(680, 460);
            ((System.ComponentModel.ISupportInitialize)dgvTabla).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

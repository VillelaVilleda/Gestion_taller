namespace GestorTaller
{
    partial class RepuestosControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblExistencia;
        private System.Windows.Forms.NumericUpDown numExistencia;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown numPrecio;
        private System.Windows.Forms.Button btnAgregarRepuesto;
        private System.Windows.Forms.Label lblMensaje;
        private GestorTaller.TablaListadoControl tablaRepuestos;
        private System.Windows.Forms.Panel panelEdicion;
        private System.Windows.Forms.Label lblEditar;
        private GestorTaller.ComboBoxFiltrable cmbRepuesto;
        private System.Windows.Forms.TextBox txtEditarNombre;
        private System.Windows.Forms.NumericUpDown numEditarExistencia;
        private System.Windows.Forms.NumericUpDown numEditarPrecio;
        private System.Windows.Forms.Button btnGuardarCambios;
        private System.Windows.Forms.Button btnEliminar;

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
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblExistencia = new Label();
            numExistencia = new NumericUpDown();
            lblPrecio = new Label();
            numPrecio = new NumericUpDown();
            btnAgregarRepuesto = new Button();
            lblMensaje = new Label();
            tablaRepuestos = new TablaListadoControl();
            panelEdicion = new Panel();
            lblEditar = new Label();
            cmbRepuesto = new ComboBoxFiltrable();
            txtEditarNombre = new TextBox();
            numEditarExistencia = new NumericUpDown();
            numEditarPrecio = new NumericUpDown();
            btnGuardarCambios = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)numExistencia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrecio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEditarExistencia).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEditarPrecio).BeginInit();
            panelEdicion.SuspendLayout();
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
            // lblNombre
            //
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(20, 55);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(60, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre";
            //
            // txtNombre
            //
            txtNombre.Location = new Point(20, 77);
            txtNombre.MaxLength = 150;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(250, 27);
            txtNombre.TabIndex = 2;
            //
            // lblExistencia
            //
            lblExistencia.AutoSize = true;
            lblExistencia.Location = new Point(285, 55);
            lblExistencia.Name = "lblExistencia";
            lblExistencia.Size = new Size(76, 20);
            lblExistencia.TabIndex = 3;
            lblExistencia.Text = "Existencia";
            //
            // numExistencia
            //
            numExistencia.Location = new Point(285, 77);
            numExistencia.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numExistencia.Name = "numExistencia";
            numExistencia.Size = new Size(90, 27);
            numExistencia.TabIndex = 4;
            //
            // lblPrecio
            //
            lblPrecio.AutoSize = true;
            lblPrecio.Location = new Point(385, 55);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(70, 20);
            lblPrecio.TabIndex = 5;
            lblPrecio.Text = "Precio (Q)";
            //
            // numPrecio
            //
            numPrecio.DecimalPlaces = 2;
            numPrecio.Location = new Point(385, 77);
            numPrecio.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPrecio.Name = "numPrecio";
            numPrecio.Size = new Size(110, 27);
            numPrecio.TabIndex = 6;
            numPrecio.ThousandsSeparator = true;
            //
            // btnAgregarRepuesto
            //
            btnAgregarRepuesto.Location = new Point(510, 75);
            btnAgregarRepuesto.Name = "btnAgregarRepuesto";
            btnAgregarRepuesto.Size = new Size(150, 30);
            btnAgregarRepuesto.TabIndex = 7;
            btnAgregarRepuesto.Text = "Agregar repuesto";
            btnAgregarRepuesto.Click += btnAgregarRepuesto_Click;
            //
            // lblMensaje
            //
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 210);
            lblMensaje.MaximumSize = new Size(640, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 8;
            //
            // tablaRepuestos
            //
            tablaRepuestos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tablaRepuestos.Location = new Point(0, 240);
            tablaRepuestos.Name = "tablaRepuestos";
            tablaRepuestos.Size = new Size(680, 220);
            tablaRepuestos.TabIndex = 9;
            //
            // panelEdicion
            //
            panelEdicion.Controls.Add(lblEditar);
            panelEdicion.Controls.Add(cmbRepuesto);
            panelEdicion.Controls.Add(txtEditarNombre);
            panelEdicion.Controls.Add(numEditarExistencia);
            panelEdicion.Controls.Add(numEditarPrecio);
            panelEdicion.Controls.Add(btnGuardarCambios);
            panelEdicion.Controls.Add(btnEliminar);
            panelEdicion.Location = new Point(0, 110);
            panelEdicion.Name = "panelEdicion";
            panelEdicion.Size = new Size(680, 95);
            panelEdicion.TabIndex = 10;
            //
            // lblEditar
            //
            lblEditar.AutoSize = true;
            lblEditar.Location = new Point(20, 2);
            lblEditar.Name = "lblEditar";
            lblEditar.Size = new Size(200, 20);
            lblEditar.TabIndex = 0;
            lblEditar.Text = "Editar o eliminar un repuesto";
            //
            // cmbRepuesto
            //
            cmbRepuesto.Location = new Point(20, 25);
            cmbRepuesto.Name = "cmbRepuesto";
            cmbRepuesto.Size = new Size(190, 28);
            cmbRepuesto.TabIndex = 1;
            //
            // txtEditarNombre
            //
            txtEditarNombre.Location = new Point(220, 26);
            txtEditarNombre.MaxLength = 150;
            txtEditarNombre.Name = "txtEditarNombre";
            txtEditarNombre.PlaceholderText = "Nombre";
            txtEditarNombre.Size = new Size(150, 27);
            txtEditarNombre.TabIndex = 2;
            //
            // numEditarExistencia
            //
            numEditarExistencia.Location = new Point(380, 26);
            numEditarExistencia.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numEditarExistencia.Name = "numEditarExistencia";
            numEditarExistencia.Size = new Size(80, 27);
            numEditarExistencia.TabIndex = 3;
            //
            // numEditarPrecio
            //
            numEditarPrecio.DecimalPlaces = 2;
            numEditarPrecio.Location = new Point(470, 26);
            numEditarPrecio.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numEditarPrecio.Name = "numEditarPrecio";
            numEditarPrecio.Size = new Size(110, 27);
            numEditarPrecio.TabIndex = 4;
            numEditarPrecio.ThousandsSeparator = true;
            //
            // btnGuardarCambios
            //
            btnGuardarCambios.Location = new Point(20, 60);
            btnGuardarCambios.Name = "btnGuardarCambios";
            btnGuardarCambios.Size = new Size(160, 30);
            btnGuardarCambios.TabIndex = 5;
            btnGuardarCambios.Text = "Guardar cambios";
            btnGuardarCambios.Click += btnGuardarCambios_Click;
            //
            // btnEliminar
            //
            btnEliminar.Location = new Point(190, 60);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(120, 30);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            //
            // RepuestosControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblExistencia);
            Controls.Add(numExistencia);
            Controls.Add(lblPrecio);
            Controls.Add(numPrecio);
            Controls.Add(btnAgregarRepuesto);
            Controls.Add(lblMensaje);
            Controls.Add(tablaRepuestos);
            Controls.Add(panelEdicion);
            Name = "RepuestosControl";
            Size = new Size(680, 460);
            ((System.ComponentModel.ISupportInitialize)numExistencia).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrecio).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEditarExistencia).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEditarPrecio).EndInit();
            panelEdicion.ResumeLayout(false);
            panelEdicion.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

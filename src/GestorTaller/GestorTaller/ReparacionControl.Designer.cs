namespace GestorTaller
{
    partial class ReparacionControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblEmpleado;
        private GestorTaller.ComboBoxFiltrable cmbEmpleado;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Label lblRepuestos;
        private GestorTaller.ComboBoxFiltrable cmbRepuesto;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Button btnAgregarRepuesto;
        private System.Windows.Forms.Label lblDisponible;
        private System.Windows.Forms.DataGridView dgvRepuestosAsignados;
        private System.Windows.Forms.Button btnGuardarReparacion;
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
            lblSubtitulo = new Label();
            lblEmpleado = new Label();
            cmbEmpleado = new ComboBoxFiltrable();
            lblDetalle = new Label();
            txtDetalle = new TextBox();
            lblRepuestos = new Label();
            cmbRepuesto = new ComboBoxFiltrable();
            numCantidad = new NumericUpDown();
            btnAgregarRepuesto = new Button();
            lblDisponible = new Label();
            dgvRepuestosAsignados = new DataGridView();
            btnGuardarReparacion = new Button();
            lblMensaje = new Label();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRepuestosAsignados).BeginInit();
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
            lblTitulo.Text = "Reparacion";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = Color.Gray;
            lblSubtitulo.Location = new Point(20, 45);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(320, 20);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Al guardar, la orden pasa a estado Terminado";
            //
            // lblEmpleado
            //
            lblEmpleado.AutoSize = true;
            lblEmpleado.Location = new Point(20, 80);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(160, 20);
            lblEmpleado.TabIndex = 2;
            lblEmpleado.Text = "Empleado encargado";
            //
            // cmbEmpleado
            //
            cmbEmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbEmpleado.Location = new Point(20, 100);
            cmbEmpleado.Name = "cmbEmpleado";
            cmbEmpleado.Size = new Size(340, 27);
            cmbEmpleado.TabIndex = 3;
            //
            // lblDetalle
            //
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(20, 135);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(150, 20);
            lblDetalle.TabIndex = 4;
            lblDetalle.Text = "Detalle de la reparacion";
            //
            // txtDetalle
            //
            txtDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDetalle.Location = new Point(20, 155);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(340, 90);
            txtDetalle.TabIndex = 5;
            //
            // lblRepuestos
            //
            lblRepuestos.AutoSize = true;
            lblRepuestos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRepuestos.Location = new Point(20, 260);
            lblRepuestos.Name = "lblRepuestos";
            lblRepuestos.Size = new Size(150, 23);
            lblRepuestos.TabIndex = 6;
            lblRepuestos.Text = "Repuestos utilizados";
            //
            // cmbRepuesto
            //
            cmbRepuesto.Location = new Point(20, 290);
            cmbRepuesto.Name = "cmbRepuesto";
            cmbRepuesto.Size = new Size(180, 28);
            cmbRepuesto.TabIndex = 7;
            //
            // numCantidad
            //
            numCantidad.Location = new Point(210, 290);
            numCantidad.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(70, 27);
            numCantidad.TabIndex = 8;
            numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnAgregarRepuesto
            //
            btnAgregarRepuesto.Location = new Point(290, 288);
            btnAgregarRepuesto.Name = "btnAgregarRepuesto";
            btnAgregarRepuesto.Size = new Size(70, 30);
            btnAgregarRepuesto.TabIndex = 9;
            btnAgregarRepuesto.Text = "Agregar";
            btnAgregarRepuesto.Click += btnAgregarRepuesto_Click;
            //
            // lblDisponible
            //
            lblDisponible.AutoSize = true;
            lblDisponible.ForeColor = Color.Gray;
            lblDisponible.Location = new Point(20, 322);
            lblDisponible.Name = "lblDisponible";
            lblDisponible.Size = new Size(100, 20);
            lblDisponible.TabIndex = 10;
            lblDisponible.Text = "Disponible: -";
            //
            // dgvRepuestosAsignados
            //
            dgvRepuestosAsignados.AllowUserToAddRows = false;
            dgvRepuestosAsignados.AllowUserToDeleteRows = false;
            dgvRepuestosAsignados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRepuestosAsignados.BackgroundColor = Color.White;
            dgvRepuestosAsignados.Location = new Point(20, 350);
            dgvRepuestosAsignados.Name = "dgvRepuestosAsignados";
            dgvRepuestosAsignados.ReadOnly = true;
            dgvRepuestosAsignados.RowHeadersVisible = false;
            dgvRepuestosAsignados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRepuestosAsignados.Size = new Size(340, 110);
            dgvRepuestosAsignados.TabIndex = 11;
            //
            // btnGuardarReparacion
            //
            btnGuardarReparacion.Location = new Point(20, 475);
            btnGuardarReparacion.Name = "btnGuardarReparacion";
            btnGuardarReparacion.Size = new Size(180, 32);
            btnGuardarReparacion.TabIndex = 12;
            btnGuardarReparacion.Text = "Guardar reparacion";
            btnGuardarReparacion.Click += btnGuardarReparacion_Click;
            //
            // lblMensaje
            //
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 520);
            lblMensaje.MaximumSize = new Size(340, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 13;
            //
            // ReparacionControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblEmpleado);
            Controls.Add(cmbEmpleado);
            Controls.Add(lblDetalle);
            Controls.Add(txtDetalle);
            Controls.Add(lblRepuestos);
            Controls.Add(cmbRepuesto);
            Controls.Add(numCantidad);
            Controls.Add(btnAgregarRepuesto);
            Controls.Add(lblDisponible);
            Controls.Add(dgvRepuestosAsignados);
            Controls.Add(btnGuardarReparacion);
            Controls.Add(lblMensaje);
            Name = "ReparacionControl";
            Size = new Size(400, 560);
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRepuestosAsignados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

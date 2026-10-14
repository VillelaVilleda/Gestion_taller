namespace GestorTaller
{
    partial class CotizacionControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblEmpleado;
        private GestorTaller.ComboBoxFiltrable cmbEmpleado;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.NumericUpDown numMontoCotizado;
        private System.Windows.Forms.Label lblRepuestos;
        private GestorTaller.ComboBoxFiltrable cmbRepuesto;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Button btnAgregarRepuesto;
        private System.Windows.Forms.Label lblDisponible;
        private System.Windows.Forms.DataGridView dgvRepuestos;
        private System.Windows.Forms.Button btnQuitarRepuesto;
        private System.Windows.Forms.Label lblSubtotalRepuestos;
        private System.Windows.Forms.Label lblTotalCotizacion;
        private System.Windows.Forms.Label lblDecision;
        private System.Windows.Forms.RadioButton rbAceptada;
        private System.Windows.Forms.RadioButton rbRechazada;
        private System.Windows.Forms.Button btnGuardarCotizacion;
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
            lblEmpleado = new Label();
            cmbEmpleado = new ComboBoxFiltrable();
            lblMonto = new Label();
            numMontoCotizado = new NumericUpDown();
            lblRepuestos = new Label();
            cmbRepuesto = new ComboBoxFiltrable();
            numCantidad = new NumericUpDown();
            btnAgregarRepuesto = new Button();
            lblDisponible = new Label();
            dgvRepuestos = new DataGridView();
            btnQuitarRepuesto = new Button();
            lblSubtotalRepuestos = new Label();
            lblTotalCotizacion = new Label();
            lblDecision = new Label();
            rbAceptada = new RadioButton();
            rbRechazada = new RadioButton();
            btnGuardarCotizacion = new Button();
            lblMensaje = new Label();
            ((System.ComponentModel.ISupportInitialize)numMontoCotizado).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRepuestos).BeginInit();
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
            lblTitulo.Text = "Cotizacion";
            //
            // lblEmpleado
            //
            lblEmpleado.AutoSize = true;
            lblEmpleado.Location = new Point(20, 55);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(160, 20);
            lblEmpleado.TabIndex = 1;
            lblEmpleado.Text = "Empleado encargado";
            //
            // cmbEmpleado
            //
            cmbEmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbEmpleado.Location = new Point(20, 75);
            cmbEmpleado.Name = "cmbEmpleado";
            cmbEmpleado.Size = new Size(340, 27);
            cmbEmpleado.TabIndex = 2;
            //
            // lblMonto
            //
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(20, 110);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(110, 20);
            lblMonto.TabIndex = 3;
            lblMonto.Text = "Monto cotizado";
            //
            // numMontoCotizado
            //
            numMontoCotizado.DecimalPlaces = 2;
            numMontoCotizado.Location = new Point(20, 130);
            numMontoCotizado.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numMontoCotizado.Name = "numMontoCotizado";
            numMontoCotizado.Size = new Size(150, 27);
            numMontoCotizado.TabIndex = 4;
            numMontoCotizado.ThousandsSeparator = true;
            //
            // lblRepuestos
            //
            lblRepuestos.AutoSize = true;
            lblRepuestos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRepuestos.Location = new Point(20, 172);
            lblRepuestos.Name = "lblRepuestos";
            lblRepuestos.Size = new Size(150, 23);
            lblRepuestos.TabIndex = 5;
            lblRepuestos.Text = "Repuestos cotizados";
            //
            // cmbRepuesto
            //
            cmbRepuesto.Location = new Point(20, 202);
            cmbRepuesto.Name = "cmbRepuesto";
            cmbRepuesto.Size = new Size(180, 28);
            cmbRepuesto.TabIndex = 6;
            //
            // numCantidad
            //
            numCantidad.Location = new Point(210, 202);
            numCantidad.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(70, 27);
            numCantidad.TabIndex = 7;
            numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnAgregarRepuesto
            //
            btnAgregarRepuesto.Location = new Point(290, 200);
            btnAgregarRepuesto.Name = "btnAgregarRepuesto";
            btnAgregarRepuesto.Size = new Size(70, 30);
            btnAgregarRepuesto.TabIndex = 8;
            btnAgregarRepuesto.Text = "Agregar";
            btnAgregarRepuesto.Click += btnAgregarRepuesto_Click;
            //
            // lblDisponible
            //
            lblDisponible.AutoSize = true;
            lblDisponible.ForeColor = Color.Gray;
            lblDisponible.Location = new Point(20, 234);
            lblDisponible.Name = "lblDisponible";
            lblDisponible.Size = new Size(100, 20);
            lblDisponible.TabIndex = 9;
            lblDisponible.Text = "Disponible: -";
            //
            // dgvRepuestos
            //
            dgvRepuestos.AllowUserToAddRows = false;
            dgvRepuestos.AllowUserToDeleteRows = false;
            dgvRepuestos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRepuestos.BackgroundColor = Color.White;
            dgvRepuestos.Location = new Point(20, 262);
            dgvRepuestos.MultiSelect = false;
            dgvRepuestos.Name = "dgvRepuestos";
            dgvRepuestos.ReadOnly = true;
            dgvRepuestos.RowHeadersVisible = false;
            dgvRepuestos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRepuestos.Size = new Size(340, 120);
            dgvRepuestos.TabIndex = 10;
            //
            // btnQuitarRepuesto
            //
            btnQuitarRepuesto.Location = new Point(20, 388);
            btnQuitarRepuesto.Name = "btnQuitarRepuesto";
            btnQuitarRepuesto.Size = new Size(340, 28);
            btnQuitarRepuesto.TabIndex = 11;
            btnQuitarRepuesto.Text = "Quitar repuesto seleccionado";
            btnQuitarRepuesto.Click += btnQuitarRepuesto_Click;
            //
            // lblSubtotalRepuestos
            //
            lblSubtotalRepuestos.AutoSize = true;
            lblSubtotalRepuestos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSubtotalRepuestos.Location = new Point(20, 424);
            lblSubtotalRepuestos.Name = "lblSubtotalRepuestos";
            lblSubtotalRepuestos.Size = new Size(200, 23);
            lblSubtotalRepuestos.TabIndex = 12;
            lblSubtotalRepuestos.Text = "Subtotal de repuestos: Q 0.00";
            //
            // lblTotalCotizacion
            //
            lblTotalCotizacion.AutoSize = true;
            lblTotalCotizacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalCotizacion.Location = new Point(20, 452);
            lblTotalCotizacion.MaximumSize = new Size(340, 0);
            lblTotalCotizacion.Name = "lblTotalCotizacion";
            lblTotalCotizacion.Size = new Size(300, 23);
            lblTotalCotizacion.TabIndex = 13;
            lblTotalCotizacion.Text = "Mano de obra + repuestos: Q 0.00";
            //
            // lblDecision
            //
            lblDecision.AutoSize = true;
            lblDecision.Location = new Point(20, 490);
            lblDecision.Name = "lblDecision";
            lblDecision.Size = new Size(160, 20);
            lblDecision.TabIndex = 14;
            lblDecision.Text = "Decision del cliente";
            //
            // rbAceptada
            //
            rbAceptada.AutoSize = true;
            rbAceptada.Checked = true;
            rbAceptada.Location = new Point(20, 515);
            rbAceptada.Name = "rbAceptada";
            rbAceptada.Size = new Size(120, 24);
            rbAceptada.TabIndex = 15;
            rbAceptada.TabStop = true;
            rbAceptada.Text = "Aceptada";
            //
            // rbRechazada
            //
            rbRechazada.AutoSize = true;
            rbRechazada.Location = new Point(160, 515);
            rbRechazada.Name = "rbRechazada";
            rbRechazada.Size = new Size(130, 24);
            rbRechazada.TabIndex = 16;
            rbRechazada.Text = "Rechazada";
            //
            // btnGuardarCotizacion
            //
            btnGuardarCotizacion.Location = new Point(20, 555);
            btnGuardarCotizacion.Name = "btnGuardarCotizacion";
            btnGuardarCotizacion.Size = new Size(180, 32);
            btnGuardarCotizacion.TabIndex = 17;
            btnGuardarCotizacion.Text = "Guardar cotizacion";
            btnGuardarCotizacion.Click += btnGuardarCotizacion_Click;
            //
            // lblMensaje
            //
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 600);
            lblMensaje.MaximumSize = new Size(340, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 18;
            //
            // CotizacionControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(lblTitulo);
            Controls.Add(lblEmpleado);
            Controls.Add(cmbEmpleado);
            Controls.Add(lblMonto);
            Controls.Add(numMontoCotizado);
            Controls.Add(lblRepuestos);
            Controls.Add(cmbRepuesto);
            Controls.Add(numCantidad);
            Controls.Add(btnAgregarRepuesto);
            Controls.Add(lblDisponible);
            Controls.Add(dgvRepuestos);
            Controls.Add(btnQuitarRepuesto);
            Controls.Add(lblSubtotalRepuestos);
            Controls.Add(lblTotalCotizacion);
            Controls.Add(lblDecision);
            Controls.Add(rbAceptada);
            Controls.Add(rbRechazada);
            Controls.Add(btnGuardarCotizacion);
            Controls.Add(lblMensaje);
            Name = "CotizacionControl";
            Size = new Size(400, 670);
            ((System.ComponentModel.ISupportInitialize)numMontoCotizado).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRepuestos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

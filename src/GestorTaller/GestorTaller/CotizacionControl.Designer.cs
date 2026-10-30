namespace GestorTaller
{
    partial class CotizacionControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblEmpleado;
        private GestorTaller.ComboBoxFiltrable cmbEmpleado;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.NumericUpDown numMontoCotizado;
        private System.Windows.Forms.Label lblRepuestos;
        private System.Windows.Forms.TableLayoutPanel filaRepuesto;
        private GestorTaller.ComboBoxFiltrable cmbRepuesto;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private BotonTema btnAgregarRepuesto;
        private System.Windows.Forms.Label lblDisponible;
        private System.Windows.Forms.DataGridView dgvRepuestos;
        private BotonTema btnQuitarRepuesto;
        private System.Windows.Forms.Label lblDesglose;
        private System.Windows.Forms.Label lblTotalCotizacion;
        private System.Windows.Forms.Label lblDecision;
        private System.Windows.Forms.FlowLayoutPanel filaDecision;
        private System.Windows.Forms.RadioButton rbAceptada;
        private System.Windows.Forms.RadioButton rbRechazada;
        private BotonTema btnGuardarCotizacion;
        private AvisoControl avisoMensaje;

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
            layout = new TableLayoutPanel();
            lblTitulo = new Label();
            lblEmpleado = new Label();
            cmbEmpleado = new ComboBoxFiltrable();
            lblMonto = new Label();
            numMontoCotizado = new NumericUpDown();
            lblRepuestos = new Label();
            filaRepuesto = new TableLayoutPanel();
            cmbRepuesto = new ComboBoxFiltrable();
            numCantidad = new NumericUpDown();
            btnAgregarRepuesto = new BotonTema();
            lblDisponible = new Label();
            dgvRepuestos = new DataGridView();
            btnQuitarRepuesto = new BotonTema();
            lblDesglose = new Label();
            lblTotalCotizacion = new Label();
            lblDecision = new Label();
            filaDecision = new FlowLayoutPanel();
            rbAceptada = new RadioButton();
            rbRechazada = new RadioButton();
            btnGuardarCotizacion = new BotonTema();
            avisoMensaje = new AvisoControl();
            ((System.ComponentModel.ISupportInitialize)numMontoCotizado).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRepuestos).BeginInit();
            layout.SuspendLayout();
            filaRepuesto.SuspendLayout();
            filaDecision.SuspendLayout();
            SuspendLayout();
            //
            // layout
            //
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(lblTitulo, 0, 0);
            layout.Controls.Add(lblEmpleado, 0, 1);
            layout.Controls.Add(cmbEmpleado, 0, 2);
            layout.Controls.Add(lblMonto, 0, 3);
            layout.Controls.Add(numMontoCotizado, 0, 4);
            layout.Controls.Add(lblRepuestos, 0, 5);
            layout.Controls.Add(filaRepuesto, 0, 6);
            layout.Controls.Add(lblDisponible, 0, 7);
            layout.Controls.Add(dgvRepuestos, 0, 8);
            layout.Controls.Add(btnQuitarRepuesto, 0, 9);
            layout.Controls.Add(lblDesglose, 0, 10);
            layout.Controls.Add(lblTotalCotizacion, 0, 11);
            layout.Controls.Add(lblDecision, 0, 12);
            layout.Controls.Add(filaDecision, 0, 13);
            layout.Controls.Add(btnGuardarCotizacion, 0, 14);
            layout.Controls.Add(avisoMensaje, 0, 15);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.RowCount = 16;
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.Size = new Size(480, 760);
            layout.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Margin = new Padding(0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(150, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cotización";
            //
            // lblEmpleado
            //
            lblEmpleado.AutoSize = true;
            lblEmpleado.Margin = new Padding(0, 12, 0, 4);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(160, 20);
            lblEmpleado.TabIndex = 1;
            lblEmpleado.Text = "Empleado encargado";
            //
            // cmbEmpleado
            //
            cmbEmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbEmpleado.Margin = new Padding(0);
            cmbEmpleado.Name = "cmbEmpleado";
            cmbEmpleado.Size = new Size(480, 27);
            cmbEmpleado.TabIndex = 2;
            //
            // lblMonto
            //
            lblMonto.AutoSize = true;
            lblMonto.Margin = new Padding(0, 12, 0, 4);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(110, 20);
            lblMonto.TabIndex = 3;
            lblMonto.Text = "Costo de mano de obra (Q)";
            //
            // numMontoCotizado
            //
            numMontoCotizado.DecimalPlaces = 2;
            numMontoCotizado.Margin = new Padding(0);
            numMontoCotizado.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numMontoCotizado.Name = "numMontoCotizado";
            numMontoCotizado.Size = new Size(160, 27);
            numMontoCotizado.TabIndex = 4;
            numMontoCotizado.ThousandsSeparator = true;
            //
            // lblRepuestos
            //
            lblRepuestos.AutoSize = true;
            lblRepuestos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRepuestos.Margin = new Padding(0, 20, 0, 6);
            lblRepuestos.Name = "lblRepuestos";
            lblRepuestos.Size = new Size(150, 23);
            lblRepuestos.TabIndex = 5;
            lblRepuestos.Text = "Repuestos cotizados";
            //
            // filaRepuesto
            //
            filaRepuesto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filaRepuesto.AutoSize = true;
            filaRepuesto.ColumnCount = 3;
            filaRepuesto.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filaRepuesto.ColumnStyles.Add(new ColumnStyle());
            filaRepuesto.ColumnStyles.Add(new ColumnStyle());
            filaRepuesto.Controls.Add(cmbRepuesto, 0, 0);
            filaRepuesto.Controls.Add(numCantidad, 1, 0);
            filaRepuesto.Controls.Add(btnAgregarRepuesto, 2, 0);
            filaRepuesto.Margin = new Padding(0);
            filaRepuesto.Name = "filaRepuesto";
            filaRepuesto.RowCount = 1;
            filaRepuesto.RowStyles.Add(new RowStyle());
            filaRepuesto.Size = new Size(480, 36);
            filaRepuesto.TabIndex = 6;
            //
            // cmbRepuesto
            //
            cmbRepuesto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbRepuesto.Margin = new Padding(0, 0, 8, 0);
            cmbRepuesto.Name = "cmbRepuesto";
            cmbRepuesto.Size = new Size(270, 28);
            cmbRepuesto.TabIndex = 0;
            //
            // numCantidad
            //
            numCantidad.Anchor = AnchorStyles.Left;
            numCantidad.Margin = new Padding(0, 0, 8, 0);
            numCantidad.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(80, 27);
            numCantidad.TabIndex = 1;
            numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnAgregarRepuesto
            //
            btnAgregarRepuesto.Margin = new Padding(0);
            btnAgregarRepuesto.Name = "btnAgregarRepuesto";
            btnAgregarRepuesto.NombreIcono = "plus";
            btnAgregarRepuesto.Size = new Size(110, 36);
            btnAgregarRepuesto.TabIndex = 2;
            btnAgregarRepuesto.Text = "Agregar";
            btnAgregarRepuesto.Click += btnAgregarRepuesto_Click;
            //
            // lblDisponible
            //
            lblDisponible.AutoSize = true;
            lblDisponible.ForeColor = Color.Gray;
            lblDisponible.Margin = new Padding(0, 6, 0, 6);
            lblDisponible.Name = "lblDisponible";
            lblDisponible.Size = new Size(100, 20);
            lblDisponible.TabIndex = 7;
            lblDisponible.Text = "Disponible: -";
            //
            // dgvRepuestos
            //
            dgvRepuestos.AllowUserToAddRows = false;
            dgvRepuestos.AllowUserToDeleteRows = false;
            dgvRepuestos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRepuestos.Dock = DockStyle.Fill;
            dgvRepuestos.Margin = new Padding(0);
            dgvRepuestos.MinimumSize = new Size(0, 120);
            dgvRepuestos.MultiSelect = false;
            dgvRepuestos.Name = "dgvRepuestos";
            dgvRepuestos.ReadOnly = true;
            dgvRepuestos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRepuestos.Size = new Size(480, 120);
            dgvRepuestos.TabIndex = 8;
            //
            // btnQuitarRepuesto
            //
            btnQuitarRepuesto.Anchor = AnchorStyles.Left;
            btnQuitarRepuesto.Margin = new Padding(0, 8, 0, 0);
            btnQuitarRepuesto.Name = "btnQuitarRepuesto";
            btnQuitarRepuesto.NombreIcono = "minus";
            btnQuitarRepuesto.Size = new Size(260, 36);
            btnQuitarRepuesto.TabIndex = 9;
            btnQuitarRepuesto.Text = "Quitar repuesto seleccionado";
            btnQuitarRepuesto.Click += btnQuitarRepuesto_Click;
            //
            // lblDesglose
            //
            lblDesglose.AutoSize = true;
            lblDesglose.Margin = new Padding(0, 16, 0, 0);
            lblDesglose.Name = "lblDesglose";
            lblDesglose.Size = new Size(300, 60);
            lblDesglose.TabIndex = 10;
            lblDesglose.Text = "Diagnóstico: Q 0.00";
            //
            // lblTotalCotizacion
            //
            lblTotalCotizacion.AutoSize = true;
            lblTotalCotizacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalCotizacion.Margin = new Padding(0, 8, 0, 0);
            lblTotalCotizacion.Name = "lblTotalCotizacion";
            lblTotalCotizacion.Size = new Size(300, 23);
            lblTotalCotizacion.TabIndex = 11;
            lblTotalCotizacion.Text = "Total a cobrar: Q 0.00";
            //
            // lblDecision
            //
            lblDecision.AutoSize = true;
            lblDecision.Margin = new Padding(0, 16, 0, 4);
            lblDecision.Name = "lblDecision";
            lblDecision.Size = new Size(160, 20);
            lblDecision.TabIndex = 12;
            lblDecision.Text = "Decisión del cliente";
            //
            // filaDecision
            //
            filaDecision.AutoSize = true;
            filaDecision.Controls.Add(rbAceptada);
            filaDecision.Controls.Add(rbRechazada);
            filaDecision.Margin = new Padding(0);
            filaDecision.Name = "filaDecision";
            filaDecision.Size = new Size(260, 30);
            filaDecision.TabIndex = 13;
            //
            // rbAceptada
            //
            rbAceptada.AutoSize = true;
            rbAceptada.Checked = true;
            rbAceptada.Margin = new Padding(0, 3, 24, 3);
            rbAceptada.Name = "rbAceptada";
            rbAceptada.Size = new Size(100, 24);
            rbAceptada.TabIndex = 0;
            rbAceptada.TabStop = true;
            rbAceptada.Text = "Aceptada";
            //
            // rbRechazada
            //
            rbRechazada.AutoSize = true;
            rbRechazada.Margin = new Padding(0, 3, 0, 3);
            rbRechazada.Name = "rbRechazada";
            rbRechazada.Size = new Size(110, 24);
            rbRechazada.TabIndex = 1;
            rbRechazada.Text = "Rechazada";
            //
            // btnGuardarCotizacion
            //
            btnGuardarCotizacion.Anchor = AnchorStyles.Left;
            btnGuardarCotizacion.Margin = new Padding(0, 16, 0, 0);
            btnGuardarCotizacion.Name = "btnGuardarCotizacion";
            btnGuardarCotizacion.NombreIcono = "save";
            btnGuardarCotizacion.Size = new Size(200, 36);
            btnGuardarCotizacion.TabIndex = 14;
            btnGuardarCotizacion.Text = "Guardar cotización";
            btnGuardarCotizacion.Variante = VarianteBoton.Primario;
            btnGuardarCotizacion.Click += btnGuardarCotizacion_Click;
            //
            // avisoMensaje
            //
            avisoMensaje.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            avisoMensaje.Margin = new Padding(0, 12, 0, 0);
            avisoMensaje.Name = "avisoMensaje";
            avisoMensaje.Size = new Size(480, 40);
            avisoMensaje.TabIndex = 15;
            //
            // CotizacionControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "CotizacionControl";
            Size = new Size(480, 780);
            ((System.ComponentModel.ISupportInitialize)numMontoCotizado).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRepuestos).EndInit();
            layout.ResumeLayout(false);
            layout.PerformLayout();
            filaRepuesto.ResumeLayout(false);
            filaRepuesto.PerformLayout();
            filaDecision.ResumeLayout(false);
            filaDecision.PerformLayout();
            ResumeLayout(false);
        }
    }
}

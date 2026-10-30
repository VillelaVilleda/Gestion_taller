namespace GestorTaller
{
    partial class ReparacionControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblEmpleado;
        private GestorTaller.ComboBoxFiltrable cmbEmpleado;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.TextBox txtDetalle;
        private System.Windows.Forms.Label lblRepuestos;
        private System.Windows.Forms.DataGridView dgvRepuestosAsignados;
        private BotonTema btnGuardarReparacion;
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
            lblSubtitulo = new Label();
            lblEmpleado = new Label();
            cmbEmpleado = new ComboBoxFiltrable();
            lblDetalle = new Label();
            txtDetalle = new TextBox();
            lblRepuestos = new Label();
            dgvRepuestosAsignados = new DataGridView();
            btnGuardarReparacion = new BotonTema();
            avisoMensaje = new AvisoControl();
            ((System.ComponentModel.ISupportInitialize)dgvRepuestosAsignados).BeginInit();
            layout.SuspendLayout();
            SuspendLayout();
            //
            // layout
            //
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(lblTitulo, 0, 0);
            layout.Controls.Add(lblSubtitulo, 0, 1);
            layout.Controls.Add(lblEmpleado, 0, 2);
            layout.Controls.Add(cmbEmpleado, 0, 3);
            layout.Controls.Add(lblDetalle, 0, 4);
            layout.Controls.Add(txtDetalle, 0, 5);
            layout.Controls.Add(lblRepuestos, 0, 6);
            layout.Controls.Add(dgvRepuestosAsignados, 0, 7);
            layout.Controls.Add(btnGuardarReparacion, 0, 8);
            layout.Controls.Add(avisoMensaje, 0, 9);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.RowCount = 10;
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.Size = new Size(460, 600);
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
            lblTitulo.Text = "Reparación";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = Color.Gray;
            lblSubtitulo.Margin = new Padding(0, 2, 0, 0);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(320, 20);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Al guardar, la orden pasa a estado Terminado";
            //
            // lblEmpleado
            //
            lblEmpleado.AutoSize = true;
            lblEmpleado.Margin = new Padding(0, 12, 0, 4);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(160, 20);
            lblEmpleado.TabIndex = 2;
            lblEmpleado.Text = "Empleado encargado";
            //
            // cmbEmpleado
            //
            cmbEmpleado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbEmpleado.Margin = new Padding(0);
            cmbEmpleado.Name = "cmbEmpleado";
            cmbEmpleado.Size = new Size(460, 27);
            cmbEmpleado.TabIndex = 3;
            //
            // lblDetalle
            //
            lblDetalle.AutoSize = true;
            lblDetalle.Margin = new Padding(0, 12, 0, 4);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(150, 20);
            lblDetalle.TabIndex = 4;
            lblDetalle.Text = "Detalle de la reparación";
            //
            // txtDetalle
            //
            txtDetalle.Dock = DockStyle.Fill;
            txtDetalle.Margin = new Padding(0);
            txtDetalle.MinimumSize = new Size(0, 90);
            txtDetalle.Multiline = true;
            txtDetalle.Name = "txtDetalle";
            txtDetalle.Size = new Size(460, 90);
            txtDetalle.TabIndex = 5;
            //
            // lblRepuestos
            //
            lblRepuestos.AutoSize = true;
            lblRepuestos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRepuestos.Margin = new Padding(0, 20, 0, 6);
            lblRepuestos.Name = "lblRepuestos";
            lblRepuestos.Size = new Size(150, 23);
            lblRepuestos.TabIndex = 6;
            lblRepuestos.Text = "Repuestos a utilizar";
            //
            // dgvRepuestosAsignados
            //
            dgvRepuestosAsignados.AllowUserToAddRows = false;
            dgvRepuestosAsignados.AllowUserToDeleteRows = false;
            dgvRepuestosAsignados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRepuestosAsignados.Dock = DockStyle.Fill;
            dgvRepuestosAsignados.Margin = new Padding(0);
            dgvRepuestosAsignados.MinimumSize = new Size(0, 110);
            dgvRepuestosAsignados.Name = "dgvRepuestosAsignados";
            dgvRepuestosAsignados.ReadOnly = true;
            dgvRepuestosAsignados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRepuestosAsignados.Size = new Size(460, 110);
            dgvRepuestosAsignados.TabIndex = 7;
            //
            // btnGuardarReparacion
            //
            btnGuardarReparacion.Anchor = AnchorStyles.Left;
            btnGuardarReparacion.Margin = new Padding(0, 16, 0, 0);
            btnGuardarReparacion.Name = "btnGuardarReparacion";
            btnGuardarReparacion.NombreIcono = "save";
            btnGuardarReparacion.Size = new Size(200, 36);
            btnGuardarReparacion.TabIndex = 8;
            btnGuardarReparacion.Text = "Guardar reparación";
            btnGuardarReparacion.Variante = VarianteBoton.Primario;
            btnGuardarReparacion.Click += btnGuardarReparacion_Click;
            //
            // avisoMensaje
            //
            avisoMensaje.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            avisoMensaje.Margin = new Padding(0, 12, 0, 0);
            avisoMensaje.Name = "avisoMensaje";
            avisoMensaje.Size = new Size(460, 40);
            avisoMensaje.TabIndex = 9;
            //
            // ReparacionControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "ReparacionControl";
            Size = new Size(460, 600);
            ((System.ComponentModel.ISupportInitialize)dgvRepuestosAsignados).EndInit();
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ResumeLayout(false);
        }
    }
}

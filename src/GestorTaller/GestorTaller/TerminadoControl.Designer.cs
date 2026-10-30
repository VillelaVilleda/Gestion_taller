namespace GestorTaller
{
    partial class TerminadoControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblEmpleado;
        private GestorTaller.ComboBoxFiltrable cmbEmpleado;
        private System.Windows.Forms.Label lblDesglose;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.Label lblTotalAPagar;
        private System.Windows.Forms.Label lblFechaEntrega;
        private BotonTema btnConfirmarPago;
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
            lblDesglose = new Label();
            lblMonto = new Label();
            lblTotalAPagar = new Label();
            lblFechaEntrega = new Label();
            btnConfirmarPago = new BotonTema();
            avisoMensaje = new AvisoControl();
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
            layout.Controls.Add(lblDesglose, 0, 4);
            layout.Controls.Add(lblMonto, 0, 5);
            layout.Controls.Add(lblTotalAPagar, 0, 6);
            layout.Controls.Add(lblFechaEntrega, 0, 7);
            layout.Controls.Add(btnConfirmarPago, 0, 8);
            layout.Controls.Add(avisoMensaje, 0, 9);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.RowCount = 11;
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.Size = new Size(460, 500);
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
            lblTitulo.Text = "Entrega";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = Color.Gray;
            lblSubtitulo.Margin = new Padding(0, 2, 0, 0);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(320, 20);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Al confirmar el pago, la orden pasa a estado Entregado";
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
            // lblDesglose
            //
            lblDesglose.AutoSize = true;
            lblDesglose.Margin = new Padding(0, 20, 0, 0);
            lblDesglose.Name = "lblDesglose";
            lblDesglose.Size = new Size(300, 60);
            lblDesglose.TabIndex = 4;
            lblDesglose.Text = "Costo de diagnóstico: Q 0.00";
            //
            // lblMonto
            //
            lblMonto.AutoSize = true;
            lblMonto.Margin = new Padding(0, 16, 0, 0);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(110, 20);
            lblMonto.TabIndex = 5;
            lblMonto.Text = "Total a pagar";
            //
            // lblTotalAPagar
            //
            lblTotalAPagar.AutoSize = true;
            lblTotalAPagar.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalAPagar.Margin = new Padding(0);
            lblTotalAPagar.Name = "lblTotalAPagar";
            lblTotalAPagar.Size = new Size(90, 32);
            lblTotalAPagar.TabIndex = 6;
            lblTotalAPagar.Text = "Q 0.00";
            //
            // lblFechaEntrega
            //
            lblFechaEntrega.AutoSize = true;
            lblFechaEntrega.ForeColor = Color.Gray;
            lblFechaEntrega.Margin = new Padding(0, 4, 0, 0);
            lblFechaEntrega.Name = "lblFechaEntrega";
            lblFechaEntrega.Size = new Size(0, 20);
            lblFechaEntrega.TabIndex = 7;
            //
            // btnConfirmarPago
            //
            btnConfirmarPago.Anchor = AnchorStyles.Left;
            btnConfirmarPago.Margin = new Padding(0, 16, 0, 0);
            btnConfirmarPago.Name = "btnConfirmarPago";
            btnConfirmarPago.NombreIcono = "circle-check";
            btnConfirmarPago.Size = new Size(200, 36);
            btnConfirmarPago.TabIndex = 8;
            btnConfirmarPago.Text = "Pago confirmado";
            btnConfirmarPago.Variante = VarianteBoton.Primario;
            btnConfirmarPago.Click += btnConfirmarPago_Click;
            //
            // avisoMensaje
            //
            avisoMensaje.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            avisoMensaje.Margin = new Padding(0, 12, 0, 0);
            avisoMensaje.Name = "avisoMensaje";
            avisoMensaje.Size = new Size(460, 40);
            avisoMensaje.TabIndex = 9;
            //
            // TerminadoControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "TerminadoControl";
            Size = new Size(460, 500);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ResumeLayout(false);
        }
    }
}

namespace GestorTaller
{
    partial class TerminadoControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblEmpleado;
        private GestorTaller.ComboBoxFiltrable cmbEmpleado;
        private System.Windows.Forms.Label lblDesglose;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.Label lblTotalAPagar;
        private System.Windows.Forms.Label lblFechaEntrega;
        private System.Windows.Forms.Button btnConfirmarPago;
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
            lblDesglose = new Label();
            lblMonto = new Label();
            lblTotalAPagar = new Label();
            lblFechaEntrega = new Label();
            btnConfirmarPago = new Button();
            lblMensaje = new Label();
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
            lblTitulo.Text = "Entrega";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = Color.Gray;
            lblSubtitulo.Location = new Point(20, 45);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(320, 20);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Al confirmar el pago, la orden pasa a estado Entregado";
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
            // lblDesglose
            //
            lblDesglose.AutoSize = true;
            lblDesglose.Location = new Point(20, 135);
            lblDesglose.MaximumSize = new Size(340, 0);
            lblDesglose.Name = "lblDesglose";
            lblDesglose.Size = new Size(300, 60);
            lblDesglose.TabIndex = 9;
            lblDesglose.Text = "Costo de diagnostico: Q 0.00";
            //
            // lblMonto
            //
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(20, 205);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(110, 20);
            lblMonto.TabIndex = 4;
            lblMonto.Text = "Total a pagar";
            //
            // lblTotalAPagar
            //
            lblTotalAPagar.AutoSize = true;
            lblTotalAPagar.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalAPagar.Location = new Point(20, 225);
            lblTotalAPagar.Name = "lblTotalAPagar";
            lblTotalAPagar.Size = new Size(100, 32);
            lblTotalAPagar.TabIndex = 5;
            lblTotalAPagar.Text = "Q 0.00";
            //
            // lblFechaEntrega
            //
            lblFechaEntrega.AutoSize = true;
            lblFechaEntrega.ForeColor = Color.Gray;
            lblFechaEntrega.Location = new Point(20, 265);
            lblFechaEntrega.Name = "lblFechaEntrega";
            lblFechaEntrega.Size = new Size(0, 20);
            lblFechaEntrega.TabIndex = 6;
            //
            // btnConfirmarPago
            //
            btnConfirmarPago.Location = new Point(20, 295);
            btnConfirmarPago.Name = "btnConfirmarPago";
            btnConfirmarPago.Size = new Size(180, 32);
            btnConfirmarPago.TabIndex = 7;
            btnConfirmarPago.Text = "Pago confirmado";
            btnConfirmarPago.Click += btnConfirmarPago_Click;
            //
            // lblMensaje
            //
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 340);
            lblMensaje.MaximumSize = new Size(340, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 8;
            //
            // TerminadoControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblEmpleado);
            Controls.Add(cmbEmpleado);
            Controls.Add(lblDesglose);
            Controls.Add(lblMonto);
            Controls.Add(lblTotalAPagar);
            Controls.Add(lblFechaEntrega);
            Controls.Add(btnConfirmarPago);
            Controls.Add(lblMensaje);
            Name = "TerminadoControl";
            Size = new Size(400, 390);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

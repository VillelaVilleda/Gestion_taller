namespace GestorTaller
{
    partial class RecepcionSoloLecturaControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblObjeto;
        private System.Windows.Forms.TextBox txtObjeto;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.TextBox txtCosto;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtFecha;

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
            lblCliente = new Label();
            txtCliente = new TextBox();
            lblObjeto = new Label();
            txtObjeto = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblCosto = new Label();
            txtCosto = new TextBox();
            lblFecha = new Label();
            txtFecha = new TextBox();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(109, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Recepcion";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(20, 55);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(55, 20);
            lblCliente.TabIndex = 1;
            lblCliente.Text = "Cliente";
            // 
            // txtCliente
            // 
            txtCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCliente.BackColor = SystemColors.Control;
            txtCliente.Location = new Point(20, 75);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(340, 27);
            txtCliente.TabIndex = 2;
            // 
            // lblObjeto
            // 
            lblObjeto.AutoSize = true;
            lblObjeto.Location = new Point(20, 110);
            lblObjeto.Name = "lblObjeto";
            lblObjeto.Size = new Size(160, 20);
            lblObjeto.TabIndex = 3;
            lblObjeto.Text = "Descripcion del objeto";
            // 
            // txtObjeto
            // 
            txtObjeto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtObjeto.BackColor = SystemColors.Control;
            txtObjeto.Location = new Point(20, 130);
            txtObjeto.Name = "txtObjeto";
            txtObjeto.ReadOnly = true;
            txtObjeto.Size = new Size(340, 27);
            txtObjeto.TabIndex = 4;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(20, 165);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(181, 20);
            lblDescripcion.TabIndex = 5;
            lblDescripcion.Text = "Descripcion del problema";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescripcion.BackColor = SystemColors.Control;
            txtDescripcion.Location = new Point(20, 185);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ReadOnly = true;
            txtDescripcion.Size = new Size(340, 70);
            txtDescripcion.TabIndex = 6;
            // 
            // lblCosto
            // 
            lblCosto.AutoSize = true;
            lblCosto.Location = new Point(20, 270);
            lblCosto.Name = "lblCosto";
            lblCosto.Size = new Size(150, 20);
            lblCosto.TabIndex = 7;
            lblCosto.Text = "Costo de diagnostico";
            // 
            // txtCosto
            // 
            txtCosto.BackColor = SystemColors.Control;
            txtCosto.Location = new Point(20, 290);
            txtCosto.Name = "txtCosto";
            txtCosto.ReadOnly = true;
            txtCosto.Size = new Size(150, 27);
            txtCosto.TabIndex = 8;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(200, 270);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(137, 20);
            lblFecha.TabIndex = 9;
            lblFecha.Text = "Fecha de recepcion";
            // 
            // txtFecha
            // 
            txtFecha.BackColor = SystemColors.Control;
            txtFecha.Location = new Point(200, 290);
            txtFecha.Name = "txtFecha";
            txtFecha.ReadOnly = true;
            txtFecha.Size = new Size(160, 27);
            txtFecha.TabIndex = 10;
            // 
            // RecepcionSoloLecturaControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(lblCliente);
            Controls.Add(txtCliente);
            Controls.Add(lblObjeto);
            Controls.Add(txtObjeto);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(lblCosto);
            Controls.Add(txtCosto);
            Controls.Add(lblFecha);
            Controls.Add(txtFecha);
            Name = "RecepcionSoloLecturaControl";
            Size = new Size(400, 340);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
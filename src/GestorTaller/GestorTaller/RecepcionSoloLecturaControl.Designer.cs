namespace GestorTaller
{
    partial class RecepcionSoloLecturaControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblObjeto;
        private System.Windows.Forms.TextBox txtObjeto;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TableLayoutPanel filaCostoFecha;
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
            layout = new TableLayoutPanel();
            lblTitulo = new Label();
            lblCliente = new Label();
            txtCliente = new TextBox();
            lblObjeto = new Label();
            txtObjeto = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            filaCostoFecha = new TableLayoutPanel();
            lblCosto = new Label();
            txtCosto = new TextBox();
            lblFecha = new Label();
            txtFecha = new TextBox();
            layout.SuspendLayout();
            filaCostoFecha.SuspendLayout();
            SuspendLayout();
            //
            // layout
            //
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(lblTitulo, 0, 0);
            layout.Controls.Add(lblCliente, 0, 1);
            layout.Controls.Add(txtCliente, 0, 2);
            layout.Controls.Add(lblObjeto, 0, 3);
            layout.Controls.Add(txtObjeto, 0, 4);
            layout.Controls.Add(lblDescripcion, 0, 5);
            layout.Controls.Add(txtDescripcion, 0, 6);
            layout.Controls.Add(filaCostoFecha, 0, 7);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.RowCount = 8;
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle());
            layout.Size = new Size(460, 400);
            layout.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Margin = new Padding(0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(109, 28);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Recepción";
            //
            // lblCliente
            //
            lblCliente.AutoSize = true;
            lblCliente.Margin = new Padding(0, 12, 0, 4);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(55, 20);
            lblCliente.TabIndex = 1;
            lblCliente.Text = "Cliente";
            //
            // txtCliente
            //
            txtCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCliente.BackColor = SystemColors.Control;
            txtCliente.Margin = new Padding(0);
            txtCliente.Name = "txtCliente";
            txtCliente.ReadOnly = true;
            txtCliente.Size = new Size(460, 27);
            txtCliente.TabIndex = 2;
            //
            // lblObjeto
            //
            lblObjeto.AutoSize = true;
            lblObjeto.Margin = new Padding(0, 12, 0, 4);
            lblObjeto.Name = "lblObjeto";
            lblObjeto.Size = new Size(160, 20);
            lblObjeto.TabIndex = 3;
            lblObjeto.Text = "Descripción del objeto";
            //
            // txtObjeto
            //
            txtObjeto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtObjeto.BackColor = SystemColors.Control;
            txtObjeto.Margin = new Padding(0);
            txtObjeto.Name = "txtObjeto";
            txtObjeto.ReadOnly = true;
            txtObjeto.Size = new Size(460, 27);
            txtObjeto.TabIndex = 4;
            //
            // lblDescripcion
            //
            lblDescripcion.AutoSize = true;
            lblDescripcion.Margin = new Padding(0, 12, 0, 4);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(181, 20);
            lblDescripcion.TabIndex = 5;
            lblDescripcion.Text = "Descripción del problema";
            //
            // txtDescripcion
            //
            txtDescripcion.BackColor = SystemColors.Control;
            txtDescripcion.Dock = DockStyle.Fill;
            txtDescripcion.Margin = new Padding(0);
            txtDescripcion.MinimumSize = new Size(0, 70);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ReadOnly = true;
            txtDescripcion.Size = new Size(460, 70);
            txtDescripcion.TabIndex = 6;
            //
            // filaCostoFecha
            //
            filaCostoFecha.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filaCostoFecha.AutoSize = true;
            filaCostoFecha.ColumnCount = 2;
            filaCostoFecha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            filaCostoFecha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            filaCostoFecha.Controls.Add(lblCosto, 0, 0);
            filaCostoFecha.Controls.Add(lblFecha, 1, 0);
            filaCostoFecha.Controls.Add(txtCosto, 0, 1);
            filaCostoFecha.Controls.Add(txtFecha, 1, 1);
            filaCostoFecha.Margin = new Padding(0);
            filaCostoFecha.Name = "filaCostoFecha";
            filaCostoFecha.RowCount = 2;
            filaCostoFecha.RowStyles.Add(new RowStyle());
            filaCostoFecha.RowStyles.Add(new RowStyle());
            filaCostoFecha.Size = new Size(460, 60);
            filaCostoFecha.TabIndex = 7;
            //
            // lblCosto
            //
            lblCosto.AutoSize = true;
            lblCosto.Margin = new Padding(0, 12, 0, 4);
            lblCosto.Name = "lblCosto";
            lblCosto.Size = new Size(150, 20);
            lblCosto.TabIndex = 0;
            lblCosto.Text = "Costo de diagnóstico (Q)";
            //
            // txtCosto
            //
            txtCosto.BackColor = SystemColors.Control;
            txtCosto.Dock = DockStyle.Fill;
            txtCosto.Margin = new Padding(0, 0, 8, 0);
            txtCosto.Name = "txtCosto";
            txtCosto.ReadOnly = true;
            txtCosto.Size = new Size(150, 27);
            txtCosto.TabIndex = 2;
            //
            // lblFecha
            //
            lblFecha.AutoSize = true;
            lblFecha.Margin = new Padding(8, 12, 0, 4);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(137, 20);
            lblFecha.TabIndex = 1;
            lblFecha.Text = "Fecha de recepción";
            //
            // txtFecha
            //
            txtFecha.BackColor = SystemColors.Control;
            txtFecha.Dock = DockStyle.Fill;
            txtFecha.Margin = new Padding(8, 0, 0, 0);
            txtFecha.Name = "txtFecha";
            txtFecha.ReadOnly = true;
            txtFecha.Size = new Size(160, 27);
            txtFecha.TabIndex = 3;
            //
            // RecepcionSoloLecturaControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "RecepcionSoloLecturaControl";
            Size = new Size(460, 400);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            filaCostoFecha.ResumeLayout(false);
            filaCostoFecha.PerformLayout();
            ResumeLayout(false);
        }
    }
}

namespace GestorTaller
{
    partial class ListadoOrdenesControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblPendientes;
        private GestorTaller.TablaListadoControl tablaPendientes;
        private System.Windows.Forms.Label lblCerradas;
        private GestorTaller.TablaListadoControl tablaCerradas;

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
            layout = new TableLayoutPanel();
            lblTitulo = new Label();
            lblPendientes = new Label();
            tablaPendientes = new TablaListadoControl();
            lblCerradas = new Label();
            tablaCerradas = new TablaListadoControl();
            layout.SuspendLayout();
            SuspendLayout();
            //
            // layout
            //
            layout.AutoScroll = true;
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(lblTitulo, 0, 0);
            layout.Controls.Add(lblPendientes, 0, 1);
            layout.Controls.Add(tablaPendientes, 0, 2);
            layout.Controls.Add(lblCerradas, 0, 3);
            layout.Controls.Add(tablaCerradas, 0, 4);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.Padding = new Padding(24);
            layout.RowCount = 5;
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            layout.Size = new Size(680, 560);
            layout.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Margin = new Padding(0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(243, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Órdenes registradas";
            //
            // lblPendientes
            //
            lblPendientes.AutoSize = true;
            lblPendientes.Font = new Font("Segoe UI Semibold", 11F);
            lblPendientes.Margin = new Padding(0, 16, 0, 4);
            lblPendientes.Name = "lblPendientes";
            lblPendientes.Size = new Size(100, 25);
            lblPendientes.TabIndex = 1;
            lblPendientes.Text = "Pendientes";
            //
            // tablaPendientes
            //
            tablaPendientes.Dock = DockStyle.Fill;
            tablaPendientes.Margin = new Padding(0);
            tablaPendientes.MinimumSize = new Size(0, 240);
            tablaPendientes.Name = "tablaPendientes";
            tablaPendientes.Size = new Size(632, 240);
            tablaPendientes.TabIndex = 2;
            //
            // lblCerradas
            //
            lblCerradas.AutoSize = true;
            lblCerradas.Font = new Font("Segoe UI Semibold", 11F);
            lblCerradas.Margin = new Padding(0, 16, 0, 4);
            lblCerradas.Name = "lblCerradas";
            lblCerradas.Size = new Size(85, 25);
            lblCerradas.TabIndex = 3;
            lblCerradas.Text = "Cerradas";
            //
            // tablaCerradas
            //
            tablaCerradas.Dock = DockStyle.Fill;
            tablaCerradas.Margin = new Padding(0);
            tablaCerradas.MinimumSize = new Size(0, 240);
            tablaCerradas.Name = "tablaCerradas";
            tablaCerradas.Size = new Size(632, 200);
            tablaCerradas.TabIndex = 4;
            //
            // ListadoOrdenesControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "ListadoOrdenesControl";
            Size = new Size(680, 560);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}

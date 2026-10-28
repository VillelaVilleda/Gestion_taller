namespace GestorTaller
{
    partial class RepuestosControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.TableLayoutPanel encabezado;
        private System.Windows.Forms.Label lblTitulo;
        private BotonTema btnNuevo;
        private AvisoControl avisoMensaje;
        private GestorTaller.TablaListadoControl tablaRepuestos;

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
            encabezado = new TableLayoutPanel();
            lblTitulo = new Label();
            btnNuevo = new BotonTema();
            avisoMensaje = new AvisoControl();
            tablaRepuestos = new TablaListadoControl();
            layout.SuspendLayout();
            encabezado.SuspendLayout();
            SuspendLayout();
            //
            // layout
            //
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(encabezado, 0, 0);
            layout.Controls.Add(avisoMensaje, 0, 1);
            layout.Controls.Add(tablaRepuestos, 0, 2);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.Padding = new Padding(24);
            layout.RowCount = 3;
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.Size = new Size(680, 460);
            layout.TabIndex = 0;
            //
            // encabezado
            //
            encabezado.AutoSize = true;
            encabezado.ColumnCount = 2;
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            encabezado.ColumnStyles.Add(new ColumnStyle());
            encabezado.Controls.Add(lblTitulo, 0, 0);
            encabezado.Controls.Add(btnNuevo, 1, 0);
            encabezado.Dock = DockStyle.Fill;
            encabezado.Margin = new Padding(0);
            encabezado.Name = "encabezado";
            encabezado.RowCount = 1;
            encabezado.RowStyles.Add(new RowStyle());
            encabezado.Size = new Size(632, 40);
            encabezado.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.Anchor = AnchorStyles.Left;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Margin = new Padding(0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(110, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Repuestos";
            //
            // btnNuevo
            //
            btnNuevo.Anchor = AnchorStyles.Right;
            btnNuevo.Margin = new Padding(0);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.NombreIcono = "plus";
            btnNuevo.Size = new Size(160, 36);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "Nuevo repuesto";
            btnNuevo.Variante = VarianteBoton.Primario;
            btnNuevo.Click += btnNuevo_Click;
            //
            // avisoMensaje
            //
            avisoMensaje.Dock = DockStyle.Fill;
            avisoMensaje.Margin = new Padding(0, 12, 0, 0);
            avisoMensaje.Name = "avisoMensaje";
            avisoMensaje.Size = new Size(632, 40);
            avisoMensaje.TabIndex = 1;
            //
            // tablaRepuestos
            //
            tablaRepuestos.Dock = DockStyle.Fill;
            tablaRepuestos.Margin = new Padding(0, 12, 0, 0);
            tablaRepuestos.Name = "tablaRepuestos";
            tablaRepuestos.Size = new Size(632, 350);
            tablaRepuestos.TabIndex = 2;
            //
            // RepuestosControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "RepuestosControl";
            Size = new Size(680, 460);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            encabezado.ResumeLayout(false);
            encabezado.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}

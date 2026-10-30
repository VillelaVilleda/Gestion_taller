namespace GestorTaller
{
    partial class SeguimientoOrdenControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.TableLayoutPanel encabezado;
        private BotonTema btnVolver;
        private System.Windows.Forms.Label lblEncabezado;
        private BotonTema btnEditarOrden;
        private BotonTema btnEliminarOrden;
        private GestorTaller.SeguimientoBreadcrumbControl breadcrumb;
        private System.Windows.Forms.Panel panelFormulario;

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
            encabezado = new TableLayoutPanel();
            btnVolver = new BotonTema();
            lblEncabezado = new Label();
            btnEditarOrden = new BotonTema();
            btnEliminarOrden = new BotonTema();
            breadcrumb = new SeguimientoBreadcrumbControl();
            panelFormulario = new Panel();
            layout.SuspendLayout();
            encabezado.SuspendLayout();
            SuspendLayout();
            //
            // layout
            //
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(encabezado, 0, 0);
            layout.Controls.Add(breadcrumb, 0, 1);
            layout.Controls.Add(panelFormulario, 0, 2);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.Padding = new Padding(24);
            layout.RowCount = 3;
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 110F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.Size = new Size(680, 560);
            layout.TabIndex = 0;
            //
            // encabezado
            //
            encabezado.AutoSize = true;
            encabezado.ColumnCount = 4;
            encabezado.ColumnStyles.Add(new ColumnStyle());
            encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            encabezado.ColumnStyles.Add(new ColumnStyle());
            encabezado.ColumnStyles.Add(new ColumnStyle());
            encabezado.Controls.Add(btnVolver, 0, 0);
            encabezado.Controls.Add(lblEncabezado, 1, 0);
            encabezado.Controls.Add(btnEditarOrden, 2, 0);
            encabezado.Controls.Add(btnEliminarOrden, 3, 0);
            encabezado.Dock = DockStyle.Fill;
            encabezado.Margin = new Padding(0);
            encabezado.Name = "encabezado";
            encabezado.RowCount = 1;
            encabezado.RowStyles.Add(new RowStyle());
            encabezado.Size = new Size(632, 36);
            encabezado.TabIndex = 0;
            //
            // btnVolver
            //
            btnVolver.Margin = new Padding(0);
            btnVolver.Name = "btnVolver";
            btnVolver.NombreIcono = "arrow-left";
            btnVolver.Size = new Size(170, 36);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver al listado";
            btnVolver.Click += btnVolver_Click;
            //
            // lblEncabezado
            //
            lblEncabezado.AutoEllipsis = true;
            lblEncabezado.Dock = DockStyle.Fill;
            lblEncabezado.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEncabezado.Margin = new Padding(16, 0, 16, 0);
            lblEncabezado.Name = "lblEncabezado";
            lblEncabezado.Size = new Size(200, 36);
            lblEncabezado.TabIndex = 1;
            lblEncabezado.TextAlign = ContentAlignment.MiddleLeft;
            //
            // btnEditarOrden
            //
            btnEditarOrden.Margin = new Padding(0, 0, 8, 0);
            btnEditarOrden.Name = "btnEditarOrden";
            btnEditarOrden.NombreIcono = "pencil";
            btnEditarOrden.Size = new Size(220, 36);
            btnEditarOrden.TabIndex = 2;
            btnEditarOrden.Text = "Editar datos de la orden";
            btnEditarOrden.Visible = false;
            btnEditarOrden.Click += btnEditarOrden_Click;
            //
            // btnEliminarOrden
            //
            btnEliminarOrden.Margin = new Padding(0);
            btnEliminarOrden.Name = "btnEliminarOrden";
            btnEliminarOrden.NombreIcono = "trash-2";
            btnEliminarOrden.Size = new Size(160, 36);
            btnEliminarOrden.TabIndex = 3;
            btnEliminarOrden.Text = "Eliminar orden";
            btnEliminarOrden.Variante = VarianteBoton.Peligro;
            btnEliminarOrden.Visible = false;
            btnEliminarOrden.Click += btnEliminarOrden_Click;
            //
            // breadcrumb
            //
            breadcrumb.Dock = DockStyle.Fill;
            breadcrumb.Margin = new Padding(0, 12, 0, 0);
            breadcrumb.Name = "breadcrumb";
            breadcrumb.Size = new Size(632, 98);
            breadcrumb.TabIndex = 1;
            //
            // panelFormulario
            //
            panelFormulario.Dock = DockStyle.Fill;
            panelFormulario.Margin = new Padding(0, 12, 0, 0);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(632, 350);
            panelFormulario.TabIndex = 2;
            //
            // SeguimientoOrdenControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "SeguimientoOrdenControl";
            Size = new Size(680, 560);
            layout.ResumeLayout(false);
            layout.PerformLayout();
            encabezado.ResumeLayout(false);
            encabezado.PerformLayout();
            ResumeLayout(false);
        }
    }
}

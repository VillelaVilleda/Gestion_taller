namespace GestorTaller
{
    partial class SeguimientoOrdenControl
    {
        private System.ComponentModel.IContainer components = null;

        private GestorTaller.SeguimientoBreadcrumbControl breadcrumb;
        private System.Windows.Forms.Panel panelFormulario;
        private System.Windows.Forms.Label lblEncabezado;
        private System.Windows.Forms.Button btnEditarOrden;
        private System.Windows.Forms.Button btnEliminarOrden;

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
            breadcrumb = new SeguimientoBreadcrumbControl();
            panelFormulario = new Panel();
            lblEncabezado = new Label();
            btnEditarOrden = new Button();
            btnEliminarOrden = new Button();
            SuspendLayout();
            // 
            // breadcrumb
            // 
            breadcrumb.Dock = DockStyle.Top;
            breadcrumb.Location = new Point(0, 0);
            breadcrumb.Name = "breadcrumb";
            breadcrumb.Size = new Size(680, 110);
            breadcrumb.TabIndex = 0;
            // 
            // panelFormulario
            // 
            panelFormulario.Dock = DockStyle.Fill;
            panelFormulario.Location = new Point(0, 110);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(680, 350);
            panelFormulario.TabIndex = 1;
            //
            // lblEncabezado
            //
            lblEncabezado.AutoEllipsis = true;
            lblEncabezado.Dock = DockStyle.Top;
            lblEncabezado.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEncabezado.Name = "lblEncabezado";
            lblEncabezado.Padding = new Padding(20, 6, 330, 0);
            lblEncabezado.Size = new Size(680, 34);
            lblEncabezado.TabIndex = 2;
            //
            // btnEditarOrden
            //
            btnEditarOrden.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditarOrden.Location = new Point(490, 3);
            btnEditarOrden.Name = "btnEditarOrden";
            btnEditarOrden.Size = new Size(180, 28);
            btnEditarOrden.TabIndex = 3;
            btnEditarOrden.Text = "Editar datos de la orden";
            btnEditarOrden.Visible = false;
            btnEditarOrden.Click += btnEditarOrden_Click;
            //
            // btnEliminarOrden
            //
            btnEliminarOrden.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminarOrden.Location = new Point(360, 3);
            btnEliminarOrden.Name = "btnEliminarOrden";
            btnEliminarOrden.Size = new Size(120, 28);
            btnEliminarOrden.TabIndex = 4;
            btnEliminarOrden.Text = "Eliminar orden";
            btnEliminarOrden.Visible = false;
            btnEliminarOrden.Click += btnEliminarOrden_Click;
            //
            // SeguimientoOrdenControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnEditarOrden);
            Controls.Add(btnEliminarOrden);
            Controls.Add(panelFormulario);
            Controls.Add(breadcrumb);
            Controls.Add(lblEncabezado);
            Name = "SeguimientoOrdenControl";
            Size = new Size(680, 460);
            ResumeLayout(false);
        }
    }
}
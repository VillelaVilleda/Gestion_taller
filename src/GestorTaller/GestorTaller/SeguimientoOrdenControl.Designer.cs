namespace GestorTaller
{
    partial class SeguimientoOrdenControl
    {
        private System.ComponentModel.IContainer components = null;

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
            breadcrumb = new SeguimientoBreadcrumbControl();
            panelFormulario = new Panel();
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
            // SeguimientoOrdenControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelFormulario);
            Controls.Add(breadcrumb);
            Name = "SeguimientoOrdenControl";
            Size = new Size(680, 460);
            ResumeLayout(false);
        }
    }
}
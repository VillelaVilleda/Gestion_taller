namespace GestorTaller
{
    partial class SeguimientoBreadcrumbControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel flowPasos;
        private System.Windows.Forms.Button btnAtras;
        private System.Windows.Forms.Button btnSiguiente;

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
            flowPasos = new FlowLayoutPanel();
            btnAtras = new Button();
            btnSiguiente = new Button();
            SuspendLayout();
            // 
            // flowPasos
            // 
            flowPasos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowPasos.AutoScroll = true;
            flowPasos.Location = new Point(0, 0);
            flowPasos.Name = "flowPasos";
            flowPasos.Size = new Size(680, 55);
            flowPasos.TabIndex = 0;
            flowPasos.WrapContents = false;
            // 
            // btnAtras
            // 
            btnAtras.Location = new Point(0, 65);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(100, 32);
            btnAtras.TabIndex = 1;
            btnAtras.Text = "Atras";
            btnAtras.Click += btnAtras_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSiguiente.Location = new Point(580, 65);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(100, 32);
            btnSiguiente.TabIndex = 2;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // SeguimientoBreadcrumbControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flowPasos);
            Controls.Add(btnAtras);
            Controls.Add(btnSiguiente);
            Name = "SeguimientoBreadcrumbControl";
            Size = new Size(680, 110);
            ResumeLayout(false);
        }
    }
}
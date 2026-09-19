namespace GestorTaller
{
    partial class EntregaResumenControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelScroll;
        private System.Windows.Forms.FlowLayoutPanel flowResumen;

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
            panelScroll = new Panel();
            flowResumen = new FlowLayoutPanel();
            panelScroll.SuspendLayout();
            SuspendLayout();
            //
            // panelScroll
            //
            panelScroll.AutoScroll = true;
            panelScroll.Controls.Add(flowResumen);
            panelScroll.Dock = DockStyle.Fill;
            panelScroll.Location = new Point(0, 0);
            panelScroll.Name = "panelScroll";
            panelScroll.Size = new Size(400, 400);
            //
            // flowResumen
            //
            flowResumen.AutoSize = true;
            flowResumen.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowResumen.Dock = DockStyle.Top;
            flowResumen.FlowDirection = FlowDirection.TopDown;
            flowResumen.Location = new Point(0, 0);
            flowResumen.Name = "flowResumen";
            flowResumen.Padding = new Padding(20, 15, 20, 15);
            flowResumen.Size = new Size(400, 10);
            flowResumen.WrapContents = false;
            //
            // EntregaResumenControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelScroll);
            Name = "EntregaResumenControl";
            Size = new Size(400, 400);
            panelScroll.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}

namespace GestorTaller
{
    partial class SeguimientoOrdenControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblInfo;

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
            lblInfo = new Label();
            SuspendLayout();
            // 
            // lblInfo
            // 
            lblInfo.Dock = DockStyle.Fill;
            lblInfo.Font = new Font("Segoe UI", 12F);
            lblInfo.Location = new Point(0, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(680, 460);
            lblInfo.TabIndex = 0;
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblInfo.Click += lblInfo_Click;
            // 
            // SeguimientoOrdenControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblInfo);
            Name = "SeguimientoOrdenControl";
            Size = new Size(680, 460);
            ResumeLayout(false);
        }
    }
}
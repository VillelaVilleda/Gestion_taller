namespace GestorTaller
{
    partial class SeguimientoBreadcrumbControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.FlowLayoutPanel flowPasos;
        private BotonTema btnAtras;
        private BotonTema btnSiguiente;

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
            flowPasos = new FlowLayoutPanel();
            btnAtras = new BotonTema();
            btnSiguiente = new BotonTema();
            layout.SuspendLayout();
            SuspendLayout();
            //
            // layout
            //
            layout.ColumnCount = 3;
            layout.ColumnStyles.Add(new ColumnStyle());
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle());
            layout.Controls.Add(flowPasos, 0, 0);
            layout.Controls.Add(btnAtras, 0, 1);
            layout.Controls.Add(btnSiguiente, 2, 1);
            layout.SetColumnSpan(flowPasos, 3);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.RowCount = 2;
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            layout.Size = new Size(680, 110);
            layout.TabIndex = 0;
            //
            // flowPasos
            //
            flowPasos.AutoScroll = true;
            flowPasos.Dock = DockStyle.Fill;
            flowPasos.Margin = new Padding(0);
            flowPasos.Name = "flowPasos";
            flowPasos.Size = new Size(680, 66);
            flowPasos.TabIndex = 0;
            flowPasos.WrapContents = false;
            //
            // btnAtras
            //
            btnAtras.Anchor = AnchorStyles.Left;
            btnAtras.Margin = new Padding(0);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(130, 36);
            btnAtras.TabIndex = 1;
            btnAtras.Text = "Paso anterior";
            btnAtras.Click += btnAtras_Click;
            //
            // btnSiguiente
            //
            btnSiguiente.Anchor = AnchorStyles.Right;
            btnSiguiente.Margin = new Padding(0);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(130, 36);
            btnSiguiente.TabIndex = 2;
            btnSiguiente.Text = "Paso siguiente";
            btnSiguiente.Click += btnSiguiente_Click;
            //
            // SeguimientoBreadcrumbControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "SeguimientoBreadcrumbControl";
            Size = new Size(680, 110);
            layout.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}

namespace GestorTaller
{
    partial class CrearOrdenControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblTitulo;
        private AvisoControl avisoMensaje;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TableLayoutPanel filaCliente;
        private GestorTaller.ComboBoxFiltrable cmbCliente;
        private BotonTema btnNuevoCliente;
        private System.Windows.Forms.Label lblObjeto;
        private System.Windows.Forms.TextBox txtObjeto;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.NumericUpDown numCostoDiagnostico;
        private BotonTema btnRegistrar;

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
            avisoMensaje = new AvisoControl();
            lblCliente = new Label();
            filaCliente = new TableLayoutPanel();
            cmbCliente = new ComboBoxFiltrable();
            btnNuevoCliente = new BotonTema();
            lblObjeto = new Label();
            txtObjeto = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblCosto = new Label();
            numCostoDiagnostico = new NumericUpDown();
            btnRegistrar = new BotonTema();
            ((System.ComponentModel.ISupportInitialize)numCostoDiagnostico).BeginInit();
            layout.SuspendLayout();
            filaCliente.SuspendLayout();
            SuspendLayout();
            //
            // layout
            //
            layout.AutoScroll = true;
            layout.ColumnCount = 1;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(lblTitulo, 0, 0);
            layout.Controls.Add(avisoMensaje, 0, 1);
            layout.Controls.Add(lblCliente, 0, 2);
            layout.Controls.Add(filaCliente, 0, 3);
            layout.Controls.Add(lblObjeto, 0, 4);
            layout.Controls.Add(txtObjeto, 0, 5);
            layout.Controls.Add(lblDescripcion, 0, 6);
            layout.Controls.Add(txtDescripcion, 0, 7);
            layout.Controls.Add(lblCosto, 0, 8);
            layout.Controls.Add(numCostoDiagnostico, 0, 9);
            layout.Controls.Add(btnRegistrar, 0, 10);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Name = "layout";
            layout.Padding = new Padding(24);
            layout.RowCount = 12;
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle());
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.Size = new Size(680, 560);
            layout.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Margin = new Padding(0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(286, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Nueva orden de trabajo";
            //
            // avisoMensaje
            //
            avisoMensaje.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            avisoMensaje.Margin = new Padding(0, 12, 0, 0);
            avisoMensaje.MaximumSize = new Size(640, 0);
            avisoMensaje.Name = "avisoMensaje";
            avisoMensaje.Size = new Size(632, 40);
            avisoMensaje.TabIndex = 1;
            //
            // lblCliente
            //
            lblCliente.AutoSize = true;
            lblCliente.Margin = new Padding(0, 12, 0, 4);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(55, 20);
            lblCliente.TabIndex = 2;
            lblCliente.Text = "Cliente";
            //
            // filaCliente
            //
            filaCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filaCliente.AutoSize = true;
            filaCliente.ColumnCount = 2;
            filaCliente.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filaCliente.ColumnStyles.Add(new ColumnStyle());
            filaCliente.Controls.Add(cmbCliente, 0, 0);
            filaCliente.Controls.Add(btnNuevoCliente, 1, 0);
            filaCliente.Margin = new Padding(0);
            filaCliente.MaximumSize = new Size(640, 0);
            filaCliente.Name = "filaCliente";
            filaCliente.RowCount = 1;
            filaCliente.RowStyles.Add(new RowStyle());
            filaCliente.Size = new Size(632, 36);
            filaCliente.TabIndex = 3;
            //
            // cmbCliente
            //
            cmbCliente.Dock = DockStyle.Fill;
            cmbCliente.Margin = new Padding(0, 0, 8, 0);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(466, 28);
            cmbCliente.TabIndex = 0;
            //
            // btnNuevoCliente
            //
            btnNuevoCliente.Anchor = AnchorStyles.Right;
            btnNuevoCliente.Margin = new Padding(0);
            btnNuevoCliente.Name = "btnNuevoCliente";
            btnNuevoCliente.NombreIcono = "plus";
            btnNuevoCliente.Size = new Size(158, 36);
            btnNuevoCliente.TabIndex = 1;
            btnNuevoCliente.Text = "Nuevo cliente";
            btnNuevoCliente.Click += btnNuevoCliente_Click;
            //
            // lblObjeto
            //
            lblObjeto.AutoSize = true;
            lblObjeto.Margin = new Padding(0, 16, 0, 4);
            lblObjeto.Name = "lblObjeto";
            lblObjeto.Size = new Size(160, 20);
            lblObjeto.TabIndex = 4;
            lblObjeto.Text = "Descripción del objeto";
            //
            // txtObjeto
            //
            txtObjeto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtObjeto.Margin = new Padding(0);
            txtObjeto.MaximumSize = new Size(640, 0);
            txtObjeto.Name = "txtObjeto";
            txtObjeto.Size = new Size(632, 27);
            txtObjeto.TabIndex = 5;
            //
            // lblDescripcion
            //
            lblDescripcion.AutoSize = true;
            lblDescripcion.Margin = new Padding(0, 16, 0, 4);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(181, 20);
            lblDescripcion.TabIndex = 6;
            lblDescripcion.Text = "Descripción del problema";
            //
            // txtDescripcion
            //
            txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescripcion.Margin = new Padding(0);
            txtDescripcion.MaximumSize = new Size(640, 0);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(632, 90);
            txtDescripcion.TabIndex = 7;
            //
            // lblCosto
            //
            lblCosto.AutoSize = true;
            lblCosto.Margin = new Padding(0, 16, 0, 4);
            lblCosto.Name = "lblCosto";
            lblCosto.Size = new Size(150, 20);
            lblCosto.TabIndex = 8;
            lblCosto.Text = "Costo de diagnóstico (Q)";
            //
            // numCostoDiagnostico
            //
            numCostoDiagnostico.DecimalPlaces = 2;
            numCostoDiagnostico.Margin = new Padding(0);
            numCostoDiagnostico.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCostoDiagnostico.Name = "numCostoDiagnostico";
            numCostoDiagnostico.Size = new Size(160, 27);
            numCostoDiagnostico.TabIndex = 9;
            numCostoDiagnostico.ThousandsSeparator = true;
            //
            // btnRegistrar
            //
            btnRegistrar.Margin = new Padding(0, 24, 0, 0);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(170, 36);
            btnRegistrar.TabIndex = 10;
            btnRegistrar.Text = "Registrar orden";
            btnRegistrar.Variante = VarianteBoton.Primario;
            btnRegistrar.Click += btnRegistrar_Click;
            //
            // CrearOrdenControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layout);
            Name = "CrearOrdenControl";
            Size = new Size(680, 560);
            Load += CrearOrdenControl_Load;
            ((System.ComponentModel.ISupportInitialize)numCostoDiagnostico).EndInit();
            layout.ResumeLayout(false);
            layout.PerformLayout();
            filaCliente.ResumeLayout(false);
            filaCliente.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}

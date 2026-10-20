namespace GestorTaller
{
    partial class EmpleadosControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.CheckBox chkAdministrador;
        private System.Windows.Forms.Button btnCrearEmpleado;
        private System.Windows.Forms.Label lblRestablecer;
        private GestorTaller.ComboBoxFiltrable cmbEmpleado;
        private System.Windows.Forms.Button btnRestablecer;
        private System.Windows.Forms.Label lblMensaje;
        private GestorTaller.TablaListadoControl tablaEmpleados;

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
            lblTitulo = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            chkAdministrador = new CheckBox();
            btnCrearEmpleado = new Button();
            lblRestablecer = new Label();
            cmbEmpleado = new ComboBoxFiltrable();
            btnRestablecer = new Button();
            lblMensaje = new Label();
            tablaEmpleados = new TablaListadoControl();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(160, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Empleados";
            //
            // lblNombre
            //
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(20, 55);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(120, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nuevo empleado";
            //
            // txtNombre
            //
            txtNombre.Location = new Point(20, 77);
            txtNombre.MaxLength = 150;
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Nombre completo";
            txtNombre.Size = new Size(250, 27);
            txtNombre.TabIndex = 2;
            //
            // chkAdministrador
            //
            chkAdministrador.AutoSize = true;
            chkAdministrador.Location = new Point(285, 79);
            chkAdministrador.Name = "chkAdministrador";
            chkAdministrador.Size = new Size(120, 24);
            chkAdministrador.TabIndex = 3;
            chkAdministrador.Text = "Administrador";
            //
            // btnCrearEmpleado
            //
            btnCrearEmpleado.Location = new Point(420, 75);
            btnCrearEmpleado.Name = "btnCrearEmpleado";
            btnCrearEmpleado.Size = new Size(150, 30);
            btnCrearEmpleado.TabIndex = 4;
            btnCrearEmpleado.Text = "Crear empleado";
            btnCrearEmpleado.Click += btnCrearEmpleado_Click;
            //
            // lblRestablecer
            //
            lblRestablecer.AutoSize = true;
            lblRestablecer.Location = new Point(20, 118);
            lblRestablecer.Name = "lblRestablecer";
            lblRestablecer.Size = new Size(200, 20);
            lblRestablecer.TabIndex = 5;
            lblRestablecer.Text = "Restablecer contrasena de";
            //
            // cmbEmpleado
            //
            cmbEmpleado.Location = new Point(20, 140);
            cmbEmpleado.Name = "cmbEmpleado";
            cmbEmpleado.Size = new Size(250, 28);
            cmbEmpleado.TabIndex = 6;
            //
            // btnRestablecer
            //
            btnRestablecer.Location = new Point(285, 138);
            btnRestablecer.Name = "btnRestablecer";
            btnRestablecer.Size = new Size(180, 30);
            btnRestablecer.TabIndex = 7;
            btnRestablecer.Text = "Restablecer contrasena";
            btnRestablecer.Click += btnRestablecer_Click;
            //
            // lblMensaje
            //
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 175);
            lblMensaje.MaximumSize = new Size(640, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 8;
            //
            // tablaEmpleados
            //
            tablaEmpleados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tablaEmpleados.Location = new Point(0, 205);
            tablaEmpleados.Name = "tablaEmpleados";
            tablaEmpleados.Size = new Size(680, 255);
            tablaEmpleados.TabIndex = 9;
            //
            // EmpleadosControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(chkAdministrador);
            Controls.Add(btnCrearEmpleado);
            Controls.Add(lblRestablecer);
            Controls.Add(cmbEmpleado);
            Controls.Add(btnRestablecer);
            Controls.Add(lblMensaje);
            Controls.Add(tablaEmpleados);
            Name = "EmpleadosControl";
            Size = new Size(680, 460);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

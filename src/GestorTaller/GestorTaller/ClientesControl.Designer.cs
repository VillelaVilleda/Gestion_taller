namespace GestorTaller
{
    partial class ClientesControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelEdicion;
        private System.Windows.Forms.Label lblEditar;
        private GestorTaller.ComboBoxFiltrable cmbCliente;
        private System.Windows.Forms.TextBox txtEditarNombre;
        private System.Windows.Forms.TextBox txtEditarTelefono;
        private System.Windows.Forms.TextBox txtEditarCorreo;
        private System.Windows.Forms.TextBox txtEditarDireccion;
        private System.Windows.Forms.Button btnGuardarCambios;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblMensaje;
        private GestorTaller.TablaListadoControl tablaClientes;

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
            panelEdicion = new Panel();
            lblEditar = new Label();
            cmbCliente = new ComboBoxFiltrable();
            txtEditarNombre = new TextBox();
            txtEditarTelefono = new TextBox();
            txtEditarCorreo = new TextBox();
            txtEditarDireccion = new TextBox();
            btnGuardarCambios = new Button();
            btnEliminar = new Button();
            lblMensaje = new Label();
            tablaClientes = new TablaListadoControl();
            panelEdicion.SuspendLayout();
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
            lblTitulo.Text = "Clientes";
            //
            // panelEdicion
            //
            panelEdicion.Controls.Add(lblEditar);
            panelEdicion.Controls.Add(cmbCliente);
            panelEdicion.Controls.Add(txtEditarNombre);
            panelEdicion.Controls.Add(txtEditarTelefono);
            panelEdicion.Controls.Add(txtEditarCorreo);
            panelEdicion.Controls.Add(txtEditarDireccion);
            panelEdicion.Controls.Add(btnGuardarCambios);
            panelEdicion.Controls.Add(btnEliminar);
            panelEdicion.Location = new Point(0, 50);
            panelEdicion.Name = "panelEdicion";
            panelEdicion.Size = new Size(680, 95);
            panelEdicion.TabIndex = 1;
            //
            // lblEditar
            //
            lblEditar.AutoSize = true;
            lblEditar.Location = new Point(20, 2);
            lblEditar.Name = "lblEditar";
            lblEditar.Size = new Size(200, 20);
            lblEditar.TabIndex = 0;
            lblEditar.Text = "Editar o eliminar un cliente";
            //
            // cmbCliente
            //
            cmbCliente.Location = new Point(20, 25);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(160, 28);
            cmbCliente.TabIndex = 1;
            //
            // txtEditarNombre
            //
            txtEditarNombre.Location = new Point(190, 26);
            txtEditarNombre.MaxLength = 150;
            txtEditarNombre.Name = "txtEditarNombre";
            txtEditarNombre.PlaceholderText = "Nombre";
            txtEditarNombre.Size = new Size(130, 27);
            txtEditarNombre.TabIndex = 2;
            //
            // txtEditarTelefono
            //
            txtEditarTelefono.Location = new Point(330, 26);
            txtEditarTelefono.MaxLength = 20;
            txtEditarTelefono.Name = "txtEditarTelefono";
            txtEditarTelefono.PlaceholderText = "Telefono";
            txtEditarTelefono.Size = new Size(100, 27);
            txtEditarTelefono.TabIndex = 3;
            //
            // txtEditarCorreo
            //
            txtEditarCorreo.Location = new Point(440, 26);
            txtEditarCorreo.MaxLength = 150;
            txtEditarCorreo.Name = "txtEditarCorreo";
            txtEditarCorreo.PlaceholderText = "Correo";
            txtEditarCorreo.Size = new Size(120, 27);
            txtEditarCorreo.TabIndex = 4;
            //
            // txtEditarDireccion
            //
            txtEditarDireccion.Location = new Point(570, 26);
            txtEditarDireccion.MaxLength = 300;
            txtEditarDireccion.Name = "txtEditarDireccion";
            txtEditarDireccion.PlaceholderText = "Direccion";
            txtEditarDireccion.Size = new Size(100, 27);
            txtEditarDireccion.TabIndex = 5;
            //
            // btnGuardarCambios
            //
            btnGuardarCambios.Location = new Point(20, 60);
            btnGuardarCambios.Name = "btnGuardarCambios";
            btnGuardarCambios.Size = new Size(160, 30);
            btnGuardarCambios.TabIndex = 6;
            btnGuardarCambios.Text = "Guardar cambios";
            btnGuardarCambios.Click += btnGuardarCambios_Click;
            //
            // btnEliminar
            //
            btnEliminar.Location = new Point(190, 60);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(120, 30);
            btnEliminar.TabIndex = 7;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            //
            // lblMensaje
            //
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(20, 150);
            lblMensaje.MaximumSize = new Size(640, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 2;
            //
            // tablaClientes
            //
            tablaClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tablaClientes.Location = new Point(0, 180);
            tablaClientes.Name = "tablaClientes";
            tablaClientes.Size = new Size(680, 280);
            tablaClientes.TabIndex = 3;
            //
            // ClientesControl
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTitulo);
            Controls.Add(panelEdicion);
            Controls.Add(lblMensaje);
            Controls.Add(tablaClientes);
            Name = "ClientesControl";
            Size = new Size(680, 460);
            panelEdicion.ResumeLayout(false);
            panelEdicion.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

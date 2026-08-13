namespace GestorTaller
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Button btnCrearOrden;
        private System.Windows.Forms.Button btnOrdenesRegistradas;
        private System.Windows.Forms.Button btnRepuestos;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnEmpleados;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnSalir;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelMenu = new Panel();
            btnCrearOrden = new Button();
            btnOrdenesRegistradas = new Button();
            btnRepuestos = new Button();
            btnClientes = new Button();
            btnInventario = new Button();
            btnEmpleados = new Button();
            btnCerrarSesion = new Button();
            btnSalir = new Button();
            panelContenido = new Panel();
            panelMenu.SuspendLayout();
            SuspendLayout();
            //
            // panelMenu
            //
            panelMenu.BackColor = Color.WhiteSmoke;
            panelMenu.Controls.Add(btnCrearOrden);
            panelMenu.Controls.Add(btnOrdenesRegistradas);
            panelMenu.Controls.Add(btnRepuestos);
            panelMenu.Controls.Add(btnClientes);
            panelMenu.Controls.Add(btnInventario);
            panelMenu.Controls.Add(btnEmpleados);
            panelMenu.Controls.Add(btnCerrarSesion);
            panelMenu.Controls.Add(btnSalir);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(200, 600);
            panelMenu.TabIndex = 0;
            //
            // btnCrearOrden
            //
            btnCrearOrden.Location = new Point(10, 20);
            btnCrearOrden.Name = "btnCrearOrden";
            btnCrearOrden.Size = new Size(180, 40);
            btnCrearOrden.TabIndex = 0;
            btnCrearOrden.Text = "Nueva orden";
            btnCrearOrden.Click += btnCrearOrden_Click;
            //
            // btnOrdenesRegistradas
            //
            btnOrdenesRegistradas.Location = new Point(10, 70);
            btnOrdenesRegistradas.Name = "btnOrdenesRegistradas";
            btnOrdenesRegistradas.Size = new Size(180, 40);
            btnOrdenesRegistradas.TabIndex = 1;
            btnOrdenesRegistradas.Text = "Ordenes";
            btnOrdenesRegistradas.Click += btnOrdenesRegistradas_Click;
            //
            // btnRepuestos
            //
            btnRepuestos.Location = new Point(10, 130);
            btnRepuestos.Name = "btnRepuestos";
            btnRepuestos.Size = new Size(180, 40);
            btnRepuestos.TabIndex = 2;
            btnRepuestos.Text = "Repuestos";
            btnRepuestos.Click += btnRepuestos_Click;
            //
            // btnClientes
            //
            btnClientes.Location = new Point(10, 180);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(180, 40);
            btnClientes.TabIndex = 3;
            btnClientes.Text = "Clientes";
            btnClientes.Click += btnClientes_Click;
            //
            // btnInventario
            //
            btnInventario.Location = new Point(10, 230);
            btnInventario.Name = "btnInventario";
            btnInventario.Size = new Size(180, 40);
            btnInventario.TabIndex = 4;
            btnInventario.Text = "Inventario";
            btnInventario.Click += btnInventario_Click;
            //
            // btnEmpleados
            //
            btnEmpleados.Location = new Point(10, 280);
            btnEmpleados.Name = "btnEmpleados";
            btnEmpleados.Size = new Size(180, 40);
            btnEmpleados.TabIndex = 5;
            btnEmpleados.Text = "Empleados";
            btnEmpleados.Click += btnEmpleados_Click;
            //
            // btnCerrarSesion
            //
            btnCerrarSesion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCerrarSesion.Location = new Point(10, 520);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(180, 35);
            btnCerrarSesion.TabIndex = 6;
            btnCerrarSesion.Text = "Cerrar sesion";
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            //
            // btnSalir
            //
            btnSalir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSalir.Location = new Point(10, 560);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(180, 35);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            //
            // panelContenido
            //
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(200, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(700, 600);
            panelContenido.TabIndex = 1;
            //
            // FormPrincipal
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 600);
            Controls.Add(panelContenido);
            Controls.Add(panelMenu);
            MinimumSize = new Size(700, 500);
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GestorTaller";
            Load += FormPrincipal_Load;
            FormClosed += FormPrincipal_FormClosed;
            panelMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}

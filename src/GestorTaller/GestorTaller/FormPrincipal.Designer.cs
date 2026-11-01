namespace GestorTaller
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.TableLayoutPanel layoutMenu;
        private System.Windows.Forms.Panel panelMarca;
        private InsigniaIcono insigniaMarca;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblSubmarca;
        private System.Windows.Forms.FlowLayoutPanel flowOpciones;
        private BotonMenu btnCrearOrden;
        private BotonMenu btnOrdenesRegistradas;
        private BotonMenu btnClientes;
        private BotonMenu btnRepuestos;
        private BotonMenu btnEmpleados;
        private System.Windows.Forms.TableLayoutPanel layoutUsuario;
        private System.Windows.Forms.Panel separadorUsuario;
        private System.Windows.Forms.TableLayoutPanel filaUsuario;
        private InsigniaIcono insigniaUsuario;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.Label lblRolUsuario;
        private BotonMenu btnCambiarContrasena;
        private BotonMenu btnCerrarSesion;
        private System.Windows.Forms.Panel panelContenido;

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
            layoutMenu = new TableLayoutPanel();
            panelMarca = new Panel();
            insigniaMarca = new InsigniaIcono();
            lblMarca = new Label();
            lblSubmarca = new Label();
            flowOpciones = new FlowLayoutPanel();
            btnCrearOrden = new BotonMenu();
            btnOrdenesRegistradas = new BotonMenu();
            btnClientes = new BotonMenu();
            btnRepuestos = new BotonMenu();
            btnEmpleados = new BotonMenu();
            layoutUsuario = new TableLayoutPanel();
            separadorUsuario = new Panel();
            filaUsuario = new TableLayoutPanel();
            insigniaUsuario = new InsigniaIcono();
            lblNombreUsuario = new Label();
            lblRolUsuario = new Label();
            btnCambiarContrasena = new BotonMenu();
            btnCerrarSesion = new BotonMenu();
            panelContenido = new Panel();
            panelMenu.SuspendLayout();
            layoutMenu.SuspendLayout();
            panelMarca.SuspendLayout();
            flowOpciones.SuspendLayout();
            layoutUsuario.SuspendLayout();
            filaUsuario.SuspendLayout();
            SuspendLayout();
            //
            // panelMenu
            //
            panelMenu.Controls.Add(layoutMenu);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(236, 700);
            panelMenu.TabIndex = 0;
            //
            // layoutMenu
            //
            layoutMenu.ColumnCount = 1;
            layoutMenu.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMenu.Controls.Add(panelMarca, 0, 0);
            layoutMenu.Controls.Add(flowOpciones, 0, 1);
            layoutMenu.Controls.Add(layoutUsuario, 0, 2);
            layoutMenu.Dock = DockStyle.Fill;
            layoutMenu.Location = new Point(0, 0);
            layoutMenu.Margin = new Padding(0);
            layoutMenu.Name = "layoutMenu";
            layoutMenu.RowCount = 3;
            layoutMenu.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
            layoutMenu.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutMenu.RowStyles.Add(new RowStyle());
            layoutMenu.Size = new Size(236, 700);
            layoutMenu.TabIndex = 0;
            //
            // panelMarca
            //
            panelMarca.Controls.Add(insigniaMarca);
            panelMarca.Controls.Add(lblMarca);
            panelMarca.Controls.Add(lblSubmarca);
            panelMarca.Dock = DockStyle.Fill;
            panelMarca.Margin = new Padding(0);
            panelMarca.Name = "panelMarca";
            panelMarca.Size = new Size(236, 96);
            panelMarca.TabIndex = 0;
            //
            // insigniaMarca
            //
            insigniaMarca.Location = new Point(16, 22);
            insigniaMarca.Name = "insigniaMarca";
            insigniaMarca.NombreIcono = "wrench";
            insigniaMarca.Size = new Size(52, 52);
            insigniaMarca.TabIndex = 0;
            //
            // lblMarca
            //
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblMarca.Location = new Point(78, 26);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(116, 28);
            lblMarca.TabIndex = 1;
            lblMarca.Text = "GestorTaller";
            //
            // lblSubmarca
            //
            lblSubmarca.AutoSize = true;
            lblSubmarca.Font = new Font("Segoe UI", 8F);
            lblSubmarca.Location = new Point(79, 52);
            lblSubmarca.Name = "lblSubmarca";
            lblSubmarca.Size = new Size(130, 19);
            lblSubmarca.TabIndex = 2;
            lblSubmarca.Text = "Taller y servicio técnico";
            //
            // flowOpciones
            //
            flowOpciones.Controls.Add(btnCrearOrden);
            flowOpciones.Controls.Add(btnOrdenesRegistradas);
            flowOpciones.Controls.Add(btnClientes);
            flowOpciones.Controls.Add(btnRepuestos);
            flowOpciones.Controls.Add(btnEmpleados);
            flowOpciones.Dock = DockStyle.Fill;
            flowOpciones.FlowDirection = FlowDirection.TopDown;
            flowOpciones.Margin = new Padding(0);
            flowOpciones.Name = "flowOpciones";
            flowOpciones.Padding = new Padding(12, 8, 12, 0);
            flowOpciones.Size = new Size(236, 508);
            flowOpciones.TabIndex = 1;
            flowOpciones.WrapContents = false;
            //
            // btnCrearOrden
            //
            btnCrearOrden.Font = new Font("Segoe UI", 10F);
            btnCrearOrden.Margin = new Padding(0, 0, 0, 4);
            btnCrearOrden.Name = "btnCrearOrden";
            btnCrearOrden.NombreIcono = "plus";
            btnCrearOrden.Size = new Size(212, 44);
            btnCrearOrden.TabIndex = 0;
            btnCrearOrden.Text = "Nueva orden";
            btnCrearOrden.Click += btnCrearOrden_Click;
            //
            // btnOrdenesRegistradas
            //
            btnOrdenesRegistradas.Font = new Font("Segoe UI", 10F);
            btnOrdenesRegistradas.Margin = new Padding(0, 0, 0, 4);
            btnOrdenesRegistradas.Name = "btnOrdenesRegistradas";
            btnOrdenesRegistradas.NombreIcono = "clipboard-list";
            btnOrdenesRegistradas.Size = new Size(212, 44);
            btnOrdenesRegistradas.TabIndex = 1;
            btnOrdenesRegistradas.Text = "Órdenes registradas";
            btnOrdenesRegistradas.Click += btnOrdenesRegistradas_Click;
            //
            // btnClientes
            //
            btnClientes.Font = new Font("Segoe UI", 10F);
            btnClientes.Margin = new Padding(0, 0, 0, 4);
            btnClientes.Name = "btnClientes";
            btnClientes.NombreIcono = "users";
            btnClientes.Size = new Size(212, 44);
            btnClientes.TabIndex = 2;
            btnClientes.Text = "Clientes";
            btnClientes.Click += btnClientes_Click;
            //
            // btnRepuestos
            //
            btnRepuestos.Font = new Font("Segoe UI", 10F);
            btnRepuestos.Margin = new Padding(0, 0, 0, 4);
            btnRepuestos.Name = "btnRepuestos";
            btnRepuestos.NombreIcono = "wrench";
            btnRepuestos.Size = new Size(212, 44);
            btnRepuestos.TabIndex = 3;
            btnRepuestos.Text = "Repuestos";
            btnRepuestos.Click += btnRepuestos_Click;
            //
            // btnEmpleados
            //
            btnEmpleados.Font = new Font("Segoe UI", 10F);
            btnEmpleados.Margin = new Padding(0, 0, 0, 4);
            btnEmpleados.Name = "btnEmpleados";
            btnEmpleados.NombreIcono = "user";
            btnEmpleados.Size = new Size(212, 44);
            btnEmpleados.TabIndex = 4;
            btnEmpleados.Text = "Empleados";
            btnEmpleados.Click += btnEmpleados_Click;
            //
            // layoutUsuario
            //
            layoutUsuario.AutoSize = true;
            layoutUsuario.ColumnCount = 1;
            layoutUsuario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutUsuario.Controls.Add(separadorUsuario, 0, 0);
            layoutUsuario.Controls.Add(filaUsuario, 0, 1);
            layoutUsuario.Controls.Add(btnCambiarContrasena, 0, 2);
            layoutUsuario.Controls.Add(btnCerrarSesion, 0, 3);
            layoutUsuario.Dock = DockStyle.Fill;
            layoutUsuario.Margin = new Padding(0);
            layoutUsuario.Name = "layoutUsuario";
            layoutUsuario.Padding = new Padding(12, 0, 12, 12);
            layoutUsuario.RowCount = 4;
            layoutUsuario.RowStyles.Add(new RowStyle());
            layoutUsuario.RowStyles.Add(new RowStyle());
            layoutUsuario.RowStyles.Add(new RowStyle());
            layoutUsuario.RowStyles.Add(new RowStyle());
            layoutUsuario.Size = new Size(236, 190);
            layoutUsuario.TabIndex = 2;
            //
            // separadorUsuario
            //
            separadorUsuario.Dock = DockStyle.Top;
            separadorUsuario.Height = 1;
            separadorUsuario.Margin = new Padding(4, 0, 4, 14);
            separadorUsuario.Name = "separadorUsuario";
            separadorUsuario.Size = new Size(204, 1);
            separadorUsuario.TabIndex = 0;
            //
            // filaUsuario
            //
            filaUsuario.ColumnCount = 2;
            filaUsuario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 56F));
            filaUsuario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            filaUsuario.Controls.Add(insigniaUsuario, 0, 0);
            filaUsuario.Controls.Add(lblNombreUsuario, 1, 0);
            filaUsuario.Controls.Add(lblRolUsuario, 1, 1);
            filaUsuario.Dock = DockStyle.Fill;
            filaUsuario.Margin = new Padding(0, 0, 0, 8);
            filaUsuario.Name = "filaUsuario";
            filaUsuario.RowCount = 2;
            filaUsuario.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            filaUsuario.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            filaUsuario.Size = new Size(212, 52);
            filaUsuario.TabIndex = 1;
            //
            // insigniaUsuario
            //
            insigniaUsuario.Circular = true;
            insigniaUsuario.Margin = new Padding(4, 2, 8, 2);
            insigniaUsuario.Name = "insigniaUsuario";
            insigniaUsuario.NombreIcono = "user";
            insigniaUsuario.Size = new Size(44, 48);
            insigniaUsuario.TabIndex = 0;
            filaUsuario.SetRowSpan(insigniaUsuario, 2);
            //
            // lblNombreUsuario
            //
            lblNombreUsuario.AutoEllipsis = true;
            lblNombreUsuario.Dock = DockStyle.Fill;
            lblNombreUsuario.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombreUsuario.Margin = new Padding(0);
            lblNombreUsuario.Name = "lblNombreUsuario";
            lblNombreUsuario.Size = new Size(156, 26);
            lblNombreUsuario.TabIndex = 1;
            lblNombreUsuario.TextAlign = ContentAlignment.BottomLeft;
            //
            // lblRolUsuario
            //
            lblRolUsuario.AutoEllipsis = true;
            lblRolUsuario.Dock = DockStyle.Fill;
            lblRolUsuario.Font = new Font("Segoe UI", 8F);
            lblRolUsuario.Margin = new Padding(0);
            lblRolUsuario.Name = "lblRolUsuario";
            lblRolUsuario.Size = new Size(156, 26);
            lblRolUsuario.TabIndex = 2;
            lblRolUsuario.TextAlign = ContentAlignment.TopLeft;
            //
            // btnCambiarContrasena
            //
            btnCambiarContrasena.Font = new Font("Segoe UI", 9F);
            btnCambiarContrasena.Margin = new Padding(0, 0, 0, 2);
            btnCambiarContrasena.Name = "btnCambiarContrasena";
            btnCambiarContrasena.NombreIcono = "key-round";
            btnCambiarContrasena.Size = new Size(212, 38);
            btnCambiarContrasena.TabIndex = 2;
            btnCambiarContrasena.Text = "Cambiar contraseña";
            btnCambiarContrasena.Click += btnCambiarContrasena_Click;
            //
            // btnCerrarSesion
            //
            btnCerrarSesion.Font = new Font("Segoe UI", 9F);
            btnCerrarSesion.Margin = new Padding(0);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.NombreIcono = "log-out";
            btnCerrarSesion.Size = new Size(212, 38);
            btnCerrarSesion.TabIndex = 3;
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            //
            // panelContenido
            //
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(236, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(864, 700);
            panelContenido.TabIndex = 1;
            //
            // FormPrincipal
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 700);
            Controls.Add(panelContenido);
            Controls.Add(panelMenu);
            MinimumSize = new Size(960, 600);
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GestorTaller";
            FormClosed += FormPrincipal_FormClosed;
            Load += FormPrincipal_Load;
            panelMenu.ResumeLayout(false);
            layoutMenu.ResumeLayout(false);
            layoutMenu.PerformLayout();
            panelMarca.ResumeLayout(false);
            panelMarca.PerformLayout();
            flowOpciones.ResumeLayout(false);
            layoutUsuario.ResumeLayout(false);
            layoutUsuario.PerformLayout();
            filaUsuario.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}

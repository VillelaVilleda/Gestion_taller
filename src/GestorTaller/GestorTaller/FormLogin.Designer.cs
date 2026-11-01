namespace GestorTaller
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel layoutPrincipal;
        private System.Windows.Forms.Panel panelMarca;
        private System.Windows.Forms.TableLayoutPanel layoutMarca;
        private InsigniaIcono insigniaLogo;
        private System.Windows.Forms.Label lblTituloMarca;
        private System.Windows.Forms.TableLayoutPanel panelAcceso;
        private TarjetaPanel tarjeta;
        private System.Windows.Forms.TableLayoutPanel layoutTarjeta;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private CampoConIcono campoUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private CampoConIcono campoContrasena;
        private BotonTema btnEntrar;
        private AvisoControl avisoMensaje;
        private System.Windows.Forms.FlowLayoutPanel filaEnlaces;
        private System.Windows.Forms.LinkLabel lnkAyuda;
        private System.Windows.Forms.LinkLabel lnkSalir;

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
            layoutPrincipal = new TableLayoutPanel();
            panelMarca = new Panel();
            layoutMarca = new TableLayoutPanel();
            insigniaLogo = new InsigniaIcono();
            lblTituloMarca = new Label();
            panelAcceso = new TableLayoutPanel();
            tarjeta = new TarjetaPanel();
            layoutTarjeta = new TableLayoutPanel();
            lblTitulo = new Label();
            lblUsuario = new Label();
            campoUsuario = new CampoConIcono();
            lblContrasena = new Label();
            campoContrasena = new CampoConIcono();
            btnEntrar = new BotonTema();
            avisoMensaje = new AvisoControl();
            filaEnlaces = new FlowLayoutPanel();
            lnkAyuda = new LinkLabel();
            lnkSalir = new LinkLabel();
            layoutPrincipal.SuspendLayout();
            panelMarca.SuspendLayout();
            layoutMarca.SuspendLayout();
            panelAcceso.SuspendLayout();
            tarjeta.SuspendLayout();
            layoutTarjeta.SuspendLayout();
            filaEnlaces.SuspendLayout();
            SuspendLayout();
            //
            // layoutPrincipal
            //
            layoutPrincipal.ColumnCount = 2;
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            layoutPrincipal.Controls.Add(panelMarca, 0, 0);
            layoutPrincipal.Controls.Add(panelAcceso, 1, 0);
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.Location = new Point(0, 0);
            layoutPrincipal.Margin = new Padding(0);
            layoutPrincipal.Name = "layoutPrincipal";
            layoutPrincipal.RowCount = 1;
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPrincipal.Size = new Size(1000, 680);
            layoutPrincipal.TabIndex = 0;
            //
            // panelMarca
            //
            panelMarca.Controls.Add(layoutMarca);
            panelMarca.Dock = DockStyle.Fill;
            panelMarca.Margin = new Padding(0);
            panelMarca.Name = "panelMarca";
            panelMarca.Padding = new Padding(56, 56, 40, 48);
            panelMarca.Size = new Size(450, 680);
            panelMarca.TabIndex = 0;
            //
            // layoutMarca
            //
            layoutMarca.ColumnCount = 1;
            layoutMarca.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutMarca.Controls.Add(insigniaLogo, 0, 1);
            layoutMarca.Controls.Add(lblTituloMarca, 0, 2);
            layoutMarca.Dock = DockStyle.Fill;
            layoutMarca.Name = "layoutMarca";
            layoutMarca.RowCount = 4;
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutMarca.RowStyles.Add(new RowStyle());
            layoutMarca.RowStyles.Add(new RowStyle());
            layoutMarca.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            layoutMarca.Size = new Size(354, 576);
            layoutMarca.TabIndex = 0;
            //
            // insigniaLogo
            //
            insigniaLogo.Margin = new Padding(0, 0, 0, 32);
            insigniaLogo.Name = "insigniaLogo";
            insigniaLogo.NombreIcono = "wrench";
            insigniaLogo.Size = new Size(80, 80);
            insigniaLogo.TabIndex = 0;
            //
            // lblTituloMarca
            //
            lblTituloMarca.AutoSize = true;
            lblTituloMarca.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTituloMarca.Margin = new Padding(0);
            lblTituloMarca.Name = "lblTituloMarca";
            lblTituloMarca.Size = new Size(202, 54);
            lblTituloMarca.TabIndex = 1;
            lblTituloMarca.Text = "GestorTaller";
            //
            // panelAcceso
            //
            panelAcceso.ColumnCount = 1;
            panelAcceso.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelAcceso.Controls.Add(tarjeta, 0, 0);
            panelAcceso.Dock = DockStyle.Fill;
            panelAcceso.Margin = new Padding(0);
            panelAcceso.Name = "panelAcceso";
            panelAcceso.Padding = new Padding(24);
            panelAcceso.RowCount = 1;
            panelAcceso.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelAcceso.Size = new Size(550, 680);
            panelAcceso.TabIndex = 1;
            //
            // tarjeta
            //
            tarjeta.Anchor = AnchorStyles.None;
            tarjeta.AutoSize = true;
            tarjeta.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tarjeta.Controls.Add(layoutTarjeta);
            tarjeta.Name = "tarjeta";
            tarjeta.Size = new Size(420, 440);
            tarjeta.TabIndex = 0;
            //
            // layoutTarjeta
            //
            layoutTarjeta.AutoSize = true;
            layoutTarjeta.ColumnCount = 1;
            layoutTarjeta.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 340F));
            layoutTarjeta.Controls.Add(lblTitulo, 0, 0);
            layoutTarjeta.Controls.Add(lblUsuario, 0, 1);
            layoutTarjeta.Controls.Add(campoUsuario, 0, 2);
            layoutTarjeta.Controls.Add(lblContrasena, 0, 3);
            layoutTarjeta.Controls.Add(campoContrasena, 0, 4);
            layoutTarjeta.Controls.Add(btnEntrar, 0, 5);
            layoutTarjeta.Controls.Add(avisoMensaje, 0, 6);
            layoutTarjeta.Controls.Add(filaEnlaces, 0, 7);
            layoutTarjeta.Location = new Point(0, 0);
            layoutTarjeta.Name = "layoutTarjeta";
            layoutTarjeta.Padding = new Padding(40);
            layoutTarjeta.RowCount = 8;
            layoutTarjeta.RowStyles.Add(new RowStyle());
            layoutTarjeta.RowStyles.Add(new RowStyle());
            layoutTarjeta.RowStyles.Add(new RowStyle());
            layoutTarjeta.RowStyles.Add(new RowStyle());
            layoutTarjeta.RowStyles.Add(new RowStyle());
            layoutTarjeta.RowStyles.Add(new RowStyle());
            layoutTarjeta.RowStyles.Add(new RowStyle());
            layoutTarjeta.RowStyles.Add(new RowStyle());
            layoutTarjeta.Size = new Size(420, 440);
            layoutTarjeta.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Margin = new Padding(0, 0, 0, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(160, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Iniciar sesión";
            //
            // lblUsuario
            //
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 8.5F);
            lblUsuario.Margin = new Padding(0, 0, 0, 6);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(62, 20);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "USUARIO";
            //
            // campoUsuario
            //
            campoUsuario.Dock = DockStyle.Fill;
            campoUsuario.Margin = new Padding(0, 0, 0, 16);
            campoUsuario.MaxLength = 50;
            campoUsuario.Name = "campoUsuario";
            campoUsuario.NombreIcono = "user";
            campoUsuario.Size = new Size(340, 44);
            campoUsuario.TabIndex = 0;
            //
            // lblContrasena
            //
            lblContrasena.AutoSize = true;
            lblContrasena.Font = new Font("Segoe UI", 8.5F);
            lblContrasena.Margin = new Padding(0, 0, 0, 6);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(86, 20);
            lblContrasena.TabIndex = 4;
            lblContrasena.Text = "CONTRASEÑA";
            //
            // campoContrasena
            //
            campoContrasena.Dock = DockStyle.Fill;
            campoContrasena.EsContrasena = true;
            campoContrasena.Margin = new Padding(0);
            campoContrasena.MaxLength = 72;
            campoContrasena.Name = "campoContrasena";
            campoContrasena.NombreIcono = "lock";
            campoContrasena.Size = new Size(340, 44);
            campoContrasena.TabIndex = 1;
            //
            // btnEntrar
            //
            btnEntrar.Dock = DockStyle.Fill;
            btnEntrar.Font = new Font("Segoe UI", 11F);
            btnEntrar.Margin = new Padding(0, 28, 0, 0);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.Size = new Size(340, 46);
            btnEntrar.TabIndex = 2;
            btnEntrar.Text = "Entrar";
            btnEntrar.Variante = VarianteBoton.Primario;
            btnEntrar.Click += btnEntrar_Click;
            //
            // avisoMensaje
            //
            avisoMensaje.Dock = DockStyle.Fill;
            avisoMensaje.Margin = new Padding(0, 16, 0, 0);
            avisoMensaje.Name = "avisoMensaje";
            avisoMensaje.Size = new Size(340, 40);
            avisoMensaje.TabIndex = 7;
            //
            // filaEnlaces
            //
            filaEnlaces.Anchor = AnchorStyles.None;
            filaEnlaces.AutoSize = true;
            filaEnlaces.Controls.Add(lnkAyuda);
            filaEnlaces.Controls.Add(lnkSalir);
            filaEnlaces.Margin = new Padding(0, 20, 0, 0);
            filaEnlaces.Name = "filaEnlaces";
            filaEnlaces.Size = new Size(160, 28);
            filaEnlaces.TabIndex = 9;
            filaEnlaces.WrapContents = false;
            //
            // lnkAyuda
            //
            lnkAyuda.AutoSize = true;
            lnkAyuda.Font = new Font("Segoe UI", 9F);
            lnkAyuda.Margin = new Padding(0, 0, 20, 0);
            lnkAyuda.Name = "lnkAyuda";
            lnkAyuda.Size = new Size(50, 25);
            lnkAyuda.TabIndex = 3;
            lnkAyuda.TabStop = true;
            lnkAyuda.Text = "Ayuda";
            lnkAyuda.LinkClicked += lnkAyuda_LinkClicked;
            //
            // lnkSalir
            //
            lnkSalir.AutoSize = true;
            lnkSalir.Font = new Font("Segoe UI", 9F);
            lnkSalir.Margin = new Padding(0);
            lnkSalir.Name = "lnkSalir";
            lnkSalir.Size = new Size(90, 25);
            lnkSalir.TabIndex = 4;
            lnkSalir.TabStop = true;
            lnkSalir.Text = "Salir de la aplicación";
            lnkSalir.LinkClicked += lnkSalir_LinkClicked;
            //
            // FormLogin
            //
            AcceptButton = btnEntrar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 680);
            Controls.Add(layoutPrincipal);
            MinimumSize = new Size(960, 640);
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GestorTaller - Iniciar sesión";
            Load += FormLogin_Load;
            layoutPrincipal.ResumeLayout(false);
            panelMarca.ResumeLayout(false);
            layoutMarca.ResumeLayout(false);
            layoutMarca.PerformLayout();
            panelAcceso.ResumeLayout(false);
            panelAcceso.PerformLayout();
            tarjeta.ResumeLayout(false);
            tarjeta.PerformLayout();
            layoutTarjeta.ResumeLayout(false);
            layoutTarjeta.PerformLayout();
            filaEnlaces.ResumeLayout(false);
            filaEnlaces.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}

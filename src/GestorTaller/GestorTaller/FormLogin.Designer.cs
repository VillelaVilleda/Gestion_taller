namespace GestorTaller
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.LinkLabel lnkAyuda;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private BotonTema btnEntrar;
        private BotonTema btnSalir;
        private AvisoControl avisoMensaje;
        private System.Windows.Forms.Panel panelTarjeta;

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
            panelTarjeta = new Panel();
            lnkAyuda = new LinkLabel();
            lblLogo = new Label();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblContrasena = new Label();
            txtContrasena = new TextBox();
            btnEntrar = new BotonTema();
            btnSalir = new BotonTema();
            avisoMensaje = new AvisoControl();
            panelTarjeta.SuspendLayout();
            SuspendLayout();
            // 
            // panelTarjeta
            // 
            panelTarjeta.Anchor = AnchorStyles.None;
            panelTarjeta.BackColor = Color.White;
            panelTarjeta.Location = new Point(70, 70);
            panelTarjeta.Name = "panelTarjeta";
            panelTarjeta.Size = new Size(420, 530);
            panelTarjeta.TabIndex = 1;
            // 
            // lnkAyuda
            // 
            lnkAyuda.AutoSize = true;
            lnkAyuda.Location = new Point(20, 15);
            lnkAyuda.Name = "lnkAyuda";
            lnkAyuda.Size = new Size(58, 20);
            lnkAyuda.TabIndex = 0;
            lnkAyuda.TabStop = true;
            lnkAyuda.Text = "Ayuda?";
            lnkAyuda.LinkClicked += lnkAyuda_LinkClicked;
            // 
            // lblLogo
            // 
            lblLogo.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblLogo.Location = new Point(0, 30);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(420, 80);
            lblLogo.TabIndex = 1;
            lblLogo.Text = "GestorTaller";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 10F);
            lblUsuario.Location = new Point(50, 130);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(62, 20);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 12F);
            txtUsuario.Location = new Point(50, 158);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(320, 34);
            txtUsuario.TabIndex = 3;
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = true;
            lblContrasena.Font = new Font("Segoe UI", 10F);
            lblContrasena.Location = new Point(50, 210);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(86, 20);
            lblContrasena.TabIndex = 4;
            lblContrasena.Text = "Contrasena:";
            // 
            // txtContrasena
            // 
            txtContrasena.Font = new Font("Segoe UI", 12F);
            txtContrasena.Location = new Point(50, 238);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '●';
            txtContrasena.Size = new Size(320, 34);
            txtContrasena.TabIndex = 5;
            // 
            // btnEntrar
            // 
            btnEntrar.Font = new Font("Segoe UI", 11F);
            btnEntrar.Location = new Point(50, 300);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.Size = new Size(320, 44);
            btnEntrar.TabIndex = 6;
            btnEntrar.Text = "Entrar";
            btnEntrar.Variante = VarianteBoton.Primario;
            btnEntrar.Click += btnEntrar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Font = new Font("Segoe UI", 11F);
            btnSalir.Location = new Point(50, 356);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(320, 44);
            btnSalir.TabIndex = 8;
            btnSalir.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // avisoMensaje
            // 
            avisoMensaje.Location = new Point(50, 414);
            avisoMensaje.Name = "avisoMensaje";
            avisoMensaje.Size = new Size(320, 40);
            avisoMensaje.TabIndex = 7;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 610);
            Controls.Add(lnkAyuda);
            Controls.Add(panelTarjeta);
            MinimumSize = new Size(560, 650);
            panelTarjeta.Controls.Add(lblLogo);
            panelTarjeta.Controls.Add(lblUsuario);
            panelTarjeta.Controls.Add(txtUsuario);
            panelTarjeta.Controls.Add(lblContrasena);
            panelTarjeta.Controls.Add(txtContrasena);
            panelTarjeta.Controls.Add(btnEntrar);
            panelTarjeta.Controls.Add(avisoMensaje);
            panelTarjeta.Controls.Add(btnSalir);
            Name = "FormLogin";
            Text = "GestorTaller - Iniciar sesion";
            Load += FormLogin_Load;
            panelTarjeta.ResumeLayout(false);
            panelTarjeta.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

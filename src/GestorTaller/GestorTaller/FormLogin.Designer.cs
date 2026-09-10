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
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblMensaje;

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
            lnkAyuda = new LinkLabel();
            lblLogo = new Label();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblContrasena = new Label();
            txtContrasena = new TextBox();
            btnEntrar = new Button();
            btnSalir = new Button();
            lblMensaje = new Label();
            SuspendLayout();
            // 
            // lnkAyuda
            // 
            lnkAyuda.AutoSize = true;
            lnkAyuda.Location = new Point(20, 15);
            lnkAyuda.Name = "lnkAyuda";
            lnkAyuda.Size = new Size(62, 20);
            lnkAyuda.TabIndex = 0;
            lnkAyuda.TabStop = true;
            lnkAyuda.Text = "? Ayuda";
            lnkAyuda.LinkClicked += lnkAyuda_LinkClicked;
            // 
            // lblLogo
            // 
            lblLogo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblLogo.Location = new Point(0, 60);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(360, 60);
            lblLogo.TabIndex = 1;
            lblLogo.Text = "GestorTaller";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(60, 150);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(62, 20);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(60, 170);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(240, 27);
            txtUsuario.TabIndex = 3;
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = true;
            lblContrasena.Location = new Point(60, 205);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(86, 20);
            lblContrasena.TabIndex = 4;
            lblContrasena.Text = "Contrasena:";
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(60, 225);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '●';
            txtContrasena.Size = new Size(240, 27);
            txtContrasena.TabIndex = 5;
            // 
            // btnEntrar
            // 
            btnEntrar.Location = new Point(60, 265);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.Size = new Size(240, 32);
            btnEntrar.TabIndex = 6;
            btnEntrar.Text = "Entrar";
            btnEntrar.Click += btnEntrar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(20, 350);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(80, 30);
            btnSalir.TabIndex = 8;
            btnSalir.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(60, 305);
            lblMensaje.MaximumSize = new Size(240, 0);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(0, 20);
            lblMensaje.TabIndex = 7;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(360, 400);
            Controls.Add(lnkAyuda);
            Controls.Add(lblLogo);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(lblContrasena);
            Controls.Add(txtContrasena);
            Controls.Add(btnEntrar);
            Controls.Add(lblMensaje);
            Controls.Add(btnSalir);
            Name = "FormLogin";
            Text = "GestorTaller - Iniciar sesion";
            Load += FormLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

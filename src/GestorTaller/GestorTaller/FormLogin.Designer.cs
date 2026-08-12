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
            components = new System.ComponentModel.Container();

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

            // lnkAyuda
            lnkAyuda.AutoSize = true;
            lnkAyuda.Location = new Point(20, 15);
            lnkAyuda.Text = "? Ayuda";
            lnkAyuda.LinkClicked += lnkAyuda_LinkClicked;

            // lblLogo
            lblLogo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblLogo.Location = new Point(0, 60);
            lblLogo.Size = new Size(360, 60);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            lblLogo.Text = "GestorTaller";

            // lblUsuario
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(60, 150);
            lblUsuario.Text = "Usuario:";

            // txtUsuario
            txtUsuario.Location = new Point(60, 170);
            txtUsuario.Size = new Size(240, 23);

            // lblContrasena
            lblContrasena.AutoSize = true;
            lblContrasena.Location = new Point(60, 205);
            lblContrasena.Text = "Contrasena:";

            // txtContrasena
            txtContrasena.Location = new Point(60, 225);
            txtContrasena.Size = new Size(240, 23);
            txtContrasena.PasswordChar = '●';

            // btnEntrar
            btnEntrar.Location = new Point(60, 265);
            btnEntrar.Size = new Size(240, 32);
            btnEntrar.Text = "Entrar";
            btnEntrar.Click += btnEntrar_Click;

            // lblMensaje
            lblMensaje.AutoSize = true;
            lblMensaje.ForeColor = Color.DarkRed;
            lblMensaje.Location = new Point(60, 305);
            lblMensaje.MaximumSize = new Size(240, 0);
            lblMensaje.Text = "";

            // btnSalir
            btnSalir.Location = new Point(20, 350);
            btnSalir.Size = new Size(80, 30);
            btnSalir.Text = "Salir";
            btnSalir.Click += btnSalir_Click;

            // FormLogin
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
            Text = "GestorTaller - Iniciar sesion";

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

namespace WinUI.WinForms.Login_Register
{
    partial class FrmNuevaContraseña
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNuevaContraseña));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LblCodigo = new System.Windows.Forms.Label();
            this.BtnCambiarPass = new System.Windows.Forms.Button();
            this.txtNuevaPassword = new System.Windows.Forms.TextBox();
            this.LblNuevaPass = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LblConfirmarPass = new System.Windows.Forms.Label();
            this.BtnMinimizar = new System.Windows.Forms.PictureBox();
            this.BtnCerrar = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 291);
            this.panel1.TabIndex = 9;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(-45, 48);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(363, 218);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Location = new System.Drawing.Point(295, 72);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(407, 1);
            this.panel4.TabIndex = 27;
            // 
            // TxtCodigo
            // 
            this.TxtCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TxtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtCodigo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCodigo.ForeColor = System.Drawing.Color.DarkGray;
            this.TxtCodigo.Location = new System.Drawing.Point(293, 51);
            this.TxtCodigo.Margin = new System.Windows.Forms.Padding(2);
            this.TxtCodigo.Name = "TxtCodigo";
            this.TxtCodigo.Size = new System.Drawing.Size(407, 22);
            this.TxtCodigo.TabIndex = 26;
            this.TxtCodigo.Text = "INGRESAR CODIGO";
            this.TxtCodigo.Enter += new System.EventHandler(this.TxtCodigo_Enter);
            this.TxtCodigo.Leave += new System.EventHandler(this.TxtCodigo_Leave);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Location = new System.Drawing.Point(295, 122);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(407, 1);
            this.panel2.TabIndex = 24;
            // 
            // LblCodigo
            // 
            this.LblCodigo.AutoSize = true;
            this.LblCodigo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LblCodigo.Location = new System.Drawing.Point(292, 36);
            this.LblCodigo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblCodigo.Name = "LblCodigo";
            this.LblCodigo.Size = new System.Drawing.Size(123, 13);
            this.LblCodigo.TabIndex = 25;
            this.LblCodigo.Text = "Codigo de recuperacion:";
            // 
            // BtnCambiarPass
            // 
            this.BtnCambiarPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.BtnCambiarPass.FlatAppearance.BorderSize = 0;
            this.BtnCambiarPass.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.BtnCambiarPass.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnCambiarPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCambiarPass.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCambiarPass.ForeColor = System.Drawing.Color.LightGray;
            this.BtnCambiarPass.Location = new System.Drawing.Point(291, 210);
            this.BtnCambiarPass.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCambiarPass.Name = "BtnCambiarPass";
            this.BtnCambiarPass.Size = new System.Drawing.Size(411, 40);
            this.BtnCambiarPass.TabIndex = 23;
            this.BtnCambiarPass.Text = "Cambiar contraseña";
            this.BtnCambiarPass.UseVisualStyleBackColor = false;
            this.BtnCambiarPass.Click += new System.EventHandler(this.BtnCambiarPass_Click);
            // 
            // txtNuevaPassword
            // 
            this.txtNuevaPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtNuevaPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNuevaPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNuevaPassword.ForeColor = System.Drawing.Color.DarkGray;
            this.txtNuevaPassword.Location = new System.Drawing.Point(293, 101);
            this.txtNuevaPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtNuevaPassword.Name = "txtNuevaPassword";
            this.txtNuevaPassword.Size = new System.Drawing.Size(407, 22);
            this.txtNuevaPassword.TabIndex = 22;
            this.txtNuevaPassword.Text = "INGRESAR NUEVA CONTRASEÑA";
            this.txtNuevaPassword.Enter += new System.EventHandler(this.txtNuevaPassword_Enter);
            this.txtNuevaPassword.Leave += new System.EventHandler(this.txtNuevaPassword_Leave);
            // 
            // LblNuevaPass
            // 
            this.LblNuevaPass.AutoSize = true;
            this.LblNuevaPass.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LblNuevaPass.Location = new System.Drawing.Point(292, 86);
            this.LblNuevaPass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblNuevaPass.Name = "LblNuevaPass";
            this.LblNuevaPass.Size = new System.Drawing.Size(98, 13);
            this.LblNuevaPass.TabIndex = 21;
            this.LblNuevaPass.Text = "Nueva contraseña:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.Location = new System.Drawing.Point(295, 169);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(407, 1);
            this.panel3.TabIndex = 27;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.DarkGray;
            this.textBox1.Location = new System.Drawing.Point(293, 148);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(407, 22);
            this.textBox1.TabIndex = 26;
            this.textBox1.Text = "CONFIRMAR NUEVA CONTRASEÑA";
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // LblConfirmarPass
            // 
            this.LblConfirmarPass.AutoSize = true;
            this.LblConfirmarPass.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LblConfirmarPass.Location = new System.Drawing.Point(292, 133);
            this.LblConfirmarPass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblConfirmarPass.Name = "LblConfirmarPass";
            this.LblConfirmarPass.Size = new System.Drawing.Size(110, 13);
            this.LblConfirmarPass.TabIndex = 25;
            this.LblConfirmarPass.Text = "Confirmar contraseña:";
            // 
            // BtnMinimizar
            // 
            this.BtnMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("BtnMinimizar.Image")));
            this.BtnMinimizar.Location = new System.Drawing.Point(708, 0);
            this.BtnMinimizar.Name = "BtnMinimizar";
            this.BtnMinimizar.Size = new System.Drawing.Size(25, 25);
            this.BtnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BtnMinimizar.TabIndex = 29;
            this.BtnMinimizar.TabStop = false;
            this.BtnMinimizar.Click += new System.EventHandler(this.BtnMinimizar_Click);
            // 
            // BtnCerrar
            // 
            this.BtnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("BtnCerrar.Image")));
            this.BtnCerrar.Location = new System.Drawing.Point(739, 0);
            this.BtnCerrar.Name = "BtnCerrar";
            this.BtnCerrar.Size = new System.Drawing.Size(25, 25);
            this.BtnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BtnCerrar.TabIndex = 28;
            this.BtnCerrar.TabStop = false;
            this.BtnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // FrmNuevaContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(764, 291);
            this.Controls.Add(this.BtnMinimizar);
            this.Controls.Add(this.BtnCerrar);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.LblConfirmarPass);
            this.Controls.Add(this.TxtCodigo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.LblCodigo);
            this.Controls.Add(this.BtnCambiarPass);
            this.Controls.Add(this.txtNuevaPassword);
            this.Controls.Add(this.LblNuevaPass);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmNuevaContraseña";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmNuevaContraseña";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnCerrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox TxtCodigo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LblCodigo;
        private System.Windows.Forms.Button BtnCambiarPass;
        private System.Windows.Forms.TextBox txtNuevaPassword;
        private System.Windows.Forms.Label LblNuevaPass;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label LblConfirmarPass;
        private System.Windows.Forms.PictureBox BtnMinimizar;
        private System.Windows.Forms.PictureBox BtnCerrar;
    }
}
namespace WinUI.WinForms.Login_Register
{
    partial class FrmPassRecovery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPassRecovery));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnPassRecovery = new System.Windows.Forms.Button();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.LblEmail = new System.Windows.Forms.Label();
            this.LblTitulo = new System.Windows.Forms.Label();
            this.lnkVolverLogin = new System.Windows.Forms.LinkLabel();
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
            this.panel1.Size = new System.Drawing.Size(250, 330);
            this.panel1.TabIndex = 8;
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
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.Location = new System.Drawing.Point(294, 147);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(407, 1);
            this.panel3.TabIndex = 13;
            // 
            // BtnPassRecovery
            // 
            this.BtnPassRecovery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.BtnPassRecovery.FlatAppearance.BorderSize = 0;
            this.BtnPassRecovery.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.BtnPassRecovery.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnPassRecovery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPassRecovery.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPassRecovery.ForeColor = System.Drawing.Color.LightGray;
            this.BtnPassRecovery.Location = new System.Drawing.Point(290, 183);
            this.BtnPassRecovery.Margin = new System.Windows.Forms.Padding(2);
            this.BtnPassRecovery.Name = "BtnPassRecovery";
            this.BtnPassRecovery.Size = new System.Drawing.Size(411, 40);
            this.BtnPassRecovery.TabIndex = 12;
            this.BtnPassRecovery.Text = "Solicitar recuperacion de contraseña";
            this.BtnPassRecovery.UseVisualStyleBackColor = false;
            this.BtnPassRecovery.Click += new System.EventHandler(this.BtnPassRecovery_Click);
            // 
            // TxtEmail
            // 
            this.TxtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TxtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmail.ForeColor = System.Drawing.Color.DarkGray;
            this.TxtEmail.Location = new System.Drawing.Point(292, 126);
            this.TxtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(407, 22);
            this.TxtEmail.TabIndex = 11;
            this.TxtEmail.Text = "INGRESAR EMAIL";
            this.TxtEmail.Enter += new System.EventHandler(this.TxtEmail_Enter);
            this.TxtEmail.Leave += new System.EventHandler(this.TxtEmail_Leave);
            // 
            // LblEmail
            // 
            this.LblEmail.AutoSize = true;
            this.LblEmail.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LblEmail.Location = new System.Drawing.Point(291, 111);
            this.LblEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblEmail.Name = "LblEmail";
            this.LblEmail.Size = new System.Drawing.Size(35, 13);
            this.LblEmail.TabIndex = 10;
            this.LblEmail.Text = "Email:";
            // 
            // LblTitulo
            // 
            this.LblTitulo.AutoSize = true;
            this.LblTitulo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitulo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LblTitulo.Location = new System.Drawing.Point(413, 29);
            this.LblTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTitulo.Name = "LblTitulo";
            this.LblTitulo.Size = new System.Drawing.Size(195, 25);
            this.LblTitulo.TabIndex = 21;
            this.LblTitulo.Text = "Recuperar contraseña";
            // 
            // lnkVolverLogin
            // 
            this.lnkVolverLogin.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lnkVolverLogin.AutoSize = true;
            this.lnkVolverLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkVolverLogin.LinkColor = System.Drawing.Color.DarkGray;
            this.lnkVolverLogin.Location = new System.Drawing.Point(404, 281);
            this.lnkVolverLogin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkVolverLogin.Name = "lnkVolverLogin";
            this.lnkVolverLogin.Size = new System.Drawing.Size(160, 21);
            this.lnkVolverLogin.TabIndex = 22;
            this.lnkVolverLogin.TabStop = true;
            this.lnkVolverLogin.Text = "Volver a iniciar sesion";
            this.lnkVolverLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkVolverLogin_LinkClicked);
            // 
            // BtnMinimizar
            // 
            this.BtnMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("BtnMinimizar.Image")));
            this.BtnMinimizar.Location = new System.Drawing.Point(723, 0);
            this.BtnMinimizar.Name = "BtnMinimizar";
            this.BtnMinimizar.Size = new System.Drawing.Size(25, 25);
            this.BtnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BtnMinimizar.TabIndex = 24;
            this.BtnMinimizar.TabStop = false;
            // 
            // BtnCerrar
            // 
            this.BtnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("BtnCerrar.Image")));
            this.BtnCerrar.Location = new System.Drawing.Point(754, 0);
            this.BtnCerrar.Name = "BtnCerrar";
            this.BtnCerrar.Size = new System.Drawing.Size(25, 25);
            this.BtnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BtnCerrar.TabIndex = 23;
            this.BtnCerrar.TabStop = false;
            this.BtnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            // 
            // FrmPassRecovery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(780, 330);
            this.Controls.Add(this.BtnMinimizar);
            this.Controls.Add(this.BtnCerrar);
            this.Controls.Add(this.lnkVolverLogin);
            this.Controls.Add(this.LblTitulo);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.BtnPassRecovery);
            this.Controls.Add(this.TxtEmail);
            this.Controls.Add(this.LblEmail);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPassRecovery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPassRecovery";
            this.Load += new System.EventHandler(this.FrmPassRecovery_Load);
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
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnPassRecovery;
        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.Label LblEmail;
        private System.Windows.Forms.Label LblTitulo;
        private System.Windows.Forms.LinkLabel lnkVolverLogin;
        private System.Windows.Forms.PictureBox BtnMinimizar;
        private System.Windows.Forms.PictureBox BtnCerrar;
    }
}
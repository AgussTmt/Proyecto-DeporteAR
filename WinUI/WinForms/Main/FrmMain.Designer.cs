namespace WinUI.WinForms
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.BtnLogout = new System.Windows.Forms.Button();
            this.BtnSettings = new System.Windows.Forms.Button();
            this.BtnUserManagment = new System.Windows.Forms.Button();
            this.BtnReportes = new System.Windows.Forms.Button();
            this.BtnCompeticion = new System.Windows.Forms.Button();
            this.btnEquipos = new System.Windows.Forms.Button();
            this.BtnCancha = new System.Windows.Forms.Button();
            this.BtnCalendario = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.l = new System.Windows.Forms.Label();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnIdiomaIngles = new System.Windows.Forms.Button();
            this.LblIdioma = new System.Windows.Forms.Label();
            this.BtnIdiomaEspañol = new System.Windows.Forms.Button();
            this.LblTitle = new System.Windows.Forms.Label();
            this.panelDesktopPane = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.panelMenu.Controls.Add(this.BtnLogout);
            this.panelMenu.Controls.Add(this.BtnSettings);
            this.panelMenu.Controls.Add(this.BtnUserManagment);
            this.panelMenu.Controls.Add(this.BtnReportes);
            this.panelMenu.Controls.Add(this.BtnCompeticion);
            this.panelMenu.Controls.Add(this.btnEquipos);
            this.panelMenu.Controls.Add(this.BtnCancha);
            this.panelMenu.Controls.Add(this.BtnCalendario);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 639);
            this.panelMenu.TabIndex = 0;
            // 
            // BtnLogout
            // 
            this.BtnLogout.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnLogout.FlatAppearance.BorderSize = 0;
            this.BtnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLogout.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLogout.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnLogout.Image = ((System.Drawing.Image)(resources.GetObject("BtnLogout.Image")));
            this.BtnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLogout.Location = new System.Drawing.Point(0, 449);
            this.BtnLogout.Name = "BtnLogout";
            this.BtnLogout.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.BtnLogout.Size = new System.Drawing.Size(220, 54);
            this.BtnLogout.TabIndex = 6;
            this.BtnLogout.Text = "Cerrar sesion";
            this.BtnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnLogout.UseVisualStyleBackColor = true;
            this.BtnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // BtnSettings
            // 
            this.BtnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnSettings.FlatAppearance.BorderSize = 0;
            this.BtnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSettings.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSettings.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnSettings.Image = ((System.Drawing.Image)(resources.GetObject("BtnSettings.Image")));
            this.BtnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSettings.Location = new System.Drawing.Point(0, 395);
            this.BtnSettings.Name = "BtnSettings";
            this.BtnSettings.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.BtnSettings.Size = new System.Drawing.Size(220, 54);
            this.BtnSettings.TabIndex = 5;
            this.BtnSettings.Text = "Configuracion y logs";
            this.BtnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnSettings.UseVisualStyleBackColor = true;
            this.BtnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // BtnUserManagment
            // 
            this.BtnUserManagment.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnUserManagment.FlatAppearance.BorderSize = 0;
            this.BtnUserManagment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUserManagment.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUserManagment.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnUserManagment.Image = ((System.Drawing.Image)(resources.GetObject("BtnUserManagment.Image")));
            this.BtnUserManagment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnUserManagment.Location = new System.Drawing.Point(0, 341);
            this.BtnUserManagment.Name = "BtnUserManagment";
            this.BtnUserManagment.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.BtnUserManagment.Size = new System.Drawing.Size(220, 54);
            this.BtnUserManagment.TabIndex = 7;
            this.BtnUserManagment.Text = "Control de usuarios";
            this.BtnUserManagment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnUserManagment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnUserManagment.UseVisualStyleBackColor = true;
            this.BtnUserManagment.Click += new System.EventHandler(this.BtnUserManagment_Click);
            // 
            // BtnReportes
            // 
            this.BtnReportes.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnReportes.FlatAppearance.BorderSize = 0;
            this.BtnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnReportes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReportes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnReportes.Image = ((System.Drawing.Image)(resources.GetObject("BtnReportes.Image")));
            this.BtnReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnReportes.Location = new System.Drawing.Point(0, 287);
            this.BtnReportes.Name = "BtnReportes";
            this.BtnReportes.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.BtnReportes.Size = new System.Drawing.Size(220, 54);
            this.BtnReportes.TabIndex = 4;
            this.BtnReportes.Text = "Reportes";
            this.BtnReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnReportes.UseVisualStyleBackColor = true;
            this.BtnReportes.Click += new System.EventHandler(this.BtnReportes_Click);
            // 
            // BtnCompeticion
            // 
            this.BtnCompeticion.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCompeticion.FlatAppearance.BorderSize = 0;
            this.BtnCompeticion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCompeticion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCompeticion.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnCompeticion.Image = ((System.Drawing.Image)(resources.GetObject("BtnCompeticion.Image")));
            this.BtnCompeticion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCompeticion.Location = new System.Drawing.Point(0, 233);
            this.BtnCompeticion.Name = "BtnCompeticion";
            this.BtnCompeticion.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.BtnCompeticion.Size = new System.Drawing.Size(220, 54);
            this.BtnCompeticion.TabIndex = 3;
            this.BtnCompeticion.Text = "Competiciones";
            this.BtnCompeticion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCompeticion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCompeticion.UseVisualStyleBackColor = true;
            this.BtnCompeticion.Click += new System.EventHandler(this.BtnCompeticion_Click);
            // 
            // btnEquipos
            // 
            this.btnEquipos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEquipos.FlatAppearance.BorderSize = 0;
            this.btnEquipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEquipos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEquipos.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnEquipos.Image = ((System.Drawing.Image)(resources.GetObject("btnEquipos.Image")));
            this.btnEquipos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEquipos.Location = new System.Drawing.Point(0, 179);
            this.btnEquipos.Name = "btnEquipos";
            this.btnEquipos.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnEquipos.Size = new System.Drawing.Size(220, 54);
            this.btnEquipos.TabIndex = 8;
            this.btnEquipos.Text = "Equipos";
            this.btnEquipos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEquipos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEquipos.UseVisualStyleBackColor = true;
            this.btnEquipos.Click += new System.EventHandler(this.btnEquipos_Click);
            // 
            // BtnCancha
            // 
            this.BtnCancha.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCancha.FlatAppearance.BorderSize = 0;
            this.BtnCancha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancha.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancha.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnCancha.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancha.Image")));
            this.BtnCancha.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCancha.Location = new System.Drawing.Point(0, 125);
            this.BtnCancha.Name = "BtnCancha";
            this.BtnCancha.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.BtnCancha.Size = new System.Drawing.Size(220, 54);
            this.BtnCancha.TabIndex = 2;
            this.BtnCancha.Text = "Cancha";
            this.BtnCancha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCancha.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancha.UseVisualStyleBackColor = true;
            this.BtnCancha.Click += new System.EventHandler(this.BtnCancha_Click);
            // 
            // BtnCalendario
            // 
            this.BtnCalendario.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCalendario.FlatAppearance.BorderSize = 0;
            this.BtnCalendario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCalendario.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCalendario.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnCalendario.Image = ((System.Drawing.Image)(resources.GetObject("BtnCalendario.Image")));
            this.BtnCalendario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCalendario.Location = new System.Drawing.Point(0, 71);
            this.BtnCalendario.Name = "BtnCalendario";
            this.BtnCalendario.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.BtnCalendario.Size = new System.Drawing.Size(220, 54);
            this.BtnCalendario.TabIndex = 1;
            this.BtnCalendario.Text = "Calendario";
            this.BtnCalendario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCalendario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCalendario.UseVisualStyleBackColor = true;
            this.BtnCalendario.Click += new System.EventHandler(this.BtnCalendario_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(39)))), ((int)(((byte)(44)))));
            this.panelLogo.Controls.Add(this.l);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(220, 71);
            this.panelLogo.TabIndex = 1;
            // 
            // l
            // 
            this.l.AutoSize = true;
            this.l.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.l.Location = new System.Drawing.Point(47, 21);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(103, 25);
            this.l.TabIndex = 0;
            this.l.Text = "DeporteAR";
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(39)))), ((int)(((byte)(44)))));
            this.panelTitleBar.Controls.Add(this.panel1);
            this.panelTitleBar.Controls.Add(this.LblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(220, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(897, 71);
            this.panelTitleBar.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnIdiomaIngles);
            this.panel1.Controls.Add(this.LblIdioma);
            this.panel1.Controls.Add(this.BtnIdiomaEspañol);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(697, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 71);
            this.panel1.TabIndex = 0;
            // 
            // BtnIdiomaIngles
            // 
            this.BtnIdiomaIngles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnIdiomaIngles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnIdiomaIngles.Location = new System.Drawing.Point(101, 40);
            this.BtnIdiomaIngles.Name = "BtnIdiomaIngles";
            this.BtnIdiomaIngles.Size = new System.Drawing.Size(87, 25);
            this.BtnIdiomaIngles.TabIndex = 2;
            this.BtnIdiomaIngles.Text = "Ingles";
            this.BtnIdiomaIngles.UseVisualStyleBackColor = true;
            this.BtnIdiomaIngles.Click += new System.EventHandler(this.BtnIdiomaIngles_Click);
            // 
            // LblIdioma
            // 
            this.LblIdioma.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblIdioma.AutoSize = true;
            this.LblIdioma.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblIdioma.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LblIdioma.Location = new System.Drawing.Point(72, 21);
            this.LblIdioma.Name = "LblIdioma";
            this.LblIdioma.Size = new System.Drawing.Size(47, 15);
            this.LblIdioma.TabIndex = 3;
            this.LblIdioma.Text = "Idioma:";
            // 
            // BtnIdiomaEspañol
            // 
            this.BtnIdiomaEspañol.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnIdiomaEspañol.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnIdiomaEspañol.Location = new System.Drawing.Point(8, 40);
            this.BtnIdiomaEspañol.Name = "BtnIdiomaEspañol";
            this.BtnIdiomaEspañol.Size = new System.Drawing.Size(87, 25);
            this.BtnIdiomaEspañol.TabIndex = 1;
            this.BtnIdiomaEspañol.Text = "Español";
            this.BtnIdiomaEspañol.UseVisualStyleBackColor = true;
            this.BtnIdiomaEspañol.Click += new System.EventHandler(this.BtnIdiomaEspañol_Click);
            // 
            // LblTitle
            // 
            this.LblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitle.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.LblTitle.Location = new System.Drawing.Point(411, 21);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(62, 25);
            this.LblTitle.TabIndex = 0;
            this.LblTitle.Text = "Home";
            // 
            // panelDesktopPane
            // 
            this.panelDesktopPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktopPane.Location = new System.Drawing.Point(220, 71);
            this.panelDesktopPane.Name = "panelDesktopPane";
            this.panelDesktopPane.Size = new System.Drawing.Size(897, 568);
            this.panelDesktopPane.TabIndex = 2;
            this.panelDesktopPane.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDesktopPane_Paint);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 639);
            this.Controls.Add(this.panelDesktopPane);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button BtnCalendario;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Button BtnLogout;
        private System.Windows.Forms.Button BtnSettings;
        private System.Windows.Forms.Button BtnReportes;
        private System.Windows.Forms.Button BtnCompeticion;
        private System.Windows.Forms.Button BtnCancha;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Label l;
        private System.Windows.Forms.Button BtnUserManagment;
        private System.Windows.Forms.Panel panelDesktopPane;
        private System.Windows.Forms.Label LblIdioma;
        private System.Windows.Forms.Button BtnIdiomaIngles;
        private System.Windows.Forms.Button BtnIdiomaEspañol;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEquipos;
    }
}
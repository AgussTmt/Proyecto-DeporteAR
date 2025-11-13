namespace WinUI.WinForms.Gestiones.Settings
{
    partial class FrmSettings
    {
        private System.ComponentModel.IContainer components = null;

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
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabDatabase = new System.Windows.Forms.TabPage();
            this.tlpDatabase = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxDB = new System.Windows.Forms.GroupBox();
            this.BtnRestore = new System.Windows.Forms.Button();
            this.BtnBackUp = new System.Windows.Forms.Button();
            this.ComboBoxBaseDeDatos = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.LbDataBase = new System.Windows.Forms.Label();
            this.tabEmailLogs = new System.Windows.Forms.TabPage();
            this.tlpEmailLogs = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxEmail = new System.Windows.Forms.GroupBox();
            this.btnGuardarEmail = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSenderEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSenderPassword = new System.Windows.Forms.TextBox();
            this.groupBoxLogs = new System.Windows.Forms.GroupBox();
            this.BtnVerLogs = new System.Windows.Forms.Button();
            this.LbLogs = new System.Windows.Forms.Label();
            this.tabControlSettings.SuspendLayout();
            this.tabDatabase.SuspendLayout();
            this.tlpDatabase.SuspendLayout();
            this.groupBoxDB.SuspendLayout();
            this.tabEmailLogs.SuspendLayout();
            this.tlpEmailLogs.SuspendLayout();
            this.groupBoxEmail.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxLogs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabDatabase);
            this.tabControlSettings.Controls.Add(this.tabEmailLogs);
            this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSettings.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(928, 555);
            this.tabControlSettings.TabIndex = 0;
            // 
            // tabDatabase
            // 
            this.tabDatabase.Controls.Add(this.tlpDatabase);
            this.tabDatabase.Location = new System.Drawing.Point(4, 26);
            this.tabDatabase.Name = "tabDatabase";
            this.tabDatabase.Padding = new System.Windows.Forms.Padding(3);
            this.tabDatabase.Size = new System.Drawing.Size(920, 525);
            this.tabDatabase.TabIndex = 0;
            this.tabDatabase.Text = "Bases de Datos";
            this.tabDatabase.UseVisualStyleBackColor = true;
            // 
            // tlpDatabase
            // 
            this.tlpDatabase.ColumnCount = 1;
            this.tlpDatabase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDatabase.Controls.Add(this.groupBoxDB, 0, 1);
            this.tlpDatabase.Controls.Add(this.lblStatus, 0, 2);
            this.tlpDatabase.Controls.Add(this.LbDataBase, 0, 0);
            this.tlpDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDatabase.Location = new System.Drawing.Point(3, 3);
            this.tlpDatabase.Name = "tlpDatabase";
            this.tlpDatabase.RowCount = 4;
            this.tlpDatabase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpDatabase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpDatabase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tlpDatabase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDatabase.Size = new System.Drawing.Size(914, 519);
            this.tlpDatabase.TabIndex = 0;
            // 
            // groupBoxDB
            // 
            this.groupBoxDB.Controls.Add(this.BtnRestore);
            this.groupBoxDB.Controls.Add(this.BtnBackUp);
            this.groupBoxDB.Controls.Add(this.ComboBoxBaseDeDatos);
            this.groupBoxDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDB.Location = new System.Drawing.Point(3, 38);
            this.groupBoxDB.Name = "groupBoxDB";
            this.groupBoxDB.Size = new System.Drawing.Size(908, 84);
            this.groupBoxDB.TabIndex = 0;
            this.groupBoxDB.TabStop = false;
            // 
            // BtnRestore
            // 
            this.BtnRestore.Location = new System.Drawing.Point(461, 32);
            this.BtnRestore.Name = "BtnRestore";
            this.BtnRestore.Size = new System.Drawing.Size(120, 35);
            this.BtnRestore.TabIndex = 2;
            this.BtnRestore.Text = "Restore";
            this.BtnRestore.UseVisualStyleBackColor = true;
            this.BtnRestore.Click += new System.EventHandler(this.BtnRestore_Click);
            // 
            // BtnBackUp
            // 
            this.BtnBackUp.Location = new System.Drawing.Point(335, 32);
            this.BtnBackUp.Name = "BtnBackUp";
            this.BtnBackUp.Size = new System.Drawing.Size(120, 35);
            this.BtnBackUp.TabIndex = 1;
            this.BtnBackUp.Text = "BackUp";
            this.BtnBackUp.UseVisualStyleBackColor = true;
            this.BtnBackUp.Click += new System.EventHandler(this.BtnBackUp_Click);
            // 
            // ComboBoxBaseDeDatos
            // 
            this.ComboBoxBaseDeDatos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxBaseDeDatos.FormattingEnabled = true;
            this.ComboBoxBaseDeDatos.Location = new System.Drawing.Point(16, 37);
            this.ComboBoxBaseDeDatos.Name = "ComboBoxBaseDeDatos";
            this.ComboBoxBaseDeDatos.Size = new System.Drawing.Size(304, 25);
            this.ComboBoxBaseDeDatos.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(433, 142);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 25);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Listo";
            // 
            // LbDataBase
            // 
            this.LbDataBase.AutoSize = true;
            this.LbDataBase.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbDataBase.Location = new System.Drawing.Point(10, 7);
            this.LbDataBase.Margin = new System.Windows.Forms.Padding(10, 7, 3, 0);
            this.LbDataBase.Name = "LbDataBase";
            this.LbDataBase.Size = new System.Drawing.Size(225, 21);
            this.LbDataBase.TabIndex = 2;
            this.LbDataBase.Text = "Operaciones de Base de datos";
            // 
            // tabEmailLogs
            // 
            this.tabEmailLogs.Controls.Add(this.tlpEmailLogs);
            this.tabEmailLogs.Location = new System.Drawing.Point(4, 26);
            this.tabEmailLogs.Name = "tabEmailLogs";
            this.tabEmailLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmailLogs.Size = new System.Drawing.Size(920, 525);
            this.tabEmailLogs.TabIndex = 1;
            this.tabEmailLogs.Text = "Correo y Logs";
            this.tabEmailLogs.UseVisualStyleBackColor = true;
            // 
            // tlpEmailLogs
            // 
            this.tlpEmailLogs.ColumnCount = 1;
            this.tlpEmailLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEmailLogs.Controls.Add(this.groupBoxEmail, 0, 0);
            this.tlpEmailLogs.Controls.Add(this.groupBoxLogs, 0, 1);
            this.tlpEmailLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEmailLogs.Location = new System.Drawing.Point(3, 3);
            this.tlpEmailLogs.Name = "tlpEmailLogs";
            this.tlpEmailLogs.RowCount = 3;
            this.tlpEmailLogs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpEmailLogs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpEmailLogs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEmailLogs.Size = new System.Drawing.Size(914, 519);
            this.tlpEmailLogs.TabIndex = 0;
            // 
            // groupBoxEmail
            // 
            this.groupBoxEmail.Controls.Add(this.btnGuardarEmail);
            this.groupBoxEmail.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxEmail.Location = new System.Drawing.Point(3, 3);
            this.groupBoxEmail.Name = "groupBoxEmail";
            this.groupBoxEmail.Size = new System.Drawing.Size(908, 174);
            this.groupBoxEmail.TabIndex = 0;
            this.groupBoxEmail.TabStop = false;
            this.groupBoxEmail.Text = "Configuración de Correo (Recuperación)";
            // 
            // btnGuardarEmail
            // 
            this.btnGuardarEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarEmail.Location = new System.Drawing.Point(15, 124);
            this.btnGuardarEmail.Name = "btnGuardarEmail";
            this.btnGuardarEmail.Size = new System.Drawing.Size(161, 35);
            this.btnGuardarEmail.TabIndex = 1;
            this.btnGuardarEmail.Text = "Guardar Cambios Email";
            this.btnGuardarEmail.UseVisualStyleBackColor = true;
            this.btnGuardarEmail.Click += new System.EventHandler(this.btnGuardarEmail_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSenderEmail, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSenderPassword, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(902, 90);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email de envío (Remitente):";
            // 
            // txtSenderEmail
            // 
            this.txtSenderEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSenderEmail.Location = new System.Drawing.Point(183, 15);
            this.txtSenderEmail.Name = "txtSenderEmail";
            this.txtSenderEmail.Size = new System.Drawing.Size(706, 25);
            this.txtSenderEmail.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña de App:";
            // 
            // txtSenderPassword
            // 
            this.txtSenderPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSenderPassword.Location = new System.Drawing.Point(183, 50);
            this.txtSenderPassword.Name = "txtSenderPassword";
            this.txtSenderPassword.PasswordChar = '*';
            this.txtSenderPassword.Size = new System.Drawing.Size(706, 25);
            this.txtSenderPassword.TabIndex = 3;
            // 
            // groupBoxLogs
            // 
            this.groupBoxLogs.Controls.Add(this.BtnVerLogs);
            this.groupBoxLogs.Controls.Add(this.LbLogs);
            this.groupBoxLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLogs.Location = new System.Drawing.Point(3, 183);
            this.groupBoxLogs.Name = "groupBoxLogs";
            this.groupBoxLogs.Size = new System.Drawing.Size(908, 114);
            this.groupBoxLogs.TabIndex = 1;
            this.groupBoxLogs.TabStop = false;
            // 
            // BtnVerLogs
            // 
            this.BtnVerLogs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVerLogs.Location = new System.Drawing.Point(15, 60);
            this.BtnVerLogs.Name = "BtnVerLogs";
            this.BtnVerLogs.Size = new System.Drawing.Size(161, 35);
            this.BtnVerLogs.TabIndex = 1;
            this.BtnVerLogs.Text = "Ver Logs";
            this.BtnVerLogs.UseVisualStyleBackColor = true;
            this.BtnVerLogs.Click += new System.EventHandler(this.BtnVerLogs_Click);
            // 
            // LbLogs
            // 
            this.LbLogs.AutoSize = true;
            this.LbLogs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbLogs.Location = new System.Drawing.Point(11, 25);
            this.LbLogs.Name = "LbLogs";
            this.LbLogs.Size = new System.Drawing.Size(117, 21);
            this.LbLogs.TabIndex = 0;
            this.LbLogs.Text = "Logs del Sitema";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 555);
            this.Controls.Add(this.tabControlSettings);
            this.Name = "FrmSettings";
            this.Text = "FrmSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.tabControlSettings.ResumeLayout(false);
            this.tabDatabase.ResumeLayout(false);
            this.tlpDatabase.ResumeLayout(false);
            this.tlpDatabase.PerformLayout();
            this.groupBoxDB.ResumeLayout(false);
            this.tabEmailLogs.ResumeLayout(false);
            this.tlpEmailLogs.ResumeLayout(false);
            this.groupBoxEmail.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxLogs.ResumeLayout(false);
            this.groupBoxLogs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabDatabase;
        private System.Windows.Forms.TabPage tabEmailLogs;
        private System.Windows.Forms.TableLayoutPanel tlpDatabase;
        private System.Windows.Forms.GroupBox groupBoxDB;
        private System.Windows.Forms.Button BtnRestore;
        private System.Windows.Forms.Button BtnBackUp;
        private System.Windows.Forms.ComboBox ComboBoxBaseDeDatos;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label LbDataBase;
        private System.Windows.Forms.TableLayoutPanel tlpEmailLogs;
        private System.Windows.Forms.GroupBox groupBoxEmail;
        private System.Windows.Forms.GroupBox groupBoxLogs;
        private System.Windows.Forms.Button BtnVerLogs;
        private System.Windows.Forms.Label LbLogs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSenderEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSenderPassword;
        private System.Windows.Forms.Button btnGuardarEmail;
    }
}
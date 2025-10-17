namespace WinUI.WinForms.Gestiones.Settings
{
    partial class FrmSettings
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
            this.PanelVerLogs = new System.Windows.Forms.Panel();
            this.BtnVerLogs = new System.Windows.Forms.Button();
            this.LbLogs = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.BtnBackUp = new System.Windows.Forms.Button();
            this.LbDataBase = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.BtnRestore = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.LbIdiomas = new System.Windows.Forms.Label();
            this.ComboBoxIdioma = new System.Windows.Forms.ComboBox();
            this.BtnCambiarIdioma = new System.Windows.Forms.Button();
            this.PanelVerLogs.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelVerLogs
            // 
            this.PanelVerLogs.Controls.Add(this.panel1);
            this.PanelVerLogs.Controls.Add(this.panel2);
            this.PanelVerLogs.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelVerLogs.Location = new System.Drawing.Point(0, 0);
            this.PanelVerLogs.Name = "PanelVerLogs";
            this.PanelVerLogs.Size = new System.Drawing.Size(928, 105);
            this.PanelVerLogs.TabIndex = 0;
            // 
            // BtnVerLogs
            // 
            this.BtnVerLogs.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnVerLogs.Location = new System.Drawing.Point(0, 0);
            this.BtnVerLogs.Name = "BtnVerLogs";
            this.BtnVerLogs.Size = new System.Drawing.Size(120, 47);
            this.BtnVerLogs.TabIndex = 0;
            this.BtnVerLogs.Text = "Ver Logs";
            this.BtnVerLogs.UseVisualStyleBackColor = true;
            this.BtnVerLogs.Click += new System.EventHandler(this.BtnVerLogs_Click);
            // 
            // LbLogs
            // 
            this.LbLogs.AutoSize = true;
            this.LbLogs.Location = new System.Drawing.Point(3, 9);
            this.LbLogs.Name = "LbLogs";
            this.LbLogs.Size = new System.Drawing.Size(30, 13);
            this.LbLogs.TabIndex = 1;
            this.LbLogs.Text = "Logs";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnVerLogs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(928, 47);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LbLogs);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(928, 48);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 105);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(928, 94);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.BtnRestore);
            this.panel4.Controls.Add(this.BtnBackUp);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 32);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(928, 50);
            this.panel4.TabIndex = 2;
            // 
            // BtnBackUp
            // 
            this.BtnBackUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnBackUp.Location = new System.Drawing.Point(0, 0);
            this.BtnBackUp.Name = "BtnBackUp";
            this.BtnBackUp.Size = new System.Drawing.Size(120, 50);
            this.BtnBackUp.TabIndex = 0;
            this.BtnBackUp.Text = "BackUp";
            this.BtnBackUp.UseVisualStyleBackColor = true;
            // 
            // LbDataBase
            // 
            this.LbDataBase.AutoSize = true;
            this.LbDataBase.Dock = System.Windows.Forms.DockStyle.Top;
            this.LbDataBase.Location = new System.Drawing.Point(0, 0);
            this.LbDataBase.Name = "LbDataBase";
            this.LbDataBase.Size = new System.Drawing.Size(153, 13);
            this.LbDataBase.TabIndex = 1;
            this.LbDataBase.Text = "Operaciones de Base de datos";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.LbDataBase);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(928, 32);
            this.panel6.TabIndex = 1;
            // 
            // BtnRestore
            // 
            this.BtnRestore.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnRestore.Location = new System.Drawing.Point(120, 0);
            this.BtnRestore.Name = "BtnRestore";
            this.BtnRestore.Size = new System.Drawing.Size(120, 50);
            this.BtnRestore.TabIndex = 1;
            this.BtnRestore.Text = "Restore";
            this.BtnRestore.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 199);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(928, 95);
            this.panel5.TabIndex = 5;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.LbIdiomas);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(928, 25);
            this.panel7.TabIndex = 6;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.BtnCambiarIdioma);
            this.panel8.Controls.Add(this.ComboBoxIdioma);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 25);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(928, 44);
            this.panel8.TabIndex = 7;
            // 
            // LbIdiomas
            // 
            this.LbIdiomas.AutoSize = true;
            this.LbIdiomas.Dock = System.Windows.Forms.DockStyle.Top;
            this.LbIdiomas.Location = new System.Drawing.Point(0, 0);
            this.LbIdiomas.Name = "LbIdiomas";
            this.LbIdiomas.Size = new System.Drawing.Size(60, 13);
            this.LbIdiomas.TabIndex = 0;
            this.LbIdiomas.Text = "MultiIdioma";
            // 
            // ComboBoxIdioma
            // 
            this.ComboBoxIdioma.Dock = System.Windows.Forms.DockStyle.Left;
            this.ComboBoxIdioma.FormattingEnabled = true;
            this.ComboBoxIdioma.Location = new System.Drawing.Point(0, 0);
            this.ComboBoxIdioma.Margin = new System.Windows.Forms.Padding(10);
            this.ComboBoxIdioma.Name = "ComboBoxIdioma";
            this.ComboBoxIdioma.Size = new System.Drawing.Size(328, 21);
            this.ComboBoxIdioma.TabIndex = 0;
            // 
            // BtnCambiarIdioma
            // 
            this.BtnCambiarIdioma.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnCambiarIdioma.Location = new System.Drawing.Point(328, 0);
            this.BtnCambiarIdioma.Margin = new System.Windows.Forms.Padding(20);
            this.BtnCambiarIdioma.Name = "BtnCambiarIdioma";
            this.BtnCambiarIdioma.Size = new System.Drawing.Size(116, 44);
            this.BtnCambiarIdioma.TabIndex = 1;
            this.BtnCambiarIdioma.Text = "cambiar idioma";
            this.BtnCambiarIdioma.UseVisualStyleBackColor = true;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 555);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.PanelVerLogs);
            this.Name = "FrmSettings";
            this.Text = "FrmSettings";
            this.PanelVerLogs.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelVerLogs;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnVerLogs;
        private System.Windows.Forms.Label LbLogs;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button BtnBackUp;
        private System.Windows.Forms.Label LbDataBase;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button BtnRestore;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label LbIdiomas;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button BtnCambiarIdioma;
        private System.Windows.Forms.ComboBox ComboBoxIdioma;
    }
}
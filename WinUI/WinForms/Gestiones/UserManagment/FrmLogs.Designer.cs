namespace WinUI.WinForms.Gestiones.Settings
{
    partial class FrmLogs
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CmbFiltroNiveles = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnActualizarLogs = new System.Windows.Forms.Button();
            this.richTextBoxLogs = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1038, 74);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CmbFiltroNiveles);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(439, 74);
            this.panel2.TabIndex = 0;
            // 
            // CmbFiltroNiveles
            // 
            this.CmbFiltroNiveles.FormattingEnabled = true;
            this.CmbFiltroNiveles.Location = new System.Drawing.Point(62, 28);
            this.CmbFiltroNiveles.Name = "CmbFiltroNiveles";
            this.CmbFiltroNiveles.Size = new System.Drawing.Size(335, 21);
            this.CmbFiltroNiveles.TabIndex = 0;
            this.CmbFiltroNiveles.SelectedIndexChanged += new System.EventHandler(this.cmbNivel_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BtnActualizarLogs);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(439, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(365, 74);
            this.panel3.TabIndex = 1;
            // 
            // BtnActualizarLogs
            // 
            this.BtnActualizarLogs.Location = new System.Drawing.Point(86, 12);
            this.BtnActualizarLogs.Name = "BtnActualizarLogs";
            this.BtnActualizarLogs.Size = new System.Drawing.Size(200, 50);
            this.BtnActualizarLogs.TabIndex = 0;
            this.BtnActualizarLogs.Text = "Actualizar";
            this.BtnActualizarLogs.UseVisualStyleBackColor = true;
            this.BtnActualizarLogs.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // richTextBoxLogs
            // 
            this.richTextBoxLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLogs.Location = new System.Drawing.Point(0, 74);
            this.richTextBoxLogs.Name = "richTextBoxLogs";
            this.richTextBoxLogs.Size = new System.Drawing.Size(1038, 589);
            this.richTextBoxLogs.TabIndex = 1;
            this.richTextBoxLogs.Text = "";
            // 
            // FrmLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 663);
            this.Controls.Add(this.richTextBoxLogs);
            this.Controls.Add(this.panel1);
            this.Name = "FrmLogs";
            this.Text = "FrmLogs";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnActualizarLogs;
        private System.Windows.Forms.ComboBox CmbFiltroNiveles;
        private System.Windows.Forms.RichTextBox richTextBoxLogs;
    }
}
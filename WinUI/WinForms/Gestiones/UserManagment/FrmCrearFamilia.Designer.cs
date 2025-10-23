namespace WinUI.WinForms.Gestiones.UserManagment
{
    partial class FrmCrearFamilia
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.CombFamilias = new System.Windows.Forms.ComboBox();
            this.LblCurrentFamilias = new System.Windows.Forms.Label();
            this.BtnSaveCrear = new System.Windows.Forms.Button();
            this.TxtNombreFamilia = new System.Windows.Forms.TextBox();
            this.LblNombreFamilia = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CheckListPatentesParaFamilias = new System.Windows.Forms.CheckedListBox();
            this.LblListaPatentes = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 494);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.CombFamilias);
            this.panel3.Controls.Add(this.LblCurrentFamilias);
            this.panel3.Controls.Add(this.BtnSaveCrear);
            this.panel3.Controls.Add(this.TxtNombreFamilia);
            this.panel3.Controls.Add(this.LblNombreFamilia);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(358, 494);
            this.panel3.TabIndex = 6;
            // 
            // CombFamilias
            // 
            this.CombFamilias.FormattingEnabled = true;
            this.CombFamilias.Location = new System.Drawing.Point(0, 54);
            this.CombFamilias.Name = "CombFamilias";
            this.CombFamilias.Size = new System.Drawing.Size(356, 21);
            this.CombFamilias.TabIndex = 4;
            this.CombFamilias.SelectedIndexChanged += new System.EventHandler(this.CombFamilias_SelectedIndexChanged);
            // 
            // LblCurrentFamilias
            // 
            this.LblCurrentFamilias.AutoSize = true;
            this.LblCurrentFamilias.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblCurrentFamilias.Location = new System.Drawing.Point(0, 38);
            this.LblCurrentFamilias.Name = "LblCurrentFamilias";
            this.LblCurrentFamilias.Size = new System.Drawing.Size(87, 13);
            this.LblCurrentFamilias.TabIndex = 3;
            this.LblCurrentFamilias.Text = "Familias actuales";
            // 
            // BtnSaveCrear
            // 
            this.BtnSaveCrear.Location = new System.Drawing.Point(43, 278);
            this.BtnSaveCrear.Name = "BtnSaveCrear";
            this.BtnSaveCrear.Size = new System.Drawing.Size(246, 48);
            this.BtnSaveCrear.TabIndex = 2;
            this.BtnSaveCrear.Text = "Guardar";
            this.BtnSaveCrear.UseVisualStyleBackColor = true;
            this.BtnSaveCrear.Click += new System.EventHandler(this.BtnSaveCrear_Click);
            // 
            // TxtNombreFamilia
            // 
            this.TxtNombreFamilia.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtNombreFamilia.Location = new System.Drawing.Point(0, 18);
            this.TxtNombreFamilia.Margin = new System.Windows.Forms.Padding(10);
            this.TxtNombreFamilia.Name = "TxtNombreFamilia";
            this.TxtNombreFamilia.Size = new System.Drawing.Size(358, 20);
            this.TxtNombreFamilia.TabIndex = 0;
            // 
            // LblNombreFamilia
            // 
            this.LblNombreFamilia.AutoSize = true;
            this.LblNombreFamilia.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblNombreFamilia.Location = new System.Drawing.Point(0, 0);
            this.LblNombreFamilia.Margin = new System.Windows.Forms.Padding(10);
            this.LblNombreFamilia.Name = "LblNombreFamilia";
            this.LblNombreFamilia.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.LblNombreFamilia.Size = new System.Drawing.Size(124, 18);
            this.LblNombreFamilia.TabIndex = 1;
            this.LblNombreFamilia.Text = "Nombre de nueva familia";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.CheckListPatentesParaFamilias);
            this.panel5.Controls.Add(this.LblListaPatentes);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(358, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(287, 494);
            this.panel5.TabIndex = 8;
            // 
            // CheckListPatentesParaFamilias
            // 
            this.CheckListPatentesParaFamilias.Dock = System.Windows.Forms.DockStyle.Top;
            this.CheckListPatentesParaFamilias.FormattingEnabled = true;
            this.CheckListPatentesParaFamilias.Location = new System.Drawing.Point(0, 13);
            this.CheckListPatentesParaFamilias.Name = "CheckListPatentesParaFamilias";
            this.CheckListPatentesParaFamilias.Size = new System.Drawing.Size(287, 289);
            this.CheckListPatentesParaFamilias.TabIndex = 2;
            // 
            // LblListaPatentes
            // 
            this.LblListaPatentes.AutoSize = true;
            this.LblListaPatentes.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblListaPatentes.Location = new System.Drawing.Point(0, 0);
            this.LblListaPatentes.Name = "LblListaPatentes";
            this.LblListaPatentes.Size = new System.Drawing.Size(74, 13);
            this.LblListaPatentes.TabIndex = 3;
            this.LblListaPatentes.Text = "Lista Patentes";
            // 
            // FrmCrearFamilia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 494);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Name = "FrmCrearFamilia";
            this.Text = "CrearFamilia";
            this.Load += new System.EventHandler(this.FrmCrearFamilia_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox CombFamilias;
        private System.Windows.Forms.Label LblCurrentFamilias;
        private System.Windows.Forms.Button BtnSaveCrear;
        private System.Windows.Forms.TextBox TxtNombreFamilia;
        private System.Windows.Forms.Label LblNombreFamilia;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckedListBox CheckListPatentesParaFamilias;
        private System.Windows.Forms.Label LblListaPatentes;
    }
}
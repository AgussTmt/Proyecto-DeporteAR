namespace WinUI.WinForms.Gestiones
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
            this.TbconUserList = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DgvListaUsuarios = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.BtnCreateFamilia = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LblSearchUser = new System.Windows.Forms.Label();
            this.BtnAsignarFamilia = new System.Windows.Forms.Button();
            this.BtnUpdateFamilia = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TbconUserList.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListaUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // TbconUserList
            // 
            this.TbconUserList.Controls.Add(this.tabPage1);
            this.TbconUserList.Controls.Add(this.tabPage2);
            this.TbconUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbconUserList.Location = new System.Drawing.Point(0, 0);
            this.TbconUserList.Name = "TbconUserList";
            this.TbconUserList.SelectedIndex = 0;
            this.TbconUserList.Size = new System.Drawing.Size(989, 543);
            this.TbconUserList.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Controls.Add(this.BtnAsignarFamilia);
            this.tabPage1.Controls.Add(this.BtnUpdateFamilia);
            this.tabPage1.Controls.Add(this.LblSearchUser);
            this.tabPage1.Controls.Add(this.BtnCreateFamilia);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.DgvListaUsuarios);
            this.tabPage1.Controls.Add(this.BtnSearch);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(981, 517);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DgvListaUsuarios
            // 
            this.DgvListaUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvListaUsuarios.Location = new System.Drawing.Point(48, 112);
            this.DgvListaUsuarios.Name = "DgvListaUsuarios";
            this.DgvListaUsuarios.Size = new System.Drawing.Size(795, 375);
            this.DgvListaUsuarios.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(974, 524);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(741, 74);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(102, 32);
            this.BtnSearch.TabIndex = 2;
            this.BtnSearch.Text = "Buscar";
            this.BtnSearch.UseVisualStyleBackColor = true;
            // 
            // BtnCreateFamilia
            // 
            this.BtnCreateFamilia.Location = new System.Drawing.Point(854, 208);
            this.BtnCreateFamilia.Name = "BtnCreateFamilia";
            this.BtnCreateFamilia.Size = new System.Drawing.Size(127, 42);
            this.BtnCreateFamilia.TabIndex = 4;
            this.BtnCreateFamilia.Text = "Crear familia";
            this.BtnCreateFamilia.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(48, 86);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(685, 20);
            this.textBox1.TabIndex = 0;
            // 
            // LblSearchUser
            // 
            this.LblSearchUser.AutoSize = true;
            this.LblSearchUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchUser.Location = new System.Drawing.Point(45, 66);
            this.LblSearchUser.Name = "LblSearchUser";
            this.LblSearchUser.Size = new System.Drawing.Size(96, 17);
            this.LblSearchUser.TabIndex = 1;
            this.LblSearchUser.Text = "Buscar usuario:";
            // 
            // BtnAsignarFamilia
            // 
            this.BtnAsignarFamilia.Location = new System.Drawing.Point(858, 256);
            this.BtnAsignarFamilia.Name = "BtnAsignarFamilia";
            this.BtnAsignarFamilia.Size = new System.Drawing.Size(127, 42);
            this.BtnAsignarFamilia.TabIndex = 6;
            this.BtnAsignarFamilia.Text = "Asignar familia";
            this.BtnAsignarFamilia.UseVisualStyleBackColor = true;
            // 
            // BtnUpdateFamilia
            // 
            this.BtnUpdateFamilia.Location = new System.Drawing.Point(849, 160);
            this.BtnUpdateFamilia.Name = "BtnUpdateFamilia";
            this.BtnUpdateFamilia.Size = new System.Drawing.Size(127, 42);
            this.BtnUpdateFamilia.TabIndex = 5;
            this.BtnUpdateFamilia.Text = "Modificar familia";
            this.BtnUpdateFamilia.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(763, 409);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 543);
            this.Controls.Add(this.TbconUserList);
            this.Name = "FrmSettings";
            this.Text = "FrmSettings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.TbconUserList.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListaUsuarios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TbconUserList;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView DgvListaUsuarios;
        private System.Windows.Forms.Label LblSearchUser;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BtnUpdateFamilia;
        private System.Windows.Forms.Button BtnAsignarFamilia;
        private System.Windows.Forms.Button BtnCreateFamilia;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
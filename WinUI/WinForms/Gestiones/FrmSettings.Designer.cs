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
            this.TabPageList = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnUpdatePatente = new System.Windows.Forms.Button();
            this.BtnCreateFamilia = new System.Windows.Forms.Button();
            this.BtnModificarFamilia = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.LblSearchUser = new System.Windows.Forms.Label();
            this.DgvListaUsuarios = new System.Windows.Forms.DataGridView();
            this.TabPageCrearFamilia = new System.Windows.Forms.TabPage();
            this.TabPageModificarPermisos = new System.Windows.Forms.TabPage();
            this.TxtNombreFamilia = new System.Windows.Forms.TextBox();
            this.LblNombreFamilia = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.LblListaPatentes = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.TbconUserList.SuspendLayout();
            this.TabPageList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListaUsuarios)).BeginInit();
            this.TabPageCrearFamilia.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // TbconUserList
            // 
            this.TbconUserList.Controls.Add(this.TabPageList);
            this.TbconUserList.Controls.Add(this.TabPageCrearFamilia);
            this.TbconUserList.Controls.Add(this.TabPageModificarPermisos);
            this.TbconUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbconUserList.Location = new System.Drawing.Point(0, 0);
            this.TbconUserList.Name = "TbconUserList";
            this.TbconUserList.SelectedIndex = 0;
            this.TbconUserList.Size = new System.Drawing.Size(996, 492);
            this.TbconUserList.TabIndex = 0;
            // 
            // TabPageList
            // 
            this.TabPageList.Controls.Add(this.tableLayoutPanel1);
            this.TabPageList.Location = new System.Drawing.Point(4, 22);
            this.TabPageList.Name = "TabPageList";
            this.TabPageList.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageList.Size = new System.Drawing.Size(988, 466);
            this.TabPageList.TabIndex = 0;
            this.TabPageList.Text = "tabPage1";
            this.TabPageList.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.53275F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.46725F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DgvListaUsuarios, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.41791F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.58209F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(982, 460);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnUpdatePatente);
            this.panel2.Controls.Add(this.BtnModificarFamilia);
            this.panel2.Controls.Add(this.BtnCreateFamilia);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(813, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(166, 379);
            this.panel2.TabIndex = 8;
            // 
            // BtnUpdatePatente
            // 
            this.BtnUpdatePatente.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnUpdatePatente.Location = new System.Drawing.Point(0, 84);
            this.BtnUpdatePatente.Name = "BtnUpdatePatente";
            this.BtnUpdatePatente.Size = new System.Drawing.Size(166, 42);
            this.BtnUpdatePatente.TabIndex = 5;
            this.BtnUpdatePatente.Text = "Modificar Patente";
            this.BtnUpdatePatente.UseVisualStyleBackColor = true;
            // 
            // BtnCreateFamilia
            // 
            this.BtnCreateFamilia.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCreateFamilia.Location = new System.Drawing.Point(0, 0);
            this.BtnCreateFamilia.Name = "BtnCreateFamilia";
            this.BtnCreateFamilia.Size = new System.Drawing.Size(166, 42);
            this.BtnCreateFamilia.TabIndex = 4;
            this.BtnCreateFamilia.Text = "Crear familia";
            this.BtnCreateFamilia.UseVisualStyleBackColor = true;
            this.BtnCreateFamilia.Click += new System.EventHandler(this.BtnCreateFamilia_Click);
            // 
            // BtnModificarFamilia
            // 
            this.BtnModificarFamilia.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnModificarFamilia.Location = new System.Drawing.Point(0, 42);
            this.BtnModificarFamilia.Name = "BtnModificarFamilia";
            this.BtnModificarFamilia.Size = new System.Drawing.Size(166, 42);
            this.BtnModificarFamilia.TabIndex = 6;
            this.BtnModificarFamilia.Text = "ModificarFamilia";
            this.BtnModificarFamilia.UseVisualStyleBackColor = true;
            this.BtnModificarFamilia.Click += new System.EventHandler(this.BtnAsignarFamilia_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.TxtSearch);
            this.panel1.Controls.Add(this.LblSearchUser);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 69);
            this.panel1.TabIndex = 8;
            // 
            // TxtSearch
            // 
            this.TxtSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtSearch.Location = new System.Drawing.Point(0, 49);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(804, 20);
            this.TxtSearch.TabIndex = 0;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // LblSearchUser
            // 
            this.LblSearchUser.AutoSize = true;
            this.LblSearchUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSearchUser.Location = new System.Drawing.Point(2, 29);
            this.LblSearchUser.Name = "LblSearchUser";
            this.LblSearchUser.Size = new System.Drawing.Size(96, 17);
            this.LblSearchUser.TabIndex = 1;
            this.LblSearchUser.Text = "Buscar usuario:";
            // 
            // DgvListaUsuarios
            // 
            this.DgvListaUsuarios.AllowUserToAddRows = false;
            this.DgvListaUsuarios.AllowUserToDeleteRows = false;
            this.DgvListaUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvListaUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvListaUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvListaUsuarios.Location = new System.Drawing.Point(3, 78);
            this.DgvListaUsuarios.Name = "DgvListaUsuarios";
            this.DgvListaUsuarios.ReadOnly = true;
            this.DgvListaUsuarios.Size = new System.Drawing.Size(804, 379);
            this.DgvListaUsuarios.TabIndex = 3;
            // 
            // TabPageCrearFamilia
            // 
            this.TabPageCrearFamilia.Controls.Add(this.panel5);
            this.TabPageCrearFamilia.Controls.Add(this.panel4);
            this.TabPageCrearFamilia.Location = new System.Drawing.Point(4, 22);
            this.TabPageCrearFamilia.Name = "TabPageCrearFamilia";
            this.TabPageCrearFamilia.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageCrearFamilia.Size = new System.Drawing.Size(988, 466);
            this.TabPageCrearFamilia.TabIndex = 1;
            this.TabPageCrearFamilia.Text = "Crear Familia";
            this.TabPageCrearFamilia.UseVisualStyleBackColor = true;
            // 
            // TabPageModificarPermisos
            // 
            this.TabPageModificarPermisos.Location = new System.Drawing.Point(4, 22);
            this.TabPageModificarPermisos.Name = "TabPageModificarPermisos";
            this.TabPageModificarPermisos.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageModificarPermisos.Size = new System.Drawing.Size(988, 466);
            this.TabPageModificarPermisos.TabIndex = 2;
            this.TabPageModificarPermisos.Text = "ModificarPermisos";
            this.TabPageModificarPermisos.UseVisualStyleBackColor = true;
            // 
            // TxtNombreFamilia
            // 
            this.TxtNombreFamilia.Dock = System.Windows.Forms.DockStyle.Left;
            this.TxtNombreFamilia.Location = new System.Drawing.Point(0, 18);
            this.TxtNombreFamilia.Margin = new System.Windows.Forms.Padding(10);
            this.TxtNombreFamilia.Name = "TxtNombreFamilia";
            this.TxtNombreFamilia.Size = new System.Drawing.Size(266, 20);
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
            this.LblNombreFamilia.Size = new System.Drawing.Size(102, 18);
            this.LblNombreFamilia.TabIndex = 1;
            this.LblNombreFamilia.Text = "Nombre de la familia";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(0, 13);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(287, 439);
            this.checkedListBox1.TabIndex = 2;
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
            // panel3
            // 
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.TxtNombreFamilia);
            this.panel3.Controls.Add(this.LblNombreFamilia);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(356, 460);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(356, 460);
            this.panel4.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(246, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.checkedListBox1);
            this.panel5.Controls.Add(this.LblListaPatentes);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(359, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(287, 460);
            this.panel5.TabIndex = 7;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 492);
            this.Controls.Add(this.TbconUserList);
            this.Name = "FrmSettings";
            this.Text = "FrmSettings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.TbconUserList.ResumeLayout(false);
            this.TabPageList.ResumeLayout(false);
            this.TabPageList.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListaUsuarios)).EndInit();
            this.TabPageCrearFamilia.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TbconUserList;
        private System.Windows.Forms.TabPage TabPageCrearFamilia;
        private System.Windows.Forms.TabPage TabPageList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView DgvListaUsuarios;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Button BtnModificarFamilia;
        private System.Windows.Forms.Button BtnUpdatePatente;
        private System.Windows.Forms.Label LblSearchUser;
        private System.Windows.Forms.Button BtnCreateFamilia;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage TabPageModificarPermisos;
        private System.Windows.Forms.Label LblNombreFamilia;
        private System.Windows.Forms.TextBox TxtNombreFamilia;
        private System.Windows.Forms.Label LblListaPatentes;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button1;
    }
}
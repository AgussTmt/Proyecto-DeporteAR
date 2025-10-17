namespace WinUI.WinForms.Gestiones
{
    partial class FrmUserManagment
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.LblSearchUser = new System.Windows.Forms.Label();
            this.DgvListaUsuarios = new System.Windows.Forms.DataGridView();
            this.TabPageCrearFamilia = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CheckListPatentesParaFamilias = new System.Windows.Forms.CheckedListBox();
            this.LblListaPatentes = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.CombFamilias = new System.Windows.Forms.ComboBox();
            this.LblCurrentFamilias = new System.Windows.Forms.Label();
            this.BtnSaveCrear = new System.Windows.Forms.Button();
            this.TxtNombreFamilia = new System.Windows.Forms.TextBox();
            this.LblNombreFamilia = new System.Windows.Forms.Label();
            this.TabPageModificarPermisos = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.LstPatenteDeFamilia = new System.Windows.Forms.ListBox();
            this.LblPatenteDeFamilia = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.CheckListFamilias = new System.Windows.Forms.CheckedListBox();
            this.lblListaFamilias = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.CheckListPatentes = new System.Windows.Forms.CheckedListBox();
            this.LblPermisosLista = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.BtnSaveModificarPermiso = new System.Windows.Forms.Button();
            this.TxtUsuario = new System.Windows.Forms.TextBox();
            this.LblUsuario = new System.Windows.Forms.Label();
            this.TbconUserList.SuspendLayout();
            this.TabPageList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListaUsuarios)).BeginInit();
            this.TabPageCrearFamilia.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.TabPageModificarPermisos.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // TbconUserList
            // 
            this.TbconUserList.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TbconUserList.Controls.Add(this.TabPageList);
            this.TbconUserList.Controls.Add(this.TabPageCrearFamilia);
            this.TbconUserList.Controls.Add(this.TabPageModificarPermisos);
            this.TbconUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TbconUserList.ItemSize = new System.Drawing.Size(96, 56);
            this.TbconUserList.Location = new System.Drawing.Point(0, 0);
            this.TbconUserList.Name = "TbconUserList";
            this.TbconUserList.SelectedIndex = 0;
            this.TbconUserList.Size = new System.Drawing.Size(1120, 683);
            this.TbconUserList.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TbconUserList.TabIndex = 0;
            // 
            // TabPageList
            // 
            this.TabPageList.Controls.Add(this.tableLayoutPanel1);
            this.TabPageList.Location = new System.Drawing.Point(4, 60);
            this.TabPageList.Name = "TabPageList";
            this.TabPageList.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageList.Size = new System.Drawing.Size(1112, 619);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1106, 613);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnUpdatePatente);
            this.panel2.Controls.Add(this.BtnCreateFamilia);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(915, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(154, 507);
            this.panel2.TabIndex = 8;
            // 
            // BtnUpdatePatente
            // 
            this.BtnUpdatePatente.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnUpdatePatente.Location = new System.Drawing.Point(0, 42);
            this.BtnUpdatePatente.Name = "BtnUpdatePatente";
            this.BtnUpdatePatente.Size = new System.Drawing.Size(154, 42);
            this.BtnUpdatePatente.TabIndex = 5;
            this.BtnUpdatePatente.Text = "Modificar permisos";
            this.BtnUpdatePatente.UseVisualStyleBackColor = true;
            this.BtnUpdatePatente.Click += new System.EventHandler(this.BtnUpdatePatente_Click);
            // 
            // BtnCreateFamilia
            // 
            this.BtnCreateFamilia.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCreateFamilia.Location = new System.Drawing.Point(0, 0);
            this.BtnCreateFamilia.Name = "BtnCreateFamilia";
            this.BtnCreateFamilia.Size = new System.Drawing.Size(154, 42);
            this.BtnCreateFamilia.TabIndex = 4;
            this.BtnCreateFamilia.Text = "Crear y modificar familia";
            this.BtnCreateFamilia.UseVisualStyleBackColor = true;
            this.BtnCreateFamilia.Click += new System.EventHandler(this.BtnCreateFamilia_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.TxtSearch);
            this.panel1.Controls.Add(this.LblSearchUser);
            this.panel1.Location = new System.Drawing.Point(3, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 69);
            this.panel1.TabIndex = 8;
            // 
            // TxtSearch
            // 
            this.TxtSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtSearch.Location = new System.Drawing.Point(0, 49);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(750, 20);
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
            this.DgvListaUsuarios.Location = new System.Drawing.Point(3, 103);
            this.DgvListaUsuarios.Name = "DgvListaUsuarios";
            this.DgvListaUsuarios.ReadOnly = true;
            this.DgvListaUsuarios.Size = new System.Drawing.Size(906, 507);
            this.DgvListaUsuarios.TabIndex = 3;
            // 
            // TabPageCrearFamilia
            // 
            this.TabPageCrearFamilia.Controls.Add(this.panel5);
            this.TabPageCrearFamilia.Controls.Add(this.panel4);
            this.TabPageCrearFamilia.Location = new System.Drawing.Point(4, 60);
            this.TabPageCrearFamilia.Name = "TabPageCrearFamilia";
            this.TabPageCrearFamilia.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageCrearFamilia.Size = new System.Drawing.Size(1112, 619);
            this.TabPageCrearFamilia.TabIndex = 1;
            this.TabPageCrearFamilia.Text = "Crear Familia";
            this.TabPageCrearFamilia.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.CheckListPatentesParaFamilias);
            this.panel5.Controls.Add(this.LblListaPatentes);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(359, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(287, 613);
            this.panel5.TabIndex = 7;
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
            // panel4
            // 
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(356, 613);
            this.panel4.TabIndex = 6;
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
            this.panel3.Size = new System.Drawing.Size(356, 613);
            this.panel3.TabIndex = 5;
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
            this.TxtNombreFamilia.Size = new System.Drawing.Size(356, 20);
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
            // TabPageModificarPermisos
            // 
            this.TabPageModificarPermisos.Controls.Add(this.panel9);
            this.TabPageModificarPermisos.Controls.Add(this.panel7);
            this.TabPageModificarPermisos.Controls.Add(this.panel8);
            this.TabPageModificarPermisos.Controls.Add(this.panel6);
            this.TabPageModificarPermisos.Location = new System.Drawing.Point(4, 60);
            this.TabPageModificarPermisos.Name = "TabPageModificarPermisos";
            this.TabPageModificarPermisos.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageModificarPermisos.Size = new System.Drawing.Size(1112, 619);
            this.TabPageModificarPermisos.TabIndex = 2;
            this.TabPageModificarPermisos.Text = "ModificarPermisos";
            this.TabPageModificarPermisos.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.LstPatenteDeFamilia);
            this.panel9.Controls.Add(this.LblPatenteDeFamilia);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(787, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(235, 613);
            this.panel9.TabIndex = 5;
            // 
            // LstPatenteDeFamilia
            // 
            this.LstPatenteDeFamilia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstPatenteDeFamilia.FormattingEnabled = true;
            this.LstPatenteDeFamilia.Location = new System.Drawing.Point(0, 13);
            this.LstPatenteDeFamilia.Name = "LstPatenteDeFamilia";
            this.LstPatenteDeFamilia.Size = new System.Drawing.Size(235, 600);
            this.LstPatenteDeFamilia.TabIndex = 1;
            // 
            // LblPatenteDeFamilia
            // 
            this.LblPatenteDeFamilia.AutoSize = true;
            this.LblPatenteDeFamilia.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblPatenteDeFamilia.Location = new System.Drawing.Point(0, 0);
            this.LblPatenteDeFamilia.Name = "LblPatenteDeFamilia";
            this.LblPatenteDeFamilia.Size = new System.Drawing.Size(94, 13);
            this.LblPatenteDeFamilia.TabIndex = 0;
            this.LblPatenteDeFamilia.Text = "Patente de Familia";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.CheckListFamilias);
            this.panel7.Controls.Add(this.lblListaFamilias);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(502, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(285, 613);
            this.panel7.TabIndex = 3;
            // 
            // CheckListFamilias
            // 
            this.CheckListFamilias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckListFamilias.FormattingEnabled = true;
            this.CheckListFamilias.Location = new System.Drawing.Point(0, 13);
            this.CheckListFamilias.Name = "CheckListFamilias";
            this.CheckListFamilias.Size = new System.Drawing.Size(285, 600);
            this.CheckListFamilias.TabIndex = 5;
            // 
            // lblListaFamilias
            // 
            this.lblListaFamilias.AutoSize = true;
            this.lblListaFamilias.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblListaFamilias.Location = new System.Drawing.Point(0, 0);
            this.lblListaFamilias.Name = "lblListaFamilias";
            this.lblListaFamilias.Size = new System.Drawing.Size(66, 13);
            this.lblListaFamilias.TabIndex = 0;
            this.lblListaFamilias.Text = "Lista familias";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.CheckListPatentes);
            this.panel8.Controls.Add(this.LblPermisosLista);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(217, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(285, 613);
            this.panel8.TabIndex = 4;
            // 
            // CheckListPatentes
            // 
            this.CheckListPatentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckListPatentes.FormattingEnabled = true;
            this.CheckListPatentes.Location = new System.Drawing.Point(0, 13);
            this.CheckListPatentes.Name = "CheckListPatentes";
            this.CheckListPatentes.Size = new System.Drawing.Size(285, 600);
            this.CheckListPatentes.TabIndex = 6;
            // 
            // LblPermisosLista
            // 
            this.LblPermisosLista.AutoSize = true;
            this.LblPermisosLista.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblPermisosLista.Location = new System.Drawing.Point(0, 0);
            this.LblPermisosLista.Name = "LblPermisosLista";
            this.LblPermisosLista.Size = new System.Drawing.Size(73, 13);
            this.LblPermisosLista.TabIndex = 5;
            this.LblPermisosLista.Text = "Lista permisos";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.BtnSaveModificarPermiso);
            this.panel6.Controls.Add(this.TxtUsuario);
            this.panel6.Controls.Add(this.LblUsuario);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(214, 613);
            this.panel6.TabIndex = 2;
            // 
            // BtnSaveModificarPermiso
            // 
            this.BtnSaveModificarPermiso.Location = new System.Drawing.Point(22, 455);
            this.BtnSaveModificarPermiso.Name = "BtnSaveModificarPermiso";
            this.BtnSaveModificarPermiso.Size = new System.Drawing.Size(159, 38);
            this.BtnSaveModificarPermiso.TabIndex = 5;
            this.BtnSaveModificarPermiso.Text = "Guardar";
            this.BtnSaveModificarPermiso.UseVisualStyleBackColor = true;
            this.BtnSaveModificarPermiso.Click += new System.EventHandler(this.BtnSaveModificarPermiso_Click);
            // 
            // TxtUsuario
            // 
            this.TxtUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtUsuario.Location = new System.Drawing.Point(0, 13);
            this.TxtUsuario.Name = "TxtUsuario";
            this.TxtUsuario.Size = new System.Drawing.Size(214, 20);
            this.TxtUsuario.TabIndex = 1;
            // 
            // LblUsuario
            // 
            this.LblUsuario.AutoSize = true;
            this.LblUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblUsuario.Location = new System.Drawing.Point(0, 0);
            this.LblUsuario.Name = "LblUsuario";
            this.LblUsuario.Size = new System.Drawing.Size(43, 13);
            this.LblUsuario.TabIndex = 0;
            this.LblUsuario.Text = "Usuario";
            // 
            // FrmUserManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 683);
            this.Controls.Add(this.TbconUserList);
            this.Name = "FrmUserManagment";
            this.Text = "FrmUserManagment";
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
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.TabPageModificarPermisos.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TbconUserList;
        private System.Windows.Forms.TabPage TabPageCrearFamilia;
        private System.Windows.Forms.TabPage TabPageList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView DgvListaUsuarios;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Button BtnUpdatePatente;
        private System.Windows.Forms.Label LblSearchUser;
        private System.Windows.Forms.Button BtnCreateFamilia;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage TabPageModificarPermisos;
        private System.Windows.Forms.Label LblNombreFamilia;
        private System.Windows.Forms.Label LblListaPatentes;
        private System.Windows.Forms.CheckedListBox CheckListPatentesParaFamilias;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button BtnSaveCrear;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox TxtUsuario;
        private System.Windows.Forms.Label LblUsuario;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label LblPermisosLista;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblListaFamilias;
        private System.Windows.Forms.CheckedListBox CheckListPatentes;
        private System.Windows.Forms.CheckedListBox CheckListFamilias;
        private System.Windows.Forms.Button BtnSaveModificarPermiso;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label LblPatenteDeFamilia;
        private System.Windows.Forms.ListBox LstPatenteDeFamilia;
        private System.Windows.Forms.ComboBox CombFamilias;
        private System.Windows.Forms.Label LblCurrentFamilias;
        private System.Windows.Forms.TextBox TxtNombreFamilia;
    }
}
namespace WinUI.WinForms.Gestiones.UserManagment
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnCrearUsuario = new System.Windows.Forms.Button();
            this.BtnUpdatePatente = new System.Windows.Forms.Button();
            this.BtnCreateFamilia = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.LblSearchUser = new System.Windows.Forms.Label();
            this.DgvListaUsuarios = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListaUsuarios)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.41791F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.58209F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1015, 611);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnCrearUsuario);
            this.panel2.Controls.Add(this.BtnUpdatePatente);
            this.panel2.Controls.Add(this.BtnCreateFamilia);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(840, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(147, 505);
            this.panel2.TabIndex = 8;
            // 
            // BtnCrearUsuario
            // 
            this.BtnCrearUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnCrearUsuario.Location = new System.Drawing.Point(0, 84);
            this.BtnCrearUsuario.Name = "BtnCrearUsuario";
            this.BtnCrearUsuario.Size = new System.Drawing.Size(147, 42);
            this.BtnCrearUsuario.TabIndex = 6;
            this.BtnCrearUsuario.Text = "Crear usuario";
            this.BtnCrearUsuario.UseVisualStyleBackColor = true;
            this.BtnCrearUsuario.Click += new System.EventHandler(this.BtnCrearUsuario_Click);
            // 
            // BtnUpdatePatente
            // 
            this.BtnUpdatePatente.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnUpdatePatente.Location = new System.Drawing.Point(0, 42);
            this.BtnUpdatePatente.Name = "BtnUpdatePatente";
            this.BtnUpdatePatente.Size = new System.Drawing.Size(147, 42);
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
            this.BtnCreateFamilia.Size = new System.Drawing.Size(147, 42);
            this.BtnCreateFamilia.TabIndex = 4;
            this.BtnCreateFamilia.Text = "Crear y/o modificar familia";
            this.BtnCreateFamilia.UseVisualStyleBackColor = true;
            this.BtnCreateFamilia.Click += new System.EventHandler(this.BtnCreateFamilia_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.TxtSearch);
            this.panel1.Controls.Add(this.LblSearchUser);
            this.panel1.Location = new System.Drawing.Point(3, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(654, 67);
            this.panel1.TabIndex = 8;
            // 
            // TxtSearch
            // 
            this.TxtSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtSearch.Location = new System.Drawing.Point(0, 47);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(654, 20);
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
            this.DgvListaUsuarios.Size = new System.Drawing.Size(831, 505);
            this.DgvListaUsuarios.TabIndex = 3;
            // 
            // FrmUserManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 611);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmUserManagment";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmUserManagment_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvListaUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BtnCrearUsuario;
        private System.Windows.Forms.Button BtnUpdatePatente;
        private System.Windows.Forms.Button BtnCreateFamilia;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Label LblSearchUser;
        private System.Windows.Forms.DataGridView DgvListaUsuarios;
    }
}
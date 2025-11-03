namespace WinUI.WinForms.Gestiones
{
    partial class FrmRegistrar
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
            this.LblNombre = new System.Windows.Forms.Label();
            this.LblEmail = new System.Windows.Forms.Label();
            this.LblContraseña = new System.Windows.Forms.Label();
            this.TxtNombre = new System.Windows.Forms.TextBox();
            this.TxtContraseña = new System.Windows.Forms.TextBox();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnGuardarYPermisos = new System.Windows.Forms.Button();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelBotones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblNombre
            // 
            this.LblNombre.AutoSize = true;
            this.LblNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblNombre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblNombre.Location = new System.Drawing.Point(3, 0);
            this.LblNombre.Name = "LblNombre";
            this.LblNombre.Size = new System.Drawing.Size(94, 40);
            this.LblNombre.TabIndex = 0;
            this.LblNombre.Text = "Nombre:";
            this.LblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblEmail
            // 
            this.LblEmail.AutoSize = true;
            this.LblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblEmail.Location = new System.Drawing.Point(3, 40);
            this.LblEmail.Name = "LblEmail";
            this.LblEmail.Size = new System.Drawing.Size(94, 40);
            this.LblEmail.TabIndex = 1;
            this.LblEmail.Text = "Email (Login):";
            this.LblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblContraseña
            // 
            this.LblContraseña.AutoSize = true;
            this.LblContraseña.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblContraseña.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblContraseña.Location = new System.Drawing.Point(3, 80);
            this.LblContraseña.Name = "LblContraseña";
            this.LblContraseña.Size = new System.Drawing.Size(94, 40);
            this.LblContraseña.TabIndex = 2;
            this.LblContraseña.Text = "Contraseña:";
            this.LblContraseña.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtNombre
            // 
            this.TxtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtNombre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtNombre.Location = new System.Drawing.Point(103, 8);
            this.TxtNombre.Name = "TxtNombre";
            this.TxtNombre.Size = new System.Drawing.Size(306, 23);
            this.TxtNombre.TabIndex = 0;
            // 
            // TxtContraseña
            // 
            this.TxtContraseña.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtContraseña.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtContraseña.Location = new System.Drawing.Point(103, 88);
            this.TxtContraseña.Name = "TxtContraseña";
            this.TxtContraseña.PasswordChar = '*';
            this.TxtContraseña.Size = new System.Drawing.Size(306, 23);
            this.TxtContraseña.TabIndex = 2;
            // 
            // TxtEmail
            // 
            this.TxtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtEmail.Location = new System.Drawing.Point(103, 48);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(306, 23);
            this.TxtEmail.TabIndex = 1;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Location = new System.Drawing.Point(12, 13);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 34);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar y Volver";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnGuardarYPermisos
            // 
            this.btnGuardarYPermisos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnGuardarYPermisos.Location = new System.Drawing.Point(138, 13);
            this.btnGuardarYPermisos.Name = "btnGuardarYPermisos";
            this.btnGuardarYPermisos.Size = new System.Drawing.Size(160, 34);
            this.btnGuardarYPermisos.TabIndex = 4;
            this.btnGuardarYPermisos.Text = "Guardar y Asignar Permisos";
            this.btnGuardarYPermisos.UseVisualStyleBackColor = true;
            this.btnGuardarYPermisos.Click += new System.EventHandler(this.btnGuardarYPermisos_Click);
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Controls.Add(this.btnGuardarYPermisos);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 353);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(460, 60);
            this.panelBotones.TabIndex = 9;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancelar.Location = new System.Drawing.Point(358, 13);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 34);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.groupBox1.Size = new System.Drawing.Size(436, 335);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Nuevo Usuario";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.LblNombre, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TxtNombre, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LblEmail, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.TxtEmail, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LblContraseña, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.TxtContraseña, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 36);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            // --- ¡¡INICIO DEL ARREGLO!! ---
            // Aumentamos la altura de las filas de 30 a 40
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 122);
            // --- FIN DEL ARREGLO!! ---
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // FrmRegistrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(460, 413);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelBotones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmRegistrar";
            this.Text = "Crear Nuevo Usuario";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRegistrar_FormClosing);
            this.Load += new System.EventHandler(this.Registro_Load);
            this.panelBotones.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblNombre;
        private System.Windows.Forms.Label LblEmail;
        private System.Windows.Forms.Label LblContraseña;
        private System.Windows.Forms.TextBox TxtNombre;
        private System.Windows.Forms.TextBox TxtContraseña;
        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnGuardarYPermisos;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
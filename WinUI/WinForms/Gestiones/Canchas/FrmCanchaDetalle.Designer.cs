namespace WinUI.WinForms.Gestiones.Canchas
{
    partial class FrmCanchaDetalle
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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.numCapacidad = new System.Windows.Forms.NumericUpDown();
            this.cmbDeporte = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvDisponibilidad = new System.Windows.Forms.DataGridView();
            this.colSeleccionDia = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDiaNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoraInicio = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colHoraFin = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.NumDuracionXPartido = new System.Windows.Forms.NumericUpDown();
            this.LblDuracionXPartido = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxDisponibilidad = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisponibilidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumDuracionXPartido)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxDisponibilidad.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNombre.Location = new System.Drawing.Point(123, 3);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(196, 23);
            this.txtNombre.TabIndex = 0;
            // 
            // txtPrecio
            // 
            this.txtPrecio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrecio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPrecio.Location = new System.Drawing.Point(123, 63);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(196, 23);
            this.txtPrecio.TabIndex = 2;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Location = new System.Drawing.Point(546, 13);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 34);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnCancelar.Location = new System.Drawing.Point(662, 13);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(90, 34);
            this.BtnCancelar.TabIndex = 6;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // numCapacidad
            // 
            this.numCapacidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numCapacidad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numCapacidad.Location = new System.Drawing.Point(123, 93);
            this.numCapacidad.Name = "numCapacidad";
            this.numCapacidad.Size = new System.Drawing.Size(196, 23);
            this.numCapacidad.TabIndex = 3;
            // 
            // cmbDeporte
            // 
            this.cmbDeporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDeporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeporte.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbDeporte.FormattingEnabled = true;
            this.cmbDeporte.Location = new System.Drawing.Point(123, 33);
            this.cmbDeporte.Name = "cmbDeporte";
            this.cmbDeporte.Size = new System.Drawing.Size(196, 23);
            this.cmbDeporte.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 30);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nombre:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 30);
            this.label2.TabIndex = 10;
            this.label2.Text = "Precio ($):";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(3, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 30);
            this.label5.TabIndex = 13;
            this.label5.Text = "Capacidad:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(3, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 30);
            this.label6.TabIndex = 14;
            this.label6.Text = "Deporte:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvDisponibilidad
            // 
            this.dgvDisponibilidad.AllowUserToAddRows = false;
            this.dgvDisponibilidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisponibilidad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSeleccionDia,
            this.colDiaNombre,
            this.colHoraInicio,
            this.colHoraFin});
            this.dgvDisponibilidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDisponibilidad.Location = new System.Drawing.Point(10, 26);
            this.dgvDisponibilidad.Name = "dgvDisponibilidad";
            this.dgvDisponibilidad.RowHeadersVisible = false;
            this.dgvDisponibilidad.Size = new System.Drawing.Size(388, 281);
            this.dgvDisponibilidad.TabIndex = 15;
            // 
            // colSeleccionDia
            // 
            this.colSeleccionDia.HeaderText = "";
            this.colSeleccionDia.Name = "colSeleccionDia";
            this.colSeleccionDia.Width = 30;
            // 
            // colDiaNombre
            // 
            this.colDiaNombre.HeaderText = "Dia";
            this.colDiaNombre.Name = "colDiaNombre";
            this.colDiaNombre.ReadOnly = true;
            this.colDiaNombre.Width = 120;
            // 
            // colHoraInicio
            // 
            this.colHoraInicio.HeaderText = "Desde";
            this.colHoraInicio.Name = "colHoraInicio";
            this.colHoraInicio.Width = 90;
            // 
            // colHoraFin
            // 
            this.colHoraFin.HeaderText = "Hasta";
            this.colHoraFin.Name = "colHoraFin";
            this.colHoraFin.Width = 90;
            // 
            // NumDuracionXPartido
            // 
            this.NumDuracionXPartido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NumDuracionXPartido.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.NumDuracionXPartido.Location = new System.Drawing.Point(123, 123);
            this.NumDuracionXPartido.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.NumDuracionXPartido.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.NumDuracionXPartido.Name = "NumDuracionXPartido";
            this.NumDuracionXPartido.Size = new System.Drawing.Size(196, 23);
            this.NumDuracionXPartido.TabIndex = 4;
            this.NumDuracionXPartido.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // LblDuracionXPartido
            // 
            this.LblDuracionXPartido.AutoSize = true;
            this.LblDuracionXPartido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblDuracionXPartido.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.LblDuracionXPartido.Location = new System.Drawing.Point(3, 120);
            this.LblDuracionXPartido.Name = "LblDuracionXPartido";
            this.LblDuracionXPartido.Size = new System.Drawing.Size(114, 30);
            this.LblDuracionXPartido.TabIndex = 17;
            this.LblDuracionXPartido.Text = "Duración (Min):";
            this.LblDuracionXPartido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelBotones.Controls.Add(this.BtnCancelar);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 335);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(764, 60);
            this.panelBotones.TabIndex = 18;
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxInfo.Location = new System.Drawing.Point(12, 12);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxInfo.Size = new System.Drawing.Size(342, 317);
            this.groupBoxInfo.TabIndex = 19;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Información de la Cancha";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtNombre, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbDeporte, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtPrecio, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.numCapacidad, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.LblDuracionXPartido, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.NumDuracionXPartido, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 26);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(322, 281);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBoxDisponibilidad
            // 
            this.groupBoxDisponibilidad.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDisponibilidad.Controls.Add(this.dgvDisponibilidad);
            this.groupBoxDisponibilidad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxDisponibilidad.Location = new System.Drawing.Point(360, 12);
            this.groupBoxDisponibilidad.Name = "groupBoxDisponibilidad";
            this.groupBoxDisponibilidad.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxDisponibilidad.Size = new System.Drawing.Size(408, 317);
            this.groupBoxDisponibilidad.TabIndex = 20;
            this.groupBoxDisponibilidad.TabStop = false;
            this.groupBoxDisponibilidad.Text = "Disponibilidad Semanal";
            // 
            // FrmCanchaDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(764, 395);
            this.Controls.Add(this.groupBoxDisponibilidad);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.panelBotones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCanchaDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmCanchaDetalle";
            this.Load += new System.EventHandler(this.FrmCanchaDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisponibilidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumDuracionXPartido)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.groupBoxInfo.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxDisponibilidad.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.NumericUpDown numCapacidad;
        private System.Windows.Forms.ComboBox cmbDeporte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvDisponibilidad;
        private System.Windows.Forms.NumericUpDown NumDuracionXPartido;
        private System.Windows.Forms.Label LblDuracionXPartido;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSeleccionDia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiaNombre;
        private System.Windows.Forms.DataGridViewComboBoxColumn colHoraInicio;
        private System.Windows.Forms.DataGridViewComboBoxColumn colHoraFin;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxDisponibilidad;
    }
}
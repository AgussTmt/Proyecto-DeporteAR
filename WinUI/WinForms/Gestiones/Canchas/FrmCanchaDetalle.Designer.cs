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
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisponibilidad)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(31, 36);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(284, 20);
            this.txtNombre.TabIndex = 0;
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(31, 88);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(284, 20);
            this.txtPrecio.TabIndex = 1;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(35, 317);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Location = new System.Drawing.Point(35, 244);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(75, 23);
            this.BtnCancelar.TabIndex = 6;
            this.BtnCancelar.Text = "Cancelar";
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // numCapacidad
            // 
            this.numCapacidad.Location = new System.Drawing.Point(31, 135);
            this.numCapacidad.Name = "numCapacidad";
            this.numCapacidad.Size = new System.Drawing.Size(284, 20);
            this.numCapacidad.TabIndex = 7;
            // 
            // cmbDeporte
            // 
            this.cmbDeporte.FormattingEnabled = true;
            this.cmbDeporte.Location = new System.Drawing.Point(31, 191);
            this.cmbDeporte.Name = "cmbDeporte";
            this.cmbDeporte.Size = new System.Drawing.Size(284, 21);
            this.cmbDeporte.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nombre:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Precio:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Capacidad:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Deporte:";
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
            this.dgvDisponibilidad.Location = new System.Drawing.Point(402, 36);
            this.dgvDisponibilidad.Name = "dgvDisponibilidad";
            this.dgvDisponibilidad.RowHeadersVisible = false;
            this.dgvDisponibilidad.Size = new System.Drawing.Size(334, 220);
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
            // 
            // colHoraInicio
            // 
            this.colHoraInicio.HeaderText = "Desde";
            this.colHoraInicio.Name = "colHoraInicio";
            // 
            // colHoraFin
            // 
            this.colHoraFin.HeaderText = "Hasta";
            this.colHoraFin.Name = "colHoraFin";
            // 
            // FrmCanchaDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 450);
            this.Controls.Add(this.dgvDisponibilidad);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDeporte);
            this.Controls.Add(this.numCapacidad);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.txtNombre);
            this.Name = "FrmCanchaDetalle";
            this.Text = "FrmCanchaDetalle";
            this.Load += new System.EventHandler(this.FrmCanchaDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisponibilidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSeleccionDia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiaNombre;
        private System.Windows.Forms.DataGridViewComboBoxColumn colHoraInicio;
        private System.Windows.Forms.DataGridViewComboBoxColumn colHoraFin;
    }
}
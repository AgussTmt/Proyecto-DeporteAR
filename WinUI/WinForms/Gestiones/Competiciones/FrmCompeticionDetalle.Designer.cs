namespace WinUI.WinForms.Gestiones.Competiciones
{
    partial class FrmCompeticionDetalle
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
            this.txtFranjaHoraria = new System.Windows.Forms.TextBox();
            this.numCupos = new System.Windows.Forms.NumericUpDown();
            this.numCuposMinimos = new System.Windows.Forms.NumericUpDown();
            this.numFrecuencia = new System.Windows.Forms.NumericUpDown();
            this.cmbFormato = new System.Windows.Forms.ComboBox();
            this.cmbCanchaAsignada = new System.Windows.Forms.ComboBox();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lbl1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numCupos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCuposMinimos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrecuencia)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(64, 57);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(202, 20);
            this.txtNombre.TabIndex = 0;
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(65, 102);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(202, 20);
            this.txtPrecio.TabIndex = 1;
            // 
            // txtFranjaHoraria
            // 
            this.txtFranjaHoraria.Location = new System.Drawing.Point(65, 154);
            this.txtFranjaHoraria.Name = "txtFranjaHoraria";
            this.txtFranjaHoraria.Size = new System.Drawing.Size(202, 20);
            this.txtFranjaHoraria.TabIndex = 2;
            // 
            // numCupos
            // 
            this.numCupos.Location = new System.Drawing.Point(64, 199);
            this.numCupos.Name = "numCupos";
            this.numCupos.Size = new System.Drawing.Size(201, 20);
            this.numCupos.TabIndex = 3;
            // 
            // numCuposMinimos
            // 
            this.numCuposMinimos.Location = new System.Drawing.Point(64, 246);
            this.numCuposMinimos.Name = "numCuposMinimos";
            this.numCuposMinimos.Size = new System.Drawing.Size(201, 20);
            this.numCuposMinimos.TabIndex = 4;
            // 
            // numFrecuencia
            // 
            this.numFrecuencia.Location = new System.Drawing.Point(64, 297);
            this.numFrecuencia.Name = "numFrecuencia";
            this.numFrecuencia.Size = new System.Drawing.Size(201, 20);
            this.numFrecuencia.TabIndex = 5;
            // 
            // cmbFormato
            // 
            this.cmbFormato.FormattingEnabled = true;
            this.cmbFormato.Location = new System.Drawing.Point(320, 57);
            this.cmbFormato.Name = "cmbFormato";
            this.cmbFormato.Size = new System.Drawing.Size(196, 21);
            this.cmbFormato.TabIndex = 6;
            // 
            // cmbCanchaAsignada
            // 
            this.cmbCanchaAsignada.FormattingEnabled = true;
            this.cmbCanchaAsignada.Location = new System.Drawing.Point(320, 102);
            this.cmbCanchaAsignada.Name = "cmbCanchaAsignada";
            this.cmbCanchaAsignada.Size = new System.Drawing.Size(196, 21);
            this.cmbCanchaAsignada.TabIndex = 7;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Location = new System.Drawing.Point(320, 154);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(196, 20);
            this.dtpFechaInicio.TabIndex = 8;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(84, 364);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(140, 39);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(311, 364);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(140, 39);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(62, 41);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(44, 13);
            this.lbl1.TabIndex = 11;
            this.lbl1.Text = "Nombre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Precio de Inscripcion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Franja horaria";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Cupos maximos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Cupos minimos";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 281);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(190, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Frecuencia de enfrentamientos en dias";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(317, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Formato de competicion";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(317, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "cancha asignada a la competicion";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(317, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Fecha de inicio";
            // 
            // FrmCompeticionDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 486);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.cmbCanchaAsignada);
            this.Controls.Add(this.cmbFormato);
            this.Controls.Add(this.numFrecuencia);
            this.Controls.Add(this.numCuposMinimos);
            this.Controls.Add(this.numCupos);
            this.Controls.Add(this.txtFranjaHoraria);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.txtNombre);
            this.Name = "FrmCompeticionDetalle";
            this.Text = "FrmCompeticionDetalle";
            this.Load += new System.EventHandler(this.FrmCompeticionDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCupos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCuposMinimos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrecuencia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtFranjaHoraria;
        private System.Windows.Forms.NumericUpDown numCupos;
        private System.Windows.Forms.NumericUpDown numCuposMinimos;
        private System.Windows.Forms.NumericUpDown numFrecuencia;
        private System.Windows.Forms.ComboBox cmbFormato;
        private System.Windows.Forms.ComboBox cmbCanchaAsignada;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
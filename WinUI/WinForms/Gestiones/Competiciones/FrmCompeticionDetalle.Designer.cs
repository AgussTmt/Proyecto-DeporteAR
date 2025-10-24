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
            this.txtPrecio.Location = new System.Drawing.Point(64, 95);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(202, 20);
            this.txtPrecio.TabIndex = 1;
            // 
            // txtFranjaHoraria
            // 
            this.txtFranjaHoraria.Location = new System.Drawing.Point(64, 138);
            this.txtFranjaHoraria.Name = "txtFranjaHoraria";
            this.txtFranjaHoraria.Size = new System.Drawing.Size(202, 20);
            this.txtFranjaHoraria.TabIndex = 2;
            // 
            // numCupos
            // 
            this.numCupos.Location = new System.Drawing.Point(65, 190);
            this.numCupos.Name = "numCupos";
            this.numCupos.Size = new System.Drawing.Size(201, 20);
            this.numCupos.TabIndex = 3;
            // 
            // numCuposMinimos
            // 
            this.numCuposMinimos.Location = new System.Drawing.Point(65, 232);
            this.numCuposMinimos.Name = "numCuposMinimos";
            this.numCuposMinimos.Size = new System.Drawing.Size(201, 20);
            this.numCuposMinimos.TabIndex = 4;
            // 
            // numFrecuencia
            // 
            this.numFrecuencia.Location = new System.Drawing.Point(65, 271);
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
            this.cmbCanchaAsignada.Location = new System.Drawing.Point(320, 127);
            this.cmbCanchaAsignada.Name = "cmbCanchaAsignada";
            this.cmbCanchaAsignada.Size = new System.Drawing.Size(196, 21);
            this.cmbCanchaAsignada.TabIndex = 7;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Location = new System.Drawing.Point(320, 186);
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
            // FrmCompeticionDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 486);
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
    }
}
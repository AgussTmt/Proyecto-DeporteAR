namespace WinUI.WinForms.Gestiones.Reportes
{
    partial class FrmReportes
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelControles = new System.Windows.Forms.Panel();
            this.cmbTipoReporte = new System.Windows.Forms.ComboBox();
            this.lblTipoReporte = new System.Windows.Forms.Label();
            this.pnlFiltros = new System.Windows.Forms.Panel();

            // --- Panel Facturación (Ahora completo) ---
            this.pnlFiltroFacturacion = new System.Windows.Forms.Panel();
            this.btnGenerarFacturacion = new System.Windows.Forms.Button();
            this.cmbCanchaFacturacion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpHastaFacturacion = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDesdeFacturacion = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();

            // --- Panel Ranking (Ahora completo) ---
            this.pnlFiltroRanking = new System.Windows.Forms.Panel();
            this.btnGenerarRanking = new System.Windows.Forms.Button();
            this.numTopN = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();

            // --- Panel Deudores (Estaba bien) ---
            this.pnlFiltroDeudores = new System.Windows.Forms.Panel();
            this.btnGenerarDeudores = new System.Windows.Forms.Button();
            this.lblInstruccionDeudores = new System.Windows.Forms.Label();

            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.panelResultados = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.panelControles.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.pnlFiltroFacturacion.SuspendLayout();
            this.pnlFiltroRanking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopN)).BeginInit();
            this.pnlFiltroDeudores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.panelResultados.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControles
            // 
            this.panelControles.Controls.Add(this.cmbTipoReporte);
            this.panelControles.Controls.Add(this.lblTipoReporte);
            this.panelControles.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControles.Location = new System.Drawing.Point(0, 0);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(984, 50);
            this.panelControles.TabIndex = 0;
            // 
            // cmbTipoReporte
            // 
            this.cmbTipoReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoReporte.FormattingEnabled = true;
            this.cmbTipoReporte.Location = new System.Drawing.Point(118, 15);
            this.cmbTipoReporte.Name = "cmbTipoReporte";
            this.cmbTipoReporte.Size = new System.Drawing.Size(300, 21);
            this.cmbTipoReporte.TabIndex = 1;
            this.cmbTipoReporte.SelectedIndexChanged += new System.EventHandler(this.cmbTipoReporte_SelectedIndexChanged);
            // 
            // lblTipoReporte
            // 
            this.lblTipoReporte.AutoSize = true;
            this.lblTipoReporte.Location = new System.Drawing.Point(12, 18);
            this.lblTipoReporte.Name = "lblTipoReporte";
            this.lblTipoReporte.Size = new System.Drawing.Size(100, 13);
            this.lblTipoReporte.TabIndex = 0;
            this.lblTipoReporte.Text = "Seleccione reporte:";
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.Controls.Add(this.pnlFiltroFacturacion);
            this.pnlFiltros.Controls.Add(this.pnlFiltroRanking);
            this.pnlFiltros.Controls.Add(this.pnlFiltroDeudores);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 50);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(984, 80);
            this.pnlFiltros.TabIndex = 1;
            // 
            // pnlFiltroFacturacion
            // 
            this.pnlFiltroFacturacion.Controls.Add(this.btnGenerarFacturacion);
            this.pnlFiltroFacturacion.Controls.Add(this.cmbCanchaFacturacion);
            this.pnlFiltroFacturacion.Controls.Add(this.label3);
            this.pnlFiltroFacturacion.Controls.Add(this.dtpHastaFacturacion);
            this.pnlFiltroFacturacion.Controls.Add(this.label2);
            this.pnlFiltroFacturacion.Controls.Add(this.dtpDesdeFacturacion);
            this.pnlFiltroFacturacion.Controls.Add(this.label1);
            this.pnlFiltroFacturacion.Location = new System.Drawing.Point(0, 0); // Ajustado
            this.pnlFiltroFacturacion.Name = "pnlFiltroFacturacion";
            this.pnlFiltroFacturacion.Size = new System.Drawing.Size(984, 80); // Ajustado
            this.pnlFiltroFacturacion.TabIndex = 0;
            // 
            // btnGenerarFacturacion
            // 
            this.btnGenerarFacturacion.Location = new System.Drawing.Point(700, 26);
            this.btnGenerarFacturacion.Name = "btnGenerarFacturacion";
            this.btnGenerarFacturacion.Size = new System.Drawing.Size(120, 23);
            this.btnGenerarFacturacion.TabIndex = 6;
            this.btnGenerarFacturacion.Text = "Generar Reporte";
            this.btnGenerarFacturacion.UseVisualStyleBackColor = true;
            this.btnGenerarFacturacion.Click += new System.EventHandler(this.btnGenerarFacturacion_Click);
            // 
            // cmbCanchaFacturacion
            // 
            this.cmbCanchaFacturacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCanchaFacturacion.FormattingEnabled = true;
            this.cmbCanchaFacturacion.Location = new System.Drawing.Point(490, 27);
            this.cmbCanchaFacturacion.Name = "cmbCanchaFacturacion";
            this.cmbCanchaFacturacion.Size = new System.Drawing.Size(180, 21);
            this.cmbCanchaFacturacion.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(438, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cancha:";
            // 
            // dtpHastaFacturacion
            // 
            this.dtpHastaFacturacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHastaFacturacion.Location = new System.Drawing.Point(290, 28);
            this.dtpHastaFacturacion.Name = "dtpHastaFacturacion";
            this.dtpHastaFacturacion.Size = new System.Drawing.Size(110, 20);
            this.dtpHastaFacturacion.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hasta:";
            // 
            // dtpDesdeFacturacion
            // 
            this.dtpDesdeFacturacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesdeFacturacion.Location = new System.Drawing.Point(90, 28);
            this.dtpDesdeFacturacion.Name = "dtpDesdeFacturacion";
            this.dtpDesdeFacturacion.Size = new System.Drawing.Size(110, 20);
            this.dtpDesdeFacturacion.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Desde:";
            // 
            // pnlFiltroRanking
            // 
            this.pnlFiltroRanking.Controls.Add(this.btnGenerarRanking);
            this.pnlFiltroRanking.Controls.Add(this.numTopN);
            this.pnlFiltroRanking.Controls.Add(this.label4);
            this.pnlFiltroRanking.Location = new System.Drawing.Point(0, 0); // Ajustado
            this.pnlFiltroRanking.Name = "pnlFiltroRanking";
            this.pnlFiltroRanking.Size = new System.Drawing.Size(984, 80); // Ajustado
            this.pnlFiltroRanking.TabIndex = 1;
            // 
            // btnGenerarRanking
            // 
            this.btnGenerarRanking.Location = new System.Drawing.Point(165, 26);
            this.btnGenerarRanking.Name = "btnGenerarRanking";
            this.btnGenerarRanking.Size = new System.Drawing.Size(120, 23);
            this.btnGenerarRanking.TabIndex = 2;
            this.btnGenerarRanking.Text = "Generar Reporte";
            this.btnGenerarRanking.UseVisualStyleBackColor = true;
            this.btnGenerarRanking.Click += new System.EventHandler(this.btnGenerarRanking_Click);
            // 
            // numTopN
            // 
            this.numTopN.Location = new System.Drawing.Point(90, 28);
            this.numTopN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTopN.Name = "numTopN";
            this.numTopN.Size = new System.Drawing.Size(55, 20);
            this.numTopN.TabIndex = 1;
            this.numTopN.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Mostrar N:";
            // 
            // pnlFiltroDeudores
            // 
            this.pnlFiltroDeudores.Controls.Add(this.btnGenerarDeudores);
            this.pnlFiltroDeudores.Controls.Add(this.lblInstruccionDeudores);
            this.pnlFiltroDeudores.Location = new System.Drawing.Point(0, 0); // Ajustado
            this.pnlFiltroDeudores.Name = "pnlFiltroDeudores";
            this.pnlFiltroDeudores.Size = new System.Drawing.Size(984, 80); // Ajustado
            this.pnlFiltroDeudores.TabIndex = 2;
            // 
            // btnGenerarDeudores
            // 
            this.btnGenerarDeudores.Location = new System.Drawing.Point(230, 28);
            this.btnGenerarDeudores.Name = "btnGenerarDeudores";
            this.btnGenerarDeudores.Size = new System.Drawing.Size(120, 23);
            this.btnGenerarDeudores.TabIndex = 1;
            this.btnGenerarDeudores.Text = "Generar Reporte";
            this.btnGenerarDeudores.UseVisualStyleBackColor = true;
            this.btnGenerarDeudores.Click += new System.EventHandler(this.btnGenerarDeudores_Click);
            // 
            // lblInstruccionDeudores
            // 
            this.lblInstruccionDeudores.AutoSize = true;
            this.lblInstruccionDeudores.Location = new System.Drawing.Point(16, 33);
            this.lblInstruccionDeudores.Name = "lblInstruccionDeudores";
            this.lblInstruccionDeudores.Size = new System.Drawing.Size(208, 13);
            this.lblInstruccionDeudores.TabIndex = 0;
            this.lblInstruccionDeudores.Text = "Presione \"Generar\" para ver los deudores:";
            // 
            // dgvResultados
            // 
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultados.Location = new System.Drawing.Point(0, 130);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.Size = new System.Drawing.Size(984, 392);
            this.dgvResultados.TabIndex = 2;
            this.dgvResultados.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResultados_CellFormatting);
            // 
            // panelResultados
            // 
            this.panelResultados.Controls.Add(this.lblTotal);
            this.panelResultados.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelResultados.Location = new System.Drawing.Point(0, 522);
            this.panelResultados.Name = "panelResultados";
            this.panelResultados.Size = new System.Drawing.Size(984, 40);
            this.panelResultados.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(672, 12);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(300, 23);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total: $ 0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.panelResultados);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.panelControles);
            this.Name = "FrmReportes";
            this.Text = "Módulo de Reportes";
            this.Load += new System.EventHandler(this.FormReportes_Load);
            this.panelControles.ResumeLayout(false);
            this.panelControles.PerformLayout();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltroFacturacion.ResumeLayout(false);
            this.pnlFiltroFacturacion.PerformLayout();
            this.pnlFiltroRanking.ResumeLayout(false);
            this.pnlFiltroRanking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTopN)).EndInit();
            this.pnlFiltroDeudores.ResumeLayout(false);
            this.pnlFiltroDeudores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.panelResultados.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.ComboBox cmbTipoReporte;
        private System.Windows.Forms.Label lblTipoReporte;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Panel pnlFiltroFacturacion;
        private System.Windows.Forms.Button btnGenerarFacturacion;
        private System.Windows.Forms.ComboBox cmbCanchaFacturacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpHastaFacturacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDesdeFacturacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlFiltroRanking;
        private System.Windows.Forms.Button btnGenerarRanking;
        private System.Windows.Forms.NumericUpDown numTopN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlFiltroDeudores;
        private System.Windows.Forms.Button btnGenerarDeudores;
        private System.Windows.Forms.Label lblInstruccionDeudores;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.Panel panelResultados;
        private System.Windows.Forms.Label lblTotal;
    }
}
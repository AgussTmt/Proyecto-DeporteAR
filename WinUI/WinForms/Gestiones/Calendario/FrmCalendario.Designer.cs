namespace WinUI.WinForms.Gestiones
{
    partial class FrmCalendario
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
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.tlpSemanaReservas = new System.Windows.Forms.TableLayoutPanel();
            this.panelFiltrosReservas = new System.Windows.Forms.Panel();
            this.btnGenerarHorarios = new System.Windows.Forms.Button();
            this.btnSemanaSiguiente = new System.Windows.Forms.Button();
            this.lblRangoSemana = new System.Windows.Forms.Label();
            this.btnSemanaAnterior = new System.Windows.Forms.Button();
            this.cmbCancha = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowPartidos = new System.Windows.Forms.FlowLayoutPanel();
            this.panelFiltrosPartidos = new System.Windows.Forms.Panel();
            this.dtpFechaPartidos = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCompeticion = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.panelFiltrosReservas.SuspendLayout();
            this.panelFiltrosPartidos.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tlpSemanaReservas);
            this.scMain.Panel1.Controls.Add(this.panelFiltrosReservas);
            this.scMain.Panel1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.flowPartidos);
            this.scMain.Panel2.Controls.Add(this.panelFiltrosPartidos);
            this.scMain.Panel2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scMain.Size = new System.Drawing.Size(1125, 619);
            this.scMain.SplitterDistance = 359;
            this.scMain.TabIndex = 0;
            // 
            // tlpSemanaReservas
            // 
            this.tlpSemanaReservas.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tlpSemanaReservas.ColumnCount = 7;
            this.tlpSemanaReservas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpSemanaReservas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpSemanaReservas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpSemanaReservas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpSemanaReservas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpSemanaReservas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpSemanaReservas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpSemanaReservas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSemanaReservas.Location = new System.Drawing.Point(0, 50);
            this.tlpSemanaReservas.Name = "tlpSemanaReservas";
            this.tlpSemanaReservas.RowCount = 1;
            this.tlpSemanaReservas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSemanaReservas.Size = new System.Drawing.Size(1125, 309);
            this.tlpSemanaReservas.TabIndex = 1;
            // 
            // panelFiltrosReservas
            // 
            this.panelFiltrosReservas.BackColor = System.Drawing.Color.White;
            this.panelFiltrosReservas.Controls.Add(this.btnGenerarHorarios);
            this.panelFiltrosReservas.Controls.Add(this.btnSemanaSiguiente);
            this.panelFiltrosReservas.Controls.Add(this.lblRangoSemana);
            this.panelFiltrosReservas.Controls.Add(this.btnSemanaAnterior);
            this.panelFiltrosReservas.Controls.Add(this.cmbCancha);
            this.panelFiltrosReservas.Controls.Add(this.label1);
            this.panelFiltrosReservas.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltrosReservas.Location = new System.Drawing.Point(0, 0);
            this.panelFiltrosReservas.Name = "panelFiltrosReservas";
            this.panelFiltrosReservas.Size = new System.Drawing.Size(1125, 50);
            this.panelFiltrosReservas.TabIndex = 0;
            // 
            // btnGenerarHorarios
            // 
            this.btnGenerarHorarios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarHorarios.Location = new System.Drawing.Point(953, 12);
            this.btnGenerarHorarios.Name = "btnGenerarHorarios";
            this.btnGenerarHorarios.Size = new System.Drawing.Size(160, 25);
            this.btnGenerarHorarios.TabIndex = 5;
            this.btnGenerarHorarios.Text = "Generar Próximos 15 Días";
            this.btnGenerarHorarios.UseVisualStyleBackColor = true;
            // 
            // btnSemanaSiguiente
            // 
            this.btnSemanaSiguiente.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSemanaSiguiente.Location = new System.Drawing.Point(549, 12);
            this.btnSemanaSiguiente.Name = "btnSemanaSiguiente";
            this.btnSemanaSiguiente.Size = new System.Drawing.Size(30, 25);
            this.btnSemanaSiguiente.TabIndex = 4;
            this.btnSemanaSiguiente.Text = ">";
            this.btnSemanaSiguiente.UseVisualStyleBackColor = true;
            this.btnSemanaSiguiente.Click += new System.EventHandler(this.btnSemanaSiguiente_Click);
            // 
            // lblRangoSemana
            // 
            this.lblRangoSemana.AutoSize = true;
            this.lblRangoSemana.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRangoSemana.Location = new System.Drawing.Point(408, 16);
            this.lblRangoSemana.Name = "lblRangoSemana";
            this.lblRangoSemana.Size = new System.Drawing.Size(129, 17);
            this.lblRangoSemana.TabIndex = 3;
            this.lblRangoSemana.Text = "28/10/2024 al 03/11";
            // 
            // btnSemanaAnterior
            // 
            this.btnSemanaAnterior.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSemanaAnterior.Location = new System.Drawing.Point(372, 12);
            this.btnSemanaAnterior.Name = "btnSemanaAnterior";
            this.btnSemanaAnterior.Size = new System.Drawing.Size(30, 25);
            this.btnSemanaAnterior.TabIndex = 2;
            this.btnSemanaAnterior.Text = "<";
            this.btnSemanaAnterior.UseVisualStyleBackColor = true;
            this.btnSemanaAnterior.Click += new System.EventHandler(this.btnSemanaAnterior_Click);
            // 
            // cmbCancha
            // 
            this.cmbCancha.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCancha.FormattingEnabled = true;
            this.cmbCancha.Location = new System.Drawing.Point(64, 14);
            this.cmbCancha.Name = "cmbCancha";
            this.cmbCancha.Size = new System.Drawing.Size(280, 21);
            this.cmbCancha.TabIndex = 1;
            this.cmbCancha.SelectedIndexChanged += new System.EventHandler(this.cmbCancha_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cancha:";
            // 
            // flowPartidos
            // 
            this.flowPartidos.AutoScroll = true;
            this.flowPartidos.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowPartidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPartidos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPartidos.Location = new System.Drawing.Point(0, 50);
            this.flowPartidos.Name = "flowPartidos";
            this.flowPartidos.Size = new System.Drawing.Size(1125, 206);
            this.flowPartidos.TabIndex = 1;
            this.flowPartidos.WrapContents = false;
            // 
            // panelFiltrosPartidos
            // 
            this.panelFiltrosPartidos.BackColor = System.Drawing.Color.White;
            this.panelFiltrosPartidos.Controls.Add(this.dtpFechaPartidos);
            this.panelFiltrosPartidos.Controls.Add(this.label3);
            this.panelFiltrosPartidos.Controls.Add(this.cmbCompeticion);
            this.panelFiltrosPartidos.Controls.Add(this.label2);
            this.panelFiltrosPartidos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltrosPartidos.Location = new System.Drawing.Point(0, 0);
            this.panelFiltrosPartidos.Name = "panelFiltrosPartidos";
            this.panelFiltrosPartidos.Size = new System.Drawing.Size(1125, 50);
            this.panelFiltrosPartidos.TabIndex = 0;
            // 
            // dtpFechaPartidos
            // 
            this.dtpFechaPartidos.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPartidos.Location = new System.Drawing.Point(372, 14);
            this.dtpFechaPartidos.Name = "dtpFechaPartidos";
            this.dtpFechaPartidos.Size = new System.Drawing.Size(100, 22);
            this.dtpFechaPartidos.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ver Día:";
            // 
            // cmbCompeticion
            // 
            this.cmbCompeticion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompeticion.FormattingEnabled = true;
            this.cmbCompeticion.Location = new System.Drawing.Point(90, 14);
            this.cmbCompeticion.Name = "cmbCompeticion";
            this.cmbCompeticion.Size = new System.Drawing.Size(200, 21);
            this.cmbCompeticion.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Competición:";
            // 
            // FrmCalendario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 619);
            this.Controls.Add(this.scMain);
            this.Name = "FrmCalendario";
            this.Text = "Calendario y Partidos";
            this.Load += new System.EventHandler(this.FrmCalendario_Load);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.panelFiltrosReservas.ResumeLayout(false);
            this.panelFiltrosReservas.PerformLayout();
            this.panelFiltrosPartidos.ResumeLayout(false);
            this.panelFiltrosPartidos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.Panel panelFiltrosReservas;
        private System.Windows.Forms.ComboBox cmbCancha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tlpSemanaReservas;
        private System.Windows.Forms.Panel panelFiltrosPartidos;
        private System.Windows.Forms.Button btnSemanaSiguiente;
        private System.Windows.Forms.Label lblRangoSemana;
        private System.Windows.Forms.Button btnSemanaAnterior;
        private System.Windows.Forms.Button btnGenerarHorarios;
        private System.Windows.Forms.FlowLayoutPanel flowPartidos;
        private System.Windows.Forms.DateTimePicker dtpFechaPartidos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCompeticion;
        private System.Windows.Forms.Label label2;
    }
}
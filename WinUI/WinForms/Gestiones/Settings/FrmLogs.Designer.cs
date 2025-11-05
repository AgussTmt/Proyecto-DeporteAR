namespace WinUI.WinForms.Gestiones.Settings
{
    partial class FrmLogs
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnActualizarLogs = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.chkFiltrarFecha = new System.Windows.Forms.CheckBox();
            this.txtFiltroTexto = new System.Windows.Forms.TextBox();
            this.CmbFiltroNiveles = new System.Windows.Forms.ComboBox();
            this.richTextBoxLogs = new System.Windows.Forms.RichTextBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnActualizarLogs);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1038, 74);
            this.panel1.TabIndex = 0;
            // 
            // BtnActualizarLogs
            // 
            this.BtnActualizarLogs.Location = new System.Drawing.Point(897, 14);
            this.BtnActualizarLogs.Name = "BtnActualizarLogs";
            this.BtnActualizarLogs.Size = new System.Drawing.Size(129, 50);
            this.BtnActualizarLogs.TabIndex = 0;
            this.BtnActualizarLogs.Text = "Actualizar";
            this.BtnActualizarLogs.UseVisualStyleBackColor = true;
            this.BtnActualizarLogs.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.dtpFechaHasta);
            this.panel3.Controls.Add(this.dtpFechaDesde);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(353, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(538, 74);
            this.panel3.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(316, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Hasta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Desde:";
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Location = new System.Drawing.Point(308, 28);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaHasta.TabIndex = 1;
            this.dtpFechaHasta.ValueChanged += new System.EventHandler(this.dtpFechaHasta_ValueChanged);
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Location = new System.Drawing.Point(30, 29);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaDesde.TabIndex = 0;
            this.dtpFechaDesde.ValueChanged += new System.EventHandler(this.dtpFechaDesde_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.chkFiltrarFecha);
            this.panel2.Controls.Add(this.txtFiltroTexto);
            this.panel2.Controls.Add(this.CmbFiltroNiveles);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(353, 74);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(145, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filtro texto";
            // 
            // chkFiltrarFecha
            // 
            this.chkFiltrarFecha.AutoSize = true;
            this.chkFiltrarFecha.Location = new System.Drawing.Point(269, 32);
            this.chkFiltrarFecha.Name = "chkFiltrarFecha";
            this.chkFiltrarFecha.Size = new System.Drawing.Size(80, 17);
            this.chkFiltrarFecha.TabIndex = 2;
            this.chkFiltrarFecha.Text = "checkBox1";
            this.chkFiltrarFecha.UseVisualStyleBackColor = true;
            this.chkFiltrarFecha.CheckedChanged += new System.EventHandler(this.chkFiltrarFecha_CheckedChanged);
            // 
            // txtFiltroTexto
            // 
            this.txtFiltroTexto.Location = new System.Drawing.Point(138, 29);
            this.txtFiltroTexto.Name = "txtFiltroTexto";
            this.txtFiltroTexto.Size = new System.Drawing.Size(125, 20);
            this.txtFiltroTexto.TabIndex = 1;
            this.txtFiltroTexto.TextChanged += new System.EventHandler(this.txtFiltroTexto_TextChanged);
            // 
            // CmbFiltroNiveles
            // 
            this.CmbFiltroNiveles.FormattingEnabled = true;
            this.CmbFiltroNiveles.Location = new System.Drawing.Point(12, 28);
            this.CmbFiltroNiveles.Name = "CmbFiltroNiveles";
            this.CmbFiltroNiveles.Size = new System.Drawing.Size(120, 21);
            this.CmbFiltroNiveles.TabIndex = 0;
            this.CmbFiltroNiveles.SelectedIndexChanged += new System.EventHandler(this.cmbNivel_SelectedIndexChanged);
            // 
            // richTextBoxLogs
            // 
            this.richTextBoxLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLogs.Location = new System.Drawing.Point(0, 74);
            this.richTextBoxLogs.Name = "richTextBoxLogs";
            this.richTextBoxLogs.Size = new System.Drawing.Size(1038, 589);
            this.richTextBoxLogs.TabIndex = 1;
            this.richTextBoxLogs.Text = "";
            this.richTextBoxLogs.TextChanged += new System.EventHandler(this.richTextBoxLogs_TextChanged);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(624, 195);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(80, 17);
            this.hScrollBar1.TabIndex = 2;
            // 
            // FrmLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 663);
            this.Controls.Add(this.richTextBoxLogs);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.hScrollBar1);
            this.Name = "FrmLogs";
            this.Text = "FrmLogs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLogs_FormClosing);
            this.Load += new System.EventHandler(this.FrmLogs_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnActualizarLogs;
        private System.Windows.Forms.ComboBox CmbFiltroNiveles;
        private System.Windows.Forms.RichTextBox richTextBoxLogs;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.CheckBox chkFiltrarFecha;
        private System.Windows.Forms.TextBox txtFiltroTexto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}
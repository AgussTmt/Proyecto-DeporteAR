namespace WinUI.WinForms.Gestiones.Calendario
{
    partial class PartidoControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblNroFecha = new System.Windows.Forms.Label();
            this.lblLocal = new System.Windows.Forms.Label();
            this.lblVisitante = new System.Windows.Forms.Label();
            this.btnCargar = new System.Windows.Forms.Button();
            this.txtResLocal = new System.Windows.Forms.TextBox();
            this.txtResVisitante = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(318, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(35, 13);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "label1";
            // 
            // lblNroFecha
            // 
            this.lblNroFecha.AutoSize = true;
            this.lblNroFecha.Location = new System.Drawing.Point(318, 20);
            this.lblNroFecha.Name = "lblNroFecha";
            this.lblNroFecha.Size = new System.Drawing.Size(35, 13);
            this.lblNroFecha.TabIndex = 1;
            this.lblNroFecha.Text = "label1";
            // 
            // lblLocal
            // 
            this.lblLocal.AutoSize = true;
            this.lblLocal.Location = new System.Drawing.Point(123, 48);
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Size = new System.Drawing.Size(35, 13);
            this.lblLocal.TabIndex = 2;
            this.lblLocal.Text = "label1";
            // 
            // lblVisitante
            // 
            this.lblVisitante.AutoSize = true;
            this.lblVisitante.Location = new System.Drawing.Point(448, 52);
            this.lblVisitante.Name = "lblVisitante";
            this.lblVisitante.Size = new System.Drawing.Size(35, 13);
            this.lblVisitante.TabIndex = 5;
            this.lblVisitante.Text = "label1";
            // 
            // btnCargar
            // 
            this.btnCargar.Location = new System.Drawing.Point(280, 42);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(102, 25);
            this.btnCargar.TabIndex = 7;
            this.btnCargar.Text = "Cargar resultado";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // txtResLocal
            // 
            this.txtResLocal.Location = new System.Drawing.Point(243, 45);
            this.txtResLocal.Name = "txtResLocal";
            this.txtResLocal.Size = new System.Drawing.Size(20, 20);
            this.txtResLocal.TabIndex = 8;
            // 
            // txtResVisitante
            // 
            this.txtResVisitante.Location = new System.Drawing.Point(388, 45);
            this.txtResVisitante.Name = "txtResVisitante";
            this.txtResVisitante.Size = new System.Drawing.Size(20, 20);
            this.txtResVisitante.TabIndex = 9;
            // 
            // PartidoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtResVisitante);
            this.Controls.Add(this.txtResLocal);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.lblVisitante);
            this.Controls.Add(this.lblLocal);
            this.Controls.Add(this.lblNroFecha);
            this.Controls.Add(this.lblInfo);
            this.Name = "PartidoControl";
            this.Size = new System.Drawing.Size(800, 80);
            this.Load += new System.EventHandler(this.PartidoControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblNroFecha;
        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.Label lblVisitante;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.TextBox txtResLocal;
        private System.Windows.Forms.TextBox txtResVisitante;
    }
}

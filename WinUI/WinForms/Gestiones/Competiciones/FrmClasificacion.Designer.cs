namespace WinUI.WinForms.Gestiones.Competiciones
{
    partial class FrmClasificacion
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTituloCompeticion = new System.Windows.Forms.Label();
            this.panelFill = new System.Windows.Forms.Panel();
            this.dgvClasificacion = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.panelFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClasificacion)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblTituloCompeticion);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 50);
            this.panelTop.TabIndex = 0;
            // 
            // lblTituloCompeticion
            // 
            this.lblTituloCompeticion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloCompeticion.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloCompeticion.Location = new System.Drawing.Point(0, 0);
            this.lblTituloCompeticion.Name = "lblTituloCompeticion";
            this.lblTituloCompeticion.Size = new System.Drawing.Size(800, 50);
            this.lblTituloCompeticion.TabIndex = 0;
            this.lblTituloCompeticion.Text = "Tabla de Clasificación";
            this.lblTituloCompeticion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFill
            // 
            this.panelFill.Controls.Add(this.dgvClasificacion);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 50);
            this.panelFill.Name = "panelFill";
            this.panelFill.Padding = new System.Windows.Forms.Padding(10);
            this.panelFill.Size = new System.Drawing.Size(800, 400);
            this.panelFill.TabIndex = 1;
            // 
            // dgvClasificacion
            // 
            this.dgvClasificacion.AllowUserToAddRows = false;
            this.dgvClasificacion.AllowUserToDeleteRows = false;
            this.dgvClasificacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClasificacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClasificacion.Location = new System.Drawing.Point(10, 10);
            this.dgvClasificacion.Name = "dgvClasificacion";
            this.dgvClasificacion.ReadOnly = true;
            this.dgvClasificacion.RowHeadersVisible = false;
            this.dgvClasificacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClasificacion.Size = new System.Drawing.Size(780, 380);
            this.dgvClasificacion.TabIndex = 0;
            // 
            // FrmClasificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panelTop);
            this.Name = "FrmClasificacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tabla de Posiciones";
            this.Load += new System.EventHandler(this.FrmClasificacion_Load);
            this.panelTop.ResumeLayout(false);
            this.panelFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClasificacion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTituloCompeticion;
        private System.Windows.Forms.Panel panelFill;
        private System.Windows.Forms.DataGridView dgvClasificacion;
    }
}
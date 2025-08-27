namespace WinUI.WinForms
{
    partial class Principal
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
            this.mnuPrincipal = new System.Windows.Forms.MenuStrip();
            this.tsmGestionVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInformes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAdministracion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGestionCompras = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmGestionInventario = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuPrincipal
            // 
            this.mnuPrincipal.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmGestionVentas,
            this.tsmInformes,
            this.tsmAdministracion,
            this.tsmGestionCompras,
            this.tsmGestionInventario});
            this.mnuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mnuPrincipal.Name = "mnuPrincipal";
            this.mnuPrincipal.Size = new System.Drawing.Size(800, 24);
            this.mnuPrincipal.TabIndex = 0;
            this.mnuPrincipal.Text = "menuStrip1";
            // 
            // tsmGestionVentas
            // 
            this.tsmGestionVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevaToolStripMenuItem,
            this.listadoToolStripMenuItem});
            this.tsmGestionVentas.Name = "tsmGestionVentas";
            this.tsmGestionVentas.Size = new System.Drawing.Size(66, 24);
            this.tsmGestionVentas.Text = "Ventas";
            this.tsmGestionVentas.Visible = false;
            this.tsmGestionVentas.Click += new System.EventHandler(this.tsmGestionVentas_Click);
            // 
            // nuevaToolStripMenuItem
            // 
            this.nuevaToolStripMenuItem.Name = "nuevaToolStripMenuItem";
            this.nuevaToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.nuevaToolStripMenuItem.Text = "Nueva";
            // 
            // listadoToolStripMenuItem
            // 
            this.listadoToolStripMenuItem.Name = "listadoToolStripMenuItem";
            this.listadoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.listadoToolStripMenuItem.Text = "Listado";
            // 
            // tsmInformes
            // 
            this.tsmInformes.Name = "tsmInformes";
            this.tsmInformes.Size = new System.Drawing.Size(81, 24);
            this.tsmInformes.Text = "Informes";
            this.tsmInformes.Visible = false;
            this.tsmInformes.Click += new System.EventHandler(this.tsmInformes_Click);
            // 
            // tsmAdministracion
            // 
            this.tsmAdministracion.Name = "tsmAdministracion";
            this.tsmAdministracion.Size = new System.Drawing.Size(118, 24);
            this.tsmAdministracion.Text = "Administrador";
            this.tsmAdministracion.Visible = false;
            this.tsmAdministracion.Click += new System.EventHandler(this.tsmAdministracion_Click);
            // 
            // tsmGestionCompras
            // 
            this.tsmGestionCompras.Name = "tsmGestionCompras";
            this.tsmGestionCompras.Size = new System.Drawing.Size(82, 24);
            this.tsmGestionCompras.Text = "Compras";
            this.tsmGestionCompras.Visible = false;
            // 
            // tsmGestionInventario
            // 
            this.tsmGestionInventario.Name = "tsmGestionInventario";
            this.tsmGestionInventario.Size = new System.Drawing.Size(89, 24);
            this.tsmGestionInventario.Text = "Inventario";
            this.tsmGestionInventario.Visible = false;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mnuPrincipal);
            this.MainMenuStrip = this.mnuPrincipal;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.mnuPrincipal.ResumeLayout(false);
            this.mnuPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem tsmGestionVentas;
        private System.Windows.Forms.ToolStripMenuItem nuevaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmInformes;
        private System.Windows.Forms.ToolStripMenuItem tsmAdministracion;
        private System.Windows.Forms.ToolStripMenuItem tsmGestionCompras;
        private System.Windows.Forms.ToolStripMenuItem tsmGestionInventario;
    }
}
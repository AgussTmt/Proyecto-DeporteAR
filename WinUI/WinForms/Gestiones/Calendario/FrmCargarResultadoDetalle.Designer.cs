namespace WinUI.WinForms.Gestiones
{
    partial class FrmCargarResultadoDetalle
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
            this.lblLocal = new System.Windows.Forms.Label();
            this.lblVisitante = new System.Windows.Forms.Label();
            this.dgvLocal = new System.Windows.Forms.DataGridView();
            this.colLocalJugador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocalGoles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocalAsistencias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocalAmarillas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocalRojas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVisitante = new System.Windows.Forms.DataGridView();
            this.colVisitanteJugador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVisitanteGoles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVisitanteAsistencias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVisitanteAmarillas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVisitanteRojas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtResLocal = new System.Windows.Forms.TextBox();
            this.txtResVisitante = new System.Windows.Forms.TextBox();
            this.lblSeparadorResultado = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.linkLblEditarLocal = new System.Windows.Forms.LinkLabel();
            this.linkLblEditarVisitante = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisitante)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLocal
            // 
            this.lblLocal.AutoSize = true;
            this.lblLocal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocal.Location = new System.Drawing.Point(12, 60);
            this.lblLocal.Name = "lblLocal";
            this.lblLocal.Size = new System.Drawing.Size(110, 21);
            this.lblLocal.TabIndex = 0;
            this.lblLocal.Text = "Equipo Local";
            // 
            // lblVisitante
            // 
            this.lblVisitante.AutoSize = true;
            this.lblVisitante.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVisitante.Location = new System.Drawing.Point(480, 60);
            this.lblVisitante.Name = "lblVisitante";
            this.lblVisitante.Size = new System.Drawing.Size(130, 21);
            this.lblVisitante.TabIndex = 1;
            this.lblVisitante.Text = "Equipo Visitante";
            // 
            // dgvLocal
            // 
            this.dgvLocal.AllowUserToAddRows = false;
            this.dgvLocal.AllowUserToDeleteRows = false;
            this.dgvLocal.AutoGenerateColumns = false;
            this.dgvLocal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLocalJugador,
            this.colLocalGoles,
            this.colLocalAsistencias,
            this.colLocalAmarillas,
            this.colLocalRojas});
            this.dgvLocal.Location = new System.Drawing.Point(16, 84);
            this.dgvLocal.Name = "dgvLocal";
            this.dgvLocal.ReadOnly = true;
            this.dgvLocal.Size = new System.Drawing.Size(450, 300);
            this.dgvLocal.TabIndex = 2;
            // 
            // colLocalJugador
            // 
            this.colLocalJugador.DataPropertyName = "NombreCompleto";
            this.colLocalJugador.HeaderText = "Jugador";
            this.colLocalJugador.Name = "colLocalJugador";
            this.colLocalJugador.ReadOnly = true;
            this.colLocalJugador.Width = 150;
            // 
            // colLocalGoles
            // 
            this.colLocalGoles.HeaderText = "Goles";
            this.colLocalGoles.Name = "colLocalGoles";
            this.colLocalGoles.ReadOnly = false;
            this.colLocalGoles.Width = 60;
            // 
            // colLocalAsistencias
            // 
            this.colLocalAsistencias.HeaderText = "Asistencias";
            this.colLocalAsistencias.Name = "colLocalAsistencias";
            this.colLocalAsistencias.ReadOnly = false;
            this.colLocalAsistencias.Width = 70;
            // 
            // colLocalAmarillas
            // 
            this.colLocalAmarillas.HeaderText = "Amarillas";
            this.colLocalAmarillas.Name = "colLocalAmarillas";
            this.colLocalAmarillas.ReadOnly = false;
            this.colLocalAmarillas.Width = 70;
            // 
            // colLocalRojas
            // 
            this.colLocalRojas.HeaderText = "Rojas";
            this.colLocalRojas.Name = "colLocalRojas";
            this.colLocalRojas.ReadOnly = false;
            this.colLocalRojas.Width = 60;
            // 
            // dgvVisitante
            // 
            this.dgvVisitante.AllowUserToAddRows = false;
            this.dgvVisitante.AllowUserToDeleteRows = false;
            this.dgvVisitante.AutoGenerateColumns = false;
            this.dgvVisitante.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVisitante.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVisitanteJugador,
            this.colVisitanteGoles,
            this.colVisitanteAsistencias,
            this.colVisitanteAmarillas,
            this.colVisitanteRojas});
            this.dgvVisitante.Location = new System.Drawing.Point(484, 84);
            this.dgvVisitante.Name = "dgvVisitante";
            this.dgvVisitante.ReadOnly = true;
            this.dgvVisitante.Size = new System.Drawing.Size(450, 300);
            this.dgvVisitante.TabIndex = 3;
            // 
            // colVisitanteJugador
            // 
            this.colVisitanteJugador.DataPropertyName = "NombreCompleto";
            this.colVisitanteJugador.HeaderText = "Jugador";
            this.colVisitanteJugador.Name = "colVisitanteJugador";
            this.colVisitanteJugador.ReadOnly = true;
            this.colVisitanteJugador.Width = 150;
            // 
            // colVisitanteGoles
            // 
            this.colVisitanteGoles.HeaderText = "Goles";
            this.colVisitanteGoles.Name = "colVisitanteGoles";
            this.colVisitanteGoles.ReadOnly = false;
            this.colVisitanteGoles.Width = 60;
            // 
            // colVisitanteAsistencias
            // 
            this.colVisitanteAsistencias.HeaderText = "Asistencias";
            this.colVisitanteAsistencias.Name = "colVisitanteAsistencias";
            this.colVisitanteAsistencias.ReadOnly = false;
            this.colVisitanteAsistencias.Width = 70;
            // 
            // colVisitanteAmarillas
            // 
            this.colVisitanteAmarillas.HeaderText = "Amarillas";
            this.colVisitanteAmarillas.Name = "colVisitanteAmarillas";
            this.colVisitanteAmarillas.ReadOnly = false;
            this.colVisitanteAmarillas.Width = 70;
            // 
            // colVisitanteRojas
            // 
            this.colVisitanteRojas.HeaderText = "Rojas";
            this.colVisitanteRojas.Name = "colVisitanteRojas";
            this.colVisitanteRojas.ReadOnly = false;
            this.colVisitanteRojas.Width = 60;
            // 
            // txtResLocal
            // 
            this.txtResLocal.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResLocal.Location = new System.Drawing.Point(390, 12);
            this.txtResLocal.Name = "txtResLocal";
            this.txtResLocal.Size = new System.Drawing.Size(50, 33);
            this.txtResLocal.TabIndex = 4;
            this.txtResLocal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtResVisitante
            // 
            this.txtResVisitante.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResVisitante.Location = new System.Drawing.Point(472, 12);
            this.txtResVisitante.Name = "txtResVisitante";
            this.txtResVisitante.Size = new System.Drawing.Size(50, 33);
            this.txtResVisitante.TabIndex = 5;
            this.txtResVisitante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSeparadorResultado
            // 
            this.lblSeparadorResultado.AutoSize = true;
            this.lblSeparadorResultado.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeparadorResultado.Location = new System.Drawing.Point(446, 15);
            this.lblSeparadorResultado.Name = "lblSeparadorResultado";
            this.lblSeparadorResultado.Size = new System.Drawing.Size(20, 25);
            this.lblSeparadorResultado.TabIndex = 6;
            this.lblSeparadorResultado.Text = "-";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(360, 390);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(230, 40);
            this.btnGuardar.TabIndex = 7;
            this.btnGuardar.Text = "Guardar y Finalizar Partido";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // linkLblEditarLocal
            // 
            this.linkLblEditarLocal.AutoSize = true;
            this.linkLblEditarLocal.Location = new System.Drawing.Point(419, 66);
            this.linkLblEditarLocal.Name = "linkLblEditarLocal";
            this.linkLblEditarLocal.Size = new System.Drawing.Size(47, 13);
            this.linkLblEditarLocal.TabIndex = 8;
            this.linkLblEditarLocal.TabStop = true;
            this.linkLblEditarLocal.Text = "(+ Editar)";
            this.linkLblEditarLocal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblEditar_LinkClicked);
            // 
            // linkLblEditarVisitante
            // 
            this.linkLblEditarVisitante.AutoSize = true;
            this.linkLblEditarVisitante.Location = new System.Drawing.Point(887, 66);
            this.linkLblEditarVisitante.Name = "linkLblEditarVisitante";
            this.linkLblEditarVisitante.Size = new System.Drawing.Size(47, 13);
            this.linkLblEditarVisitante.TabIndex = 9;
            this.linkLblEditarVisitante.TabStop = true;
            this.linkLblEditarVisitante.Text = "(+ Editar)";
            this.linkLblEditarVisitante.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblEditar_LinkClicked);
            // 
            // FrmCargarResultadoDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 442);
            this.Controls.Add(this.linkLblEditarVisitante);
            this.Controls.Add(this.linkLblEditarLocal);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblSeparadorResultado);
            this.Controls.Add(this.txtResVisitante);
            this.Controls.Add(this.txtResLocal);
            this.Controls.Add(this.dgvVisitante);
            this.Controls.Add(this.dgvLocal);
            this.Controls.Add(this.lblVisitante);
            this.Controls.Add(this.lblLocal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCargarResultadoDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cargar Resultado Detallado";
            this.Load += new System.EventHandler(this.FrmCargarResultadoDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisitante)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLocal;
        private System.Windows.Forms.Label lblVisitante;
        private System.Windows.Forms.DataGridView dgvLocal;
        private System.Windows.Forms.DataGridView dgvVisitante;
        private System.Windows.Forms.TextBox txtResLocal;
        private System.Windows.Forms.TextBox txtResVisitante;
        private System.Windows.Forms.Label lblSeparadorResultado;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalJugador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalGoles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalAsistencias;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalAmarillas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalRojas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVisitanteJugador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVisitanteGoles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVisitanteAsistencias;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVisitanteAmarillas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVisitanteRojas;
        private System.Windows.Forms.LinkLabel linkLblEditarLocal;
        private System.Windows.Forms.LinkLabel linkLblEditarVisitante;
    }
}
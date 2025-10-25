namespace WinUI.WinForms.Gestiones.Canchas
{
    partial class FrmCanchas
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.BtnNueva = new System.Windows.Forms.Button();
            this.dgvCanchas = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.colIdCancha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCapacidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDuracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanchas)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.dgvCanchas);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1223, 459);
            this.panel1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnBorrar);
            this.panel4.Controls.Add(this.btnEditar);
            this.panel4.Controls.Add(this.BtnNueva);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(745, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(197, 459);
            this.panel4.TabIndex = 1;
            // 
            // btnBorrar
            // 
            this.btnBorrar.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBorrar.Location = new System.Drawing.Point(0, 120);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(197, 60);
            this.btnBorrar.TabIndex = 2;
            this.btnBorrar.Text = "Cambiar Habilitado";
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEditar.Location = new System.Drawing.Point(0, 60);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(197, 60);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // BtnNueva
            // 
            this.BtnNueva.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnNueva.Location = new System.Drawing.Point(0, 0);
            this.BtnNueva.Name = "BtnNueva";
            this.BtnNueva.Size = new System.Drawing.Size(197, 60);
            this.BtnNueva.TabIndex = 0;
            this.BtnNueva.Text = "Nueva";
            this.BtnNueva.UseVisualStyleBackColor = true;
            this.BtnNueva.Click += new System.EventHandler(this.BtnNueva_Click);
            // 
            // dgvCanchas
            // 
            this.dgvCanchas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCanchas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdCancha,
            this.colNombre,
            this.colDeporte,
            this.colCapacidad,
            this.colPrecio,
            this.colDuracion,
            this.colEstado});
            this.dgvCanchas.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvCanchas.Location = new System.Drawing.Point(0, 0);
            this.dgvCanchas.Name = "dgvCanchas";
            this.dgvCanchas.Size = new System.Drawing.Size(745, 459);
            this.dgvCanchas.TabIndex = 0;
            this.dgvCanchas.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCanchas_CellFormatting);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1223, 558);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1223, 99);
            this.panel3.TabIndex = 0;
            // 
            // colIdCancha
            // 
            this.colIdCancha.DataPropertyName = "IdCancha";
            this.colIdCancha.HeaderText = "ID";
            this.colIdCancha.Name = "colIdCancha";
            this.colIdCancha.Visible = false;
            // 
            // colNombre
            // 
            this.colNombre.DataPropertyName = "Nombre";
            this.colNombre.HeaderText = "Nombre Cancha";
            this.colNombre.Name = "colNombre";
            // 
            // colDeporte
            // 
            this.colDeporte.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDeporte.DataPropertyName = "Deporte";
            this.colDeporte.HeaderText = "Deporte";
            this.colDeporte.Name = "colDeporte";
            this.colDeporte.Width = 70;
            // 
            // colCapacidad
            // 
            this.colCapacidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCapacidad.DataPropertyName = "Capacidad";
            this.colCapacidad.HeaderText = "Capacidad";
            this.colCapacidad.Name = "colCapacidad";
            this.colCapacidad.Width = 83;
            // 
            // colPrecio
            // 
            this.colPrecio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colPrecio.DataPropertyName = "Precio";
            this.colPrecio.HeaderText = "Precio ($)";
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.Width = 71;
            // 
            // colDuracion
            // 
            this.colDuracion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colDuracion.DataPropertyName = "DuracionXPartidoMin";
            this.colDuracion.HeaderText = "Duración (Min)";
            this.colDuracion.Name = "colDuracion";
            this.colDuracion.Width = 93;
            // 
            // colEstado
            // 
            this.colEstado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colEstado.DataPropertyName = "Estado";
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.Width = 65;
            // 
            // FrmCanchas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 558);
            this.Controls.Add(this.panel2);
            this.Name = "FrmCanchas";
            this.Text = "FrmCanchas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCanchas_FormClosing);
            this.Load += new System.EventHandler(this.FrmCanchas_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanchas)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvCanchas;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button BtnNueva;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdCancha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCapacidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDuracion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
    }
}
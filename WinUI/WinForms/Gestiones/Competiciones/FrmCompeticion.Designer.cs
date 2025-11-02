
namespace WinUI.WinForms.Gestiones.Competiciones
{
    partial class FrmCompeticion
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnGenerarFixture = new System.Windows.Forms.Button();
            this.btnDesinscribirEquipo = new System.Windows.Forms.Button();
            this.btnInscribirEquipo = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.BtnNueva = new System.Windows.Forms.Button();
            this.dgvCompeticiones = new System.Windows.Forms.DataGridView();
            this.colIdCompeticion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOcupacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCupos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCuposMinimos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFormato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCanchaAsignada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFranjaHoraria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFrecuencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompeticiones)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1330, 560);
            this.panel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.dgvCompeticiones);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1330, 459);
            this.panel1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnGenerarFixture);
            this.panel4.Controls.Add(this.btnDesinscribirEquipo);
            this.panel4.Controls.Add(this.btnInscribirEquipo);
            this.panel4.Controls.Add(this.btnBorrar);
            this.panel4.Controls.Add(this.btnEditar);
            this.panel4.Controls.Add(this.BtnNueva);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(1133, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(197, 459);
            this.panel4.TabIndex = 1;
            // 
            // btnGenerarFixture
            // 
            this.btnGenerarFixture.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGenerarFixture.Location = new System.Drawing.Point(0, 300);
            this.btnGenerarFixture.Name = "btnGenerarFixture";
            this.btnGenerarFixture.Size = new System.Drawing.Size(197, 60);
            this.btnGenerarFixture.TabIndex = 4;
            this.btnGenerarFixture.Text = "Generar fixture";
            this.btnGenerarFixture.UseVisualStyleBackColor = true;
            this.btnGenerarFixture.Click += new System.EventHandler(this.btnGenerarFixture_Click);
            // 
            // btnDesinscribirEquipo
            // 
            this.btnDesinscribirEquipo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDesinscribirEquipo.Location = new System.Drawing.Point(0, 240);
            this.btnDesinscribirEquipo.Name = "btnDesinscribirEquipo";
            this.btnDesinscribirEquipo.Size = new System.Drawing.Size(197, 60);
            this.btnDesinscribirEquipo.TabIndex = 5;
            this.btnDesinscribirEquipo.Text = "Desinscribir equipo";
            this.btnDesinscribirEquipo.UseVisualStyleBackColor = true;
            this.btnDesinscribirEquipo.Click += new System.EventHandler(this.btnDesinscribirEquipo_Click);
            // 
            // btnInscribirEquipo
            // 
            this.btnInscribirEquipo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInscribirEquipo.Location = new System.Drawing.Point(0, 180);
            this.btnInscribirEquipo.Name = "btnInscribirEquipo";
            this.btnInscribirEquipo.Size = new System.Drawing.Size(197, 60);
            this.btnInscribirEquipo.TabIndex = 3;
            this.btnInscribirEquipo.Text = "Inscribir equipo";
            this.btnInscribirEquipo.UseVisualStyleBackColor = true;
            this.btnInscribirEquipo.Click += new System.EventHandler(this.btnInscribirEquipo_Click);
            // 
            // btnBorrar
            // 
            this.btnBorrar.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBorrar.Location = new System.Drawing.Point(0, 120);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(197, 60);
            this.btnBorrar.TabIndex = 2;
            this.btnBorrar.Text = "Deshabilitar";
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
            // dgvCompeticiones
            // 
            this.dgvCompeticiones.AllowUserToOrderColumns = true;
            this.dgvCompeticiones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvCompeticiones.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvCompeticiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompeticiones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdCompeticion,
            this.colNombre,
            this.colEstado,
            this.colOcupacion,
            this.colCupos,
            this.colCuposMinimos,
            this.colFechaInicio,
            this.colFormato,
            this.colCanchaAsignada,
            this.colDeporte,
            this.colPrecio,
            this.colFranjaHoraria,
            this.colFrecuencia});
            this.dgvCompeticiones.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvCompeticiones.Location = new System.Drawing.Point(0, 0);
            this.dgvCompeticiones.Name = "dgvCompeticiones";
            this.dgvCompeticiones.Size = new System.Drawing.Size(1133, 459);
            this.dgvCompeticiones.TabIndex = 0;
            this.dgvCompeticiones.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvCompeticiones_CellFormatting);
            // 
            // colIdCompeticion
            // 
            this.colIdCompeticion.DataPropertyName = "IdCompeticion";
            this.colIdCompeticion.HeaderText = "";
            this.colIdCompeticion.Name = "colIdCompeticion";
            this.colIdCompeticion.Visible = false;
            this.colIdCompeticion.Width = 19;
            // 
            // colNombre
            // 
            this.colNombre.DataPropertyName = "Nombre";
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.Name = "colNombre";
            this.colNombre.Width = 69;
            // 
            // colEstado
            // 
            this.colEstado.DataPropertyName = "Estado";
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.Width = 65;
            // 
            // colOcupacion
            // 
            this.colOcupacion.HeaderText = "Ocupación";
            this.colOcupacion.Name = "colOcupacion";
            this.colOcupacion.Width = 84;
            // 
            // colCupos
            // 
            this.colCupos.DataPropertyName = "Cupos";
            this.colCupos.HeaderText = "Cupos Max";
            this.colCupos.Name = "colCupos";
            this.colCupos.Width = 79;
            // 
            // colCuposMinimos
            // 
            this.colCuposMinimos.DataPropertyName = "CuposMinimos";
            this.colCuposMinimos.HeaderText = "Cupos Min.";
            this.colCuposMinimos.Name = "colCuposMinimos";
            this.colCuposMinimos.Width = 79;
            // 
            // colFechaInicio
            // 
            this.colFechaInicio.DataPropertyName = "FechaInicio";
            this.colFechaInicio.HeaderText = "Fecha de Inicio";
            this.colFechaInicio.Name = "colFechaInicio";
            this.colFechaInicio.Width = 74;
            // 
            // colFormato
            // 
            this.colFormato.DataPropertyName = "Formato";
            this.colFormato.HeaderText = "Formato";
            this.colFormato.Name = "colFormato";
            this.colFormato.Width = 70;
            // 
            // colCanchaAsignada
            // 
            this.colCanchaAsignada.HeaderText = "Cancha Asignada";
            this.colCanchaAsignada.Name = "colCanchaAsignada";
            this.colCanchaAsignada.Width = 106;
            // 
            // colDeporte
            // 
            this.colDeporte.DataPropertyName = "Deporte";
            this.colDeporte.HeaderText = "Deporte";
            this.colDeporte.Name = "colDeporte";
            this.colDeporte.Width = 70;
            // 
            // colPrecio
            // 
            this.colPrecio.DataPropertyName = "Precio";
            this.colPrecio.HeaderText = "Precio Inscripción ($)";
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.Width = 109;
            // 
            // colFranjaHoraria
            // 
            this.colFranjaHoraria.DataPropertyName = "FranjaHoraria";
            this.colFranjaHoraria.HeaderText = "Franja Horaria";
            this.colFranjaHoraria.Name = "colFranjaHoraria";
            this.colFranjaHoraria.Width = 90;
            // 
            // colFrecuencia
            // 
            this.colFrecuencia.DataPropertyName = "Frecuencia";
            this.colFrecuencia.HeaderText = "Frecuencia (días)";
            this.colFrecuencia.Name = "colFrecuencia";
            this.colFrecuencia.Width = 106;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1330, 99);
            this.panel3.TabIndex = 0;
            // 
            // FrmCompeticion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 560);
            this.Controls.Add(this.panel2);
            this.Name = "FrmCompeticion";
            this.Text = "FmCompeticion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCompeticion_FormClosing);
            this.Load += new System.EventHandler(this.FrmCompeticion_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompeticiones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnGenerarFixture;
        private System.Windows.Forms.Button btnInscribirEquipo;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button BtnNueva;
        private System.Windows.Forms.DataGridView dgvCompeticiones;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdCompeticion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOcupacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCupos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCuposMinimos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFormato;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCanchaAsignada;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFranjaHoraria;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFrecuencia;
        private System.Windows.Forms.Button btnDesinscribirEquipo;
    }
}
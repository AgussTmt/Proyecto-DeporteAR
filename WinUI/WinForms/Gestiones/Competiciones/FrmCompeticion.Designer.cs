
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
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.BtnNueva = new System.Windows.Forms.Button();
            this.dgvCompeticiones = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnInscribirEquipo = new System.Windows.Forms.Button();
            this.btnGenerarFixture = new System.Windows.Forms.Button();
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
            this.panel2.Size = new System.Drawing.Size(1220, 559);
            this.panel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.dgvCompeticiones);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1220, 459);
            this.panel1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnGenerarFixture);
            this.panel4.Controls.Add(this.btnInscribirEquipo);
            this.panel4.Controls.Add(this.btnBorrar);
            this.panel4.Controls.Add(this.btnEditar);
            this.panel4.Controls.Add(this.BtnNueva);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(1024, 0);
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
            this.btnBorrar.Text = "Eliminar";
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
            this.dgvCompeticiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompeticiones.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvCompeticiones.Location = new System.Drawing.Point(0, 0);
            this.dgvCompeticiones.Name = "dgvCompeticiones";
            this.dgvCompeticiones.Size = new System.Drawing.Size(1024, 459);
            this.dgvCompeticiones.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1220, 99);
            this.panel3.TabIndex = 0;
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
            // btnGenerarFixture
            // 
            this.btnGenerarFixture.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGenerarFixture.Location = new System.Drawing.Point(0, 240);
            this.btnGenerarFixture.Name = "btnGenerarFixture";
            this.btnGenerarFixture.Size = new System.Drawing.Size(197, 60);
            this.btnGenerarFixture.TabIndex = 4;
            this.btnGenerarFixture.Text = "Generar fixture";
            this.btnGenerarFixture.UseVisualStyleBackColor = true;
            this.btnGenerarFixture.Click += new System.EventHandler(this.btnGenerarFixture_Click);
            // 
            // FrmCompeticion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 559);
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
    }
}
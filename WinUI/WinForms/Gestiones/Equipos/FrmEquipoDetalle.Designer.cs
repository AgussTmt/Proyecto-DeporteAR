namespace WinUI.WinForms.Gestiones.Equipos
{
    partial class FrmEquipoDetalle
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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblEtiquetaCapitan = new System.Windows.Forms.Label();
            this.lblCapitanSeleccionado = new System.Windows.Forms.Label();
            this.btnSeleccionarCapitan = new System.Windows.Forms.Button();
            this.gbJugadores = new System.Windows.Forms.GroupBox();
            this.dgvJugadores = new System.Windows.Forms.DataGridView();
            this.btnAgregarJugador = new System.Windows.Forms.Button();
            this.btnQuitarJugador = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbJugadores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJugadores)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(55, 52);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(229, 20);
            this.txtNombre.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(55, 180);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(229, 20);
            this.textBox2.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(52, 36);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre";
            // 
            // lblEtiquetaCapitan
            // 
            this.lblEtiquetaCapitan.AutoSize = true;
            this.lblEtiquetaCapitan.Location = new System.Drawing.Point(65, 92);
            this.lblEtiquetaCapitan.Name = "lblEtiquetaCapitan";
            this.lblEtiquetaCapitan.Size = new System.Drawing.Size(43, 13);
            this.lblEtiquetaCapitan.TabIndex = 3;
            this.lblEtiquetaCapitan.Text = "Capitan";
            // 
            // lblCapitanSeleccionado
            // 
            this.lblCapitanSeleccionado.AutoSize = true;
            this.lblCapitanSeleccionado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCapitanSeleccionado.Location = new System.Drawing.Point(66, 126);
            this.lblCapitanSeleccionado.Name = "lblCapitanSeleccionado";
            this.lblCapitanSeleccionado.Size = new System.Drawing.Size(31, 15);
            this.lblCapitanSeleccionado.TabIndex = 4;
            this.lblCapitanSeleccionado.Text = "label";
            // 
            // btnSeleccionarCapitan
            // 
            this.btnSeleccionarCapitan.Location = new System.Drawing.Point(468, 377);
            this.btnSeleccionarCapitan.Name = "btnSeleccionarCapitan";
            this.btnSeleccionarCapitan.Size = new System.Drawing.Size(152, 23);
            this.btnSeleccionarCapitan.TabIndex = 5;
            this.btnSeleccionarCapitan.Text = "Seleccionar capitan";
            this.btnSeleccionarCapitan.UseVisualStyleBackColor = true;
            // 
            // gbJugadores
            // 
            this.gbJugadores.Controls.Add(this.dgvJugadores);
            this.gbJugadores.Location = new System.Drawing.Point(468, 36);
            this.gbJugadores.Name = "gbJugadores";
            this.gbJugadores.Size = new System.Drawing.Size(309, 187);
            this.gbJugadores.TabIndex = 6;
            this.gbJugadores.TabStop = false;
            this.gbJugadores.Text = "groupBox1";
            // 
            // dgvJugadores
            // 
            this.dgvJugadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJugadores.Location = new System.Drawing.Point(29, 31);
            this.dgvJugadores.Name = "dgvJugadores";
            this.dgvJugadores.Size = new System.Drawing.Size(240, 150);
            this.dgvJugadores.TabIndex = 0;
            // 
            // btnAgregarJugador
            // 
            this.btnAgregarJugador.Location = new System.Drawing.Point(354, 241);
            this.btnAgregarJugador.Name = "btnAgregarJugador";
            this.btnAgregarJugador.Size = new System.Drawing.Size(152, 23);
            this.btnAgregarJugador.TabIndex = 7;
            this.btnAgregarJugador.Text = "Agregar jugador";
            this.btnAgregarJugador.UseVisualStyleBackColor = true;
            // 
            // btnQuitarJugador
            // 
            this.btnQuitarJugador.Location = new System.Drawing.Point(354, 293);
            this.btnQuitarJugador.Name = "btnQuitarJugador";
            this.btnQuitarJugador.Size = new System.Drawing.Size(152, 23);
            this.btnQuitarJugador.TabIndex = 8;
            this.btnQuitarJugador.Text = "Quitar jugador";
            this.btnQuitarJugador.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(27, 392);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(157, 48);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(204, 392);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(153, 48);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FrmEquipoDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 505);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnQuitarJugador);
            this.Controls.Add(this.btnAgregarJugador);
            this.Controls.Add(this.gbJugadores);
            this.Controls.Add(this.btnSeleccionarCapitan);
            this.Controls.Add(this.lblCapitanSeleccionado);
            this.Controls.Add(this.lblEtiquetaCapitan);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txtNombre);
            this.Name = "FrmEquipoDetalle";
            this.Text = "FrmEquipoDetalle";
            this.gbJugadores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJugadores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblEtiquetaCapitan;
        private System.Windows.Forms.Label lblCapitanSeleccionado;
        private System.Windows.Forms.Button btnSeleccionarCapitan;
        private System.Windows.Forms.GroupBox gbJugadores;
        private System.Windows.Forms.DataGridView dgvJugadores;
        private System.Windows.Forms.Button btnAgregarJugador;
        private System.Windows.Forms.Button btnQuitarJugador;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
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
            this.btnAgregarJugador = new System.Windows.Forms.Button();
            this.btnQuitarJugador = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCrearNuevoJugador = new System.Windows.Forms.Button();
            this.listBoxJugadoresLibres = new System.Windows.Forms.ListBox();
            this.listBoxJugadoresAsignados = new System.Windows.Forms.ListBox();
            this.lblListboxLibre = new System.Windows.Forms.Label();
            this.lblJugadoresActuales = new System.Windows.Forms.Label();
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
            this.btnSeleccionarCapitan.Location = new System.Drawing.Point(376, 392);
            this.btnSeleccionarCapitan.Name = "btnSeleccionarCapitan";
            this.btnSeleccionarCapitan.Size = new System.Drawing.Size(153, 48);
            this.btnSeleccionarCapitan.TabIndex = 5;
            this.btnSeleccionarCapitan.Text = "Seleccionar capitan";
            this.btnSeleccionarCapitan.UseVisualStyleBackColor = true;
            this.btnSeleccionarCapitan.Click += new System.EventHandler(this.btnSeleccionarCapitan_Click);
            // 
            // btnAgregarJugador
            // 
            this.btnAgregarJugador.Location = new System.Drawing.Point(518, 309);
            this.btnAgregarJugador.Name = "btnAgregarJugador";
            this.btnAgregarJugador.Size = new System.Drawing.Size(48, 34);
            this.btnAgregarJugador.TabIndex = 7;
            this.btnAgregarJugador.Text = ">";
            this.btnAgregarJugador.UseVisualStyleBackColor = true;
            this.btnAgregarJugador.Click += new System.EventHandler(this.btnAgregarJugador_Click);
            // 
            // btnQuitarJugador
            // 
            this.btnQuitarJugador.Location = new System.Drawing.Point(711, 309);
            this.btnQuitarJugador.Name = "btnQuitarJugador";
            this.btnQuitarJugador.Size = new System.Drawing.Size(48, 34);
            this.btnQuitarJugador.TabIndex = 8;
            this.btnQuitarJugador.Text = "<";
            this.btnQuitarJugador.UseVisualStyleBackColor = true;
            this.btnQuitarJugador.Click += new System.EventHandler(this.btnQuitarJugador_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(27, 392);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(157, 48);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(204, 392);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(153, 48);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCrearNuevoJugador
            // 
            this.btnCrearNuevoJugador.Location = new System.Drawing.Point(547, 392);
            this.btnCrearNuevoJugador.Name = "btnCrearNuevoJugador";
            this.btnCrearNuevoJugador.Size = new System.Drawing.Size(153, 48);
            this.btnCrearNuevoJugador.TabIndex = 11;
            this.btnCrearNuevoJugador.Text = "Crear jugador para equipo";
            this.btnCrearNuevoJugador.UseVisualStyleBackColor = true;
            this.btnCrearNuevoJugador.Click += new System.EventHandler(this.btnCrearNuevoJugador_Click);
            // 
            // listBoxJugadoresLibres
            // 
            this.listBoxJugadoresLibres.FormattingEnabled = true;
            this.listBoxJugadoresLibres.Location = new System.Drawing.Point(463, 52);
            this.listBoxJugadoresLibres.Name = "listBoxJugadoresLibres";
            this.listBoxJugadoresLibres.Size = new System.Drawing.Size(165, 251);
            this.listBoxJugadoresLibres.TabIndex = 12;
            // 
            // listBoxJugadoresAsignados
            // 
            this.listBoxJugadoresAsignados.FormattingEnabled = true;
            this.listBoxJugadoresAsignados.Location = new System.Drawing.Point(663, 52);
            this.listBoxJugadoresAsignados.Name = "listBoxJugadoresAsignados";
            this.listBoxJugadoresAsignados.Size = new System.Drawing.Size(148, 251);
            this.listBoxJugadoresAsignados.TabIndex = 13;
            // 
            // lblListboxLibre
            // 
            this.lblListboxLibre.AutoSize = true;
            this.lblListboxLibre.Location = new System.Drawing.Point(475, 36);
            this.lblListboxLibre.Name = "lblListboxLibre";
            this.lblListboxLibre.Size = new System.Drawing.Size(107, 13);
            this.lblListboxLibre.TabIndex = 14;
            this.lblListboxLibre.Text = "Jugadores sin equipo";
            // 
            // lblJugadoresActuales
            // 
            this.lblJugadoresActuales.AutoSize = true;
            this.lblJugadoresActuales.Location = new System.Drawing.Point(660, 36);
            this.lblJugadoresActuales.Name = "lblJugadoresActuales";
            this.lblJugadoresActuales.Size = new System.Drawing.Size(151, 13);
            this.lblJugadoresActuales.TabIndex = 15;
            this.lblJugadoresActuales.Text = "Jugadores actuales del equipo";
            // 
            // FrmEquipoDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 505);
            this.Controls.Add(this.lblJugadoresActuales);
            this.Controls.Add(this.lblListboxLibre);
            this.Controls.Add(this.listBoxJugadoresAsignados);
            this.Controls.Add(this.listBoxJugadoresLibres);
            this.Controls.Add(this.btnCrearNuevoJugador);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnQuitarJugador);
            this.Controls.Add(this.btnAgregarJugador);
            this.Controls.Add(this.btnSeleccionarCapitan);
            this.Controls.Add(this.lblCapitanSeleccionado);
            this.Controls.Add(this.lblEtiquetaCapitan);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txtNombre);
            this.Name = "FrmEquipoDetalle";
            this.Text = "FrmEquipoDetalle";
            this.Load += new System.EventHandler(this.FrmEquipoDetalle_Load);
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
        private System.Windows.Forms.Button btnAgregarJugador;
        private System.Windows.Forms.Button btnQuitarJugador;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCrearNuevoJugador;
        private System.Windows.Forms.ListBox listBoxJugadoresLibres;
        private System.Windows.Forms.ListBox listBoxJugadoresAsignados;
        private System.Windows.Forms.Label lblListboxLibre;
        private System.Windows.Forms.Label lblJugadoresActuales;
    }
}
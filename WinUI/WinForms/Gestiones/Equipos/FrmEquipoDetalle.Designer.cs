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
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblEtiquetaCapitan = new System.Windows.Forms.Label();
            this.lblCapitanSeleccionado = new System.Windows.Forms.Label();
            this.btnSeleccionarCapitan = new System.Windows.Forms.Button();
            this.btnAgregarJugador = new System.Windows.Forms.Button();
            this.btnQuitarJugador = new System.Windows.Forms.Button();
            this.btnCrearNuevoJugador = new System.Windows.Forms.Button();
            this.listBoxJugadoresLibres = new System.Windows.Forms.ListBox();
            this.listBoxJugadoresAsignados = new System.Windows.Forms.ListBox();
            this.lblListboxLibre = new System.Windows.Forms.Label();
            this.lblJugadoresActuales = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnGuardarYNuevo = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.groupBoxPlantel = new System.Windows.Forms.GroupBox();
            this.panelBotones.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxPlantel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNombre.Location = new System.Drawing.Point(19, 44);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(325, 23);
            this.txtNombre.TabIndex = 0;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNombre.Location = new System.Drawing.Point(16, 26);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(110, 15);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre del Equipo";
            // 
            // lblEtiquetaCapitan
            // 
            this.lblEtiquetaCapitan.AutoSize = true;
            this.lblEtiquetaCapitan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEtiquetaCapitan.Location = new System.Drawing.Point(16, 81);
            this.lblEtiquetaCapitan.Name = "lblEtiquetaCapitan";
            this.lblEtiquetaCapitan.Size = new System.Drawing.Size(48, 15);
            this.lblEtiquetaCapitan.TabIndex = 3;
            this.lblEtiquetaCapitan.Text = "Capitán";
            // 
            // lblCapitanSeleccionado
            // 
            this.lblCapitanSeleccionado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCapitanSeleccionado.BackColor = System.Drawing.SystemColors.Window;
            this.lblCapitanSeleccionado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCapitanSeleccionado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCapitanSeleccionado.Location = new System.Drawing.Point(19, 99);
            this.lblCapitanSeleccionado.Name = "lblCapitanSeleccionado";
            this.lblCapitanSeleccionado.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblCapitanSeleccionado.Size = new System.Drawing.Size(222, 23);
            this.lblCapitanSeleccionado.TabIndex = 4;
            this.lblCapitanSeleccionado.Text = "(Ninguno)";
            this.lblCapitanSeleccionado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSeleccionarCapitan
            // 
            this.btnSeleccionarCapitan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionarCapitan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSeleccionarCapitan.Location = new System.Drawing.Point(247, 99);
            this.btnSeleccionarCapitan.Name = "btnSeleccionarCapitan";
            this.btnSeleccionarCapitan.Size = new System.Drawing.Size(97, 23);
            this.btnSeleccionarCapitan.TabIndex = 5;
            this.btnSeleccionarCapitan.Text = "Seleccionar...";
            this.btnSeleccionarCapitan.UseVisualStyleBackColor = true;
            this.btnSeleccionarCapitan.Click += new System.EventHandler(this.btnSeleccionarCapitan_Click);
            // 
            // btnAgregarJugador
            // 
            this.btnAgregarJugador.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarJugador.Location = new System.Drawing.Point(232, 114);
            this.btnAgregarJugador.Name = "btnAgregarJugador";
            this.btnAgregarJugador.Size = new System.Drawing.Size(40, 34);
            this.btnAgregarJugador.TabIndex = 7;
            this.btnAgregarJugador.Text = ">";
            this.btnAgregarJugador.UseVisualStyleBackColor = true;
            this.btnAgregarJugador.Click += new System.EventHandler(this.btnAgregarJugador_Click);
            // 
            // btnQuitarJugador
            // 
            this.btnQuitarJugador.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitarJugador.Location = new System.Drawing.Point(232, 168);
            this.btnQuitarJugador.Name = "btnQuitarJugador";
            this.btnQuitarJugador.Size = new System.Drawing.Size(40, 34);
            this.btnQuitarJugador.TabIndex = 8;
            this.btnQuitarJugador.Text = "<";
            this.btnQuitarJugador.UseVisualStyleBackColor = true;
            this.btnQuitarJugador.Click += new System.EventHandler(this.btnQuitarJugador_Click);
            // 
            // btnCrearNuevoJugador
            // 
            this.btnCrearNuevoJugador.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCrearNuevoJugador.Location = new System.Drawing.Point(19, 269);
            this.btnCrearNuevoJugador.Name = "btnCrearNuevoJugador";
            this.btnCrearNuevoJugador.Size = new System.Drawing.Size(193, 30);
            this.btnCrearNuevoJugador.TabIndex = 11;
            this.btnCrearNuevoJugador.Text = "Crear Nuevo Jugador...";
            this.btnCrearNuevoJugador.UseVisualStyleBackColor = true;
            this.btnCrearNuevoJugador.Click += new System.EventHandler(this.btnCrearNuevoJugador_Click);
            // 
            // listBoxJugadoresLibres
            // 
            this.listBoxJugadoresLibres.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listBoxJugadoresLibres.FormattingEnabled = true;
            this.listBoxJugadoresLibres.ItemHeight = 15;
            this.listBoxJugadoresLibres.Location = new System.Drawing.Point(19, 44);
            this.listBoxJugadoresLibres.Name = "listBoxJugadoresLibres";
            this.listBoxJugadoresLibres.Size = new System.Drawing.Size(193, 214);
            this.listBoxJugadoresLibres.TabIndex = 12;
            // 
            // listBoxJugadoresAsignados
            // 
            this.listBoxJugadoresAsignados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxJugadoresAsignados.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listBoxJugadoresAsignados.FormattingEnabled = true;
            this.listBoxJugadoresAsignados.ItemHeight = 15;
            this.listBoxJugadoresAsignados.Location = new System.Drawing.Point(292, 44);
            this.listBoxJugadoresAsignados.Name = "listBoxJugadoresAsignados";
            this.listBoxJugadoresAsignados.Size = new System.Drawing.Size(193, 259);
            this.listBoxJugadoresAsignados.TabIndex = 13;
            // 
            // lblListboxLibre
            // 
            this.lblListboxLibre.AutoSize = true;
            this.lblListboxLibre.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblListboxLibre.Location = new System.Drawing.Point(16, 26);
            this.lblListboxLibre.Name = "lblListboxLibre";
            this.lblListboxLibre.Size = new System.Drawing.Size(121, 15);
            this.lblListboxLibre.TabIndex = 14;
            this.lblListboxLibre.Text = "Jugadores sin equipo:";
            // 
            // lblJugadoresActuales
            // 
            this.lblJugadoresActuales.AutoSize = true;
            this.lblJugadoresActuales.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblJugadoresActuales.Location = new System.Drawing.Point(289, 26);
            this.lblJugadoresActuales.Name = "lblJugadoresActuales";
            this.lblJugadoresActuales.Size = new System.Drawing.Size(105, 15);
            this.lblJugadoresActuales.TabIndex = 15;
            this.lblJugadoresActuales.Text = "Plantel del Equipo:";
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelBotones.Controls.Add(this.btnGuardarYNuevo);
            this.panelBotones.Controls.Add(this.btnCancelar);
            this.panelBotones.Controls.Add(this.btnGuardar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 483);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(524, 61);
            this.panelBotones.TabIndex = 16;
            // 
            // btnGuardarYNuevo
            // 
            this.btnGuardarYNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarYNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarYNuevo.Location = new System.Drawing.Point(197, 15);
            this.btnGuardarYNuevo.Name = "btnGuardarYNuevo";
            this.btnGuardarYNuevo.Size = new System.Drawing.Size(120, 34);
            this.btnGuardarYNuevo.TabIndex = 6;
            this.btnGuardarYNuevo.Text = "Guardar y Nuevo";
            this.btnGuardarYNuevo.UseVisualStyleBackColor = true;
            this.btnGuardarYNuevo.Click += new System.EventHandler(this.btnGuardarYNuevo_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(429, 15);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(83, 34);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(12, 15);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 34);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfo.Controls.Add(this.lblNombre);
            this.groupBoxInfo.Controls.Add(this.txtNombre);
            this.groupBoxInfo.Controls.Add(this.lblEtiquetaCapitan);
            this.groupBoxInfo.Controls.Add(this.lblCapitanSeleccionado);
            this.groupBoxInfo.Controls.Add(this.btnSeleccionarCapitan);
            this.groupBoxInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxInfo.Location = new System.Drawing.Point(12, 12);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(500, 142);
            this.groupBoxInfo.TabIndex = 17;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Información Básica";
            // 
            // groupBoxPlantel
            // 
            this.groupBoxPlantel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPlantel.Controls.Add(this.lblListboxLibre);
            this.groupBoxPlantel.Controls.Add(this.btnAgregarJugador);
            this.groupBoxPlantel.Controls.Add(this.lblJugadoresActuales);
            this.groupBoxPlantel.Controls.Add(this.btnQuitarJugador);
            this.groupBoxPlantel.Controls.Add(this.listBoxJugadoresAsignados);
            this.groupBoxPlantel.Controls.Add(this.btnCrearNuevoJugador);
            this.groupBoxPlantel.Controls.Add(this.listBoxJugadoresLibres);
            this.groupBoxPlantel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxPlantel.Location = new System.Drawing.Point(12, 160);
            this.groupBoxPlantel.Name = "groupBoxPlantel";
            this.groupBoxPlantel.Size = new System.Drawing.Size(500, 317);
            this.groupBoxPlantel.TabIndex = 18;
            this.groupBoxPlantel.TabStop = false;
            this.groupBoxPlantel.Text = "Gestión de Plantel";
            // 
            // FrmEquipoDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(524, 544);
            this.Controls.Add(this.groupBoxPlantel);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.panelBotones);
            this.MinimumSize = new System.Drawing.Size(540, 583);
            this.Name = "FrmEquipoDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmEquipoDetalle";
            this.Load += new System.EventHandler(this.FrmEquipoDetalle_Load);
            this.panelBotones.ResumeLayout(false);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBoxPlantel.ResumeLayout(false);
            this.groupBoxPlantel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblEtiquetaCapitan;
        private System.Windows.Forms.Label lblCapitanSeleccionado;
        private System.Windows.Forms.Button btnSeleccionarCapitan;
        private System.Windows.Forms.Button btnAgregarJugador;
        private System.Windows.Forms.Button btnQuitarJugador;
        private System.Windows.Forms.Button btnCrearNuevoJugador;
        private System.Windows.Forms.ListBox listBoxJugadoresLibres;
        private System.Windows.Forms.ListBox listBoxJugadoresAsignados;
        private System.Windows.Forms.Label lblListboxLibre;
        private System.Windows.Forms.Label lblJugadoresActuales;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnGuardarYNuevo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.GroupBox groupBoxPlantel;
    }
}
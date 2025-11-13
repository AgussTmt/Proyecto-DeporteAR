using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection; 
using System.Windows.Forms;
using Services.Facade;
using Services__ArqBase_.Bll;
using Services__ArqBase_.Facade; 

namespace WinUI.WinForms
{
    public partial class FrmConfiguracionInicial : Form
    {
        public FrmConfiguracionInicial()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. --- Validaciones de UI ---
            if (string.IsNullOrWhiteSpace(txtServidor.Text) ||
                string.IsNullOrWhiteSpace(txtUsuarioSQL.Text) ||
                string.IsNullOrWhiteSpace(txtPassSQL.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassEmail.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;

            try
            {
                // 2. --- ¡Llamamos a la BLL! ---
                SetupService.InitializeApplication(
                    txtServidor.Text,
                    txtUsuarioSQL.Text,
                    txtPassSQL.Text,
                    txtEmail.Text,
                    txtPassEmail.Text
                );

                // 3. --- Éxito ---
                this.Cursor = Cursors.Default;
                MessageBox.Show("¡Configuración guardada y bases de datos creadas con éxito!\n\nLa aplicación se reiniciará para aplicar los cambios.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Application.Restart();
            }
            catch (Exception ex)
            {
                // 4. --- Error ---
                this.Cursor = Cursors.Default;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                MessageBox.Show($"Falló la configuración:\n\n{ex.Message}\n\nAsegúrese de que el servidor SQL esté accesible y que el usuario '{txtUsuarioSQL.Text}' tenga permisos 'dbcreator'.", "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
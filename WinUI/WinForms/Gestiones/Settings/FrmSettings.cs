using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services__ArqBase_.Facade;
using System.Configuration;
using Services.Facade; 

namespace WinUI.WinForms.Gestiones.Settings
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {

            IdiomaHelper.TraducirControles(this);


            LlenarComboBoxBaseDeDatos();

  
            CargarDatosEmail();
        }

        private void LlenarComboBoxBaseDeDatos()
        {
            var opciones = new Dictionary<string, string>();
            opciones.Add("SecurityString", "Base de Seguridad");
            opciones.Add("BusinessString", "Base de Negocio");
            ComboBoxBaseDeDatos.DataSource = new BindingSource(opciones, null);
            ComboBoxBaseDeDatos.DisplayMember = "Value";
            ComboBoxBaseDeDatos.ValueMember = "Key";
        }

        /// <summary>
        ///Carga el email y DESENCRIPTA la contraseña desde app.config
        /// </summary>
        private void CargarDatosEmail()
        {
            try
            {
                string email = ConfigurationManager.AppSettings["SenderEmail"];
                string passEncriptada = ConfigurationManager.AppSettings["ContraseñaEmail"];

                txtSenderEmail.Text = email;

                if (!string.IsNullOrEmpty(passEncriptada))
                {

                    txtSenderPassword.Text = CryptographyService.Decrypt(passEncriptada);
                }
                else
                {
                    txtSenderPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la configuración de email. Es posible que la contraseña esté corrupta.\n\n{ex.Message}", "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenderPassword.Text = "ERROR AL LEER";
            }
        }



        private void BtnVerLogs_Click(object sender, EventArgs e)
        {
            var frmMain = this.ParentForm as FrmMain;
            if (frmMain != null)
            {
                frmMain.OpenChildForm(new FrmLogs(), sender);
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el formulario principal.");
            }
        }

        private async void BtnBackUp_Click(object sender, EventArgs e)
        {
            if (ComboBoxBaseDeDatos.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una base de datos para respaldar.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string nombreConexion = ComboBoxBaseDeDatos.SelectedValue.ToString();
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Backup File (*.bak)|*.bak";
                saveDialog.FileName = $"{ComboBoxBaseDeDatos.SelectedText.ToString()}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                saveDialog.Title = "Guardar Backup de Base de Datos";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;
                        lblStatus.Text = "Realizando backup, por favor espera...";

                        await DatabaseService.RealizarBackupAsync(saveDialog.FileName, nombreConexion);

                        MessageBox.Show("¡Backup completado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error durante el backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        lblStatus.Text = "Listo.";
                    }
                }
            }
        }

        private async void BtnRestore_Click(object sender, EventArgs e)
        {
            if (ComboBoxBaseDeDatos.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona la base de datos que deseas restaurar.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string nombreConexion = ComboBoxBaseDeDatos.SelectedValue.ToString();

            var confirmResult = MessageBox.Show(
                $"¿Estás seguro de que deseas restaurar la base de datos '{ComboBoxBaseDeDatos.SelectedText.ToString()}'?\n\n¡ADVERTENCIA! Todos los datos actuales en esa base de datos se perderán permanentemente.",
                "Confirmación de Restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
            {
                return;
            }

            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Backup File (*.bak)|*.bak";
                openDialog.Title = "Seleccionar Archivo de Backup para Restaurar";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;
                        lblStatus.Text = "Restaurando base de datos, esto puede tardar...";

                        await DatabaseService.RealizarRestoreAsync(openDialog.FileName, nombreConexion);

                        MessageBox.Show("¡Restauración completada exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error durante la restauración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        lblStatus.Text = "Listo.";
                    }
                }
            }
        }

        /// <summary>
        /// Guarda la configuración del email en el app.config
        /// </summary>
        private void btnGuardarEmail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSenderEmail.Text) || string.IsNullOrWhiteSpace(txtSenderPassword.Text))
            {
                MessageBox.Show("Debe completar tanto el email como la contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["SenderEmail"].Value = txtSenderEmail.Text;
                config.AppSettings.Settings["ContraseñaEmail"].Value = CryptographyService.Encrypt(txtSenderPassword.Text);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                this.Cursor = Cursors.Default;
                MessageBox.Show("Configuración de email guardada y encriptada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Error al guardar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
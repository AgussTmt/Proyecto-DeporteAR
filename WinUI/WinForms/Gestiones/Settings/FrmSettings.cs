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

namespace WinUI.WinForms.Gestiones.Settings
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
            LlenarComboBoxBaseDeDatos();
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

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnVerLogs_Click(object sender, EventArgs e)
        {
            var frmMain = this.ParentForm as FrmMain;
            if (frmMain != null)
            {
                // Simula la apertura normal de un form hijo
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
                // Sugerir un nombre de archivo por defecto (¡muy útil!)
                saveDialog.FileName = $"{ComboBoxBaseDeDatos.SelectedText.ToString()}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                saveDialog.Title = "Guardar Backup de Base de Datos";

                // 3. Comprobar si el usuario hizo clic en "Guardar"
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 4. Dar feedback visual al usuario y deshabilitar la UI
                        this.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;
                        lblStatus.Text = "Realizando backup, por favor espera...";

                        // 5. Llamar a tu capa de negocio (BLL) con los datos necesarios

                        await DatabaseService.RealizarBackupAsync(saveDialog.FileName, nombreConexion);

                        // 6. Informar al usuario que todo salió bien
                        MessageBox.Show("¡Backup completado exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // 7. Si algo falla, informar del error
                        MessageBox.Show($"Ocurrió un error durante el backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // 8. ¡MUY IMPORTANTE! Rehabilitar la UI, sin importar si hubo éxito o error
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

            // --- ¡Paso de Confirmación Crucial! ---
            var confirmResult = MessageBox.Show(
                $"¿Estás seguro de que deseas restaurar la base de datos '{ComboBoxBaseDeDatos.SelectedText.ToString()}'?\n\n¡ADVERTENCIA! Todos los datos actuales en esa base de datos se perderán permanentemente.",
                "Confirmación de Restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.No)
            {
                return; // El usuario canceló la operación
            }

            // 2. Configurar y mostrar el OpenFileDialog
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Backup File (*.bak)|*.bak";
                openDialog.Title = "Seleccionar Archivo de Backup para Restaurar";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 3. Dar feedback y deshabilitar UI
                        this.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;
                        lblStatus.Text = "Restaurando base de datos, esto puede tardar...";

                        // 4. Llamar a la BLL

                        await DatabaseService.RealizarRestoreAsync(openDialog.FileName, nombreConexion);

                        MessageBox.Show("¡Restauración completada exitosamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error durante la restauración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // 5. Rehabilitar la UI
                        this.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        lblStatus.Text = "Listo.";
                    }
                }
            }
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            IdiomaHelper.TraducirControles(this);
        }


        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}

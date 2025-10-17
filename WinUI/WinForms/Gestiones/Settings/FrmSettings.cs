using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUI.WinForms.Gestiones.Settings
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
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
    }
}

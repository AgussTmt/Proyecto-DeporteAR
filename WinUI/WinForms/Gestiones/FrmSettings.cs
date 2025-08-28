using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.Bll;

namespace WinUI.WinForms.Gestiones
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }
        //7d9875d7-965d-4ef3-9ae7-39236678c57c
        //0a7c5d0d-7d9d-4fcd-b8b3-7b80f8306181
        private void FrmSettings_Load(object sender, EventArgs e)
        {
            DgvListaUsuarios.DataSource = UsuarioBll.TraerUsuarios();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

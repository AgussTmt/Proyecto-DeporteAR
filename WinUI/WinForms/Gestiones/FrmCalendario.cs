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

namespace WinUI.WinForms.Gestiones
{
    public partial class FrmCalendario : Form
    {
        public FrmCalendario()
        {
            InitializeComponent();
        }

        private void FrmCalendario_Load(object sender, EventArgs e)
        {
            IdiomaHelper.TraducirControles(this);
        }

        

        private void FrmCalendario_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}

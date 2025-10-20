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
    public partial class FrmCanchas : Form
    {
        public FrmCanchas()
        {
            IdiomaHelper.TraducirControles(this);
        }

        private void FrmCanchas_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}

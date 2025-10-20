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
    public partial class FrmCompeticion : Form
    {
        public FrmCompeticion()
        {
            InitializeComponent();
        }

        private void FrmCompeticion_Load(object sender, EventArgs e)
        {
            IdiomaHelper.IdiomaCambio += TraducirFormulario;
        }

        private void TraducirFormulario()
        {
            IdiomaHelper.TraducirControles(this);
        }

        private void FrmCompeticion_FormClosing(object sender, FormClosingEventArgs e)
        {
            IdiomaHelper.IdiomaCambio -= TraducirFormulario;
        }
    }
}

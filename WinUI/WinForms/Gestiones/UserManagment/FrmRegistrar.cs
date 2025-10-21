using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.DomainModel;
using Services.Facade;
using Services__ArqBase_.Facade;

namespace WinUI.WinForms.Gestiones
{
    public partial class FrmRegistrar : Form
    {
        public FrmRegistrar()
        {
            InitializeComponent();
        }

        private void Registro_Load(object sender, EventArgs e)
        {
            IdiomaHelper.TraducirControles(this);
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario(TxtNombre.Text, TxtEmail.Text, TxtContraseña.Text);
                LoginService.RegistrarUsuario(usuario);
                var frmMain = this.ParentForm as FrmMain;
                if (frmMain != null)
                {

                    frmMain.OpenChildForm(new FrmUserManagment(usuario), sender);
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar el formulario principal.");
                }
                //44dff3c8-6c67-4fd7-b48f-d2aa0bbaec60
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
        }

        private void FrmRegistrar_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}

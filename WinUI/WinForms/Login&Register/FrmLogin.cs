using Services.DomainModel;
using Services.Facade;
using Services__ArqBase_.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinUI.WinForms;
using WinUI.WinForms.Login_Register;

namespace WinUI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

            //Validamos las credenciales del usuario
            try
            {
                Usuario usuario = LoginService.ValidarCredenciales(txtUsuario.Text, txtContraseña.Text);
                this.Hide();
                new FrmMain(usuario).ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtContraseña.Clear();
                txtUsuario.Clear();
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
            IdiomaHelper.TraducirControles(this);
        }

       

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FrmLogin_VisibleChanged(object sender, EventArgs e)
        {
            IdiomaHelper.TraducirControles(this);
            
        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "USUARIO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
            }
            
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUARIO";
                txtUsuario.ForeColor = Color.DarkGray;
            }
        }

        private void txtContraseña_Enter(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "CONTRASEÑA")
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = Color.LightGray;
                txtContraseña.UseSystemPasswordChar = true;
            }
        }

        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "")
            {
                txtContraseña.Text = "CONTRASEÑA";
                txtContraseña.ForeColor = Color.DarkGray;
                txtContraseña.UseSystemPasswordChar = false;
            }
        }

        private void lnkRecuperacion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new FrmPassRecovery().ShowDialog();
            this.Close();
            
        }
    }
}

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
                Usuario usuario = LoginService.ValidarCredenciales(txtUsuario.Text, txtContraseña.Text);
                //Si las credenciales son correctas, mostramos un mensaje de bienvenida
                
                //MessageBox.Show($"Bienvenido {usuario.Nombre} ");

                //foreach (var item in usuario.Patentes)
                //{
                //    MessageBox.Show(item.DataKey);
                //}

                //Cerramos el formulario de login
                this.Hide();
                new FrmMain(usuario).ShowDialog();
                this.Show();
                txtContraseña.Clear();
                txtUsuario.Clear();

            //6f5e615b-70c2-4b05-aaf7-c12f88d3645a
            //3075A247-1996-47F6-9ADC-B52ADB6E501E



        }

        private void Login_Load(object sender, EventArgs e)
        {
            //Primero vamos a crear un usuario admin con una pass hasheada
            //LoginService.RegistrarUsuario(new Usuario( "gaston", 
            //    "gastonweingand@gmail.com", "1234" ));
            IdiomaHelper.TraducirControles(this);
            //Console.WriteLine($"Contraseña: {CryptographyService.HashMd5("admin")}");
            

        }

       

        private void LinkRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRegistrar frmRegistrar = new FrmRegistrar();
            this.Close();
            frmRegistrar.Show();
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
    }
}

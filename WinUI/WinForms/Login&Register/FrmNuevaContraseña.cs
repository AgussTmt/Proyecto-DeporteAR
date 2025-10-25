using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.Dal.Interfaces;
using Services__ArqBase_.Facade;

namespace WinUI.WinForms.Login_Register
{
    public partial class FrmNuevaContraseña : Form
    {
        private string Email;
        public FrmNuevaContraseña(string email)
        {

            InitializeComponent();
            Email = email;
        }

        private void TxtCodigo_Enter(object sender, EventArgs e)
        {
            if (TxtCodigo.Text == "INGRESAR CODIGO")
            {
                TxtCodigo.Text = "";
                TxtCodigo.ForeColor = Color.LightGray;
            }
        }

        private void TxtCodigo_Leave(object sender, EventArgs e)
        {
            if (TxtCodigo.Text == "")
            {
                TxtCodigo.Text = "INGRESAR CODIGO";
                TxtCodigo.ForeColor = Color.DarkGray;
            }
        }

        

        private void txtNuevaPassword_Enter(object sender, EventArgs e)
        {
            if (txtNuevaPassword.Text == "INGRESAR NUEVA CONTRASEÑA")
            {
                txtNuevaPassword.Text = "";
                txtNuevaPassword.ForeColor = Color.LightGray;
                txtNuevaPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtNuevaPassword_Leave(object sender, EventArgs e)
        {
            if (txtNuevaPassword.Text == "")
            {
                txtNuevaPassword.Text = "INGRESAR NUEVA CONTRASEÑA";
                txtNuevaPassword.ForeColor = Color.DarkGray;
                txtNuevaPassword.UseSystemPasswordChar = false;
            }
        }

        

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "CONFIRMAR NUEVA CONTRASEÑA")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.LightGray;
                textBox1.UseSystemPasswordChar = true;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "CONFIRMAR NUEVA CONTRASEÑA";
                textBox1.ForeColor = Color.DarkGray;
                textBox1.UseSystemPasswordChar = false;
            }
        }

        private void BtnCambiarPass_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = TxtCodigo.Text;

                if (txtNuevaPassword.Text != textBox1.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden");
                    return;
                }

            string nuevaPass = txtNuevaPassword.Text;

            
                PassRecoveryService.ResetearPassword(Email, codigo, nuevaPass);

                MessageBox.Show("¡Contraseña actualizada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                new FrmLogin().ShowDialog();
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error al resetear la password");
            }
            


        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

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

namespace WinUI.WinForms.Login_Register
{
    public partial class FrmPassRecovery : Form
    {
        public FrmPassRecovery()
        {
            InitializeComponent();
        }

        private void BtnPassRecovery_Click(object sender, EventArgs e)
        {
            string email = TxtEmail.Text.ToLower();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Por favor, ingresa un email.");
                return;
            }

            bool exito = PassRecoveryService.SolicitarRecuperacion(email);

            if (exito)
            {
                MessageBox.Show("Si el email está registrado, recibirás un código en tu correo.", "Solicitud Enviada");
                this.Hide();
                new FrmNuevaContraseña(TxtEmail.Text).ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ocurrió un error inesperado al enviar el correo. Por favor, intenta de nuevo más tarde.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmPassRecovery_Load(object sender, EventArgs e)
        {

        }

        private void lnkVolverLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new FrmLogin().ShowDialog();
            this.Close();
        }

        private void TxtEmail_Enter(object sender, EventArgs e)
        {
            if (TxtEmail.Text == "INGRESAR EMAIL")
            {
                TxtEmail.Text = "";
                TxtEmail.ForeColor = Color.LightGray;
            }
        }

        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            if (TxtEmail.Text == "")
            {
                TxtEmail.Text = "INGRESAR EMAIL";
                TxtEmail.ForeColor = Color.DarkGray;
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

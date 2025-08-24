using Services.DomainModel;
using Services.Facade;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
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
                new Principal(usuario).ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                //Si hay un error, mostramos un mensaje de error
                MessageBox.Show(ex.Message);
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
            //Primero vamos a crear un usuario admin con una pass hasheada
            //LoginService.RegistrarUsuario(new Usuario( "gaston", 
            //    "gastonweingand@gmail.com", "1234" ));

            //Console.WriteLine($"Contraseña: {CryptographyService.HashMd5("admin")}");

        }
    }
}

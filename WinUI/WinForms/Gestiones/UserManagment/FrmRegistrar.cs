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
using Services.DomainModel;
using Services.Facade;
using Services__ArqBase_.Facade;
using WinUI.WinForms.Gestiones.UserManagment;

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
            TxtContraseña.PasswordChar = '*';
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario(TxtNombre.Text, TxtEmail.Text, TxtContraseña.Text);
                LoginService.RegistrarUsuario(usuario);
                NavegarHaciaUserManagment(sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardarYPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Crear usuario
                Usuario usuario = new Usuario(TxtNombre.Text, TxtEmail.Text, TxtContraseña.Text);
                LoginService.RegistrarUsuario(usuario);

                // 2. Traer el usuario que acabamos de crear (para tener su ID)
                usuario = UserManagmentService.GetByEmail(usuario.Email);

                // 3. Abrir el form de permisos pasándole el nuevo usuario
                var frmMain = this.ParentForm as FrmMain;
                if (frmMain != null)
                {
                    frmMain.OpenChildForm(new FrmModificarPermisos(usuario), sender);
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar el formulario principal.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // "Cancelar" significa volver a la grilla de usuarios
            NavegarHaciaUserManagment(sender);
        }

        /// <summary>
        /// Método helper para centralizar la navegación de vuelta a la grilla de usuarios.
        /// </summary>
        private void NavegarHaciaUserManagment(object sender)
        {
            var frmMain = this.ParentForm as FrmMain;
            if (frmMain != null)
            {
                //
                frmMain.OpenChildForm(new FrmUserManagment(), sender);
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el formulario principal.");
            }
        }

        // El LinkLabel ya no existe
        // private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { ... }

        private void FrmRegistrar_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
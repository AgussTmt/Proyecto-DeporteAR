using Services.DomainModel;
using Services.Facade.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUI.WinForms
{
    public partial class Principal : Form
    {
        public Usuario Usuario { get; set; }
        public Principal(Usuario user)
        {
            Usuario = user;
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            this.Text = $"Bienvenido {Usuario.Nombre}";
            // Cargamos los privilegios del usuario en el menú
            //foreach (var item in Usuario.Patentes)
            //{
            //    for
            //}

            foreach (ToolStripItem item in mnuPrincipal.Items)
            {
                if(Usuario.Patentes.Exists(o => o.DataKey == item.Name))
                {
                    item.Visible = true;
                }
                else
                {
                    item.Visible = false;
                }
                //item.Text = item.Text.Traducir();
            } 
        }

        private void tsmGestionVentas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gestión de Ventas no implementada.");
        }

        private void tsmInformes_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Informes no implementados.");
        }

        private void tsmAdministracion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Administración no implementada.");
        }
    }
}

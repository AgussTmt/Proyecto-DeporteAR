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
    public partial class FrmPrincipal : Form
    {
        public Usuario Usuario { get; set; }
        public FrmPrincipal(Usuario user)
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

        //    foreach (ToolStripItem item in mnuPrincipal.Items)
        //    {
        //        if(Usuario.Patentes.Exists(o => o.DataKey == item.Name))
        //        {
        //            item.Visible = true;
        //        }
        //        else
        //        {
        //            item.Visible = false;
        //        }
        //        //item.Text = item.Text.Traducir();
        //    } 
       /* //*/}

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

 

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        bool sidebarExpand = true;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 5;
                if (sidebar.Width <= 62)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sidebar.Width += 5;
                if (sidebar.Width >= 186)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }
        }

        private void BtnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

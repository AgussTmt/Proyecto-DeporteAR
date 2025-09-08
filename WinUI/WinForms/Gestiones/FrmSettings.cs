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

namespace WinUI.WinForms.Gestiones
{
    public partial class FrmSettings : Form
    {
        private BindingSource BsListaUsuarios;
        public FrmSettings()
        {
            InitializeComponent();
        }
        private void FrmSettings_Load(object sender, EventArgs e)
        {
            var usuarios = UsuarioBll.TraerUsuarios();

            if(usuarios is not null && usuarios.Count > 0)
            {

                DataTable DtUsuarios = ConvertUserListToDT(usuarios);
                BsListaUsuarios = new BindingSource();

                BsListaUsuarios.DataSource = DtUsuarios;

                DgvListaUsuarios.DataSource = BsListaUsuarios;
            }

            else
            {
                MessageBox.Show("no se encontraro usuarios");
            }
        }    
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string filtro = TxtSearch.Text;

            if (string.IsNullOrEmpty(filtro))
            {
                BsListaUsuarios.RemoveFilter();
            }

            else
            {
                BsListaUsuarios.Filter = $"Nombre LIKE '%{filtro}%'";
            }
        }

        private DataTable ConvertUserListToDT(List<Usuario> usuarios)
        {
            var dt = new DataTable();

            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("PatentesAsignadas", typeof(string));
            dt.Columns.Add("RolesAsignados", typeof(string));
            dt.Columns.Add("Habilitado", typeof(bool));


            foreach (var u in usuarios)
            {
                dt.Rows.Add(u.Nombre, u.Email, u.PatentesAsignadas, u.RolesAsignados, u.Habilitado);
            }

            return dt;
        }

        private void BtnAsignarFamilia_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnCreateFamilia_Click(object sender, EventArgs e)
        {
            TbconUserList.TabPages.Remove(TabPageList);
            TbconUserList.TabPages.Add(TabPageCrearFamilia);
            TabPageCrearFamilia.Text = "Crear Rol";
        }
    }
}

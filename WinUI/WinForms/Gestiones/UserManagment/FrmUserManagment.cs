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
using Services__ArqBase_.Bll;
using Services__ArqBase_.Bll.Interfaces;
using Services__ArqBase_.Facade;

namespace WinUI.WinForms.Gestiones.UserManagment
{
    public partial class FrmUserManagment : Form
    {
        private BindingSource BsListaUsuarios;
        private List<Usuario> usuarios;
        public FrmUserManagment()
        {
            InitializeComponent();
            
        }

        private void FrmUserManagment_Load(object sender, EventArgs e)
        {
            usuarios = UserManagmentService.TraerUsuarios();

            if (usuarios is not null && usuarios.Count > 0)
            {

                DataTable DtUsuarios = ConvertUserListToDT(usuarios);
                BsListaUsuarios = new BindingSource();

                BsListaUsuarios.DataSource = DtUsuarios;

                DgvListaUsuarios.DataSource = BsListaUsuarios;
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

        private void BtnCreateFamilia_Click(object sender, EventArgs e)
        {
            var frmMain = this.ParentForm as FrmMain;
            if (frmMain != null)
            {

                frmMain.OpenChildForm(new FrmCrearFamilia(), sender);
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el formulario principal.");
            }
        }

        private void BtnUpdatePatente_Click(object sender, EventArgs e)
        {
            
            if (DgvListaUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.");
                return; 
            }
            var selectedRow = DgvListaUsuarios.SelectedRows[0];
            var dataRowView = selectedRow.DataBoundItem as DataRowView;

            if (dataRowView == null)
            {
                MessageBox.Show("No se pudieron cargar los datos del usuario seleccionado.");
                return;
            }

            string userEmail = dataRowView["Email"].ToString();
            Usuario usuarioSeleccionado = usuarios.FirstOrDefault(u => u.Email == userEmail);

            if (usuarioSeleccionado is null)
            {
                MessageBox.Show("Error seleccionando al usuario");
                return;
            }
            var frmMain = this.ParentForm as FrmMain;
            if (frmMain != null)
            {
                frmMain.OpenChildForm(new FrmModificarPermisos(usuarioSeleccionado), sender);
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el formulario principal.");
            }
        }

        private void BtnCrearUsuario_Click(object sender, EventArgs e)
        {
            var frmMain = this.ParentForm as FrmMain;
            if (frmMain != null)
            {

                frmMain.OpenChildForm(new FrmRegistrar(), sender);
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el formulario principal.");
            }
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
    }
}

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
using WinUI.WinForms.Gestiones.Settings;
using Component = Services.DomainModel.Component;

namespace WinUI.WinForms.Gestiones
{
    public partial class FrmSettings : Form
    {
        private BindingSource BsListaUsuarios;
        private List<Usuario> usuarios;
        private IPermisosService permisosService;
        private Usuario usuarioSeleccionado;
        private List<Familia> AllFamilias;
        private List<Patente> AllPatentes;

        public FrmSettings()
        {
            InitializeComponent();
            permisosService = new PermisosService();

        }
        private void FrmSettings_Load(object sender, EventArgs e)
        {
            usuarios = UsuarioBll.TraerUsuarios();
            AllFamilias = permisosService.GetFamilias();
            AllPatentes = permisosService.GetPatentes();

            if (usuarios is not null && usuarios.Count > 0)
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

        

        private void BtnCreateFamilia_Click(object sender, EventArgs e)
        {
            TbconUserList.SelectedTab = TabPageCrearFamilia;
            TabPageCrearFamilia.Text = "Crear Rol";
        }

        private void BtnUpdatePatente_Click(object sender, EventArgs e)
        {

            if (DgvListaUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.");
                return; // Salir del método.
            }
            var selectedRow = DgvListaUsuarios.SelectedRows[0];
            var dataRowView = selectedRow.DataBoundItem as DataRowView;

            if (dataRowView == null)
            {
              MessageBox.Show("Por favor, seleccione un usuario de la lista.");  
                
            }
            else
            {
                string userEmail = dataRowView["Email"].ToString();
                usuarioSeleccionado = usuarios.FirstOrDefault(u => u.Email == userEmail);

                if (usuarioSeleccionado == null)
                {
                   MessageBox.Show("Problema identificando al usuario");
                }
                else
                {

                   TbconUserList.SelectedTab = TabPageModificarPermisos;
                   CargarPatentes(usuarioSeleccionado);
                   CargarFamilias(usuarioSeleccionado);
                   CargarPatentesDeFamilias(usuarioSeleccionado);
                }
            }
            


        }

        private void CargarPatentes(Usuario usuario)
        {
            CheckListPatentes.Items.Clear();
            
            var PatenteDirectasUsuario = usuario.Privilegios.OfType<Patente>().ToList();

            foreach (var item in AllPatentes)
            {
                Patente patente = PatenteDirectasUsuario.FirstOrDefault(p => p.Id == item.Id);
                string Nombre = item.DataKey;
                bool habilitado = false;

                if (patente != null)
                {
                    habilitado = patente.Habilitado;
                    if(!patente.Habilitado)
                    {
                        Nombre = $"{item.DataKey} (Deshabilitado)";
                    }
                }
                CheckListPatentes.Items.Add(Nombre, habilitado);
            }
        }
        //58A78003-3700-4388-BE91-FC8E6FED3E35
        //0a7c5d0d-7d9d-4fcd-b8b3-7b80f8306181
        //F8DCEE4C-DF4D-4630-90E0-72B7EBEC30D1

        private void CargarFamilias(Usuario usuario)
        {
            CheckListFamilias.Items.Clear();
            


            foreach (var item in AllFamilias)
            {

                Familia familia = usuario.Privilegios.FirstOrDefault(p => p is Familia familia1 && p.Id == item.Id) as Familia;

                string Nombre = item.Nombre;
                bool habilitado = false;
                if (familia != null)
                {
                    habilitado = familia.Habilitado;
                    if (!familia.Habilitado)
                    {
                        Nombre = $"{item.Nombre} (Deshabilitado)";
                    }
                }
                CheckListFamilias.Items.Add(Nombre, habilitado);
                

            }
        }
        

        private void CargarPatentesDeFamilias(Usuario usuario)
        {
            LstPatenteDeFamilia.Items.Clear();

            var items = usuario.GetPatentesAgrupadasPorRol();

            foreach (var linea in items)
            {
                LstPatenteDeFamilia.Items.Add(linea);
            }
        }

        private void BtnSaveModificarPermiso_Click(object sender, EventArgs e)
        {
            string itemText;
            List<Component> permisos = new List<Component>();
            for (int i = 0; i < CheckListFamilias.Items.Count; i++)
            {
                itemText = CheckListFamilias.Items[i].ToString().Replace(" (Deshabilitado)", "");
                Component permiso = AllFamilias.FirstOrDefault(p => p.Nombre == itemText);
                permiso.Habilitado = CheckListFamilias.GetItemChecked(i);
                permisos.Add(permiso);
            }
            
            for (int i = 0; i < CheckListPatentes.Items.Count; i++)
            {
                itemText = CheckListPatentes.Items[i].ToString().Replace(" (Deshabilitado)", "");
                Component permiso = AllPatentes.FirstOrDefault(p => p.DataKey == itemText);
                permiso.Habilitado = CheckListPatentes.GetItemChecked(i);
                permisos.Add(permiso);
            }
            

            permisosService.cambiarPermisosAUsuario(usuarioSeleccionado, permisos);
            usuarioSeleccionado = UsuarioBll.GetById(usuarioSeleccionado.IdUsuario);


            usuarios = UsuarioBll.TraerUsuarios();

            CargarPatentes(usuarioSeleccionado);
            CargarFamilias(usuarioSeleccionado);
            CargarPatentesDeFamilias(usuarioSeleccionado);

            ActualizarDataTableUsuario(usuarioSeleccionado);

            DgvListaUsuarios.ClearSelection();
            usuarioSeleccionado = null;

            TbconUserList.SelectedTab = TabPageList;
        }

        private void ActualizarDataTableUsuario(Usuario usuario)
        {
            if (BsListaUsuarios.DataSource is DataTable dt)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Email"].ToString() == usuario.Email)
                    {
                        row["PatentesAsignadas"] = usuario.PatentesAsignadas;
                        row["RolesAsignados"] = usuario.RolesAsignados;
                        row["Habilitado"] = usuario.Habilitado;
                        break;
                    }
                }
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Services.Bll;
using Services.DomainModel;
using Services__ArqBase_.Bll;
using Services__ArqBase_.Bll.Interfaces;
using Services__ArqBase_.Facade;
using WinUI.WinForms.Gestiones.Settings;
using Component = Services.DomainModel.Component;

namespace WinUI.WinForms.Gestiones.UserManagment
{
    public partial class FrmModificarPermisos : Form
    {
        private Usuario Usuario;
        private List<Familia> AllFamilias;
        private List<Patente> AllPatentes;
        public FrmModificarPermisos(Usuario user)
        {
            InitializeComponent();
            Usuario = user;

        }

        private void FrmModificarPermisos_Load(object sender, EventArgs e)
        {
            AllFamilias = UserManagmentService.TraerFamilias();
            AllPatentes = UserManagmentService.traerPatentes();
            LblUsuario.Text = Usuario.Nombre;
            CargarPatentes(Usuario);
            CargarFamilias(Usuario);
            CargarPatentesDeFamilias(Usuario);

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
                    if (!patente.Habilitado)
                    {
                        Nombre = $"{item.DataKey} (Deshabilitado)";
                    }
                }
                CheckListPatentes.Items.Add(Nombre, habilitado);
            }
        }

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


            UserManagmentService.CambiarPermisosAUsuario(Usuario, permisos);
            var frmMain = this.ParentForm as FrmMain;
            if (frmMain != null)
            {

                frmMain.OpenChildForm(new FrmUserManagment(), sender);
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el formulario principal.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.DomainModel;
using Services__ArqBase_.Facade;

namespace WinUI.WinForms.Gestiones.UserManagment
{
    public partial class FrmCrearFamilia : Form
    {
        private Familia FamiliaCombo;
        private List<Familia> AllFamilias;
        private List<Patente> AllPatentes;
        public FrmCrearFamilia()
        {
            InitializeComponent();
        }

        private void FrmCrearFamilia_Load(object sender, EventArgs e)
        {
            AllFamilias = UserManagmentService.TraerFamilias();
            AllPatentes = UserManagmentService.traerPatentes();
            CombFamilias.Items.Clear();
            CombFamilias.Items.Add("-- Crear Nuevo Rol --");

            foreach (var familia in AllFamilias)
            {
                CombFamilias.Items.Add(familia);
            }

            CombFamilias.DisplayMember = "Nombre";
            CombFamilias.SelectedIndex = 0;
        }

        private void CargarPatentesParaFamilia(Familia familia)
        {
            CheckListPatentesParaFamilias.Items.Clear();

            List<Patente> patentesAsignadas = new List<Patente>();
            if (familia != null)
            {
                patentesAsignadas = UserManagmentService.TraerPatentesDeFamilia(familia);
            }

            foreach (var patente in AllPatentes)
            {
                var patenteAsignada = patentesAsignadas.FirstOrDefault(p => p.Id == patente.Id);

                string nombre = patente.DataKey;
                bool habilitada = false;

                if (patenteAsignada != null)
                {
                    habilitada = patenteAsignada.Habilitado;
                    if (!patenteAsignada.Habilitado)
                        nombre = $"{patente.DataKey} (Deshabilitado)";
                }

                CheckListPatentesParaFamilias.Items.Add(nombre, habilitada);
            }
        }

        private void BtnSaveCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNombreFamilia.Text))
            {
                MessageBox.Show("Debe ingresar un nombre para el rol");
                return;
            }
            

            //Capturo las patentes seleccionadas
            List<Patente> patentes = new List<Patente>();
            for (int i = 0; i < CheckListPatentesParaFamilias.Items.Count; i++)
            {
                string itemText = CheckListPatentesParaFamilias.Items[i].ToString().Replace(" (Deshabilitado)", "");
                Patente patente = AllPatentes.FirstOrDefault(p => p.DataKey == itemText);
                if (patente != null)
                {
                    patente.Habilitado = CheckListPatentesParaFamilias.GetItemChecked(i);
                    patentes.Add(patente);
                }
            }


            // Crear nuevo
            if (CombFamilias.SelectedIndex == 0) 
            {
                
                Familia nuevaFamilia = new Familia { Nombre = TxtNombreFamilia.Text };
                nuevaFamilia = UserManagmentService.CrearRol(nuevaFamilia);

                UserManagmentService.CambiarPermisosFamilia(nuevaFamilia, patentes); 

                MessageBox.Show("Rol creado exitosamente");
            }
            // Modificar existente
            else
            {
                UserManagmentService.CambiarPermisosFamilia(FamiliaCombo, patentes);
                MessageBox.Show("Rol modificado exitosamente");
            }


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

        private void CombFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CombFamilias.SelectedIndex == 0)
            {
                LimpiarFormularioFamilia();
                CargarPatentesParaFamilia(null);
            }
            else
            {
                FamiliaCombo = CombFamilias.SelectedItem as Familia;
                if (FamiliaCombo != null)
                {
                    TxtNombreFamilia.Text = FamiliaCombo.Nombre;
                    TxtNombreFamilia.Enabled = false;
                    CargarPatentesParaFamilia(FamiliaCombo);
                }
            }
        }

        private void LimpiarFormularioFamilia()
        {
            TxtNombreFamilia.Text = string.Empty;
            TxtNombreFamilia.Enabled = true;
            FamiliaCombo = null;
        }


    }
}

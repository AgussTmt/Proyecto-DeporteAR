using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Facade;
using DomainModel;

namespace WinUI.WinForms.Gestiones.Equipos
{
    public partial class FrmSeleccionarEquipo : Form
    {
        public Equipo EquipoSeleccionado { get; private set; }
        public FrmSeleccionarEquipo()
        {
            InitializeComponent();
            EquipoSeleccionado = null;
        }


        private void FrmSeleccionarEquipo_Load(object sender, EventArgs e)
        {
            try
            {
                dgvEquipos.DataSource = BLLFacade.Current.EquipoService.GetAll().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar equipos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Opcional: Cerrar el formulario si no se pueden cargar equipos
                // this.DialogResult = DialogResult.Cancel;
                // this.Close();
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvEquipos.SelectedRows.Count > 0)
            {
                
                EquipoSeleccionado = (Equipo)dgvEquipos.SelectedRows[0].DataBoundItem;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un equipo de la lista.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvEquipos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvEquipos.Rows[e.RowIndex].Selected = true;
                btnSeleccionar_Click(sender, e);
            }
        }
    }
}

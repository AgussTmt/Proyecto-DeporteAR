using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services__ArqBase_.Facade;
using BLL.Facade;
using DomainModel;

namespace WinUI.WinForms.Gestiones.Canchas
{
    public partial class FrmCanchas : Form
    {
        public FrmCanchas()
        {
            InitializeComponent();
            IdiomaHelper.TraducirControles(this);
            this.Text = "FrmCanchas";
        }

        private void FrmCanchas_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FrmCanchas_Load(object sender, EventArgs e)
        {
            RefrescarGrid();
        }

        private void RefrescarGrid()
        {
            try
            {
                dgvCanchas.DataSource = null;
                dgvCanchas.DataSource = BLLFacade.Current.CanchaService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar canchas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNueva_Click(object sender, EventArgs e)
        {
            using (var formDetalle = new FrmCanchaDetalle())
            {
                
                formDetalle.ShowDialog();
            }

            
            RefrescarGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCanchas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una cancha para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            var canchaSeleccionada = (Cancha)dgvCanchas.SelectedRows[0].DataBoundItem;

            
            using (var formDetalle = new FrmCanchaDetalle(canchaSeleccionada))
            {
                formDetalle.ShowDialog();
            }

           
            RefrescarGrid();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvCanchas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una cancha para borrar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            var canchaSeleccionada = (Cancha)dgvCanchas.SelectedRows[0].DataBoundItem;

            
            var confirmacion = MessageBox.Show($"¿Está seguro de que desea borrar la cancha '{canchaSeleccionada.Nombre}'?", "Confirmar Borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    
                    BLLFacade.Current.CanchaService.Delete(canchaSeleccionada.IdCancha);

                    
                    RefrescarGrid();
                }
                catch (Exception ex)
                {
                   
                    MessageBox.Show($"Error al borrar la cancha: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

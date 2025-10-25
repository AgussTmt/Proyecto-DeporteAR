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
    public partial class FrmEquipos : Form
    {
        public FrmEquipos()
        {
            InitializeComponent();
        }

        private void FrmEquipos_Load(object sender, EventArgs e)
        {
            dgvEquipos.AutoGenerateColumns = false;
            dgvEquipos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEquipos.MultiSelect = false;
            // Define manualmente las columnas en el diseñador:
            // - Nombre (DataPropertyName="Nombre")
            // - Capitan (Name="colCapitan", DataPropertyName vacío, usar CellFormatting)
            // - CantAusencias (DataPropertyName="CantAusencias")
            // - EstadoProxPartido (DataPropertyName="EstadoProxPartido")
            // - IdEquipo (DataPropertyName="IdEquipo", Visible=False)

            RefrescarGrid();
        }

        private void RefrescarGrid()
        {
            try
            {
                dgvEquipos.DataSource = null;
                dgvEquipos.DataSource = BLLFacade.Current.EquipoService.GetAll().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar equipos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            using (var formDetalle = new FrmEquipoDetalle())
            {
                var result = formDetalle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    RefrescarGrid();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEquipos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un equipo para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var equipoSeleccionado = (Equipo)dgvEquipos.SelectedRows[0].DataBoundItem;

            using (var formDetalle = new FrmEquipoDetalle(equipoSeleccionado))
            {
                var result = formDetalle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    RefrescarGrid();
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvEquipos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un equipo para deshabilitar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var equipoSeleccionado = (Equipo)dgvEquipos.SelectedRows[0].DataBoundItem;

            // Cambiamos el mensaje
            var confirmacion = MessageBox.Show($"¿Está seguro de deshabilitar el equipo '{equipoSeleccionado.Nombre}'? No aparecerá en las listas.", "Confirmar Deshabilitación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // ¡Llamamos al nuevo método de la BLL!
                    BLLFacade.Current.EquipoService.CambiarHabilitado(equipoSeleccionado.IdEquipo, false); // false = Deshabilitar
                    RefrescarGrid(); // El equipo desaparecerá de la lista
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al deshabilitar equipo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

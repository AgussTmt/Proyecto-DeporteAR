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
using Services__ArqBase_.Facade;

namespace WinUI.WinForms.Gestiones.Competiciones
{
    public partial class FrmCompeticion : Form
    {
        public FrmCompeticion()
        {
            InitializeComponent();
        }

        private void FrmCompeticion_Load(object sender, EventArgs e)
        {
            IdiomaHelper.TraducirControles(this);
            RefrescarGrid();
        }

        private void RefrescarGrid()
        {
            try
            {
                dgvCompeticiones.DataSource = null;
                // Obtenemos todas las competiciones y las convertimos a lista
                dgvCompeticiones.DataSource = BLLFacade.Current.CompeticionService.GetAll().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar competiciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void FrmCompeticion_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void BtnNueva_Click(object sender, EventArgs e)
        {
            using (var formDetalle = new FrmCompeticionDetalle())
            {
                // ShowDialog() pausa aquí y espera a que el form se cierre
                var result = formDetalle.ShowDialog();
                // Si el usuario guardó (no canceló), refrescamos
                if (result == DialogResult.OK)
                {
                    RefrescarGrid();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCompeticiones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una competición para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var competicionSeleccionada = (Competicion)dgvCompeticiones.SelectedRows[0].DataBoundItem;

            using (var formDetalle = new FrmCompeticionDetalle(competicionSeleccionada))
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
            if (dgvCompeticiones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una competición para borrar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var competicionSeleccionada = (Competicion)dgvCompeticiones.SelectedRows[0].DataBoundItem;

            var confirmacion = MessageBox.Show($"¿Está seguro de borrar la competición '{competicionSeleccionada.Nombre}'? Esto borrará también sus equipos inscriptos, fixture y clasificación.", "Confirmar Borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    BLLFacade.Current.CompeticionService.Delete(competicionSeleccionada.IdCompeticion);
                    RefrescarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al borrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnInscribirEquipo_Click(object sender, EventArgs e)
        {
            if (dgvCompeticiones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una competición para inscribir un equipo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var competicionSeleccionada = (Competicion)dgvCompeticiones.SelectedRows[0].DataBoundItem;

            // Abre el diálogo para seleccionar un equipo
            using (var frmSeleccionar = new FrmSeleccionarEquipo())
            {
                var result = frmSeleccionar.ShowDialog();

                if (result == DialogResult.OK && frmSeleccionar.EquipoSeleccionado != null)
                {
                    var equipoAInscribir = frmSeleccionar.EquipoSeleccionado;
                    try
                    {
                        
                        BLLFacade.Current.CompeticionService.AñadirEquipo(competicionSeleccionada, equipoAInscribir);
                        MessageBox.Show($"Equipo '{equipoAInscribir.Nombre}' inscripto correctamente.", "Inscripción Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        RefrescarGrid();
                    }
                    catch (InvalidOperationException bizEx) 
                    {
                        MessageBox.Show($"No se pudo inscribir: {bizEx.Message}", "Error de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex) 
                    {
                        MessageBox.Show($"Error al inscribir equipo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnGenerarFixture_Click(object sender, EventArgs e)
        {
            if (dgvCompeticiones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una competición para generar su fixture.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var competicionSeleccionada = (Competicion)dgvCompeticiones.SelectedRows[0].DataBoundItem;

            var confirmacion = MessageBox.Show($"¿Está seguro de generar el fixture para '{competicionSeleccionada.Nombre}'? Esta acción cerrará las inscripciones y no se puede deshacer.", "Confirmar Generación de Fixture", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    
                    BLLFacade.Current.CompeticionService.CrearFixture(competicionSeleccionada);
                    MessageBox.Show("¡Fixture generado con éxito!", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    RefrescarGrid();
                }
                catch (InvalidOperationException bizEx) 
                {
                    MessageBox.Show($"No se pudo generar el fixture: {bizEx.Message}", "Error de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex) 
                {
                    MessageBox.Show($"Error al generar el fixture: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    }
}

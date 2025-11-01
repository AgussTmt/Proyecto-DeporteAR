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
using WinUI.WinForms.Gestiones.Jugadores;

namespace WinUI.WinForms.Gestiones.Equipos
{
    public partial class FrmEquipos : Form
    {
        private List<Equipo> _listaCompletaEquipos;
        
        public FrmEquipos()
        {
            InitializeComponent();
            _listaCompletaEquipos = new List<Equipo>();
        }

        private void FrmEquipos_Load(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void CargarGrid()
        {
            try
            {
                _listaCompletaEquipos = BLLFacade.Current.EquipoService.GetAllIncludingDisabled().ToList();
                RefrescarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar equipos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _listaCompletaEquipos = new List<Equipo>();
                RefrescarGrid();
            }
        }

        private void RefrescarGrid()
        {
            string filtro = txtBuscar.Text.ToLower().Trim();


            List<Equipo> listaFiltrada;

            if (string.IsNullOrEmpty(filtro))
            {
                listaFiltrada = _listaCompletaEquipos;
            }
            else
            {
                listaFiltrada = _listaCompletaEquipos
                    .Where(e => e.Nombre.ToLower().Contains(filtro))
                    .ToList();
            }


            dgvEquipos.DataSource = null;
            dgvEquipos.DataSource = listaFiltrada;

            if (dgvEquipos.Columns.Contains("IdEquipo"))
            {
                dgvEquipos.Columns["IdEquipo"].Visible = false;
            }
            if (dgvEquipos.Columns.Contains("Jugadores"))
            {
                dgvEquipos.Columns["Jugadores"].Visible = false;
            }
            if (dgvEquipos.Columns.Contains("EstadoProxPartido"))
            {
                dgvEquipos.Columns["EstadoProxPartido"].HeaderText = "Estado";
            }

            if (dgvEquipos.Columns.Contains("CantidadJugadores"))
            {
                dgvEquipos.Columns["CantidadJugadores"].HeaderText = "N° Jugadores";
            }


            PintarFilasDeshabilitadas();
        }

        private void PintarFilasDeshabilitadas()
        {
            foreach (DataGridViewRow row in dgvEquipos.Rows)
            {
                var equipo = (Equipo)row.DataBoundItem;
                if (equipo != null && !equipo.Habilitado) 
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.ForeColor = Color.DarkGray;
                    row.DefaultCellStyle.SelectionBackColor = Color.Gray;
                    string toolTip = "Este equipo está deshabilitado.";
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ToolTipText = toolTip;
                    }
                }
                else if (equipo != null) 
                    row.DefaultCellStyle.BackColor = SystemColors.Window;
                    row.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            using (var formDetalle = new FrmEquipoDetalle())
            {
                var result = formDetalle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CargarGrid();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEquipos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un equipo para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Obtener el item desde CurrentRow
            var equipoSeleccionado = (Equipo)dgvEquipos.CurrentRow.DataBoundItem;

            using (var formDetalle = new FrmEquipoDetalle(equipoSeleccionado))
            {
                var result = formDetalle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CargarGrid();
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvEquipos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un equipo para deshabilitar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var equipoSeleccionado = (Equipo)dgvEquipos.CurrentRow.DataBoundItem;

            if (!equipoSeleccionado.Habilitado)
            {
                MessageBox.Show($"El equipo '{equipoSeleccionado.Nombre}' ya está deshabilitado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Está seguro de deshabilitar el equipo '{equipoSeleccionado.Nombre}'? No aparecerá en las listas.", "Confirmar Deshabilitación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    BLLFacade.Current.EquipoService.CambiarHabilitado(equipoSeleccionado.IdEquipo, false);
                    CargarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al deshabilitar equipo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            RefrescarGrid();
        }

        private void dgvEquipos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvEquipos.Columns[e.ColumnIndex].Name == "Capitan")
            {
                if (e.Value != null)
                {
                    Cliente stubCliente = (Cliente)e.Value;

                    try
                    {
                        var capitanCompleto = BLLFacade.Current.ClienteService.GetById(stubCliente.IdCliente);
                        if (capitanCompleto != null)
                        {
                            e.Value = $"{capitanCompleto.Nombre}";
                        }
                        else
                        {
                            e.Value = "(Capitán no encontrado)";
                        }
                    }
                    catch (Exception)
                    {
                        e.Value = "(Error al cargar)";
                    }
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = "(Sin Capitán)";
                    e.FormattingApplied = true;
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnVerJugadores_Click(object sender, EventArgs e)
        {
            var frmMain = this.ParentForm as FrmMain;
            if (frmMain != null)
            {

                frmMain.OpenChildForm(new FrmJugadores(), sender);
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el formulario principal.");
            }
        }
    }
}

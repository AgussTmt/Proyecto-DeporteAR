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
        public List<Equipo> EquiposSeleccionados { get; private set; }
        private readonly List<Guid> _idsEquiposYaInscriptos;
        private List<Equipo> _listaCompletaEquipos;
        public FrmSeleccionarEquipo(List<Guid> idsEquiposYaInscriptos)
        {
            InitializeComponent();
            _idsEquiposYaInscriptos = idsEquiposYaInscriptos ?? new List<Guid>();
            EquiposSeleccionados = new List<Equipo>();
            _listaCompletaEquipos = new List<Equipo>();
        }


        private void FrmSeleccionarEquipo_Load(object sender, EventArgs e)
        {
            try
            {
                _listaCompletaEquipos = BLLFacade.Current.EquipoService.GetAll().ToList();                
                RefrescarGrid();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar equipos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvEquipos.SelectedRows.Count > 0)
            {
                EquiposSeleccionados.Clear();
                foreach (DataGridViewRow row in dgvEquipos.SelectedRows)
                {
                    if (row.ReadOnly) continue;
                    EquiposSeleccionados.Add((Equipo)row.DataBoundItem);
                }
                if (EquiposSeleccionados.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione uno o más equipos que no estén ya inscriptos.", "Selección Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione al menos un equipo de la lista.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                
                if (dgvEquipos.Rows[e.RowIndex].ReadOnly) return;
                dgvEquipos.Rows[e.RowIndex].Selected = true;
                btnSeleccionar_Click(sender, e);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            RefrescarGrid();
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


            foreach (DataGridViewRow row in dgvEquipos.Rows)
            {
                var equipo = (Equipo)row.DataBoundItem;
                if (equipo != null && _idsEquiposYaInscriptos.Contains(equipo.IdEquipo))
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                    row.ReadOnly = true;

                    string toolTip = "Este equipo ya está inscripto en la competición.";
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ToolTipText = toolTip;
                    }
                }
            }
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
    }
}

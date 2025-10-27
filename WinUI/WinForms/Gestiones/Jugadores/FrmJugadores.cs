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

namespace WinUI.WinForms.Gestiones.Jugadores
{
    public partial class FrmJugadores : Form
    {
        private List<Jugador> _listaCompletaJugadores;

        public FrmJugadores()
        {
            InitializeComponent();
            _listaCompletaJugadores = new List<Jugador>();
        }

        private void FrmJugadores_Load(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void CargarGrid()
        {
            try
            {
                _listaCompletaJugadores = BLLFacade.Current.JugadorService.GetAllIncludingDisabled();
                RefrescarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar jugadores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _listaCompletaJugadores = new List<Jugador>();
                RefrescarGrid();
            }
        }

        private void RefrescarGrid()
        {
            string filtro = txtBuscar.Text.ToLower().Trim();

            List<Jugador> listaFiltrada;

            if (string.IsNullOrEmpty(filtro))
            {
                listaFiltrada = _listaCompletaJugadores;
            }
            else
            {
                listaFiltrada = _listaCompletaJugadores
                    .Where(j => j.NombreCompleto.ToLower().Contains(filtro))
                    .ToList();
            }

            dgvJugadores.DataSource = null;
            dgvJugadores.DataSource = listaFiltrada;
            if (dgvJugadores.Columns.Contains("IdJugador"))
            {
                dgvJugadores.Columns["IdJugador"].Visible = false;
            }
            if (dgvJugadores.Columns.Contains("IdEquipo"))
            {
                dgvJugadores.Columns["IdEquipo"].Visible = false;
            }
            if (dgvJugadores.Columns.Contains("NombreCompleto"))
            {
                dgvJugadores.Columns["NombreCompleto"].Visible = false;
            }
            
            // Cambiar nombre de columna si es necesario
            if (dgvJugadores.Columns.Contains("NombreEquipo"))
            {
                dgvJugadores.Columns["NombreEquipo"].HeaderText = "Equipo";
            }

            PintarFilasDeshabilitadas();
        }

        private void PintarFilasDeshabilitadas()
        {
            foreach (DataGridViewRow row in dgvJugadores.Rows)
            {
                var jugador = (Jugador)row.DataBoundItem;
                if (jugador != null && !jugador.Habilitado)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.ForeColor = Color.DarkGray;
                    row.DefaultCellStyle.SelectionBackColor = Color.Gray;
                    string toolTip = "Este jugador está deshabilitado.";
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ToolTipText = toolTip;
                    }
                }
                else if (jugador != null)
                {
                    row.DefaultCellStyle.BackColor = SystemColors.Window;
                    row.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                    row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            RefrescarGrid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Llamamos al FrmJugadorDetalle en modo 'Crear'
            using (var formDetalle = new FrmJugadorDetalle())
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
            if (dgvJugadores.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un jugador para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           var jugadorSeleccionado = (Jugador)dgvJugadores.CurrentRow.DataBoundItem;
            using (var formDetalle = new FrmJugadorDetalle(jugadorSeleccionado))
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
            if (dgvJugadores.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un jugador para deshabilitar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var jugadorSeleccionado = (Jugador)dgvJugadores.CurrentRow.DataBoundItem;

            if (!jugadorSeleccionado.Habilitado)
            {
                MessageBox.Show($"El jugador '{jugadorSeleccionado.NombreCompleto}' ya está deshabilitado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Está seguro de deshabilitar al jugador '{jugadorSeleccionado.NombreCompleto}'?", "Confirmar Deshabilitación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // Asumiendo que existe un método 'CambiarHabilitado' en JugadorService
                    // (Si no existe, tendríamos que crearlo en BLL y DAL)
                    BLLFacade.Current.JugadorService.CambiarHabilitado(jugadorSeleccionado.Idjugador);
                    CargarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al deshabilitar jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvJugadores_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvJugadores.Columns[e.ColumnIndex].Name == "Puntuacion")
            {
                
                if (e.Value is Dictionary<string, int> puntuacionDict)
                {
                    if (puntuacionDict.Count > 0)
                    {
                        string PuntuacionStr = string.Join(", ", puntuacionDict.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
                        e.Value = PuntuacionStr;
                    }
                    else
                    {
                        e.Value = "(Vacío)";
                    }
                    e.FormattingApplied = true;
                }
                else if (e.Value != null)
                {
                    e.Value = "(Error de tipo)";
                    e.FormattingApplied = true;
                }
            }
            if (this.dgvJugadores.Columns[e.ColumnIndex].Name == "Sanciones")
            {
                if (e.Value is Dictionary<string, int> sancionesDict)
                {
                    if (sancionesDict.Count > 0)
                    {
                        string SancionesStr = string.Join(", ", sancionesDict.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
                        e.Value = SancionesStr;
                    }
                    else
                    {
                        e.Value = "(Sin sanciones)";
                    }
                    e.FormattingApplied = true;
                }
                else if (e.Value != null)
                {
                    e.Value = "(Error de tipo)";
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
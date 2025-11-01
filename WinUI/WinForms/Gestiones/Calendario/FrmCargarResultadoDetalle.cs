using DomainModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BLL.Facade;
using WinUI.WinForms.Gestiones.Equipos;

namespace WinUI.WinForms.Gestiones
{
    public partial class FrmCargarResultadoDetalle : Form
    {
        private Fixture _partido;
        private Equipo _equipoLocal;
        private Equipo _equipoVisitante;

        public FrmCargarResultadoDetalle(Fixture partido)
        {
            InitializeComponent();
            _partido = partido;
            // Guardamos los stubs, se cargarán completos en el Load
            _equipoLocal = partido.Equipos[0];
            _equipoVisitante = partido.Equipos[1];
        }

        private void FrmCargarResultadoDetalle_Load(object sender, EventArgs e)
        {
            lblLocal.Text = _equipoLocal.Nombre;
            lblVisitante.Text = _equipoVisitante.Nombre;

            if (!string.IsNullOrEmpty(_partido.Resultado))
            {
                txtResLocal.Text = _partido.Resultado.Split('-')[0];
                txtResVisitante.Text = _partido.Resultado.Split('-')[1];
            }

            // Cargar las grillas
            CargarYRefrescarGrilla(ref _equipoLocal, dgvLocal, "colLocal");
            CargarYRefrescarGrilla(ref _equipoVisitante, dgvVisitante, "colVisitante");
        }

        /// <summary>
        /// Trae el equipo de la BLL, lo bindea a la grilla y rellena las estadísticas.
        /// </summary>
        private void CargarYRefrescarGrilla(ref Equipo equipo, DataGridView dgv, string colPrefix)
        {
            try
            {
                equipo = BLLFacade.Current.EquipoService.TraerPorId(equipo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fatal al cargar los jugadores de {equipo.Nombre}: {ex.Message}");
                return;
            }

            dgv.DataSource = null;
            dgv.DataSource = equipo.Jugadores;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.DataBoundItem is Jugador j)
                {
                    row.Cells[$"{colPrefix}Goles"].Value = j.Puntuacion.ContainsKey("Goles") ? j.Puntuacion["Goles"] : 0;
                    row.Cells[$"{colPrefix}Asistencias"].Value = j.Puntuacion.ContainsKey("Asistencias") ? j.Puntuacion["Asistencias"] : 0;
                    row.Cells[$"{colPrefix}Amarillas"].Value = j.Sanciones.ContainsKey("Amarillas") ? j.Sanciones["Amarillas"] : 0;
                    row.Cells[$"{colPrefix}Rojas"].Value = j.Sanciones.ContainsKey("Rojas") ? j.Sanciones["Rojas"] : 0;
                }
            }
        }

        /// <summary>
        /// Se dispara al hacer clic en "(+ Editar)" para Local o Visitante
        /// </summary>
        private void linkLblEditar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bool esLocal = (sender == linkLblEditarLocal);
            Equipo equipoParaEditar = esLocal ? _equipoLocal : _equipoVisitante;
            DataGridView dgvParaRefrescar = esLocal ? dgvLocal : dgvVisitante;
            string colPrefix = esLocal ? "colLocal" : "colVisitante";

            using (var frmEquipo = new FrmEquipoDetalle(equipoParaEditar))
            {
                if (frmEquipo.ShowDialog() == DialogResult.OK)
                {
                    if (esLocal)
                    {
                        CargarYRefrescarGrilla(ref _equipoLocal, dgvParaRefrescar, colPrefix);
                    }
                    else
                    {
                        CargarYRefrescarGrilla(ref _equipoVisitante, dgvParaRefrescar, colPrefix);
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtResLocal.Text, out int resLocal) || !int.TryParse(txtResVisitante.Text, out int resVisitante))
            {
                MessageBox.Show("El resultado final debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string resultadoString = $"{resLocal}-{resVisitante}";

            try
            {
                var jugadoresActualizados = new List<Jugador>();

                // Lee la grilla local
                foreach (DataGridViewRow row in dgvLocal.Rows)
                {
                    if (row.DataBoundItem is Jugador j)
                    {
                        j.Puntuacion["Goles"] = Convert.ToInt32(row.Cells["colLocalGoles"].Value ?? 0);
                        j.Puntuacion["Asistencias"] = Convert.ToInt32(row.Cells["colLocalAsistencias"].Value ?? 0);
                        j.Sanciones["Amarillas"] = Convert.ToInt32(row.Cells["colLocalAmarillas"].Value ?? 0);
                        j.Sanciones["Rojas"] = Convert.ToInt32(row.Cells["colLocalRojas"].Value ?? 0);
                        j.PartidosJugados += 1;
                        jugadoresActualizados.Add(j);
                    }
                }

                // Lee la grilla visitante
                foreach (DataGridViewRow row in dgvVisitante.Rows)
                {
                    if (row.DataBoundItem is Jugador j)
                    {
                        j.Puntuacion["Goles"] = Convert.ToInt32(row.Cells["colVisitanteGoles"].Value ?? 0);
                        j.Puntuacion["Asistencias"] = Convert.ToInt32(row.Cells["colVisitanteAsistencias"].Value ?? 0);
                        j.Sanciones["Amarillas"] = Convert.ToInt32(row.Cells["colVisitanteAmarillas"].Value ?? 0);
                        j.Sanciones["Rojas"] = Convert.ToInt32(row.Cells["colVisitanteRojas"].Value ?? 0);
                        j.PartidosJugados += 1;
                        jugadoresActualizados.Add(j);
                    }
                }

                Fixture fixtureStub = new Fixture
                {
                    IdFixture = _partido.IdFixture,
                    Resultado = resultadoString
                };

                // Leemos el estado de los CheckBox en el momento de guardar
                bool localAusente = chkLocalAusente.Checked;
                bool visitanteAusente = chkVisitanteAusente.Checked;

                // Llamamos a la BLL con los 4 parámetros
                BLLFacade.Current.FixtureService.CargarResul(
                    fixtureStub,
                    jugadoresActualizados,
                    localAusente,
                    visitanteAusente
                );

                MessageBox.Show("Resultado detallado guardado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Esto le avisa al PartidoControl
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
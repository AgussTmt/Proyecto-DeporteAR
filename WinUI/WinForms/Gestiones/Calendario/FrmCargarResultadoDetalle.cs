using DomainModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BLL.Facade;
using WinUI.WinForms.Gestiones.Equipos; // <-- Este using es nuevo

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
            // Mostramos nombres de equipos y resultado (si ya existe)
            lblLocal.Text = _equipoLocal.Nombre;
            lblVisitante.Text = _equipoVisitante.Nombre;

            if (!string.IsNullOrEmpty(_partido.Resultado))
            {
                txtResLocal.Text = _partido.Resultado.Split('-')[0];
                txtResVisitante.Text = _partido.Resultado.Split('-')[1];
            }

            // Llamamos al nuevo método de carga/refresco
            CargarYRefrescarGrilla(ref _equipoLocal, dgvLocal, "colLocal");
            CargarYRefrescarGrilla(ref _equipoVisitante, dgvVisitante, "colVisitante");
        }

        /// <summary>
        /// (NUEVO MÉTODO REUTILIZABLE)
        /// Trae el equipo de la BLL, lo bindea a la grilla y rellena las estadísticas.
        /// </summary>
        private void CargarYRefrescarGrilla(ref Equipo equipo, DataGridView dgv, string colPrefix)
        {
            try
            {
                // 1. Traer el equipo FRESCO de la BLL (con su lista de jugadores)
                //
                equipo = BLLFacade.Current.EquipoService.TraerPorId(equipo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fatal al cargar los jugadores de {equipo.Nombre}: {ex.Message}");
                return;
            }

            // 2. Re-bindear el DataSource
            dgv.DataSource = null; // Limpiar bindeo anterior
            dgv.DataSource = equipo.Jugadores;

            // 3. Rellenar celdas de estadísticas
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
        /// (NUEVO EVENTO)
        /// Se dispara al hacer clic en "(+ Editar)" para Local o Visitante
        /// </summary>
        private void linkLblEditar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 1. Determinar qué equipo editar
            bool esLocal = (sender == linkLblEditarLocal);
            Equipo equipoParaEditar = esLocal ? _equipoLocal : _equipoVisitante;
            DataGridView dgvParaRefrescar = esLocal ? dgvLocal : dgvVisitante;
            string colPrefix = esLocal ? "colLocal" : "colVisitante";

            // 2. Abrir el FrmEquipoDetalle que ya tenías
            //
            using (var frmEquipo = new FrmEquipoDetalle(equipoParaEditar))
            {
                // 3. Si el usuario guardó (ej. añadió un jugador)...
                if (frmEquipo.ShowDialog() == DialogResult.OK)
                {
                    // 4. ...recargamos ESA grilla.
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
            // 1. Validar y obtener el resultado final (ej: "2-1")
            if (!int.TryParse(txtResLocal.Text, out int resLocal) || !int.TryParse(txtResVisitante.Text, out int resVisitante))
            {
                MessageBox.Show("El resultado final debe ser numérico.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string resultadoString = $"{resLocal}-{resVisitante}";

            try
            {
                // 2. Recolectar todas las estadísticas de los jugadores
                var jugadoresActualizados = new List<Jugador>();

                // --- Lectura Grilla LOCAL ---
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

                // --- Lectura Grilla VISITANTE ---
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

                // 3. Crear el stub del Fixture
                Fixture fixtureStub = new Fixture
                {
                    IdFixture = _partido.IdFixture,
                    Resultado = resultadoString
                };

                // 4. Llamar al nuevo método de BLL
                BLLFacade.Current.FixtureService.CargarResul(fixtureStub, jugadoresActualizados);

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
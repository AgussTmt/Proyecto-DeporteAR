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
using WinUI.WinForms.Gestiones.Calendario;
using WinUI.WinForms.Gestiones.Competiciones;
using WinUI.WinForms.Gestiones.Reservas;

namespace WinUI.WinForms.Gestiones
{
    public partial class FrmCalendario : Form
    {

        private List<Cancha> _listaCanchas;
        private List<Competicion> _listaCompeticiones;
        private DateTime _fechaReferenciaSemana;

        private List<Fixture> _partidosCompeticionActual;
        private List<IGrouping<DateTime, Fixture>> _jornadasCompeticion;
        private int _jornadaActualIndex;
        public FrmCalendario()
        {
            InitializeComponent();
            _listaCanchas = new List<Cancha>();
            _listaCompeticiones = new List<Competicion>();
            _fechaReferenciaSemana = DateTime.Today;

            _partidosCompeticionActual = new List<Fixture>();
            _jornadasCompeticion = new List<IGrouping<DateTime, Fixture>>();
            _jornadaActualIndex = 0;
        }

        private void FrmCalendario_Load(object sender, EventArgs e)
        {
            CargarComboBoxes();

            this.btnSemanaAnterior.Click += new System.EventHandler(this.btnSemanaAnterior_Click);
            this.btnSemanaSiguiente.Click += new System.EventHandler(this.btnSemanaSiguiente_Click);
            this.cmbCancha.SelectedIndexChanged += new System.EventHandler(this.cmbCancha_SelectedIndexChanged);

            this.cmbCompeticion.SelectedIndexChanged += new System.EventHandler(this.cmbCompeticion_SelectedIndexChanged);

            RefrescarVistaSemanal();
            CargarPartidosDeCompeticion();

        }

        private void CargarComboBoxes()
        {
            try
            {
                _listaCanchas = BLLFacade.Current.CanchaService.GetAll().ToList();
                cmbCancha.DataSource = _listaCanchas;
                cmbCancha.DisplayMember = "Nombre";
                cmbCancha.ValueMember = "IdCancha";


                _listaCompeticiones = BLLFacade.Current.CompeticionService.GetAll().ToList();
                cmbCompeticion.DataSource = _listaCompeticiones;
                cmbCompeticion.DisplayMember = "Nombre";
                cmbCompeticion.ValueMember = "IdCompeticion";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar filtros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Panel superior reservas

        private void btnSemanaSiguiente_Click(object sender, EventArgs e)
        {
            _fechaReferenciaSemana = _fechaReferenciaSemana.AddDays(7);
            RefrescarVistaSemanal();
        }

        private void btnSemanaAnterior_Click(object sender, EventArgs e)
        {
            _fechaReferenciaSemana = _fechaReferenciaSemana.AddDays(-7);
            RefrescarVistaSemanal();
        }

        private void cmbCancha_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefrescarVistaSemanal();
        }

        private void RefrescarVistaSemanal()
        {
            if (cmbCancha.SelectedItem == null || cmbCancha.SelectedValue == null)
            {
                lblRangoSemana.Text = "Seleccione una cancha";
                tlpSemanaReservas.Controls.Clear();
                return;
            }

            try
            {
                Cancha canchaSeleccionada = (Cancha)cmbCancha.SelectedItem;
                Guid idCancha = canchaSeleccionada.IdCancha;

                DateTime inicioSemana = GetInicioSemana(_fechaReferenciaSemana);
                DateTime finSemana = inicioSemana.AddDays(7);

                lblRangoSemana.Text = $"{inicioSemana:dd/MM} al {finSemana.AddDays(-1):dd/MM/yyyy}";
                tlpSemanaReservas.Controls.Clear();

                var listaCompletaSemana = BLLFacade.Current.CanchaHorarioService.GetHorariosRango(idCancha, inicioSemana, finSemana);

                var slotsAgrupadosPorDia = listaCompletaSemana
                    .GroupBy(h => h.FechaHorario.Date)
                    .ToDictionary(g => g.Key, g => g.ToList());

                for (int i = 0; i < 7; i++)
                {
                    DateTime fechaDia = inicioSemana.AddDays(i);

                    //columna
                    var panelDia = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    var lblTitulo = new Label
                    {
                        Text = $"{fechaDia.ToString("ddd").ToUpper()} {fechaDia:dd/MM}",
                        Dock = DockStyle.Top,
                        BackColor = Color.LightGray,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font(this.Font, FontStyle.Bold),
                        Height = 30
                    };
                    panelDia.Controls.Add(lblTitulo);

                    var flowDia = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        FlowDirection = FlowDirection.TopDown,
                        AutoScroll = true,
                        BackColor = Color.White,
                        WrapContents = false
                    };
                    panelDia.Controls.Add(flowDia);
                    flowDia.BringToFront();

                    if (slotsAgrupadosPorDia.ContainsKey(fechaDia.Date))
                    {
                        var slotsDelDia = slotsAgrupadosPorDia[fechaDia.Date];

                        foreach (var slot in slotsDelDia.OrderBy(s => s.FechaHorario))
                        {
                            // 1. Creamos nuestro nuevo UserControl
                            SlotReservaControl panelSlot = new SlotReservaControl();

                            // 2. Nos suscribimos a SU evento
                            panelSlot.EditarReservaClick += PanelSlot_EditarReservaClick;

                            // 3. Lo añadimos al panel
                            flowDia.Controls.Add(panelSlot); // ¡¡Importante añadirlo ANTES de CargarSlot!!

                            // 4. Le pasamos los datos (esto ajustará su tamaño)
                            panelSlot.CargarSlot(slot);
                        }
                    }


                    tlpSemanaReservas.Controls.Add(panelDia, i, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al refrescar el calendario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PanelSlot_EditarReservaClick(object sender, CanchaHorario slotParaEditar)
        {
            // Esta es la lógica que antes estaba en 'BtnEditarReserva_Click'
            try
            {
                using (var frmDetalle = new FrmReservaDetalle(slotParaEditar))
                {
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        RefrescarVistaSemanal(); // Refrescamos todo el calendario
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir detalle de reserva: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DateTime GetInicioSemana(DateTime fecha)
        {
            int diff = (7 + (fecha.DayOfWeek - DayOfWeek.Monday)) % 7;
            return fecha.AddDays(-1 * diff).Date;
        }
        #endregion 


        #region === PANEL INFERIOR (COMPETICIONES) ===
        private void cmbCompeticion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPartidosDeCompeticion();
        }

        private void BtnJornadaAnterior_Click(object sender, EventArgs e)
        {
            if (_jornadaActualIndex > 0)
            {
                _jornadaActualIndex--;
                MostrarJornadaActual();
            }
        }

        private void BtnJornadaSiguiente_Click(object sender, EventArgs e)
        {
            if (_jornadaActualIndex < _jornadasCompeticion.Count - 1)
            {
                _jornadaActualIndex++;
                MostrarJornadaActual();
            }
        }

        private void CargarPartidosDeCompeticion()
        {
            flowPartidos.Controls.Clear();
            _partidosCompeticionActual.Clear();
            _jornadasCompeticion.Clear();
            _jornadaActualIndex = 0;

            if (cmbCompeticion.SelectedItem == null || !(cmbCompeticion.SelectedItem is Competicion))
            {
                lblJornadaInfo.Text = "Seleccione competición";
                ActualizarEstadoBotonesJornada();
                return;
            }

            var competicion = (Competicion)cmbCompeticion.SelectedItem;

            try
            {
                _partidosCompeticionActual = BLLFacade.Current.FixtureService.GetByCompeticion(competicion.IdCompeticion).ToList();

                if (!_partidosCompeticionActual.Any())
                {
                    var lblVacio = new Label { Text = "No hay partidos pendientes programados para esta competición.", Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(10) };
                    flowPartidos.Controls.Add(lblVacio);
                    lblJornadaInfo.Text = "Sin partidos";
                    ActualizarEstadoBotonesJornada();
                    return;
                }

                // 2. Agrupamos por día (esto crea las "Jornadas")
                _jornadasCompeticion = _partidosCompeticionActual
                    .OrderBy(p => p.CanchaHorario.FechaHorario)
                    .GroupBy(p => p.CanchaHorario.FechaHorario.Date)
                    .ToList();

                // 3. Mostramos la primera jornada
                MostrarJornadaActual();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar partidos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void MostrarJornadaActual()
        {
            flowPartidos.Controls.Clear();

            if (_jornadaActualIndex < 0 || _jornadaActualIndex >= _jornadasCompeticion.Count)
            {
                ActualizarEstadoBotonesJornada();
                return;
            }

            var jornadaActual = _jornadasCompeticion[_jornadaActualIndex];
            var fechaJornada = jornadaActual.Key;

            // 2. Título del Día (sin cambios)
            var lblTituloDia = new Label
            {
                Text = fechaJornada.ToString("dddd, dd 'de' MMMM").ToUpper(),
                Font = new Font(this.Font, FontStyle.Bold),
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                Width = flowPartidos.Width - 25,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 10, 0, 5)
            };
            flowPartidos.Controls.Add(lblTituloDia);

            // 3. (NUEVA LÓGICA) Dibujar usando el UserControl
            foreach (var partido in jornadaActual)
            {
                // Creamos nuestro nuevo control
                PartidoControl panelPartido = new PartidoControl();

                // Le pasamos los datos
                panelPartido.CargarPartido(partido);

                // Nos suscribimos a su evento para saber cuándo refrescar
                panelPartido.ResultadoCargado += (s, ev) => {
                    // Cuando un partido se cargue, recargamos toda la competición
                    CargarPartidosDeCompeticion();
                };

                // Lo añadimos al panel
                flowPartidos.Controls.Add(panelPartido);
            }

            // 4. Actualizar UI de navegación (sin cambios)
            lblJornadaInfo.Text = $"Jornada {_jornadaActualIndex + 1} de {_jornadasCompeticion.Count} ({fechaJornada:dd/MM})";
            ActualizarEstadoBotonesJornada();
        }


        private void ActualizarEstadoBotonesJornada()
        {
            BtnJornadaAnterior.Enabled = _jornadaActualIndex > 0;
            BtnJornadaSiguiente.Enabled = _jornadaActualIndex < _jornadasCompeticion.Count - 1;
        }

        #endregion

        private void btnGenerarHorarios_Click(object sender, EventArgs e)
        {
            if (cmbCancha.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una cancha primero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            const int DIAS_HORIZONTE = 15;
            var cancha = (Cancha)cmbCancha.SelectedItem;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                int generados = BLLFacade.Current.CanchaHorarioService.GenerarHorariosParaCancha(cancha.IdCancha, DIAS_HORIZONTE);
                if (generados > 0)
                {
                    MessageBox.Show($"¡Se generaron {generados} nuevos horarios para la cancha '{cancha.Nombre}'!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontraron nuevos horarios para generar (probablemente ya existían).", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                RefrescarVistaSemanal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar horarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnVerClasificacion_Click(object sender, EventArgs e)
        {
            if (cmbCompeticion.SelectedItem == null || !(cmbCompeticion.SelectedItem is Competicion))
            {
                MessageBox.Show("Por favor, seleccione una competición del combo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var competicion = (Competicion)cmbCompeticion.SelectedItem;

            try
            {
                // Abrimos el nuevo formulario y le pasamos los datos
                using (var frm = new FrmClasificacion(competicion.IdCompeticion, competicion.Nombre))
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la clasificación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
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
using WinUI.WinForms.Gestiones.Reservas;

namespace WinUI.WinForms.Gestiones
{
    public partial class FrmCalendario : Form
    {

        private List<Cancha> _listaCanchas;
        private List<Competicion> _listaCompeticiones;
        private DateTime _fechaReferenciaSemana;
        public FrmCalendario()
        {
            InitializeComponent();
            _listaCanchas = new List<Cancha>();
            _listaCompeticiones = new List<Competicion>();
            _fechaReferenciaSemana = DateTime.Today;
        }

        private void FrmCalendario_Load(object sender, EventArgs e)
        {
            CargarComboBoxes();

            this.btnSemanaAnterior.Click += new System.EventHandler(this.btnSemanaAnterior_Click);
            this.btnSemanaSiguiente.Click += new System.EventHandler(this.btnSemanaSiguiente_Click);
            this.cmbCancha.SelectedIndexChanged += new System.EventHandler(this.cmbCancha_SelectedIndexChanged);
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
                            Panel panelSlot = CrearPanelSlot(slot);
                            flowDia.Controls.Add(panelSlot);
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

        private DateTime GetInicioSemana(DateTime fecha)
        {
            int diff = (7 + (fecha.DayOfWeek - DayOfWeek.Monday)) % 7;
            return fecha.AddDays(-1 * diff).Date;
        }


        private Panel CrearPanelSlot(CanchaHorario slot)
        {
            // Panel principal del slot
            var panelSlot = new Panel
            {
                // El ancho se ajusta al FlowLayoutPanel
                Width = tlpSemanaReservas.GetColumnWidths()[0] - 25, // Ancho de la columna menos scrollbar
                Height = 110, // Alto fijo para cada slot
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(3)
            };

            // --- Hora (Ej: 09:00 - 10:00) ---
            // ¡Usa la duración hidratada desde la Cancha!
            var lblHora = new Label
            {
                Text = $"{slot.FechaHorario:HH:mm} - {slot.FechaHorario.AddMinutes(slot.Cancha.DuracionXPartidoMin):HH:mm}",
                Dock = DockStyle.Top,
                Font = new Font(this.Font, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelSlot.Controls.Add(lblHora);

            // --- Estado (Ej: LIBRE / RESERVADA) ---
            string estado = slot.Estado.ToString().ToUpper();
            var lblEstado = new Label
            {
                Text = estado,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = (estado == "LIBRE") ? Color.DarkGreen : Color.DarkRed,
                Font = new Font(this.Font, FontStyle.Bold)
            };
            panelSlot.Controls.Add(lblEstado);

            // --- Cliente (si está reservado) ---
            // ¡Usa el Cliente hidratado!
            if (slot.Estado == EstadoReserva.Reservada && slot.ReservadaPor != null)
            {
                var lblCliente = new Label
                {
                    Text = $"Cliente: {slot.ReservadaPor.Nombre ?? "..."}",
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Height = 15
                };
                panelSlot.Controls.Add(lblCliente);

                // --- Estado de Pago ---
                var lblPago = new Label
                {
                    Text = slot.Abonada ? "PAGADO" : "NO PAGO",
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = slot.Abonada ? Color.Blue : Color.OrangeRed,
                    Font = new Font(this.Font, FontStyle.Bold)
                };
                panelSlot.Controls.Add(lblPago);
            }

            // --- Botón Editar ---
            var btnEditar = new Button
            {
                Text = "Editar",
                Dock = DockStyle.Bottom,
                Tag = slot
            };
            btnEditar.Click += BtnEditarReserva_Click; // Conectamos el evento
            panelSlot.Controls.Add(btnEditar);

            // Asignar color de fondo
            if (estado == "LIBRE")
                panelSlot.BackColor = Color.FromArgb(230, 255, 230); // Verde claro
            else if (estado == "RESERVADA")
                panelSlot.BackColor = Color.FromArgb(255, 230, 230); // Rojo claro
            else
                panelSlot.BackColor = Color.LightYellow; // Otro estado (ej: Mantenimiento)


            return panelSlot;
        }

        private void BtnEditarReserva_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            CanchaHorario slotSeleccionado = (CanchaHorario)btn.Tag;

            try
            {
                
                using (var frmDetalle = new FrmReservaDetalle(slotSeleccionado))
                {
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        RefrescarVistaSemanal();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir detalle de reserva: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
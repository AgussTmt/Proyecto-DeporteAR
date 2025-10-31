using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainModel;

namespace WinUI.WinForms.Gestiones.Calendario
{
    public partial class SlotReservaControl : UserControl
    {
        private CanchaHorario _slot;
        public event EventHandler<CanchaHorario> EditarReservaClick;
        public SlotReservaControl()
        {
            InitializeComponent();
        }

        public void CargarSlot(CanchaHorario slot)
        {
            _slot = slot;

            // Ajustar el ancho (esto no cambia)
            if (this.Parent != null && this.Parent is FlowLayoutPanel)
            {
                this.Width = this.Parent.Width - 25;
            }

            // --- Panel "OCUPADO POR TORNEO" ---
            if (slot.Estado == EstadoReserva.OcupadoPorTorneo)
            {
                this.BackColor = Color.LightGray;
                lblHora.Text = $"{slot.FechaHorario:HH:mm} - {slot.FechaHorario.AddMinutes(slot.Cancha.DuracionXPartidoMin):HH:mm}";

                // Ocultamos el panel de detalles y el botón
                tlpDetalles.Visible = false;
                btnEditar.Visible = false;

                // Limpiamos controles viejos (si el slot se recicla)
                var oldLblTorneo = this.Controls.Find("lblTorneo", false).FirstOrDefault();
                if (oldLblTorneo != null) this.Controls.Remove(oldLblTorneo);

                // Creamos un label temporal para el estado
                var lblTorneo = new Label
                {
                    Name = "lblTorneo", // Le damos nombre para encontrarlo después
                    Text = "OCUPADO POR TORNEO",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(this.Font, FontStyle.Bold),
                    ForeColor = Color.DarkSlateGray
                };
                this.Controls.Add(lblTorneo);
                lblTorneo.BringToFront(); // Lo pone encima del tlpDetalles (que está oculto)
            }
            // --- Panel de Reserva Normal (Libre, Reservada, etc.) ---
            else
            {
                // Limpiamos el label de Torneo si existía
                var oldLblTorneo = this.Controls.Find("lblTorneo", false).FirstOrDefault();
                if (oldLblTorneo != null) this.Controls.Remove(oldLblTorneo);

                // Nos aseguramos de que todo sea visible
                tlpDetalles.Visible = true;
                btnEditar.Visible = true;

                // --- Hora ---
                lblHora.Text = $"{slot.FechaHorario:HH:mm} - {slot.FechaHorario.AddMinutes(slot.Cancha.DuracionXPartidoMin):HH:mm}";

                // --- Estado ---
                string estado = slot.Estado.ToString().ToUpper();
                lblEstado.Text = estado;
                lblEstado.ForeColor = (estado == "LIBRE") ? Color.DarkGreen : Color.DarkRed;

                // --- Cliente y Pago (si está reservado) ---
                if (slot.Estado == EstadoReserva.Reservada && slot.ReservadaPor != null)
                {
                    // Mostramos Fila Cliente
                    tlpDetalles.RowStyles[1].Height = 25; // Le damos altura (o usa AutoSize)
                    lblCliente.Text = $"Cliente: {slot.ReservadaPor.Nombre ?? "..."}";
                    lblCliente.Visible = true;

                    // Mostramos Fila Pago
                    tlpDetalles.RowStyles[2].Height = 25; // Le damos altura
                    lblPago.Text = slot.Abonada ? "PAGADO" : "NO PAGO";
                    lblPago.ForeColor = slot.Abonada ? Color.Blue : Color.OrangeRed;
                    lblPago.Visible = true;
                }
                else
                {
                    // Si está Libre, ocultamos las filas de Cliente y Pago
                    // La mejor forma es poner su altura a 0
                    tlpDetalles.RowStyles[1].Height = 0;
                    lblCliente.Text = "";
                    lblCliente.Visible = false;

                    tlpDetalles.RowStyles[2].Height = 0;
                    lblPago.Text = "";
                    lblPago.Visible = false;
                }

                // --- Color de Fondo ---
                if (estado == "LIBRE")
                    this.BackColor = Color.FromArgb(230, 255, 230); // Verde claro
                else if (estado == "RESERVADA")
                    this.BackColor = Color.FromArgb(255, 230, 230); // Rojo claro
                else
                    this.BackColor = Color.LightYellow; // Otro estado
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarReservaClick?.Invoke(this, _slot);
        }
    }
}

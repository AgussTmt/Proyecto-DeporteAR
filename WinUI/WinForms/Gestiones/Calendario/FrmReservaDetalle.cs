using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BLL.Facade;
using DomainModel;
using WinUI.WinForms.Gestiones.Clientes; // Para FrmSeleccionarCliente

namespace WinUI.WinForms.Gestiones.Reservas
{
    public partial class FrmReservaDetalle : Form
    {
        private CanchaHorario _slotActual;
        private Cliente _clienteSeleccionado; // Para guardar el cliente a asignar

        public FrmReservaDetalle(CanchaHorario slot)
        {
            InitializeComponent();
            _slotActual = slot;
            _clienteSeleccionado = slot.ReservadaPor; // Empezamos con el cliente que ya tenía
        }

        private void FrmReservaDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Cargar datos (no editables)
                lblCancha.Text = _slotActual.Cancha.Nombre;
                lblFecha.Text = _slotActual.FechaHorario.ToString("dd/MM/yyyy");
                lblHora.Text = $"{_slotActual.FechaHorario:HH:mm} - {_slotActual.FechaHorario.AddMinutes(_slotActual.Cancha.DuracionXPartidoMin):HH:mm}";

                // 2. Cargar ComboBox de Estados
                // Asumo que tu Enum EstadoReserva tiene "Libre", "Reservada", "Mantenimiento"
                cmbEstado.DataSource = Enum.GetValues(typeof(EstadoReserva));
                cmbEstado.SelectedItem = _slotActual.Estado;

                // 3. Cargar Cliente y Pago
                MostrarClienteSeleccionado();
                chkAbonada.Checked = _slotActual.Abonada;

                // 4. Lógica de UI
                HabilitarControlesSegunEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void MostrarClienteSeleccionado()
        {
            if (_clienteSeleccionado != null)
            {
                lblClienteSeleccionado.Text = _clienteSeleccionado.Nombre;
            }
            else
            {
                lblClienteSeleccionado.Text = "(Ninguno)";
            }
        }

        private void HabilitarControlesSegunEstado()
        {
            // Obtenemos el estado seleccionado en el ComboBox
            EstadoReserva estadoSeleccionado = (EstadoReserva)cmbEstado.SelectedItem;

            if (estadoSeleccionado == EstadoReserva.Reservada)
            {
                btnSeleccionarCliente.Enabled = true;
                chkAbonada.Enabled = true;
            }
            else // Si es "Libre" o "Mantenimiento"
            {
                btnSeleccionarCliente.Enabled = false;
                chkAbonada.Enabled = false;
                chkAbonada.Checked = false; // No puede estar abonado si no está reservado
                _clienteSeleccionado = null; // Quitamos el cliente
                MostrarClienteSeleccionado();
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarControlesSegunEstado();
        }

        private void btnSeleccionarCliente_Click(object sender, EventArgs e)
        {
            // Reutilizamos el FrmSeleccionarCliente que ya existe
            using (var frm = new FrmSeleccionarCliente())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _clienteSeleccionado = frm.ClienteSeleccionado;
                    MostrarClienteSeleccionado();
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            EstadoReserva nuevoEstado = (EstadoReserva)cmbEstado.SelectedItem;

            // Validación
            if (nuevoEstado == EstadoReserva.Reservada && _clienteSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un cliente para un turno 'Reservado'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                
                BLLFacade.Current.CanchaHorarioService.ActualizarReserva(
                    _slotActual.IdCanchaHorario,
                    nuevoEstado,
                    _clienteSeleccionado,
                    chkAbonada.Checked
                );

                MessageBox.Show("Reserva actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException bizEx) // Errores de negocio
            {
                MessageBox.Show(bizEx.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) // Errores generales
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
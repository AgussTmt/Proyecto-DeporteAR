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

namespace WinUI.WinForms.Gestiones.Jugadores
{
    public partial class FrmJugadorDetalle : Form
    {
        public FrmJugadorDetalle()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El Nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El Apellido es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return;
            }            
            try
            {
                var nuevoJugador = new Jugador();
                nuevoJugador.Idjugador = Guid.NewGuid();
                nuevoJugador.Nombre = txtNombre.Text.Trim();
                nuevoJugador.Apellido = txtApellido.Text.Trim();
                nuevoJugador.IdEquipo = null;
                nuevoJugador.PartidosJugados = 0;
                nuevoJugador.CantMvp = 0; 
                nuevoJugador.Habilitado = true;

                BLLFacade.Current.JugadorService.Add(nuevoJugador);
                MessageBox.Show("Jugador creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

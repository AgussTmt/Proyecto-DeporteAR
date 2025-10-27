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
        private Jugador _jugadorActual;
        public FrmJugadorDetalle()
        {
            InitializeComponent();
            _jugadorActual = new Jugador(); 
        }

        public FrmJugadorDetalle(Jugador jugadorAEditar)
        {
            InitializeComponent();

            _jugadorActual = BLLFacade.Current.JugadorService.GetById(jugadorAEditar.Idjugador); 
        }

        private void FrmJugadorDetalle_Load(object sender, EventArgs e)
        {
            if (_jugadorActual.Idjugador != Guid.Empty)
            {
                this.Text = "Editar Jugador";
                txtNombre.Text = _jugadorActual.Nombre;
                txtApellido.Text = _jugadorActual.Apellido;
            }
            else
            {
                this.Text = "Nuevo Jugador";
            }
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
                
                _jugadorActual.Nombre = txtNombre.Text.Trim();
                _jugadorActual.Apellido = txtApellido.Text.Trim();
                //crear
                if (_jugadorActual.Idjugador == Guid.Empty) 
                {
                    
                    _jugadorActual.Idjugador = Guid.NewGuid();
                    _jugadorActual.IdEquipo = null;
                    _jugadorActual.PartidosJugados = 0;
                    _jugadorActual.CantMvp = 0;
                    _jugadorActual.Habilitado = true;

                    BLLFacade.Current.JugadorService.Add(_jugadorActual);
                    MessageBox.Show("Jugador creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //editar
                else 
                {
                    
                    BLLFacade.Current.JugadorService.Update(_jugadorActual);
                    MessageBox.Show("Jugador actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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
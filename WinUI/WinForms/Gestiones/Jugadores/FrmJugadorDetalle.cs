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
        private bool _seHizoUnCambio = false;
        // Esta bandera nos ayuda a saber si estamos en modo "Crear" o "Editar"
        private bool _esNuevo;

        public FrmJugadorDetalle()
        {
            InitializeComponent();
            _jugadorActual = new Jugador();
            _esNuevo = true;
        }

        public FrmJugadorDetalle(Jugador jugadorAEditar)
        {
            InitializeComponent();
            _jugadorActual = BLLFacade.Current.JugadorService.GetById(jugadorAEditar.Idjugador);
            _esNuevo = false;
        }

        private void FrmJugadorDetalle_Load(object sender, EventArgs e)
        {
            if (!_esNuevo)
            {
                this.Text = "Editar Jugador";
                txtNombre.Text = _jugadorActual.Nombre;
                txtApellido.Text = _jugadorActual.Apellido;
                // No tiene sentido "Guardar y Nuevo" cuando estás editando
                btnGuardarYNuevo.Visible = false;
            }
            else
            {
                this.Text = "Nuevo Jugador";
            }
        }

        /// <summary>
        /// Lógica centralizada de validación y guardado.
        /// Devuelve 'true' si el guardado fue exitoso.
        /// </summary>
        private bool GuardarJugador()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El Nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El Apellido es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            try
            {
                _jugadorActual.Nombre = txtNombre.Text.Trim();
                _jugadorActual.Apellido = txtApellido.Text.Trim();

                if (_esNuevo) // (Adaptado de tu lógica original)
                {
                    _jugadorActual.Idjugador = Guid.NewGuid();
                    _jugadorActual.IdEquipo = null;
                    _jugadorActual.PartidosJugados = 0;
                    _jugadorActual.CantMvp = 0;
                    _jugadorActual.Habilitado = true;

                    BLLFacade.Current.JugadorService.Add(_jugadorActual);
                    // (Quitamos el MessageBox de éxito para que sea más rápido)
                }
                else
                {
                    BLLFacade.Current.JugadorService.Update(_jugadorActual);
                    MessageBox.Show("Jugador actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _seHizoUnCambio = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el jugador: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Botón "Guardar": Guarda y Cierra.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (GuardarJugador())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Botón "Guardar y Nuevo": Guarda y Limpia el form.
        /// </summary>
        private void btnGuardarYNuevo_Click(object sender, EventArgs e)
        {
            if (GuardarJugador())
            {
                // Si guardó bien, reseteamos el formulario
                LimpiarFormulario();
            }
        }

        /// <summary>
        /// Resetea el formulario para cargar un nuevo jugador.
        /// </summary>
        private void LimpiarFormulario()
        {
            _jugadorActual = new Jugador();
            _esNuevo = true;

            txtNombre.Clear();
            txtApellido.Clear();

            this.Text = "Nuevo Jugador";
            txtNombre.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_seHizoUnCambio)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }
    }
}
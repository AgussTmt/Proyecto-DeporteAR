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
using WinUI.WinForms.Gestiones.Clientes; 
using WinUI.WinForms.Gestiones.Jugadores;

namespace WinUI.WinForms.Gestiones.Equipos
{
    public partial class FrmEquipoDetalle : Form
    {
        private Equipo _equipoActual;
        private Cliente _capitanSeleccionado;
        private BindingList<Jugador> _jugadoresLibres;
        private BindingList<Jugador> _jugadoresAsignados;


        public FrmEquipoDetalle()
        {
            InitializeComponent();
            _equipoActual = new Equipo();
            _equipoActual.Jugadores = new List<Jugador>();
            _capitanSeleccionado = null;
        }
        
        public FrmEquipoDetalle(Equipo equipoAEditar)
        {
            InitializeComponent();
            try
            {
                _equipoActual = BLLFacade.Current.EquipoService.TraerPorId(equipoAEditar);
                if (_equipoActual == null)
                {
                    MessageBox.Show("Error: No se pudo cargar el equipo seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
                if (_equipoActual.Jugadores == null)
                    _equipoActual.Jugadores = new List<Jugador>();
                _capitanSeleccionado = _equipoActual.Capitan;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del equipo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void FrmEquipoDetalle_Load(object sender, EventArgs e)
        {
            ConfigurarListBoxes();
            if (_equipoActual != null && _equipoActual.IdEquipo != Guid.Empty)
            {
                this.Text = "Editar Equipo";
                txtNombre.Text = _equipoActual.Nombre;
                MostrarCapitan();
            }
            else if (_equipoActual != null)
            {
                this.Text = "Nuevo Equipo";
                lblCapitanSeleccionado.Text = "(Ninguno)";
            }
            CargarDatosListas();

        }

        private void CargarDatosListas()
        {
            try
            {
                _jugadoresAsignados = new BindingList<Jugador>(_equipoActual.Jugadores ?? new List<Jugador>());

                var jugadoresLibresDb = BLLFacade.Current.JugadorService.TraerJugadoresSinEquipo();
                _jugadoresLibres = new BindingList<Jugador>(jugadoresLibresDb);

                listBoxJugadoresLibres.DataSource = _jugadoresLibres;
                listBoxJugadoresAsignados.DataSource = _jugadoresAsignados;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar listas de jugadores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarListBoxes()
        {
            listBoxJugadoresLibres.DisplayMember = "NombreCompleto";
            listBoxJugadoresAsignados.DisplayMember = "NombreCompleto";

            listBoxJugadoresLibres.ValueMember = "Idjugador";
            listBoxJugadoresAsignados.ValueMember = "Idjugador";
        }

        private void RefrescarListaDeJugadoresDisponibles() // <-- NUEVO MÉTODO
        {
            try
            {
                var jugadorSeleccionadoAntes = listBoxJugadoresLibres.SelectedItem;
                var jugadoresLibresDb = BLLFacade.Current.JugadorService.TraerJugadoresSinEquipo();
                _jugadoresLibres = new BindingList<Jugador>(jugadoresLibresDb);

                listBoxJugadoresLibres.DataSource = _jugadoresLibres;

                if (jugadorSeleccionadoAntes != null)
                    listBoxJugadoresLibres.SelectedItem = jugadorSeleccionadoAntes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al refrescar jugadores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Actualiza el Label que muestra el nombre del capitán seleccionado.
        /// </summary>
        private void MostrarCapitan()
        {
            if (_capitanSeleccionado != null)
            {
                try
                {
                    var capitanCompleto = BLLFacade.Current.ClienteService.GetById(_capitanSeleccionado.IdCliente);
                    if (capitanCompleto != null)
                    {
                        _capitanSeleccionado = capitanCompleto; 
                        lblCapitanSeleccionado.Text = $"{_capitanSeleccionado.Nombre}".Trim();
                    }
                    else
                    {
                        lblCapitanSeleccionado.Text = "(Capitán no encontrado)";
                        _capitanSeleccionado = null; 
                    }
                }
                catch
                {
                    lblCapitanSeleccionado.Text = "(Error al cargar capitán)";
                    _capitanSeleccionado = null; 
                }
            }
            else
            {
                lblCapitanSeleccionado.Text = "(Ninguno)";
            }
        }

        /// <summary>
        /// Refresca el DataGridView con la lista actual de jugadores del equipo.
        /// </summary>

        private void btnSeleccionarCapitan_Click(object sender, EventArgs e)
        {
            // Asumo que tu FrmSeleccionarCliente devuelve el objeto en ClienteSeleccionado
            using (var frm = new FrmSeleccionarCliente())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _capitanSeleccionado = frm.ClienteSeleccionado;
                    MostrarCapitan();
                }
            }
        }

        private void btnAgregarJugador_Click(object sender, EventArgs e)
        {

            var jugadorSeleccionado = listBoxJugadoresLibres.SelectedItem as Jugador;

            if (jugadorSeleccionado != null)
            {
                
                _jugadoresLibres.Remove(jugadorSeleccionado);
                _jugadoresAsignados.Add(jugadorSeleccionado);
            }
        }

        private void btnQuitarJugador_Click(object sender, EventArgs e)
        {
            var jugadorSeleccionado = listBoxJugadoresAsignados.SelectedItem as Jugador;
            if (jugadorSeleccionado != null)
            {
                _jugadoresAsignados.Remove(jugadorSeleccionado);
                _jugadoresLibres.Add(jugadorSeleccionado);
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del equipo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            try
            {
                _equipoActual.Nombre = txtNombre.Text.Trim();
                _equipoActual.Capitan = _capitanSeleccionado;

                _equipoActual.Jugadores = _jugadoresAsignados.ToList();

                if (_equipoActual.IdEquipo == Guid.Empty)
                {
                    BLLFacade.Current.EquipoService.Crear(_equipoActual);
                    MessageBox.Show("Equipo creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    BLLFacade.Current.EquipoService.Update(_equipoActual);
                    MessageBox.Show("Equipo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el equipo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCrearNuevoJugador_Click(object sender, EventArgs e)
        {
            using (FrmJugadorDetalle frmCrearJugador = new FrmJugadorDetalle())
            {
                frmCrearJugador.ShowDialog();
                RefrescarListaDeJugadoresDisponibles();
            }
        }
    }
}
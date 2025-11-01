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

        private bool _esNuevo = false;
        private bool _seHizoUnCambio = false; // Bandera para refrescar


        public FrmEquipoDetalle()
        {
            InitializeComponent();
            _equipoActual = new Equipo();
            _equipoActual.Jugadores = new List<Jugador>();
            _capitanSeleccionado = null;
            _esNuevo = true;
        }

        public FrmEquipoDetalle(Equipo equipoAEditar)
        {
            InitializeComponent();
            _esNuevo = false;
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
            if (!_esNuevo)
            {
                this.Text = "Editar Equipo";
                txtNombre.Text = _equipoActual.Nombre;
                btnGuardarYNuevo.Visible = false; // No se puede "Guardar y Nuevo" en modo Edición
            }
            else
            {
                this.Text = "Nuevo Equipo";
            }

            MostrarCapitan();
            CargarDatosListas();
        }

        private void CargarDatosListas()
        {
            try
            {
                // Solo cargamos los asignados desde el equipo actual
                _jugadoresAsignados = new BindingList<Jugador>(_equipoActual.Jugadores ?? new List<Jugador>());
                listBoxJugadoresAsignados.DataSource = _jugadoresAsignados;

                // Los libres siempre los traemos de la BLL
                RefrescarListaDeJugadoresDisponibles();
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

        private void RefrescarListaDeJugadoresDisponibles()
        {
            try
            {
                var jugadorSeleccionadoAntes = listBoxJugadoresLibres.SelectedItem;

                var jugadoresLibresDb = BLLFacade.Current.JugadorService.TraerJugadoresSinEquipo();

                var idsYaAsignados = new HashSet<Guid>(_jugadoresAsignados.Select(j => j.Idjugador));

                var jugadoresRealmenteLibres = jugadoresLibresDb
                    .Where(j => !idsYaAsignados.Contains(j.Idjugador))
                    .ToList();

                _jugadoresLibres = new BindingList<Jugador>(jugadoresRealmenteLibres);

                listBoxJugadoresLibres.DataSource = _jugadoresLibres;

                if (jugadorSeleccionadoAntes != null)
                {
                    var jugadorEncontrado = _jugadoresLibres.FirstOrDefault(j => j.Idjugador == ((Jugador)jugadorSeleccionadoAntes).Idjugador);
                    if (jugadorEncontrado != null)
                    {
                        listBoxJugadoresLibres.SelectedItem = jugadorEncontrado;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al refrescar jugadores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarCapitan()
        {
            if (_capitanSeleccionado != null)
            {
                try
                {
                    // Solo traemos el capitan si no lo tenemos completo
                    if (string.IsNullOrEmpty(_capitanSeleccionado.Nombre))
                    {
                        var capitanCompleto = BLLFacade.Current.ClienteService.GetById(_capitanSeleccionado.IdCliente);
                        if (capitanCompleto != null)
                        {
                            _capitanSeleccionado = capitanCompleto;
                        }
                        else
                        {
                            lblCapitanSeleccionado.Text = "(Capitán no encontrado)";
                            _capitanSeleccionado = null;
                            return;
                        }
                    }
                    lblCapitanSeleccionado.Text = $"{_capitanSeleccionado.Nombre}".Trim();
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

        private void btnSeleccionarCapitan_Click(object sender, EventArgs e)
        {
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

        /// <summary>
        /// Lógica centralizada de validación y guardado.
        /// </summary>
        private bool GuardarEquipo()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del equipo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            try
            {
                _equipoActual.Nombre = txtNombre.Text.Trim();
                _equipoActual.Capitan = _capitanSeleccionado;
                _equipoActual.Jugadores = _jugadoresAsignados.ToList();

                if (_esNuevo)
                {
                    BLLFacade.Current.EquipoService.Crear(_equipoActual);
                }
                else
                {
                    BLLFacade.Current.EquipoService.Update(_equipoActual);
                }

                _seHizoUnCambio = true; // Marcamos que se guardó algo
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el equipo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Resetea el formulario para cargar un nuevo equipo.
        /// </summary>
        private void LimpiarFormulario()
        {
            _equipoActual = new Equipo();
            _equipoActual.Jugadores = new List<Jugador>();
            _capitanSeleccionado = null;
            _esNuevo = true;

            txtNombre.Clear();
            lblCapitanSeleccionado.Text = "(Ninguno)";

            // Recargamos las listas
            CargarDatosListas();

            this.Text = "Nuevo Equipo";
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (GuardarEquipo())
            {
                if (!_esNuevo) // Solo mostrar mensaje en modo Edición
                {
                    MessageBox.Show("Equipo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnGuardarYNuevo_Click(object sender, EventArgs e)
        {
            if (GuardarEquipo())
            {
                // Si guardó bien, reseteamos el formulario
                LimpiarFormulario();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Si guardamos algo (ej. con "Guardar y Nuevo"),
            // devolvemos OK para que el padre refresque la grilla.
            this.DialogResult = _seHizoUnCambio ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }

        private void btnCrearNuevoJugador_Click(object sender, EventArgs e)
        {
            using (FrmJugadorDetalle frmCrearJugador = new FrmJugadorDetalle())
            {
                // Verificamos si se guardó algo (OK o Cancel con _seHizoUnCambio)
                if (frmCrearJugador.ShowDialog() == DialogResult.OK)
                {
                    RefrescarListaDeJugadoresDisponibles();
                }
            }
        }
    }
}
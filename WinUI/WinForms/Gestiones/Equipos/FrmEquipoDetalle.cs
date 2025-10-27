using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Facade; // Asegúrate de tener este
using DomainModel; // Y este
using WinUI.WinForms.Gestiones.Clientes; // Para FrmSeleccionarCliente
//using WinUI.WinForms.Gestiones.Jugadores; // Necesitarás crear FrmJugadorDetalle

namespace WinUI.WinForms.Gestiones.Equipos
{
    public partial class FrmEquipoDetalle : Form
    {
        // Guarda el equipo que se está creando o editando
        private Equipo _equipo;
        // Guarda el objeto Cliente seleccionado como capitán
        private Cliente _capitanSeleccionado;

        // Constructor para CREAR un equipo nuevo
        public FrmEquipoDetalle()
        {
            InitializeComponent();
            _equipo = new Equipo(); // Crea un equipo vacío
            // Inicializa la lista de jugadores AHORA para evitar nulls
            _equipo.Jugadores = new List<Jugador>();
            _capitanSeleccionado = null;
        }

        // Constructor para EDITAR un equipo existente
        public FrmEquipoDetalle(Equipo equipoAEditar)
        {
            InitializeComponent();

            // --- Carga del equipo completo ---
            // Llamamos a TraerPorId para asegurarnos de tener la lista de jugadores.
            try
            {
                // Pasamos el objeto stub equipoAEditar que solo tiene el ID
                _equipo = BLLFacade.Current.EquipoService.TraerPorId(equipoAEditar);
                if (_equipo == null)
                {
                    MessageBox.Show("Error: No se pudo cargar el equipo seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
                // Aseguramos que la lista de jugadores no sea null después de cargar
                if (_equipo.Jugadores == null)
                    _equipo.Jugadores = new List<Jugador>();

                // Guardamos el capitán actual (puede ser null si no tiene)
                _capitanSeleccionado = _equipo.Capitan;
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
            // Configurar DataGridView de Jugadores
            dgvJugadores.AutoGenerateColumns = false; // Control manual
            dgvJugadores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJugadores.MultiSelect = false;
            // Define las columnas en el diseñador (Nombre, Apellido, etc.)
            // Asegúrate de poner el DataPropertyName correcto.

            if (_equipo != null && _equipo.IdEquipo != Guid.Empty) // Editando
            {
                this.Text = "Editar Equipo";
                txtNombre.Text = _equipo.Nombre;
                MostrarCapitan(); // Muestra el capitán cargado en el constructor
                CargarJugadoresGrid(); // Carga la lista de jugadores cargada
            }
            else if (_equipo != null) // Creando
            {
                this.Text = "Nuevo Equipo";
                lblCapitanSeleccionado.Text = "(Ninguno)"; // Texto inicial
                // La lista _equipo.Jugadores ya fue inicializada vacía en el constructor
                CargarJugadoresGrid();
            }
            // Si _equipo es null (por error en el constructor), el Load no hace nada más
        }

        /// <summary>
        /// Actualiza el Label que muestra el nombre del capitán seleccionado.
        /// </summary>
        private void MostrarCapitan()
        {
            if (_capitanSeleccionado != null)
            {
                // Intentamos cargar el cliente completo por si el _capitanSeleccionado
                // es solo un "stub" con el ID (depende de cómo EquipoService.TraerPorId lo cargue)
                try
                {
                    var capitanCompleto = BLLFacade.Current.ClienteService.GetById(_capitanSeleccionado.IdCliente);
                    if (capitanCompleto != null)
                    {
                        _capitanSeleccionado = capitanCompleto; // Actualizamos a la versión completa
                        lblCapitanSeleccionado.Text = $"{_capitanSeleccionado.Nombre} {_capitanSeleccionado.Apellido}".Trim();
                    }
                    else
                    {
                        lblCapitanSeleccionado.Text = "(Capitán no encontrado)";
                        _capitanSeleccionado = null; // Limpiamos si no se encontró
                    }
                }
                catch
                {
                    lblCapitanSeleccionado.Text = "(Error al cargar capitán)";
                    _capitanSeleccionado = null; // Limpiamos en caso de error
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
        private void CargarJugadoresGrid()
        {
            dgvJugadores.DataSource = null;
            if (_equipo?.Jugadores != null)
            {
                // Creamos una BindingList para que la grilla se actualice si la lista cambia
                dgvJugadores.DataSource = new BindingList<Jugador>(_equipo.Jugadores);
            }
            // Ocultar columnas
            if (dgvJugadores.Columns.Contains("Idjugador")) dgvJugadores.Columns["Idjugador"].Visible = false;
            if (dgvJugadores.Columns.Contains("IdEquipo")) dgvJugadores.Columns["IdEquipo"].Visible = false;
            // Oculta otras que no quieras ver (Puntuacion, Sanciones, Habilitado?)
            if (dgvJugadores.Columns.Contains("Habilitado")) dgvJugadores.Columns["Habilitado"].Visible = false;
            if (dgvJugadores.Columns.Contains("Puntuacion")) dgvJugadores.Columns["Puntuacion"].Visible = false;
            if (dgvJugadores.Columns.Contains("Sanciones")) dgvJugadores.Columns["Sanciones"].Visible = false;
        }


        private void btnSeleccionarCapitan_Click(object sender, EventArgs e)
        {
            // Asumo que tu FrmSeleccionarCliente devuelve el objeto en ClienteSeleccionado
            using (var frm = new FrmSeleccionarCliente())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _capitanSeleccionado = frm.ClienteSeleccionado;
                    MostrarCapitan(); // Actualiza la UI
                }
            }
        }

        private void btnAgregarJugador_Click(object sender, EventArgs e)
        {
            // Si estamos creando un equipo nuevo, debe guardarse primero
            if (_equipo.IdEquipo == Guid.Empty)
            {
                MessageBox.Show("Primero debe guardar el equipo para poder agregarle jugadores.", "Guardar Equipo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // --- NECESITAS CREAR FrmJugadorDetalle ---
            // Debe recibir IdEquipo, permitir crear/editar datos del jugador
            // y devolver el Jugador creado/editado.

            /* EJEMPLO (descomentar cuando tengas FrmJugadorDetalle):
            using (var frm = new FrmJugadorDetalle(_equipo.IdEquipo)) // Pasa IdEquipo
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var jugadorGuardado = frm.JugadorGuardado; // Propiedad que devuelve el jugador
                    if (jugadorGuardado != null)
                    {
                        // Si era un jugador nuevo, lo añadimos a la lista
                        var existente = _equipo.Jugadores.FirstOrDefault(j => j.Idjugador == jugadorGuardado.Idjugador);
                        if (existente == null)
                        {
                            _equipo.Jugadores.Add(jugadorGuardado);
                        }
                        else // Si era edición, actualizamos el objeto en la lista (opcional, si BindingList no lo hace solo)
                        {
                             // Podrías necesitar actualizar las propiedades del 'existente' con las de 'jugadorGuardado'
                             // O simplemente recargar la grilla si la BindingList se actualiza sola.
                        }
                        CargarJugadoresGrid(); // Refresca la grilla
                    }
                }
            }
            */
            MessageBox.Show("Funcionalidad pendiente: Crear FrmJugadorDetalle.", "Info", MessageBoxButtons.OK); // Temporal
        }

        private void btnQuitarJugador_Click(object sender, EventArgs e)
        {
            if (dgvJugadores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un jugador para quitar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var jugadorSeleccionado = (Jugador)dgvJugadores.SelectedRows[0].DataBoundItem;

            var confirmacion = MessageBox.Show($"¿Está seguro de quitar al jugador '{jugadorSeleccionado.Nombre} {jugadorSeleccionado.Apellido}' del equipo?\n\n(El jugador no se borrará, solo se desvinculará).", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                // Simplemente lo quitamos de la lista EN MEMORIA.
                // La lógica de desvinculación en la BD se hará en btnGuardar_Click
                // al llamar a EquipoService.Update.
                _equipo.Jugadores.Remove(jugadorSeleccionado);

                // Refresca la grilla inmediatamente
                CargarJugadoresGrid();
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
            // Puedes añadir más validaciones si quieres (ej: mínimo de jugadores?)

            try
            {
                // Actualiza el objeto _equipo con los datos del formulario
                _equipo.Nombre = txtNombre.Text.Trim();
                _equipo.Capitan = _capitanSeleccionado; // Asigna el capitán seleccionado
                // La lista _equipo.Jugadores ya fue modificada por Agregar/Quitar

                if (_equipo.IdEquipo == Guid.Empty) // Creando nuevo
                {
                    // Asignamos un nuevo ID antes de crearlo
                    _equipo.IdEquipo = Guid.NewGuid();
                    BLLFacade.Current.EquipoService.Crear(_equipo);
                    MessageBox.Show("Equipo creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Editando existente
                {
                    // Llama al Update que ya modificamos para manejar jugadores
                    BLLFacade.Current.EquipoService.Update(_equipo);
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
    }
}
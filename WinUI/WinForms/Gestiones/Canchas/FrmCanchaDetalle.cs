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
using Services__ArqBase_.Facade;


namespace WinUI.WinForms.Gestiones.Canchas
{
    public partial class FrmCanchaDetalle : Form
    {
        private readonly Cancha _canchaAEditar;
        
        private Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)> _disponibilidadOriginal;

        
        public FrmCanchaDetalle()
        {
            InitializeComponent();
            IdiomaHelper.TraducirControles(this);
            _canchaAEditar = null;
            _disponibilidadOriginal = new Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)>();
        }

        
        public FrmCanchaDetalle(Cancha cancha)
        {
            InitializeComponent();
            IdiomaHelper.TraducirControles(this);
            _canchaAEditar = cancha;
            _disponibilidadOriginal = new Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)>();
        }

        private void FrmCanchaDetalle_Load(object sender, EventArgs e)
        {
            
            try
            {
                var deportes = BLLFacade.Current.CatalogService.GetDeportes();
                cmbDeporte.DataSource = deportes.ToList();
                cmbDeporte.DisplayMember = "Descripcion";
                cmbDeporte.ValueMember = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar deportes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SetupDisponibilidadGrid();
            
            if (_canchaAEditar != null) 
            {
                this.Text = "Editar Cancha";
                txtNombre.Text = _canchaAEditar.Nombre;
                txtPrecio.Text = _canchaAEditar.Precio.ToString();
                numCapacidad.Value = _canchaAEditar.Capacidad;
                cmbDeporte.SelectedValue = _canchaAEditar.Deporte;

                CargarDisponibilidadGuardada(_canchaAEditar.IdCancha);
            }
            else 
            {
                this.Text = "Nueva Cancha";
            }
        }

        /// <summary>
        /// Prepara el DataGridView de disponibilidad con días y horas.
        /// </summary>
        private void SetupDisponibilidadGrid()
        {
            dgvDisponibilidad.Rows.Clear();

            var horas = Enumerable.Range(8, 16)
                                   .Select(h => TimeSpan.FromHours(h).ToString(@"hh\:mm"))
                                   .ToList();

            var cmbColInicio = dgvDisponibilidad.Columns["colHoraInicio"] as DataGridViewComboBoxColumn;
            var cmbColFin = dgvDisponibilidad.Columns["colHoraFin"] as DataGridViewComboBoxColumn;

            if (cmbColInicio != null) cmbColInicio.DataSource = new List<string>(horas);
            if (cmbColFin != null) cmbColFin.DataSource = new List<string>(horas);

           
            string[] diasNombres = { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
            

            // Agregar filas, una por cada día (0=Domingo a 6=Sábado)
            for (int i = 0; i < 7; i++)
            {
                DayOfWeek diaEnum = (DayOfWeek)i; 
                string nombreDia = diasNombres[i]; 

                int rowIndex = dgvDisponibilidad.Rows.Add();
                var row = dgvDisponibilidad.Rows[rowIndex];

                row.Cells["colDiaNombre"].Value = nombreDia;
                row.Tag = diaEnum; 

                // Valores por defecto (Lunes a Viernes, 9 a 18)
                bool checkDefault = (diaEnum >= DayOfWeek.Monday && diaEnum <= DayOfWeek.Friday);
                string horaInicioDefault = "09:00";
                string horaFinDefault = "18:00";

                row.Cells["colSeleccionDia"].Value = checkDefault;
                row.Cells["colHoraInicio"].Value = horaInicioDefault;
                row.Cells["colHoraFin"].Value = horaFinDefault;
            }
        }

        /// <summary>
        /// Carga la disponibilidad guardada en la BD para el modo Editar.
        /// </summary>
        private void CargarDisponibilidadGuardada(Guid idCancha)
        {
            try
            {
                _disponibilidadOriginal = BLLFacade.Current.CanchaService.GetDisponibilidadSemanal(idCancha);

                foreach (DataGridViewRow row in dgvDisponibilidad.Rows)
                {
                    if (row.Tag is DayOfWeek dia && _disponibilidadOriginal.ContainsKey(dia))
                    {
                        var (start, end) = _disponibilidadOriginal[dia];
                        row.Cells["colSeleccionDia"].Value = true; 
                        row.Cells["colHoraInicio"].Value = start.ToString(@"hh\:mm"); 
                        row.Cells["colHoraFin"].Value = end.ToString(@"hh\:mm");      
                    }
                    else
                    {
                        row.Cells["colSeleccionDia"].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la disponibilidad guardada: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                { MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                { MessageBox.Show("El formato del precio no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (cmbDeporte.SelectedValue == null)
                { MessageBox.Show("Debe seleccionar un deporte.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                if (NumDuracionXPartido.Value <= 0)
                {
                    MessageBox.Show("La duración por partido debe ser mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var disponibilidadNueva = new Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)>();
                foreach (DataGridViewRow row in dgvDisponibilidad.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["colSeleccionDia"].Value ?? false))
                    {
                        DayOfWeek dia = (DayOfWeek)row.Tag;
                        string strInicio = row.Cells["colHoraInicio"].Value?.ToString();
                        string strFin = row.Cells["colHoraFin"].Value?.ToString();

                        if (string.IsNullOrEmpty(strInicio) || string.IsNullOrEmpty(strFin))
                        { MessageBox.Show($"Seleccione horas para {row.Cells["colDiaNombre"].Value}.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                        TimeSpan horaInicio = TimeSpan.Parse(strInicio);
                        TimeSpan horaFin = TimeSpan.Parse(strFin);

                        if (horaInicio >= horaFin)
                        { MessageBox.Show($"Hora inicio debe ser menor a fin para {row.Cells["colDiaNombre"].Value}.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                        disponibilidadNueva.Add(dia, (horaInicio, horaFin));
                    }
                }
                if (disponibilidadNueva.Count == 0)
                { MessageBox.Show("Seleccione al menos un día.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                string deporteSeleccionado = cmbDeporte.SelectedValue.ToString();


                if (_canchaAEditar == null) 
                {
                    var nuevaCancha = new Cancha
                    {
                        IdCancha = Guid.NewGuid(),
                        Nombre = txtNombre.Text,
                        Precio = precio,
                        Capacidad = (int)numCapacidad.Value,
                        Deporte = deporteSeleccionado,
                        FechaCreacion = DateTime.Now,
                        Estado = true,
                        DuracionXPartidoMin = (int)NumDuracionXPartido.Value

                    };
                    BLLFacade.Current.CanchaService.Add(nuevaCancha, disponibilidadNueva);
                }
                else 
                {
                    _canchaAEditar.Nombre = txtNombre.Text;
                    _canchaAEditar.Precio = precio;
                    _canchaAEditar.Capacidad = (int)numCapacidad.Value;
                    _canchaAEditar.Deporte = deporteSeleccionado;
                    _canchaAEditar.DuracionXPartidoMin = (int)NumDuracionXPartido.Value;
                    BLLFacade.Current.CanchaService.Update(_canchaAEditar, disponibilidadNueva);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (FormatException fx)
            {
                MessageBox.Show($"Error en formato numérico: {fx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
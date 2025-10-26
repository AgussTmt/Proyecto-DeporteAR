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

namespace WinUI.WinForms.Gestiones.Competiciones
{
    public partial class FrmCompeticionDetalle : Form
    {
        private readonly Competicion _competicionAEditar;
        private Cancha _canchaSeleccionada;
        public FrmCompeticionDetalle()
        {
            InitializeComponent();
            _competicionAEditar = null;
            _canchaSeleccionada = null;
        }

        public FrmCompeticionDetalle(Competicion competicion)
        {
            InitializeComponent();
            _competicionAEditar = competicion;
        }

        private void FrmCompeticionDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                cmbFormato.DataSource = Enum.GetValues(typeof(FormatoEnum));
                var canchas = BLLFacade.Current.CanchaService.GetAll().ToList();
                cmbCanchaAsignada.DataSource = canchas;
                cmbCanchaAsignada.DisplayMember = "Nombre"; 
                cmbCanchaAsignada.ValueMember = "IdCancha";    
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

            
            if (_competicionAEditar != null)
            {
                this.Text = "Edit Competition";
                txtNombre.Text = _competicionAEditar.Nombre;
                numCupos.Value = _competicionAEditar.Cupos;
                numCuposMinimos.Value = _competicionAEditar.CuposMinimos;
                dtpFechaInicio.Value = _competicionAEditar.FechaInicio;
                txtFranjaHoraria.Text = _competicionAEditar.FranjaHoraria;
                numFrecuencia.Value = _competicionAEditar.Frecuencia;
                txtPrecio.Text = _competicionAEditar.Precio.ToString();
                cmbFormato.SelectedItem = _competicionAEditar.Formato;
                cmbCanchaAsignada.SelectedValue = _competicionAEditar.canchaAsignada?.IdCancha ?? Guid.Empty;
            }
            else
            {
                this.Text = "New Competition";
                cmbCanchaAsignada.SelectedIndex = -1;
                
                dtpFechaInicio.Value = DateTime.Today.AddDays(1);
            }
            cmbCanchaAsignada_SelectedIndexChanged(sender, e);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || cmbCanchaAsignada.SelectedValue == null)
                {
                    MessageBox.Show("Name and Assigned Pitch are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    MessageBox.Show("Invalid price format.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCapacidadFranja())
                {
                    return; // Detiene el guardado si la validación falla
                }

                Competicion compToSave;

                if (_competicionAEditar == null) 
                {
                    compToSave = new Competicion
                    {
                        IdCompeticion = Guid.NewGuid(),
                        FechaCreacion = DateTime.Now, 
                        FechaInicio = dtpFechaInicio.Value,
                        Nombre = txtNombre.Text,
                        Estado = EstadoCompeticion.SinFixture, 
                        Formato = (FormatoEnum)cmbFormato.SelectedItem, 
                        Cupos = (int)numCupos.Value,
                        CuposMinimos = (int)numCuposMinimos.Value,
                        FranjaHoraria = txtFranjaHoraria.Text,
                        Frecuencia = (int)numFrecuencia.Value, 
                        Precio = precio, 
                        canchaAsignada = new Cancha { IdCancha = (Guid)cmbCanchaAsignada.SelectedValue },
                        ListaEquipos = new List<Equipo>()

                    };
                }
                else 
                {
                    compToSave = _competicionAEditar;
                    
                }


                compToSave.Nombre = txtNombre.Text;
                compToSave.Cupos = (int)numCupos.Value;
                compToSave.CuposMinimos = (int)numCuposMinimos.Value;
                compToSave.FechaInicio = dtpFechaInicio.Value;
                compToSave.FranjaHoraria = txtFranjaHoraria.Text;
                compToSave.Frecuencia = (int)numFrecuencia.Value;
                compToSave.Precio = precio;
                compToSave.Formato = (FormatoEnum)cmbFormato.SelectedItem;
                compToSave.canchaAsignada = new Cancha { IdCancha = (Guid)cmbCanchaAsignada.SelectedValue };
                if (_competicionAEditar == null)
                {
                    BLLFacade.Current.CompeticionService.Add(compToSave);
                }
                else
                {
                    BLLFacade.Current.CompeticionService.Update(compToSave);
                }

                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving competition: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void cmbCanchaAsignada_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCanchaAsignada.SelectedValue == null || !(cmbCanchaAsignada.SelectedValue is Guid))
            {
                lblDisponibilidad.Text = "Seleccione una cancha...";
                _canchaSeleccionada = null;
                return;
            }

            try
            {
                //ID de la cancha seleccionada
                Guid idCancha = (Guid)cmbCanchaAsignada.SelectedValue;

                _canchaSeleccionada = BLLFacade.Current.CanchaService.GetById(idCancha);
                if (_canchaSeleccionada == null)
                {
                    lblDisponibilidad.Text = "No se encontró la cancha seleccionada.";
                    return;
                }

                var disponibilidad = BLLFacade.Current.CanchaService.GetDisponibilidadSemanal(idCancha);

                // 4. Formatea y muestra la disponibilidad
                if (disponibilidad.Count == 0)
                {
                    lblDisponibilidad.Text = "Esta cancha no tiene horarios definidos.";
                }
                else
                {
                    lblDisponibilidad.Text = FormatearDisponibilidad(disponibilidad);
                }
            }
            catch (Exception ex)
            {
                lblDisponibilidad.Text = $"Error al cargar horarios: {ex.Message}";
            }
        }

        private string FormatearDisponibilidad(Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)> disponibilidad)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Disponibilidad de la cancha:");

            // Ordenamos los días de Lunes a Domingo (con Domingo al final)
            var diasOrdenados = disponibilidad.Keys
                .OrderBy(d => (d == DayOfWeek.Sunday) ? 7 : (int)d);

            // Creamos una cultura en español para los nombres de los días
            var culturaES = new System.Globalization.CultureInfo("es-ES");

            foreach (var dia in diasOrdenados)
            {
                var franja = disponibilidad[dia];

                // Obtenemos el nombre del día en español y con mayúscula
                string nombreDia = culturaES.DateTimeFormat.GetDayName(dia);
                nombreDia = char.ToUpper(nombreDia[0]) + nombreDia.Substring(1);

                // Formato "HH:mm"
                string horaInicio = franja.start.ToString(@"hh\:mm");
                string horaFin = franja.end.ToString(@"hh\:mm");

                sb.AppendLine($"- {nombreDia}: de {horaInicio} a {horaFin} hs.");
            }

            return sb.ToString();
        }

        private bool ValidarCapacidadFranja()
        {
            // A. Validar que los datos básicos existan
            if (_canchaSeleccionada == null)
            {
                MessageBox.Show("Debe seleccionar una cancha.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFranjaHoraria.Text) || !txtFranjaHoraria.Text.Contains("-"))
            {
                MessageBox.Show("El formato de la franja horaria no es válido. Use HH-HH (ej: 10-14).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_canchaSeleccionada.DuracionXPartidoMin <= 0)
            {
                string msgError = $"La cancha '{_canchaSeleccionada.Nombre}' tiene una duración por partido de 0 minutos y no puede usarse para torneos.\n\n" +
                                  "Por favor, edite la cancha en la 'Gestión de Canchas' y asígnele una duración.";
                MessageBox.Show(msgError, "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // B. Parsear la franja horaria
            TimeSpan horaInicio, horaFin;
            try
            {
                string[] franja = txtFranjaHoraria.Text.Split('-');
                horaInicio = TimeSpan.Parse(franja[0] + ":00");
                horaFin = TimeSpan.Parse(franja[1] + ":00");

                if (horaFin <= horaInicio)
                {
                    MessageBox.Show("La hora de fin de la franja debe ser mayor a la hora de inicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer la franja horaria: {ex.Message}. Use el formato HH-HH.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // C. Calcular partidos y minutos necesarios
            int cupos = (int)numCupos.Value;

            // Si cupos es impar (ej 9), se añade 1 (DESCANSA) -> 10 equipos -> 5 partidos
            int equiposEnFixture = (cupos % 2 != 0) ? cupos + 1 : cupos;
            int partidosPorRonda = equiposEnFixture / 2;

            int duracionMinutos = _canchaSeleccionada.DuracionXPartidoMin;
            double minutosTotalesNecesarios = partidosPorRonda * duracionMinutos;

            // D. Calcular minutos disponibles
            double minutosDisponiblesEnFranja = (horaFin - horaInicio).TotalMinutes;

            // E. Comparar y validar
            if (minutosTotalesNecesarios > minutosDisponiblesEnFranja)
            {
                string mensaje = $"La configuración es inviable:\n\n" +
                                 $"Cupos: {cupos} (implica {partidosPorRonda} partidos por ronda)\n" +
                                 $"Duración por Partido: {duracionMinutos} min\n" +
                                 $"Minutos Requeridos: {partidosPorRonda} x {duracionMinutos} = {minutosTotalesNecesarios} minutos\n\n" +
                                 $"Franja Horaria: {txtFranjaHoraria.Text} ({minutosDisponiblesEnFranja} minutos disponibles)\n\n" +
                                 "La franja horaria no es lo suficientemente larga para albergar todos los partidos de una ronda. Por favor, aumente la franja horaria o reduzca los cupos.";

                MessageBox.Show(mensaje, "Validación de Fixture", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Detiene el guardado
            }

            return true; // ¡Validación exitosa!
        }
    }
}

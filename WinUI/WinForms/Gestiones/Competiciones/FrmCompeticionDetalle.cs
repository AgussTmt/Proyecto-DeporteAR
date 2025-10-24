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
        public FrmCompeticionDetalle()
        {
            InitializeComponent();
            _competicionAEditar = null;
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
                dtpFechaInicio.Value = DateTime.Today.AddDays(1);
            }
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
    }
}

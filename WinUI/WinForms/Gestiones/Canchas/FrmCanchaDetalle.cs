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

namespace WinUI.WinForms.Gestiones.Canchas
{
    public partial class FrmCanchaDetalle : Form
    {
        private readonly Cancha _canchaAEditar;
        public FrmCanchaDetalle()
        {
            InitializeComponent();
            _canchaAEditar = null;
        }


        public FrmCanchaDetalle(Cancha cancha)
        {
            InitializeComponent();
            _canchaAEditar = cancha;
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
            if (_canchaAEditar != null)
            {
                
                this.Text = "Editar Cancha";
                txtNombre.Text = _canchaAEditar.Nombre;
                txtPrecio.Text = _canchaAEditar.Precio.ToString();
                txtFranjaHoraria.Text = _canchaAEditar.FranjaHoraria;
                numCapacidad.Value = _canchaAEditar.Capacidad;
                cmbDeporte.SelectedValue = _canchaAEditar.Deporte;
            }
            else
            {
                
                this.Text = "Nueva Cancha";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtFranjaHoraria.Text))
                {
                    MessageBox.Show("Nombre y Franja Horaria son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbDeporte.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un deporte.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string deporteSeleccionado = cmbDeporte.SelectedValue.ToString();

                if (_canchaAEditar == null)
                {
                    
                    var nuevaCancha = new Cancha
                    {
                        IdCancha = Guid.NewGuid(),
                        Nombre = txtNombre.Text,
                        Precio = decimal.Parse(txtPrecio.Text), 
                        FranjaHoraria = txtFranjaHoraria.Text, 
                        Capacidad = (int)numCapacidad.Value,
                        Deporte = deporteSeleccionado,
                        FechaCreacion = DateTime.Now,
                        Estado = true
                    };

                    
                    BLLFacade.Current.CanchaService.Add(nuevaCancha);
                }
                else
                {
                    
                    _canchaAEditar.Nombre = txtNombre.Text;
                    _canchaAEditar.Precio = decimal.Parse(txtPrecio.Text);
                    _canchaAEditar.FranjaHoraria = txtFranjaHoraria.Text;
                    _canchaAEditar.Capacidad = (int)numCapacidad.Value;
                    _canchaAEditar.Deporte = deporteSeleccionado;



                    BLLFacade.Current.CanchaService.Update(_canchaAEditar);
                }

                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("El formato del precio no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

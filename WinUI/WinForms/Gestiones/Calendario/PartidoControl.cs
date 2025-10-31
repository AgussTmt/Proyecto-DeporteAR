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

namespace WinUI.WinForms.Gestiones.Calendario
{
    public partial class PartidoControl : UserControl
    {
        private Fixture _partido;
        public event EventHandler ResultadoCargado;
        public PartidoControl()
        {
            InitializeComponent();
        }

        private void PartidoControl_Load(object sender, EventArgs e)
        {

        }

        public void CargarPartido(Fixture partido)
        {
            _partido = partido; // Guardamos la referencia

            // --- Configurar textos ---
            lblInfo.Text = $"{partido.CanchaHorario.FechaHorario:HH:mm} hs - {partido.CanchaHorario.Cancha.Nombre}";
            lblNroFecha.Text = $"Partido ID: {partido.IdFixture.ToString().Substring(0, 8)}...";
            lblLocal.Text = partido.Equipos[0].Nombre;
            lblVisitante.Text = partido.Equipos[1].Nombre;

            txtResLocal.Text = partido.Resultado?.Split('-')[0] ?? "-";
            txtResVisitante.Text = partido.Resultado?.Split('-').Length > 1 ? partido.Resultado.Split('-')[1] : "-";

            // --- Configurar estado (Finalizado / Pendiente) ---
            if (partido.Estado == EstadoFixture.Finalizado)
            {
                txtResLocal.ReadOnly = true;
                txtResVisitante.ReadOnly = true;
                btnCargar.Text = "Cargado";
                btnCargar.Enabled = false;
                this.BackColor = Color.LightGray; // 'this' se refiere al UserControl
            }
            else
            {
                // Asegurarnos de que esté en el estado por defecto
                txtResLocal.ReadOnly = false;
                txtResVisitante.ReadOnly = false;
                btnCargar.Text = "Cargar";
                btnCargar.Enabled = true;
                this.BackColor = Color.FromArgb(230, 240, 255); // Azul claro
            }

            // --- Ajustar el ancho del control ---
            // Esto es opcional pero recomendado para que ocupe el ancho del FlowPanel
            if (this.Parent != null && this.Parent is FlowLayoutPanel)
            {
                this.Width = this.Parent.Width - 25; // Ajuste por el scrollbar
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                // _partido fue guardado cuando llamamos a CargarPartido()
                using (var frmDetalle = new FrmCargarResultadoDetalle(_partido))
                {
                    // Abrimos el formulario como un diálogo modal
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        // Si el usuario guardó (DialogResult.OK),
                        // disparamos el evento para que FrmCalendario se refresque.
                        ResultadoCargado?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el detalle del partido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

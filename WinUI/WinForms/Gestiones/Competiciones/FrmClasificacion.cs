using System;
using System.Windows.Forms;
using BLL.Facade;
using DomainModel; // Asegúrate de tener este using

namespace WinUI.WinForms.Gestiones.Competiciones // O el namespace que prefieras
{
    public partial class FrmClasificacion : Form
    {
        private Guid _idCompeticion;
        private string _nombreCompeticion;

        public FrmClasificacion(Guid idCompeticion, string nombreCompeticion)
        {
            InitializeComponent();
            _idCompeticion = idCompeticion;
            _nombreCompeticion = nombreCompeticion;
        }

        private void FrmClasificacion_Load(object sender, EventArgs e)
        {
            lblTituloCompeticion.Text = $"Tabla de Posiciones: {_nombreCompeticion}";
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                // 1. Llamamos a la BLL que ya existe
                var lista = BLLFacade.Current.ClasificacionService.ListarPorCompeticion(_idCompeticion);

                // 2. Asignamos los datos a la grilla
                dgvClasificacion.DataSource = lista;

                // 3. Configuramos la apariencia
                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la tabla de clasificación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ConfigurarGrilla()
        {
            // Ocultamos las columnas que el usuario no necesita ver
            if (dgvClasificacion.Columns.Contains("IdClasificacion"))
                dgvClasificacion.Columns["IdClasificacion"].Visible = false;

            if (dgvClasificacion.Columns.Contains("IdCompeticion"))
                dgvClasificacion.Columns["IdCompeticion"].Visible = false;

            // Renombramos y ajustamos las columnas visibles
            if (dgvClasificacion.Columns.Contains("Equipo"))
            {
                dgvClasificacion.Columns["Equipo"].HeaderText = "Equipo";
                dgvClasificacion.Columns["Equipo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvClasificacion.Columns.Contains("Puntos"))
                dgvClasificacion.Columns["Puntos"].HeaderText = "Pts";

            if (dgvClasificacion.Columns.Contains("PartidosJugados"))
                dgvClasificacion.Columns["PartidosJugados"].HeaderText = "PJ";

            if (dgvClasificacion.Columns.Contains("Victorias"))
                dgvClasificacion.Columns["Victorias"].HeaderText = "V";

            if (dgvClasificacion.Columns.Contains("Empates"))
                dgvClasificacion.Columns["Empates"].HeaderText = "E";

            if (dgvClasificacion.Columns.Contains("Derrotas"))
                dgvClasificacion.Columns["Derrotas"].HeaderText = "D";

            if (dgvClasificacion.Columns.Contains("GolesAFavor"))
                dgvClasificacion.Columns["GolesAFavor"].HeaderText = "GF";
        }
    }
}
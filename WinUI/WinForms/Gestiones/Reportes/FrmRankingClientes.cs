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
using BLL.Services;
using BLL.Services.Dto;

namespace WinUI.WinForms.Gestiones.Reportes
{
    public partial class FormRankingClientes : Form
    {
        public FormRankingClientes()
        {
            InitializeComponent();
        }

        private void FormRankingClientes_Load(object sender, EventArgs e)
        {
            // Configuramos el DataGridView
            // Lo hicimos en el designer, pero por las dudas
            // nos aseguramos de que no autogenere columnas
            dgvRanking.AutoGenerateColumns = false;

            // Generamos el reporte inicial al cargar
            GenerarReporte();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void GenerarReporte()
        {
            try
            {
                // 1. Obtenemos el valor del NumericUpDown
                int topN = (int)numTopN.Value;

                // 2. Llamamos a nuestra nueva función de la BLL
                // (Asumiendo que tienes una Facade estática BLLFacade.Current)
                List<RankingClienteDTO> ranking = BLLFacade.Current.ClienteService.GetRankingClientes(topN);

                // 3. Bindeamos el resultado (la lista de DTOs) al DataGridView
                dgvRanking.DataSource = null; // Limpiamos bindeo anterior
                dgvRanking.DataSource = ranking;

                // 4. (Opcional) Ajustar el texto si no hay resultados
                if (ranking.Count == 0)
                {
                    MessageBox.Show("No se encontraron reservas para generar un ranking.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al generar el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
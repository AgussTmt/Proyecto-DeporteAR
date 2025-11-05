using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL.Facade;
using BLL.Services; // Para el DTO de Ranking
using BLL.Services.Dto;
using DomainModel;
using Services__ArqBase_.Facade;

namespace WinUI.WinForms.Gestiones.Reportes
{
    public partial class FrmReportes : Form
    {
        // Enum interno para manejar el estado
        private enum TipoReporteSeleccionado
        {
            Ninguno,
            RankingClientes,
            Deudores,
            Facturacion
        }

        private TipoReporteSeleccionado _reporteActual;

        public FrmReportes()
        {
            InitializeComponent();
        }

        private void FormReportes_Load(object sender, EventArgs e)
        {

            IdiomaHelper.TraducirControles(this);
            // 1. Cargar el ComboBox principal
            cmbTipoReporte.Items.Add("Seleccione...");
            cmbTipoReporte.Items.Add("Ranking de Clientes");
            cmbTipoReporte.Items.Add("Reporte de Deudores");
            cmbTipoReporte.Items.Add("Facturación por Período");
            cmbTipoReporte.SelectedIndex = 0;

            // 2. Ocultar todos los paneles de filtros
            pnlFiltroRanking.Visible = false;
            pnlFiltroDeudores.Visible = false;
            pnlFiltroFacturacion.Visible = false;

            // 3. Cargar filtros que se usan en otros paneles
            CargarCanchaComboBox();
            dtpDesdeFacturacion.Value = DateTime.Today.AddDays(-30);
            dtpHastaFacturacion.Value = DateTime.Today;
        }
        private void CargarCanchaComboBox()
        {
            try
            {
                var canchas = BLLFacade.Current.CanchaService.GetAll().ToList();

                // Creamos un item "Todas" genérico
                var todas = new { IdCancha = Guid.Empty, Nombre = "(Todas)" };

                // Proyectamos a un tipo anónimo para bindeo
                var listaParaCombo = canchas
                    .Select(c => new { c.IdCancha, c.Nombre })
                    .ToList();

                listaParaCombo.Insert(0, todas); // Insertamos "Todas" al principio

                cmbCanchaFacturacion.DataSource = listaParaCombo;
                cmbCanchaFacturacion.DisplayMember = "Nombre";
                cmbCanchaFacturacion.ValueMember = "IdCancha";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar canchas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpiar la grilla y totales
            dgvResultados.DataSource = null;
            dgvResultados.Columns.Clear(); // Limpia columnas autogeneradas
            lblTotal.Text = "";

            // Ocultar todos los paneles
            pnlFiltroRanking.Visible = false;
            pnlFiltroDeudores.Visible = false;
            pnlFiltroFacturacion.Visible = false;

            // Mostrar el panel correcto
            switch (cmbTipoReporte.SelectedItem.ToString())
            {
                case "Ranking de Clientes":
                    _reporteActual = TipoReporteSeleccionado.RankingClientes;
                    pnlFiltroRanking.Visible = true;
                    pnlFiltroRanking.Dock = DockStyle.Fill; // Asegura que ocupe el espacio
                    lblTotal.Text = "Mostrando Top Clientes";
                    break;

                case "Reporte de Deudores":
                    _reporteActual = TipoReporteSeleccionado.Deudores;
                    pnlFiltroDeudores.Visible = true;
                    pnlFiltroDeudores.Dock = DockStyle.Fill;
                    lblTotal.Text = "Total Adeudado: $ 0.00";
                    break;

                case "Facturación por Período":
                    _reporteActual = TipoReporteSeleccionado.Facturacion;
                    pnlFiltroFacturacion.Visible = true;
                    pnlFiltroFacturacion.Dock = DockStyle.Fill;
                    lblTotal.Text = "Total Facturado: $ 0.00";
                    break;

                default:
                    _reporteActual = TipoReporteSeleccionado.Ninguno;
                    break;
            }
        }

        // --- Botón de "Reporte de Deudores" ---
        private void btnGenerarDeudores_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Llamar a la BLL
                List<CanchaHorario> deudores = BLLFacade.Current.CanchaHorarioService.GetReporteDeudores();

                // 2. Configurar la Grilla (¡NO Autogenerar!)
                ConfigurarGridDeudores(); // <--- USAREMOS LA VERSIÓN CORREGIDA

                // 3. Bindear
                dgvResultados.DataSource = deudores;

                // 4. Calcular Total
                decimal totalDeuda = deudores.Sum(h => h.Cancha?.Precio ?? 0m); //
                lblTotal.Text = $"Total Adeudado: $ {totalDeuda:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGridDeudores()
        {
            dgvResultados.DataSource = null;
            dgvResultados.Columns.Clear();
            dgvResultados.AutoGenerateColumns = false;

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha Vencimiento",
                DataPropertyName = "FechaHorario", // Este está bien, es simple
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = null // <--- ¡CAMBIO!
            });

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cancha",
                HeaderText = "Cancha",
                DataPropertyName = null // <--- ¡CAMBIO!
            });

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Monto Adeudado",
                DataPropertyName = null, // <--- CAMBIO: Nulo
                // Borramos el DefaultCellStyle
            });
        }

        // --- Botones de los otros reportes (quedarían pendientes) ---
        private void btnGenerarRanking_Click(object sender, EventArgs e)
        {
            // (Este era el que faltaba)
            try
            {
                // 1. Obtener filtros
                int topN = (int)numTopN.Value;

                // 2. Llamar a la BLL (¡ESTO REQUIERE que hayas implementado el DTO y el ClienteService!)
                List<RankingClienteDTO> ranking = BLLFacade.Current.ClienteService.GetRankingClientes(topN);

                // 3. Configurar la Grilla
                ConfigurarGridRanking();

                // 4. Bindear
                dgvResultados.DataSource = ranking;

                // 5. Mostrar total
                lblTotal.Text = $"Mostrando {ranking.Count} clientes";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGridRanking()
        {
            dgvResultados.DataSource = null;
            dgvResultados.Columns.Clear();
            dgvResultados.AutoGenerateColumns = false; // ¡Importante!

            // Añadimos las columnas para el DTO
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = "Nombre" // Bindea a la propiedad del DTO
            });

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                HeaderText = "Teléfono",
                DataPropertyName = "Telefono" // Bindea a la propiedad del DTO
            });

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CantidadReservas",
                HeaderText = "Cantidad de Reservas",
                DataPropertyName = "CantidadReservas" // Bindea a la propiedad del DTO
            });
        }

        private void btnGenerarFacturacion_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Obtener filtros
                DateTime desde = dtpDesdeFacturacion.Value.Date; // Inicio del día
                DateTime hasta = dtpHastaFacturacion.Value.Date.AddDays(1).AddTicks(-1); // Fin del día
                Guid? idCancha = (Guid)cmbCanchaFacturacion.SelectedValue;

                if (idCancha == Guid.Empty)
                {
                    idCancha = null; // Es "Todas"
                }

                // 2. Llamar a la BLL
                List<CanchaHorario> facturacion = BLLFacade.Current.CanchaHorarioService.GetReporteFacturacion(desde, hasta, idCancha);

                // 3. Configurar la Grilla
                ConfigurarGridFacturacion(); // <--- USAREMOS LA VERSIÓN CORREGIDA

                // 4. Bindear
                dgvResultados.DataSource = facturacion;

                // 5. Calcular Total
                decimal totalFacturado = facturacion.Sum(h => h.Cancha?.Precio ?? 0m);
                lblTotal.Text = $"Total Facturado: $ {totalFacturado:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGridFacturacion()
        {
            dgvResultados.DataSource = null;
            dgvResultados.Columns.Clear();
            dgvResultados.AutoGenerateColumns = false;

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha de Pago",
                DataPropertyName = "FechaHorario", // Este está bien
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                HeaderText = "Cliente",
                DataPropertyName = null // <--- ¡CAMBIO!
            });

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cancha",
                HeaderText = "Cancha",
                DataPropertyName = null // <--- ¡CAMBIO!
            });

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Monto Abonado",
                DataPropertyName = null, // <--- CAMBIO: Nulo
                // Borramos el DefaultCellStyle
            });
        }


        // --- ¡EL MÉTODO MÁGICO! ---
        // Reemplazamos el formateador por este, que imita tu FrmCompeticion.cs
        private void dgvResultados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Si es la fila de cabecera, no hagas nada
            if (e.RowIndex < 0) return;

            // Si es un DTO de Ranking, no tiene objetos complejos, así que salimos.
            if (dgvResultados.Rows[e.RowIndex].DataBoundItem is RankingClienteDTO)
            {
                return;
            }

            // Si llegamos aquí, es un CanchaHorario
            var horario = (CanchaHorario)dgvResultados.Rows[e.RowIndex].DataBoundItem;
            if (horario == null) return;

            string colName = dgvResultados.Columns[e.ColumnIndex].Name;

            // --- INICIO DEL CÓDIGO CORREGIDO ---
            switch (colName)
            {
                // Dejamos que la columna "Fecha" (que es automática) pase de largo

                case "Cliente":
                    if (horario.ReservadaPor != null)
                    {
                        e.Value = $"{horario.ReservadaPor.Nombre} (Tel: {horario.ReservadaPor.Telefono})";
                    }
                    else
                    {
                        e.Value = "(N/A)";
                    }
                    e.FormattingApplied = true;
                    break;

                case "Cancha":
                    if (horario.Cancha != null)
                    {
                        e.Value = horario.Cancha.Nombre;
                    }
                    else
                    {
                        e.Value = "(N/A)";
                    }
                    e.FormattingApplied = true;
                    break;

                case "Precio":
                    // ¡AQUÍ APLICAMOS TU IDEA!
                    if (horario.Cancha != null)
                    {
                        // Convertimos el decimal (2000) a un string con formato.
                        e.Value = horario.Cancha.Precio.ToString("C2");
                    }
                    else
                    {
                        e.Value = (0m).ToString("C2");
                    }
                    // Como el valor (e.Value) YA ES UN STRING, no hay nada
                    // que la grilla pueda formatear mal.
                    e.FormattingApplied = true;
                    break;
            }
            // --- FIN DEL CÓDIGO CORREGIDO ---
        }
    }
}
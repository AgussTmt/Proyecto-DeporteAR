using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; // <--- Agregamos esto
using System.Threading.Tasks;
using System.Windows.Forms;
using Services__ArqBase_.Facade;

namespace WinUI.WinForms.Gestiones.Settings
{
    public partial class FrmLogs : Form
    {
        private readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "mi_app.log");

        public FrmLogs()
        {
            InitializeComponent();
            CargarNiveles();
            // Le seteamos valores por defecto a los filtros de fecha
            dtpFechaDesde.Value = DateTime.Today.AddDays(-7);
            dtpFechaHasta.Value = DateTime.Today;
            CargarLogs();
        }

        private void FrmLogs_Load(object sender, EventArgs e)
        {
            IdiomaHelper.TraducirControles(this);
        }

        private void CargarNiveles()
        {
            CmbFiltroNiveles.Items.AddRange(new[] { "Todos", "Trace", "Debug", "Information", "Warning", "Error", "Fatal" });
            CmbFiltroNiveles.SelectedIndex = 0;
        }

        /// <summary>
        /// El corazón del visor. Lee el archivo y aplica los filtros de la UI.
        /// </summary>
        private void CargarLogs()
        {
            try
            {
                if (!File.Exists(logFilePath))
                {
                    richTextBoxLogs.Text = "No se encontró el archivo de log.";
                    return;
                }

                // 1. Leemos el archivo (con FileShare por si está en uso)
                string contenido;
                using (FileStream fs = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader reader = new StreamReader(fs))
                {
                    contenido = reader.ReadToEnd();
                }

                // 2. Parseamos CADA entrada de log a un objeto
                var todasLasEntradas = SepararEntradasLog(contenido) // Tu método helper
                                    .Select(ParseLogEntry) // El helper nuevo
                                    .Where(e => e != null)
                                    .ToList();

                // 3. Traemos los valores de los filtros de la UI
                string nivelFiltro = CmbFiltroNiveles.SelectedItem?.ToString() ?? "Todos";
                string textoFiltro = txtFiltroTexto.Text.ToLowerInvariant();
                DateTime fechaDesde = dtpFechaDesde.Value.Date;
                DateTime fechaHasta = dtpFechaHasta.Value.Date.AddDays(1).AddTicks(-1); // Fin del día

                // 4. Aplicamos los filtros con LINQ (¡la magia!)
                IEnumerable<LogEntry> entradasFiltradas = todasLasEntradas;

                if (nivelFiltro != "Todos")
                {
                    entradasFiltradas = entradasFiltradas.Where(e => e.Level == nivelFiltro);
                }

                if (!string.IsNullOrWhiteSpace(textoFiltro))
                {
                    // Buscamos en el texto crudo (así busca en stack traces también)
                    entradasFiltradas = entradasFiltradas.Where(e => e.RawText.ToLowerInvariant().Contains(textoFiltro));
                }

                if (chkFiltrarFecha.Checked) // <-- El CheckBox
                {
                    entradasFiltradas = entradasFiltradas.Where(e => e.Timestamp >= fechaDesde && e.Timestamp <= fechaHasta);
                }

                // 5. Mostramos los resultados
                richTextBoxLogs.Clear();
                // Reverse() para ver lo último primero
                var entradasParaMostrar = entradasFiltradas.Reverse().ToList();

                if (!entradasParaMostrar.Any())
                {
                    richTextBoxLogs.Text = "--- No se encontraron entradas con esos filtros ---";
                    return;
                }

                // Usamos un StringBuilder para que sea más rápido
                var sb = new StringBuilder();
                foreach (var entrada in entradasParaMostrar)
                {
                    sb.AppendLine(entrada.RawText);
                    sb.AppendLine(); // Un espacio extra
                }
                richTextBoxLogs.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el log: {ex.Message}");
            }
        }

        /// <summary>
        /// Separa el archivo de log en "bloques" (cada entrada con su stack trace).
        /// </summary>
        private List<string> SepararEntradasLog(string contenido)
        {
            var entradas = new List<string>();
            var lineas = contenido.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var entradaActual = new StringBuilder();

            foreach (var linea in lineas)
            {
                // Si la línea arranca con "[AAAA-MM-DD...", es una nueva entrada
                if (Regex.IsMatch(linea, @"^\[\d{4}-\d{2}-\d{2}"))
                {
                    // Guardamos la entrada anterior (si había una)
                    if (entradaActual.Length > 0)
                    {
                        entradas.Add(entradaActual.ToString().TrimEnd());
                        entradaActual.Clear();
                    }
                    entradaActual.Append(linea);
                }
                else
                {
                    // Es la continuación (ej: un stack trace), la agregamos
                    if (entradaActual.Length > 0)
                        entradaActual.AppendLine();
                    entradaActual.Append(linea);
                }
            }

            // Guardamos la última entrada
            if (entradaActual.Length > 0)
            {
                entradas.Add(entradaActual.ToString().TrimEnd());
            }

            return entradas;
        }

        /// <summary>
        /// Parsea un string de log (la primera línea) a un objeto LogEntry.
        /// </summary>
        private LogEntry ParseLogEntry(string rawText)
        {
            try
            {
                var match = Regex.Match(rawText,
                    @"^\[(?<timestamp>.*?)\] \[(?<level>.*?)\]");

                if (!match.Success)
                {
                    return null;
                }

                return new LogEntry
                {
                    Timestamp = DateTime.Parse(match.Groups["timestamp"].Value),
                    Level = match.Groups["level"].Value,
                    RawText = rawText
                };
            }
            catch
            {
                // Si el parseo de fecha falla
                return new LogEntry { Timestamp = DateTime.MinValue, Level = "Unknown", RawText = rawText };
            }
        }

        // --- EVENTOS DE LA UI ---

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarLogs();
        }

        private void cmbNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLogs();
        }

        private void txtFiltroTexto_TextChanged(object sender, EventArgs e)
        {
            // (Podrías agregar un Timer para que no filtre con cada tecla,
            // pero para empezar, esto funciona)
            CargarLogs();
        }

        private void dtpFechaDesde_ValueChanged(object sender, EventArgs e)
        {
            CargarLogs();
        }

        private void dtpFechaHasta_ValueChanged(object sender, EventArgs e)
        {
            CargarLogs();
        }

        private void chkFiltrarFecha_CheckedChanged(object sender, EventArgs e)
        {

            dtpFechaDesde.Enabled = chkFiltrarFecha.Checked;
            dtpFechaHasta.Enabled = chkFiltrarFecha.Checked;
            CargarLogs();
        }

        private void richTextBoxLogs_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void FrmLogs_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }

    /// <summary>
    /// Clase helper para tratar cada entrada de log como un objeto
    /// y poder usar LINQ para filtrarla.
    /// </summary>
    internal class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string RawText { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
            CargarLogs();
        }
        private void CargarNiveles()
        {
            CmbFiltroNiveles.Items.AddRange(new[] { "Todos", "Trace", "Debug", "Information", "Warning", "Error", "Fatal" });
            CmbFiltroNiveles.SelectedIndex = 0;
        }

        private void CargarLogs()
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    string contenido;
                    using (FileStream fs = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        contenido = reader.ReadToEnd();
                    }

                    var entradas = SepararEntradasLog(contenido);

                    string nivelSeleccionado = CmbFiltroNiveles.SelectedItem?.ToString() ?? "Todos";
                    if (nivelSeleccionado != "Todos")
                    {
                        entradas = entradas
                            .Where(e => e.Contains($"[{nivelSeleccionado}]"))
                            .ToList();
                    }

                    richTextBoxLogs.Clear();

                    foreach (var entrada in entradas.AsEnumerable().Reverse())
                    {
                        richTextBoxLogs.AppendText(entrada + Environment.NewLine + Environment.NewLine);
                    }
                }
                else
                {
                    richTextBoxLogs.Text = "No se encontró el archivo de log.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el log: {ex.Message}");
            }
        }
        

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarLogs();
        }

        private void cmbNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLogs();
        }
        private List<string> SepararEntradasLog(string contenido)
        {
            var entradas = new List<string>();
            var lineas = contenido.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var entradaActual = new StringBuilder();

            foreach (var linea in lineas)
            {
                
                if (System.Text.RegularExpressions.Regex.IsMatch(linea, @"^\[\d{4}-\d{2}-\d{2}"))
                {
                    
                    if (entradaActual.Length > 0)
                    {
                        entradas.Add(entradaActual.ToString().TrimEnd()); 
                        entradaActual.Clear();
                    }

                    entradaActual.Append(linea); 
                }
                else
                {
                   
                    if (entradaActual.Length > 0)
                        entradaActual.AppendLine();
                    entradaActual.Append(linea);
                }
            }

            
            if (entradaActual.Length > 0)
            {
                entradas.Add(entradaActual.ToString().TrimEnd());
            }

            return entradas;
        }

        private void richTextBoxLogs_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmLogs_Load(object sender, EventArgs e)
        {
            IdiomaHelper.TraducirControles(this);
        }
        private void FrmLogs_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

    }
}

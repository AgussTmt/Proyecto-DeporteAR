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
using WinUI.WinForms.Gestiones.Equipos;

namespace WinUI.WinForms.Gestiones.Competiciones
{
    public partial class FrmCompeticion : Form
    {
        public FrmCompeticion()
        {
            InitializeComponent();
        }

        private void FrmCompeticion_Load(object sender, EventArgs e)
        {
            IdiomaHelper.TraducirControles(this);
            dgvCompeticiones.AutoGenerateColumns = false;
            RefrescarGrid();
        }

        private void RefrescarGrid()
        {
            try
            {
                dgvCompeticiones.DataSource = null;
                
                dgvCompeticiones.DataSource = BLLFacade.Current.CompeticionService.GetAll().ToList();
                if (dgvCompeticiones.Columns.Contains("IdCompeticion"))
                {
                    dgvCompeticiones.Columns["IdCompeticion"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar competiciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void FrmCompeticion_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void BtnNueva_Click(object sender, EventArgs e)
        {
            using (var formDetalle = new FrmCompeticionDetalle())
            {
                var result = formDetalle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    RefrescarGrid();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCompeticiones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una competición para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var competicionSeleccionada = (Competicion)dgvCompeticiones.SelectedRows[0].DataBoundItem;

            using (var formDetalle = new FrmCompeticionDetalle(competicionSeleccionada))
            {
                var result = formDetalle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    RefrescarGrid();
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvCompeticiones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una competición para inactivar (cancelar o archivar).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var comp = (Competicion)dgvCompeticiones.SelectedRows[0].DataBoundItem;
            if (comp == null) return;

            var confirmacion = MessageBox.Show($"¿Está seguro de inactivar la competición '{comp.Nombre}'?\n\n(Si no tiene fixture se 'Cancelará', si ya terminó se 'Archivará').", "Confirmar Inactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {

                    BLLFacade.Current.CompeticionService.ActivarODesactivar(comp.IdCompeticion);

                    MessageBox.Show($"¡Competición desactivada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarGrid();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error en la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnInscribirEquipo_Click(object sender, EventArgs e)
        {
            if (dgvCompeticiones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una competición para inscribir un equipo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var competicionSeleccionada = (Competicion)dgvCompeticiones.SelectedRows[0].DataBoundItem;


            try
            {
                var idsYaInscriptos = competicionSeleccionada.ListaEquipos
                                        .Select(eq => eq.IdEquipo)
                                        .ToList();
                using (var frm = new FrmSeleccionarEquipo(idsYaInscriptos))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        
                        var nuevosEquipos = frm.EquiposSeleccionados;

                        if (nuevosEquipos.Count == 0) return; 

                        var erroresInscripcion = new System.Text.StringBuilder();
                        int exitosos = 0;

                        
                        foreach (var equipo in nuevosEquipos)
                        {
                            try
                            {
                               
                                BLLFacade.Current.CompeticionService.AñadirEquipo(competicionSeleccionada, equipo);
                                exitosos++;
                            }
                            catch (Exception ex)
                            {
                                
                                erroresInscripcion.AppendLine($"- {equipo.Nombre}: {ex.Message}");
                            }
                        }

                        RefrescarGrid();

                        var resumen = new System.Text.StringBuilder();
                        resumen.AppendLine($"Se inscribieron {exitosos} equipo(s) correctamente.");

                        if (erroresInscripcion.Length > 0)
                        {
                            resumen.AppendLine("\nNo se pudieron inscribir los siguientes equipos:");
                            resumen.Append(erroresInscripcion.ToString());
                            MessageBox.Show(resumen.ToString(), "Resultado de Inscripción (con errores)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show(resumen.ToString(), "Resultado de Inscripción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inscribir equipo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarFixture_Click(object sender, EventArgs e)
        {
            if (dgvCompeticiones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una competición para generar su fixture.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var competicionSeleccionada = (Competicion)dgvCompeticiones.SelectedRows[0].DataBoundItem;

            var confirmacion = MessageBox.Show($"¿Está seguro de generar el fixture para '{competicionSeleccionada.Nombre}'? Esta acción cerrará las inscripciones y no se puede deshacer.", "Confirmar Generación de Fixture", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                   
                    List<string> resultado = BLLFacade.Current.CompeticionService.CrearFixture(competicionSeleccionada);

                    
                    if (resultado == null || !resultado.Any()) 
                    {
                        MessageBox.Show("¡Fixture generado con éxito!", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefrescarGrid(); 
                    }
                    else 
                    {
                        
                        string mensajesError = string.Join(Environment.NewLine, resultado);
                        MessageBox.Show($"No se pudo generar el fixture debido a los siguientes conflictos:{Environment.NewLine}{mensajesError}",
                                        "Conflictos de Horario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                    }
                    
                }
                
                catch (InvalidOperationException bizEx)
                {
                    MessageBox.Show($"No se pudo generar el fixture: {bizEx.Message}", "Error de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inesperado al generar el fixture: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCompeticiones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvCompeticiones.Columns[e.ColumnIndex].Name == "colOcupacion")
            {
                var competicionOcupacion = dgvCompeticiones.Rows[e.RowIndex].DataBoundItem as Competicion;
                if (competicionOcupacion == null || competicionOcupacion.ListaEquipos == null)
                {
                    e.Value = "-";
                    e.FormattingApplied = true;
                    return; 
                }
                e.Value = $"{competicionOcupacion.ListaEquipos.Count} / {competicionOcupacion.Cupos}";
                e.FormattingApplied = true;
                return; 
            }
            if (dgvCompeticiones.Columns[e.ColumnIndex].Name == "colCanchaAsignada")
            {
                var competicionCancha = dgvCompeticiones.Rows[e.RowIndex].DataBoundItem as Competicion;
                if (competicionCancha?.canchaAsignada == null)
                {
                    e.Value = "(Ninguna)";
                    e.FormattingApplied = true;
                    return; 
                }
                if (!string.IsNullOrEmpty(competicionCancha.canchaAsignada.Nombre))
                {
                    e.Value = competicionCancha.canchaAsignada.Nombre;
                    e.FormattingApplied = true;
                    return;
                }
                if (competicionCancha.canchaAsignada.IdCancha == Guid.Empty)
                {
                    e.Value = "(ID Inválido)";
                    e.FormattingApplied = true;
                    return;
                }
                try
                {
                    var canchaCompleta = BLLFacade.Current.CanchaService.GetById(competicionCancha.canchaAsignada.IdCancha);
                    if (canchaCompleta == null)
                    {
                        e.Value = "(Cancha no encontrada)";
                        e.FormattingApplied = true;
                        return; 
                    }
                    e.Value = canchaCompleta.Nombre;
                    competicionCancha.canchaAsignada.Nombre = canchaCompleta.Nombre;
                    e.FormattingApplied = true;
                }
                catch (Exception ex) 
                {
                    
                    e.Value = "(Error al cargar)";
                    e.FormattingApplied = true;
                    
                }
            }


        }

        private void btnDesinscribirEquipo_Click(object sender, EventArgs e)
        {
            if (dgvCompeticiones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una competición para desinscribir un equipo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var competicionSeleccionada = (Competicion)dgvCompeticiones.SelectedRows[0].DataBoundItem;

            if (competicionSeleccionada.ListaEquipos == null || !competicionSeleccionada.ListaEquipos.Any())
            {
                MessageBox.Show("Esta competición no tiene equipos inscriptos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            
            if (competicionSeleccionada.Estado != EstadoCompeticion.SinFixture)
            {
                MessageBox.Show("No se pueden quitar equipos, el fixture ya está generado.", "Acción Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var IdEquipos = competicionSeleccionada.ListaEquipos
                                     .Select(e => e.IdEquipo)
                                     .ToList();

                var AllEquipos = BLLFacade.Current.EquipoService.GetAll().ToList();
                var EquiposInscriptos = AllEquipos
                                             .Where(e => IdEquipos.Contains(e.IdEquipo))
                                             .ToList();
                using (var frm = new FrmSeleccionarEquipo(EquiposInscriptos))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        var equiposAQuitar = frm.EquiposSeleccionados;
                        if (equiposAQuitar.Count == 0) return;

                        var confirmacion = MessageBox.Show($"¿Está seguro de quitar {equiposAQuitar.Count} equipo(s) de la competición '{competicionSeleccionada.Nombre}'?", "Confirmar Desinscripción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (confirmacion == DialogResult.No) return;

                        int exitosos = 0;
                        var errores = new System.Text.StringBuilder();

                        foreach (var equipo in equiposAQuitar)
                        {
                            try
                            {
                                BLLFacade.Current.CompeticionService.QuitarEquipo(competicionSeleccionada, equipo);
                                exitosos++;
                            }
                            catch (Exception ex)
                            {
                                errores.AppendLine($"- {equipo.Nombre}: {ex.Message}");
                            }
                        }

                        RefrescarGrid(); 
                        var resumen = new System.Text.StringBuilder();
                        resumen.AppendLine($"Se quitaron {exitosos} equipo(s) correctamente.");

                        if (errores.Length > 0)
                        {
                            resumen.AppendLine("\nNo se pudieron quitar los siguientes equipos:");
                            resumen.Append(errores.ToString());
                            MessageBox.Show(resumen.ToString(), "Resultado (con errores)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show(resumen.ToString(), "Resultado de Desinscripción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al desinscribir equipo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.Facade.Extensions;

namespace Services__ArqBase_.Facade
{
    /// <summary>
    /// Provee métodos de ayuda (helpers) estáticos para aplicar la traducción
    /// de idiomas (i18n) a controles de Windows Forms.
    /// </summary>
    public static class IdiomaHelper
    {

        /// <summary>
        /// Traduce recursivamente el texto de un control y todos sus controles hijos
        /// utilizando el método de extensión <c>.Traducir()</c> (que llama a <see cref="Services.Bll.IdiomaService"/>).
        /// </summary>
        /// <param name="control">El control contenedor (ej: un Form, Panel, o UserControl)
        /// desde el cual iniciar la traducción.</param>
        /// <remarks>
        /// Este método itera sobre la colección <c>Controls</c> del control padre.
        /// Contiene lógica especial para traducir:
        /// - Cabeceras de <see cref="DataGridViewColumn"/>.
        /// - Texto de <see cref="TabPage"/> en un <see cref="TabControl"/>.
        /// <br/>
        /// Ignora explícitamente los <see cref="RichTextBox"/> para no traducir
        /// el contenido ingresado por el usuario (solo traduce el <c>.Text</c> del control, no el <c>.Rtf</c>).
        /// </remarks>
        public static void TraducirControles(Control control)
        {
            // --- LA CORRECCIÓN ESTÁ ACÁ ---
            // Le decimos que ignore los controles de entrada de datos
            // (TextBox, DateTimePicker, etc.) para no traducir
            // ni la fecha, ni lo que el usuario escribió.
            if (!(control is RichTextBox) &&
                !(control is TextBox) &&
                !(control is DateTimePicker) &&
                !(control is NumericUpDown) &&
                !(control is MaskedTextBox))
            {
                if (!string.IsNullOrEmpty(control.Text))
                {
                    control.Text = control.Text.Traducir();
                }
            }
            // --- FIN DE LA CORRECCIÓN ---


            // La lógica especial para DataGridViews (traduce cabeceras)
            if (control is DataGridView dgv)
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!string.IsNullOrEmpty(col.HeaderText))
                    {
                        col.HeaderText = col.HeaderText.Traducir();
                    }
                }
            }


            // La lógica especial para TabControls (traduce pestañas)
            if (control is TabControl tc)
            {
                foreach (TabPage page in tc.TabPages)
                {
                    if (!string.IsNullOrEmpty(page.Text))
                    {
                        page.Text = page.Text.Traducir();
                    }
                }
            }


            // La llamada recursiva para todos los controles hijos
            foreach (Control c in control.Controls)
            {
                TraducirControles(c);
            }
        }
    }

        
}


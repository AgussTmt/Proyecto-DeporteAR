using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.Facade.Extensions;

namespace Services__ArqBase_.Facade
{
    public static class IdiomaHelper
    {
        public static event Action IdiomaCambio;
        public static void TraducirControles(Control control)
        {
            
            if (!(control is RichTextBox))
            {
                control.Text = control.Text.Traducir();
            }
            // Este es un bucle recursivo que recorre todos los controles dentro del control actual
            foreach (Control c in control.Controls)
            {
                // Llama a la misma función para los controles hijos
                TraducirControles(c);

                if (control is DataGridView)
                {
                    DataGridView dgv = (DataGridView)control;
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        col.HeaderText = col.HeaderText.Traducir();
                    }
                }

                
                if (control is TabControl)
                {
                    TabControl tc = (TabControl)control;
                    foreach (TabPage page in tc.TabPages)
                    {
                        page.Text = page.Text.Traducir();
                    }
                }

            }
        }

        
    }
}

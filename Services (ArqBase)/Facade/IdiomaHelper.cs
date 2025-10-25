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
        public static void TraducirControles(Control control)
        {
            
            if (!(control is RichTextBox))
            {
                if (!string.IsNullOrEmpty(control.Text))
                {
                    control.Text = control.Text.Traducir();
                }
            }

            
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

            
            foreach (Control c in control.Controls)
            {
                TraducirControles(c);
            }
        }
    }

        
}


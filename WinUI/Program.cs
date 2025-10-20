using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinUI
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            string securityConString = ConfigurationManager.ConnectionStrings["SecurityString"].ConnectionString;
            
            // 2. Inicializa el SqlHelper de la capa DAL
            Dal.Tools.SqlHelper.Initialize(securityConString);


            string savedLanguage = Properties.Settings.Default.LastLanguage;

            // 2. Si hay un idioma guardado (no es la primera vez que se abre), lo establecemos.
            if (!string.IsNullOrEmpty(savedLanguage))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(savedLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(savedLanguage);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}

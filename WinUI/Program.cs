using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}

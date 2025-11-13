using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinUI.WinForms;
using WinUI.WinForms.Gestiones;

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


            bool firstRun;
            try
            {
                // 1. Leemos la flag del app.config
                firstRun = bool.Parse(ConfigurationManager.AppSettings["FirstRun"]);
            }
            catch (Exception)
            {
                // Si la key no existe o está corrupta, forzamos el setup
                firstRun = true;
            }

            if (firstRun)
            {
                MessageBox.Show("Bienvenido a DeporteAR. Antes de comenzar, se debe configurar la conexión a la base de datos y el correo.",
                                "Configuración Inicial Requerida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                // 3. Llamamos al formulario nuevo que vamos a crear
                // (Este form tiene que existir, aunque esté vacío por ahora)
                using (var frmSetup = new FrmConfiguracionInicial())
                {
                    if (frmSetup.ShowDialog() != DialogResult.OK)
                    {
                        // Si el usuario cancela el setup, la app no puede correr.
                        MessageBox.Show("La configuración de la base de datos es obligatoria para usar la aplicación. La aplicación se cerrará.",
                                        "Error Crítico",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                        Application.Exit(); // Cierra la app
                        return; // No sigue al Application.Run()
                    }
                }
            }

            Application.Run(new FrmLogin());
        }
    }
}

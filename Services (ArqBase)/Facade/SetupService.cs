using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Services.Facade;

namespace Services__ArqBase_.Bll
{
    public static class SetupService
    {
        /// <summary>
        /// El método principal de la instalación.
        /// </summary>
        public static void InitializeApplication(string servidor, string usuarioSql, string passSql, string email, string passEmail)
        {
            // 1. Armar la Connection String "Maestra"
            string masterConnString = $"Data Source={servidor};Initial Catalog=master;User ID={usuarioSql};Password={passSql};";

            // 2. Ejecutar los scripts SQL
            string ns = "WinUI.ScriptsSQL.";

            EjecutarScriptEmbebido(ns + "1-CrearBaseSecurity.sql", masterConnString);
            EjecutarScriptEmbebido(ns + "2-SecurityDB_SchemaData.sql", masterConnString);
            EjecutarScriptEmbebido(ns + "3-CrearBaseNegocio.sql", masterConnString);
            EjecutarScriptEmbebido(ns + "4-DeporteAR_SchemaData.sql", masterConnString);

            // 3. Guardar la configuración en app.config
            GuardarAppConfig(servidor, usuarioSql, passSql, email, passEmail);
        }

        /// <summary>
        /// Lee un script SQL incrustado y lo ejecuta.
        /// </summary>
        private static void EjecutarScriptEmbebido(string resourceName, string connectionString)
        {
            string script = "";

            //el script esta en el winUi
            using (Stream stream = Assembly.GetEntryAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new Exception($"Error crítico: No se encontró el script incrustado '{resourceName}'.");
                using (StreamReader reader = new StreamReader(stream))
                {
                    script = reader.ReadToEnd();
                }
            }

            IEnumerable<string> comandos = script.Split(
                new[] { "GO\r\n", "GO ", "GO\t", "GO" },
                StringSplitOptions.RemoveEmptyEntries);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (string comando in comandos)
                {
                    if (string.IsNullOrWhiteSpace(comando)) continue;
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Guarda permanentemente la configuración en el app.config.
        /// </summary>
        private static void GuardarAppConfig(string servidor, string usuarioSql, string passSql, string email, string passEmail)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string connStrSecurity = $"Data Source={servidor};Initial Catalog=SecurityDB;User ID={usuarioSql};Password={passSql};";
            string connStrBusiness = $"Data Source={servidor};Initial Catalog=DeporteAR;User ID={usuarioSql};Password={passSql};";

            config.ConnectionStrings.ConnectionStrings["SecurityString"].ConnectionString = connStrSecurity;
            config.ConnectionStrings.ConnectionStrings["BusinessString"].ConnectionString = connStrBusiness;

            config.AppSettings.Settings["SenderEmail"].Value = email;

            config.AppSettings.Settings["ContraseñaEmail"].Value = CryptographyService.Encrypt(passEmail);

            config.AppSettings.Settings["FirstRun"].Value = "false";

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}


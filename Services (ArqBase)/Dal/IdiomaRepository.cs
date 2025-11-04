using Services.DomainModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Dal
{
    /// <summary>
    /// Repositorio Singleton para manejar la internacionalización (i18n) y localización (l10n) del sistema.
    /// Lee traducciones desde archivos de texto plano (formato .ini: Clave=Valor)
    /// basados en la cultura (culture) del hilo (thread) actual.
    /// </summary>
    public sealed class IdiomaRepository
    {
        #region Singleton
        private readonly static IdiomaRepository _instance = new IdiomaRepository();

        /// <summary>
        /// Obtiene la instancia única (Singleton) del repositorio de idiomas.
        /// </summary>
        public static IdiomaRepository Current
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Constructor privado para implementar el patrón Singleton.
        /// </summary>
        private IdiomaRepository()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        private static string folderPath = ConfigurationManager.AppSettings["IdiomaFolderPath"];

        private static string fileName = ConfigurationManager.AppSettings["IdiomaFileName"];

        private static string path = default;

        private static readonly object _fileLock = new object();

        /// <summary>
        /// Constructor estático para inicializar la ruta base de los archivos de idioma desde el App.config.
        /// </summary>
        static IdiomaRepository()
        {
            path = Path.Combine(folderPath, fileName);
        }


        /// <summary>
        /// Traduce una clave (DataKey/palabra) al texto correspondiente en el idioma 
        /// de la cultura actual del hilo (Thread.CurrentCulture).
        /// </summary>
        /// <param name="word">La clave (key) que se desea traducir (ej: 'WelcomeMessage').</param>
        /// <returns>El string traducido (el 'value' del archivo).</returns>
        /// <exception cref="WordNotFoundException">Se lanza si la clave no se encuentra en el archivo de idioma correspondiente.</exception>
        public string Traducir(string word)
        {
            try
            {
                string cultura = Thread.CurrentThread.CurrentCulture.Name;

                string localPath = $"{path}.{cultura}";

                using (StreamReader sr = new StreamReader(localPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                        {
                            string[] strings = line.Split('=');
                            string key = strings[0];
                            string value = strings[1];

                            if (key.ToLower() == word.ToLower())
                            {
                                return value;
                            }
                        }
                    }
                }
                throw new WordNotFoundException();
            }
            catch (Exception ex)
            {
                //Tratamiento de excepciones genéricas.
                Console.WriteLine(ex.Message);
                throw;
            }                  
        }


        /// <summary>
        /// Agrega una nueva clave (key) al archivo de idioma de la cultura actual, si esta no existe. 
        /// La agrega con el formato 'key=key'.
        /// </summary>
        /// <param name="key">La clave a agregar.</param>
        /// <remarks>
        /// Este método es thread-safe (usa un lock) y se utiliza para auto-poblar los archivos 
        /// de idioma con claves que faltan, facilitando el desarrollo.
        /// </remarks>
        public void AgregarDataKey(string key)
        {
            try
            {
                string cultura = Thread.CurrentThread.CurrentCulture.Name;
                string localPath = $"{path}.{cultura}";

                
                lock (_fileLock)
                {
                    if (File.Exists(localPath))
                    {
                        var lines = File.ReadAllLines(localPath);

                        if (lines.Any(line => line.Trim().StartsWith(key + "=", StringComparison.OrdinalIgnoreCase)))
                        {
                            return;
                        }
                    }
                    string newLine = $"{Environment.NewLine}{key}={key}";
                    File.AppendAllText(localPath, newLine);
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error al agregar la clave '{key}': {ex.Message}");
            }
        }

    }

}

using Services.Bll;
using Services.Dal.Interfaces;
using Services.DomainModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade
{
    /// <summary>
    /// Fachada estática (Facade) o Service Locator simple para
    /// obtener instancias pre-configuradas de <see cref="ILogger"/>.
    /// </summary>
    public class LoggerService
    {
        /// <summary>
        /// Obtiene una instancia de <see cref="ILogger"/> (un <see cref="FileLogger"/>)
        /// basada en una configuración específica.
        /// </summary>
        /// <returns>Una instancia de <see cref="ILogger"/> lista para usar.</returns>
        public static ILogger GetLogger()
        {
            var config = new LoggerConfiguration
            {
                LogFilePath = "Logs/mi_app.log",
                MinimumLogLevel = LogLevel.Debug 
            };

            return config.CreateFileLogger();
        }
    }
}

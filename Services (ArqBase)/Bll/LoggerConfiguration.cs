using Services.Dal.Implementations;
using Services.Dal.Interfaces;
using Services.DomainModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    /// <summary>
    /// Proporciona la configuración y un método de fábrica (Factory Method)
    /// para crear instancias de <see cref="ILogger"/>.
    /// </summary>
    /// <remarks>
    /// Esta clase permite centralizar la configuración del sistema de logging.
    /// </remarks>
    public class LoggerConfiguration
    {
        /// <summary>
        /// Obtiene o establece la ruta completa del archivo donde se guardarán los logs.
        /// Valor por defecto: "Logs/app.log".
        /// </summary>
        public string LogFilePath { get; set; } = "Logs/app.log"; //Por defecto
        /// <summary>
        /// Obtiene o establece el nivel mínimo de log que se registrará.
        /// Mensajes con un nivel inferior serán ignorados.
        /// Valor por defecto: <see cref="LogLevel.Information"/>.
        /// </summary>
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information; //Por defecto

        /// <summary>
        /// Método de fábrica (Factory Method) que crea una nueva instancia de <see cref="ILogger"/>
        /// (específicamente un <see cref="FileLogger"/>) utilizando la configuración actual.
        /// </summary>
        /// <returns>Una instancia de <see cref="ILogger"/> configurada.</returns>
        public ILogger CreateFileLogger()
        {
            return new FileLogger(LogFilePath, MinimumLogLevel);
        }
    }
}

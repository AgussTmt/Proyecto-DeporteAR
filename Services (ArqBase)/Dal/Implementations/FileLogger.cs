using Services.Dal.Interfaces;
using Services.DomainModel.Logging;
using System.IO;
using System;

/// <summary>
/// Implementación de <see cref="ILogger"/> que escribe logs en un archivo de texto.
/// Es thread-safe.
/// </summary>
public class FileLogger : ILogger, IDisposable
{
    private readonly string _logFilePath;
    private readonly LogLevel _minimumLogLevel;
    private readonly object _lockObject = new object(); // Para thread-safety
    private bool _disposed = false;


    /// <summary>
    /// Inicializa una nueva instancia de <see cref="FileLogger"/>.
    /// </summary>
    /// <param name="logFilePath">La ruta completa del archivo donde se guardarán los logs.</param>
    /// <param name="minimumLogLevel">El nivel mínimo de log que se registrará. Mensajes con un nivel inferior serán ignorados.</param>
    public FileLogger(string logFilePath, LogLevel minimumLogLevel)
    {
        _logFilePath = logFilePath;
        _minimumLogLevel = minimumLogLevel;

        string directoryPath = Path.GetDirectoryName(_logFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    /// <summary>
    /// Método central (privado) para escribir la entrada de log en el archivo.
    /// </summary>
    private void Log(LogLevel level, string message, Exception exception = null)
    {
        if (level < _minimumLogLevel) return;

        try
        {
            lock (_lockObject)
            {
                var logEntry = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Level = level,
                    Message = message,
                    Exception = exception
                };

                
                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine(logEntry.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            
            System.Diagnostics.Debug.WriteLine($"Error escribiendo log: {ex.Message}");
        }
    }
    /// <summary>
    /// Registra un mensaje de nivel Trace.
    /// </summary>
    public void Trace(string message) => Log(LogLevel.Trace, message);

    /// <summary>
    /// Registra un mensaje de nivel Trace con una excepción.
    /// </summary>
    public void Trace(string message, Exception exception) => Log(LogLevel.Trace, message, exception);

    /// <summary>
    /// Registra un mensaje de nivel Debug.
    /// </summary>
    public void Debug(string message) => Log(LogLevel.Debug, message);

    /// <summary>
    /// Registra un mensaje de nivel Debug con una excepción.
    /// </summary>
    public void Debug(string message, Exception exception) => Log(LogLevel.Debug, message, exception);

    /// <summary>
    /// Registra un mensaje de nivel Information.
    /// </summary>
    public void Information(string message) => Log(LogLevel.Information, message);

    /// <summary>
    /// Registra un mensaje de nivel Information con una excepción.
    /// </summary>
    public void Information(string message, Exception exception) => Log(LogLevel.Information, message, exception);

    /// <summary>
    /// Registra un mensaje de nivel Warning.
    /// </summary>
    public void Warning(string message) => Log(LogLevel.Warning, message);

    /// <summary>
    /// Registra un mensaje de nivel Warning con una excepción.
    /// </summary>
    public void Warning(string message, Exception exception) => Log(LogLevel.Warning, message, exception);

    /// <summary>
    /// Registra un mensaje de nivel Error.
    /// </summary>
    public void Error(string message) => Log(LogLevel.Error, message);

    /// <summary>
    /// Registra un mensaje de nivel Error con una excepción.
    /// </summary>
    public void Error(string message, Exception exception) => Log(LogLevel.Error, message, exception);

    /// <summary>
    /// Registra un mensaje de nivel Fatal.
    /// </summary>
    public void Fatal(string message) => Log(LogLevel.Fatal, message);

    /// <summary>
    /// Registra un mensaje de nivel Fatal con una excepción.
    /// </summary>
    public void Fatal(string message, Exception exception) => Log(LogLevel.Fatal, message, exception);


    /// <summary>
    /// Implementación del patrón Dispose para liberar recursos (aunque en este caso, se manejan con 'using' en cada 'Log').
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _disposed = true;
        }
    }

    /// <summary>
    /// Libera los recursos utilizados por el Logger.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
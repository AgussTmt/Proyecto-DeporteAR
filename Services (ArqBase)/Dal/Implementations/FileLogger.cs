using Services.Dal.Interfaces;
using Services.DomainModel.Logging;
using System.IO;
using System;

public class FileLogger : ILogger, IDisposable
{
    private readonly string _logFilePath;
    private readonly LogLevel _minimumLogLevel;
    private readonly object _lockObject = new object(); // Para thread-safety
    private bool _disposed = false;

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

    public void Trace(string message) => Log(LogLevel.Trace, message);
    public void Trace(string message, Exception exception) => Log(LogLevel.Trace, message, exception);
    public void Debug(string message) => Log(LogLevel.Debug, message);
    public void Debug(string message, Exception exception) => Log(LogLevel.Debug, message, exception);
    public void Information(string message) => Log(LogLevel.Information, message);
    public void Information(string message, Exception exception) => Log(LogLevel.Information, message, exception);
    public void Warning(string message) => Log(LogLevel.Warning, message);
    public void Warning(string message, Exception exception) => Log(LogLevel.Warning, message, exception);
    public void Error(string message) => Log(LogLevel.Error, message);
    public void Error(string message, Exception exception) => Log(LogLevel.Error, message, exception);
    public void Fatal(string message) => Log(LogLevel.Fatal, message);
    public void Fatal(string message, Exception exception) => Log(LogLevel.Fatal, message, exception);

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
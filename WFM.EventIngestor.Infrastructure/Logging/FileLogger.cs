using NLog;
using WFM.EventIngestor.Application.Common.Interfaces;

namespace WFM.EventIngestor.Infrastructure.Logging
{
    public class FileLogger : IAppLogger
    {       
        private readonly Logger _logger;

        public FileLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }        
                
        public Task LogAsync(
            Domain.Enums.ObjectResultEnum.LogLevel logLevel,
            string logger,
            string message,
            string? exception = null,
            string? callsite = null,
            string? thread = null,
            string? username = null,
            string? properties = null)
        {
            var logEvent = new LogEventInfo
            {
                LoggerName = logger,
                Message = message,
                Level = MapLevel(logLevel),
                Exception = exception != null ? new System.Exception(exception) : null
            };

            // Propiedades personalizadas para el layout
            logEvent.Properties["CallSite"] = callsite;
            logEvent.Properties["Thread"] = thread;
            logEvent.Properties["UserName"] = username;
            logEvent.Properties["Properties"] = properties;

            _logger.Log(logEvent);
            return Task.CompletedTask;
        }
       private NLog.LogLevel MapLevel(Domain.Enums.ObjectResultEnum.LogLevel level) => // Fully qualify LogLevel here as well
              level switch
              {
                  Domain.Enums.ObjectResultEnum.LogLevel.Trace => NLog.LogLevel.Trace,
                  Domain.Enums.ObjectResultEnum.LogLevel.Debug => NLog.LogLevel.Debug,
                  Domain.Enums.ObjectResultEnum.LogLevel.Info => NLog.LogLevel.Info,
                  Domain.Enums.ObjectResultEnum.LogLevel.Warn => NLog.LogLevel.Warn,
                  Domain.Enums.ObjectResultEnum.LogLevel.Error => NLog.LogLevel.Error,
                  Domain.Enums.ObjectResultEnum.LogLevel.Fatal => NLog.LogLevel.Fatal,
                  _ => NLog.LogLevel.Info
              };
    }
}
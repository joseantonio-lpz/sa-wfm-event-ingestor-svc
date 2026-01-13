using Microsoft.Extensions.Options;
using WFM.EventIngestor.Application.Common.Interfaces;
using WFM.EventIngestor.Infrastructure.Configuration;
using static WFM.EventIngestor.Domain.Enums.ObjectResultEnum;

namespace WFM.EventIngestor.Infrastructure.Logging{
    public class CompositeLogger : IAppLogger
    {
        private readonly DbLogger _dbLogger;
        private readonly FileLogger _fileLogger;
        private readonly CustomLoggingSettings _settings;

        public CompositeLogger(FileLogger fileLogger,DbLogger dbLogger,IOptions<CustomLoggingSettings> settings)
        {
            _fileLogger = fileLogger;
            _dbLogger = dbLogger;            
            _settings = settings.Value;
        }

        public async Task LogAsync(
            LogLevel logLevel,
            string logger,
            string message,
            string? exception = null,
            string? callsite = null,
            string? thread = null,
            string? username = null,
            string? properties = null)
        {
            var tasks = new List<Task>();  
            if (_settings.LogToDatabase)
                tasks.Add(_dbLogger.LogAsync(logLevel, logger, message, exception, callsite, thread, username, properties));
            if (_settings.LogToFile)
                tasks.Add(_fileLogger.LogAsync(logLevel, logger, message, exception, callsite, thread, username, properties));                
            await Task.WhenAll(tasks);
        }

    }
}
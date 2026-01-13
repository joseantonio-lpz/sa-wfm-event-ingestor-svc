using WFM.EventIngestor.Application.Common.Interfaces;
using WFM.EventIngestor.Application.Interfaces;
using static WFM.EventIngestor.Domain.Enums.ObjectResultEnum;

namespace WFM.EventIngestor.Infrastructure.Logging
{
    public class DbLogger : IAppLogger
    {
         private readonly ILogRepository _logRepository;

        public DbLogger(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }        
       
        public Task LogAsync(LogLevel logLevel, string logger, string message, string? exception = null, string? callsite = null, string? thread = null, string? username = null, string? properties = null)
        {
            // Database logging implementation
            return _logRepository.SaveLogEntryAsync(logLevel, logger, message, exception, callsite, thread, username, properties);
        }
    }
}
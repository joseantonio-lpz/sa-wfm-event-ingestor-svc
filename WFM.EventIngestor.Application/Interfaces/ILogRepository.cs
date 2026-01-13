using WFM.EventIngestor.Application.Common.Models;
using static WFM.EventIngestor.Domain.Enums.ObjectResultEnum;

namespace WFM.EventIngestor.Application.Interfaces
{
    public interface ILogRepository
    {
        Task SaveLogEntryAsync(LogLevel logLevel,
            string logger,
            string message,
            string? exception = null,
            string? callsite = null,
            string? thread = null,
            string? username = null,
            string? properties = null);
    }
}

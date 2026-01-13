using static WFM.EventIngestor.Domain.Enums.ObjectResultEnum;

namespace WFM.EventIngestor.Application.Common.Interfaces
{
    public interface IAppLogger
    {
        Task LogAsync(
            LogLevel logLevel,
            string logger,
            string message,
            string? exception = null,
            string? callsite = null,
            string? thread = null,
            string? username = null,
            string? properties = null);
    }
}
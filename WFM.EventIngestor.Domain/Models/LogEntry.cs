namespace WFM.EventIngestor.Domain.Models
{
    public class LogEntry
    {
        public string Source { get; set; } = string.Empty;
        public string LogLevel { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
    }
}

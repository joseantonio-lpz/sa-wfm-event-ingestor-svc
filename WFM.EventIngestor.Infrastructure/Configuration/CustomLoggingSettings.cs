namespace WFM.EventIngestor.Infrastructure.Configuration
{
    public class CustomLoggingSettings
    {
        public bool LogToDatabase { get; set; } = true;
        public bool LogToFile { get; set; } = false;
    }
}
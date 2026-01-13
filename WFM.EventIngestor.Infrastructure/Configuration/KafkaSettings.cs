namespace WFM.EventIngestor.Infrastructure.Configuration
{
    public  class KafkaSettings
    {
        public string Hostname { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public KafkaTopics Topics { get; set; } = new();
    }

    public class KafkaTopics
    {
        public string SocialContent { get; set; } = default!;
        public string SinergiaFlow { get; set; } = default!;
        public string AdsaStore { get; set; } = default!;
        public string ExceptionFirma { get; set; } = default!;
    }
}
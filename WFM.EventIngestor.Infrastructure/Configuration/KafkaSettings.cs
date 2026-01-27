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
        public string TaskRejected { get; set; } = default!;
        public string EntityCreated { get; set; } = default!;
        public string FormSubmit { get; set; } = default!;
        public string TaskStarted { get; set; } = default!;
        public string TaskCompleted { get; set; } = default!;
        public string TaskAssigned { get; set; } = default!;
    }
}
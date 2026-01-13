
namespace WFM.EventIngestor.Infrastructure.Kafka
{
    using Confluent.Kafka;
    using Microsoft.Extensions.Options;
    using System.Text.Json;
    using WFM.EventIngestor.Application.Common.Interfaces;
    using WFM.EventIngestor.Application.Common.Models;
    using WFM.EventIngestor.Application.Interfaces;
    using WFM.EventIngestor.Domain.Enums;
    using WFM.EventIngestor.Infrastructure.Configuration;

    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly KafkaSettings _kafkaSettings;
        private readonly IAppLogger _logger;
        public KafkaProducerService(IOptions<KafkaSettings> kafkaSettings, IAppLogger logger)
        {
            _kafkaSettings = kafkaSettings.Value;
            _logger = logger;
        }

        public async Task<Result<string>> SendMessageAsync<T>(string topic, T message)
        {
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = $"{_kafkaSettings.Hostname}:{_kafkaSettings.Port}"
                };

                using var _producer = new ProducerBuilder<string, string>(config)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .Build();

                var kafkaMessage = new Message<string, string>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = JsonSerializer.Serialize(message)
                };

                var deliveryResult = await _producer.ProduceAsync(topic, kafkaMessage);
                var persisted = deliveryResult.Status == PersistenceStatus.Persisted;
                var _message = persisted
                    ? $"Mensaje enviado a {deliveryResult.TopicPartitionOffset}"
                    : $"Mensaje no persistido. Estado: {deliveryResult.Status}, por la siguiente razon: {deliveryResult.Message}";

                await _logger.LogAsync(
                    persisted ? ObjectResultEnum.LogLevel.Info : ObjectResultEnum.LogLevel.Error,
                    "KafkaProducerService",
                    _message,
                    username: "kafka",
                    callsite: "SendMessageAsync",
                    thread: Thread.CurrentThread.ManagedThreadId.ToString()
                );

                return persisted ? Result<string>.Ok(_message) : Result<string>.Fail(_message);               
            }
            catch (ProduceException<string, string> ex)
            {
                 return Result<string>.Fail($"Error al enviar mensaje: {ex.Error.Reason}");
            }
            catch (Exception ex)
            {
                 return Result<string>.Fail($"Mensaje no persistido. Estado: {ex.Message}");
            }
        }
    }
}
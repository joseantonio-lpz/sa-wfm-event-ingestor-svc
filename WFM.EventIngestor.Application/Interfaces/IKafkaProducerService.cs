using WFM.EventIngestor.Application.Common.Models;

namespace WFM.EventIngestor.Application.Interfaces
{
    public interface IKafkaProducerService
    {
        Task<Result<string>> SendMessageAsync<T>(string topic, T message);
    }
}
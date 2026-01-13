using System.ComponentModel.DataAnnotations;

namespace WFM.EventIngestor.Application.DTOs
{
    public class SinergiaFlowMessage
    {
        [Required(ErrorMessage = "El campo 'AccountId' es obligatorio."), Range(1, long.MaxValue, ErrorMessage = "El campo 'AccountId' debe ser un número valido.")]
        public long AccountId { get; set; } = default!;
        [Required(ErrorMessage = "El campo 'SubscriptionId' es obligatorio."), Range(1, long.MaxValue, ErrorMessage = "El campo 'SubscriptionId' debe ser un número valido.")]
        public long SubscriptionId { get; set; } = default!;
    }
}
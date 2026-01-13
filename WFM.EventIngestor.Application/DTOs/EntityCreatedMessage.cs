using System.ComponentModel.DataAnnotations;

namespace WFM.EventIngestor.Application.DTOs
{
    public class EntityCreatedMessage
    {
        [Required(ErrorMessage = "El campo 'EntityId' es obligatorio.")]
        public string EntityId { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'ProductCode' es obligatorio.")]
        public string ProductCode { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'ExternalId' es obligatorio.")]       
        public string ExternalId { get; set; } = default!;
    }
}
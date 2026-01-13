using System.ComponentModel.DataAnnotations;

namespace WFM.EventIngestor.Application.DTOs
{
    public class TaskRejectedMessage
    {
        [Required(ErrorMessage = "El campo 'EntityId' es obligatorio.")]
        public string EntityId { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'ProductCode' es obligatorio.")]
        public string ProductCode { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'TaskCode' es obligatorio.")]
        public string TaskCode { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'FlowCode' es obligatorio.")]
        public string FlowCode { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'Date' es obligatorio.")]
        public string Date { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'UserId' es obligatorio.")]
        public string UserId { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'Reject_reason' es obligatorio.")]
        public string Reject_reason { get; set; } = default!;

    }
}   
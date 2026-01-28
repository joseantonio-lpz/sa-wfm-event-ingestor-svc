using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WFM.EventIngestor.Application.DTOs
{
    public class TaskAssignedMessage
    {
        [Required(ErrorMessage = "El campo 'entity_id' es obligatorio.")]
        public string EntityId { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'external_id' es obligatorio.")]
        public string ExternalId { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'product_code' es obligatorio.")]
        public string ProductCode { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'task_code' es obligatorio.")]
        public string TaskCode { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'flow_code' es obligatorio.")]
        public string FlowCode { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'flow_instance_id' es obligatorio.")]
        public string FlowInstanceId { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'date' es obligatorio.")]
        public string Date { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'user_id' es obligatorio.")]
        public string UserId { get; set; } = default!;
    }
}

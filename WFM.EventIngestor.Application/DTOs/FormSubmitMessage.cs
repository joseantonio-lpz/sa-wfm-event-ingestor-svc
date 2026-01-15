using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFM.EventIngestor.Application.DTOs
{
    public class FormSubmitMessage
    {
        [Required(ErrorMessage = "El campo 'ResponseId' es obligatorio.")]
        public string ResponseId { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'ExternalId' es obligatorio.")]
        public string ExternalId { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'FormCode' es obligatorio.")]
        public string FormCode { get; set; } = default!;

        [Required(ErrorMessage = "El campo 'Token' es obligatorio.")]
        public string Token { get; set; } = default!;
    }
}

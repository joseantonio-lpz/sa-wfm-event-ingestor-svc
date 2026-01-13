using System.ComponentModel.DataAnnotations;

namespace WFM.EventIngestor.Application.DTOs
{
    public class ExceptionFirmaMessage
    {
        [Required(ErrorMessage = "El campo 'AdvertiserId' es obligatorio."), Range(1, long.MaxValue, ErrorMessage = "El campo 'AdvertiserId' debe ser un número valido.")]
        public long AdvertiserId { get; set; } = default!;
        [Required(ErrorMessage = "El campo 'BcProductId' es obligatorio."), Range(1, long.MaxValue, ErrorMessage = "El campo 'BcProductId' debe ser un número valido.")]
        public long BcProductId { get; set; } = default!;
        [Required(ErrorMessage = "El campo 'Contrato' es obligatorio."), MaxLength(11, ErrorMessage = "El campo 'Contrato' no puede tener más de 11 caracteres.")]
        public string Contrato { get; set; } = default!;
    }
}
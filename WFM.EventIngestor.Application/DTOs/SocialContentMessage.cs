using System.ComponentModel.DataAnnotations;

namespace WFM.EventIngestor.Application.DTOs
{
    public class SocialContentMessage
    {
        [Required(ErrorMessage = "El campo 'AdvertiserId' es obligatorio."), Range(1, long.MaxValue, ErrorMessage = "El campo 'AdvertiserId' debe ser un número valido.")]
        public long AdvertiserId { get; set; } = default!;
        [Required(ErrorMessage = "El campo 'BcProductId' es obligatorio."), Range(1, long.MaxValue, ErrorMessage = "El campo 'BcProductId' debe ser un número valido")]
        public long BcProductId { get; set; } = default!;
        [Required(ErrorMessage = "El campo 'Folio' es obligatorio."), MaxLength(11, ErrorMessage = "El campo 'Folio' no puede tener más de 11 caracteres.")]
        public string Folio { get; set; } = default!;
        [Required(ErrorMessage = "El campo 'IdScheduleIc' es obligatorio."), Range(1, long.MaxValue, ErrorMessage = "El campo 'IdScheduleIc' debe ser un número valido.")]
        public long IdScheduleIc { get; set; } = default!;
        [Required(ErrorMessage = "El campo 'L5FamilyId' es obligatorio."), Range(1, long.MaxValue, ErrorMessage = "El campo 'L5FamilyId' debe ser un número valido.")]   
        public long L5FamilyId { get; set; } = default!;
    }
}   
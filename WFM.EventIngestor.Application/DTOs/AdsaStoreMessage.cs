using System.ComponentModel.DataAnnotations;

namespace WFM.EventIngestor.Application.DTOs
{
    public class AdsaStoreMessage
    {
        [Required(ErrorMessage = "El campo 'NumPedidoAts' es obligatorio."), Range(1, long.MaxValue, ErrorMessage = "El campo 'NumPedidoAts' debe ser un número valido.")]
        public long NumPedidoAts { get; set; } = default!;
    }
}
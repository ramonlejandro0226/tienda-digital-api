using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.CreatedOrderDTO
{
    public class CreatedOrderDTO
    {
        [Required]
        public OrderDetailCreatedOrderDTO OrderDetail { get; set; } = null!;
    }
}

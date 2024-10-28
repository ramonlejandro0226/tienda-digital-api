using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.CreatedOrderDTO
{
    public class OrderProductCreatedOrderDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be at least 1.")]
        public int UserSelectedQuantity { get; set; }

        [Required]
        public ProductCreatedOrderDTO Product { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.CreatedOrderDTO
{
    public class OrderDetailCreatedOrderDTO
    {
        [Required]
        public StatusCreatedOrderDTO Status { get; set; } = null!;

        [Required]
        public ICollection<OrderProductCreatedOrderDTO> OrderProducts { get; set; } = new List<OrderProductCreatedOrderDTO>();

        [Required]
        public PaymentMethodCreatedOrderDTO PaymentMethod { get; set; } = null!;
    }
}

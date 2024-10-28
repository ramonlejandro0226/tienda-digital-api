using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.OrderDTO
{
    public class OrderProductOrderDTO
    {
        [Key]
        public Guid Id { get; set; }
        public int UserSelectedQuantity { get; set; }

        public ProductOrderDTO Product { get; set; } = null!;
    }
}

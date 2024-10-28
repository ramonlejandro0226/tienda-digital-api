using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.OrderDTO
{
    public class OrderDetailOrderDTO
    {
        [Key]
        public Guid Id { get; set; }
        public StatusOrderDTO Status { get; set; } = null!;

        public double Total { get; set; }

        public ICollection<OrderProductOrderDTO> OrderProducts { get; set; } = new List<OrderProductOrderDTO>();

        public PaymentMethodOrderDTO PaymentMethod { get; set; } = null!;
    }
}

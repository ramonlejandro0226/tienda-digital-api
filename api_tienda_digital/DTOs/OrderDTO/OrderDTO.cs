
using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.OrderDTO
{
    public class OrderDTO
    {
        [Key]
        public Guid Id { get; set; }
        public OrderDetailOrderDTO OrderDetail { get; set; } = null!;

        public UserExtendDTO.UserExtendDTO UserExtend { get; set; } = null!;
    }
}

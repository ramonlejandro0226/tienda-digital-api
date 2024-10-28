using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.CreatedOrderDTO
{
    public class PaymentMethodCreatedOrderDTO
    {
        [Key]
        public Guid Id { get; set; }
    }
}

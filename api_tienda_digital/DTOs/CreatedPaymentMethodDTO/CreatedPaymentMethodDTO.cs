using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.CreatedPaymentMethodDTO
{
    public class CreatedPaymentMethodDTO
    {
        [StringLength(50, ErrorMessage = "The description cannot exceed 50 characters.")]
        public string Name { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.UpdatedPaymentMethodDTO
{
    public class UpdatedPaymentMethodDTO
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessage = "The description cannot exceed 50 characters.")]
        public string Name { get; set; } = null!;
    }
}

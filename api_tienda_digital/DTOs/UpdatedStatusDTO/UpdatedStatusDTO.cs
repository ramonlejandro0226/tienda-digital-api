using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.Context.UpdatedStatusOrderDTO
{
    public class UpdatedStatusDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The description cannot exceed 50 characters.")]
        public string Name { get; set; } = null!;
    }
}

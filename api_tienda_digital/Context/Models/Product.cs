using api_tienda_digital.Context.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.Context.Models
{
    public partial class Product : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The product name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string Description { get; set; } = null!;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "UserSelectedQuantity cannot be negative.")]
        public int Quantity { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The URL cannot exceed 255 characters.")]
        public string UrlImage { get; set; } = null!;
    }
}

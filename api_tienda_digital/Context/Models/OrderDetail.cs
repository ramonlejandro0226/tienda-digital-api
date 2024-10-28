using api_tienda_digital.Context.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.Context.Models
{
    public class OrderDetail : BaseEntity
    {
        [Required]
        public Status Status { get; set; } = null!;

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Total must be a positive value.")]
        public double Total { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        [Required]
        public PaymentMethod PaymentMethod { get; set; } = null!;
    }
}

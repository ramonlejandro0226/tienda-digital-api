using api_tienda_digital.Context.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.Context.Models;

public partial class PaymentMethod : BaseEntity
{
    [StringLength(50, ErrorMessage = "The description cannot exceed 50 characters.")]
    public string Name { get; set; } = null!;
}

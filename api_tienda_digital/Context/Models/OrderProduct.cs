using api_tienda_digital.Context.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using api_tienda_digital.Context.Models.Common;

public class OrderProduct: BaseEntity
{
    [Required]
    public Product Product { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The quantity must be at least 1.")]
    public int UserSelectedQuantity { get; set; }
}

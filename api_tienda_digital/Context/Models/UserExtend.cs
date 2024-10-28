using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.Context.Models;

public partial class UserExtend : IdentityUser
{

    [Required]
    [DataType(DataType.Date)]
    public DateTime UpdatedDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }

    [StringLength(50, ErrorMessage = "The description cannot exceed 50 characters.")]
    public string? City { get; set; }

    [StringLength(50, ErrorMessage = "The description cannot exceed 50 characters.")]
    public string? State { get; set; }

    [StringLength(10, ErrorMessage = "The description cannot exceed 10 characters.")]
    public string? PostalCode { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;
}

using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.Context.CreatedUserExtendDTO;

public partial class CreatedUserExtendDTO
{
    [Required(ErrorMessage = "The UserName field is required.")]
    [StringLength(100, ErrorMessage = "The UserName cannot exceed 100 characters.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The PhoneNumber field is required.")]
    [Phone(ErrorMessage = "The PhoneNumber field is not a valid phone number.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "The Password field is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "The Password must be at least 6 characters long and cannot exceed 100 characters.")]
    public string Password { get; set; }

    [StringLength(10, ErrorMessage = "The PostalCode cannot exceed 10 characters.")]
    public string Role { get; set; }

    [StringLength(50, ErrorMessage = "The City cannot exceed 50 characters.")]
    public string? City { get; set; }

    [StringLength(50, ErrorMessage = "The State cannot exceed 50 characters.")]
    public string? State { get; set; }

    [StringLength(10, ErrorMessage = "The PostalCode cannot exceed 10 characters.")]
    public string? PostalCode { get; set; }
}

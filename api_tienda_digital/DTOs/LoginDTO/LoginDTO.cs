using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.LoginDTO
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}

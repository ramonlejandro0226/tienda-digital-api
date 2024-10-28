using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.UserExtendDTO
{
    public class UserExtendDTO
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
    }
}
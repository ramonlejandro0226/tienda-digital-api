using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.CreatedOrderDTO
{
    public class StatusCreatedOrderDTO
    {
        [Key]
        public Guid Id { get; set; }
    }
}

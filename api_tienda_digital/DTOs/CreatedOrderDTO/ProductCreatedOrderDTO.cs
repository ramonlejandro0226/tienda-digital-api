using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.DTOs.CreatedOrderDTO
{
    public class ProductCreatedOrderDTO
    {
        [Key]
        public Guid Id { get; set; }
    }
}

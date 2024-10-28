namespace api_tienda_digital.DTOs.OrderDTO
{
    public class ProductOrderDTO
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double Price { get; set; }
    }
}

using api_tienda_digital.Context.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace api_tienda_digital.Context.Models
{
    public partial class Order : BaseEntity
    {

        [Required]
        public UserExtend UserExtend { get; set; } = null!;

        [Required]
        public OrderDetail OrderDetail { get; set; } = null!;
    }
}

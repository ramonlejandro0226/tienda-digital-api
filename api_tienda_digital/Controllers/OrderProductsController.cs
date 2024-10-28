using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_tienda_digital.Context;
using AutoMapper;
using api_tienda_digital.DTOs.OrderDTO;
using Microsoft.AspNetCore.Authorization;

namespace api_tienda_digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductsController : ControllerBase
    {
        private readonly TiendaDigitalContext _context;
        private readonly IMapper _mapper;

        public OrderProductsController(TiendaDigitalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OrderProducts
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public async Task<ActionResult<ICollection<OrderProductOrderDTO>>> GetOrderProduct()
        {
            var orderProducts = await _context.OrderProducts
                .Include(x => x.Product)
                .ToListAsync();

            var orderProductDTOs = _mapper.Map<ICollection<OrderProductOrderDTO>>(orderProducts);

            return Ok(orderProductDTOs);
        }

        // GET: api/OrderProducts/5
        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProductOrderDTO>> GetOrderProduct(Guid id)
        {
            var orderProduct = await _context.OrderProducts
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (orderProduct == null)
            {
                return NotFound();
            }

            var orderProductDTO = _mapper.Map<OrderProductOrderDTO>(orderProduct);

            return Ok(orderProductDTO);
        }

    }
}

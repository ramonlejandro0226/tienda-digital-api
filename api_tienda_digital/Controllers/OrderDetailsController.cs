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
    public class OrderDetailsController : ControllerBase
    {
        private readonly TiendaDigitalContext _context;
        private readonly IMapper _mapper;

        public OrderDetailsController(TiendaDigitalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OrderDetails
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailOrderDTO>>> GetOrderDetails()
        {
            var orderDetails = await _context.OrderDetails
                .Include(d => d.OrderProducts).ThenInclude(op => op.Product)
                .Include(d => d.PaymentMethod)
                .Include(d => d.Status)
                .ToListAsync();

            var orderDetailDTOs = _mapper.Map<IEnumerable<OrderDetailOrderDTO>>(orderDetails);

            return Ok(orderDetailDTOs);
        }

        // GET: api/OrderDetails/5
        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailOrderDTO>> GetOrderDetail(Guid id)
        {
            var orderDetail = await _context.OrderDetails
                .Include(d => d.OrderProducts).ThenInclude(op => op.Product)
                .Include(d => d.PaymentMethod)
                .Include(d => d.Status)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            var orderDetailDTO = _mapper.Map<OrderDetailOrderDTO>(orderDetail);

            return Ok(orderDetailDTO);
        }

    }
}

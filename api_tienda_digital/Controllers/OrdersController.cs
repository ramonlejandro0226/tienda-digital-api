using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_tienda_digital.Context.Models;
using api_tienda_digital.Context;
using Microsoft.AspNetCore.Identity;
using api_tienda_digital.DTOs.CreatedOrderDTO;
using AutoMapper;
using api_tienda_digital.DTOs.OrderDTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace api_tienda_digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TiendaDigitalContext _context;
        private readonly UserManager<UserExtend> _userManager;
        private readonly IMapper _mapper;

        public OrdersController(TiendaDigitalContext context, UserManager<UserExtend> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: api/Orders
        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetail)
                .Include(o => o.OrderDetail.Status)
                .Include(o => o.OrderDetail.PaymentMethod)
                .Include(o => o.OrderDetail.OrderProducts).ThenInclude(op => op.Product)
                .Include(o => o.UserExtend)
                .ToListAsync();

            var orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);

            return Ok(orderDTOs);
        }

        // GET: api/Orders/5
        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetail)
                .Include(o => o.OrderDetail.Status)
                .Include(o => o.OrderDetail.PaymentMethod)
                .Include(o => o.OrderDetail.OrderProducts).ThenInclude(op => op.Product)
                .Include(o => o.UserExtend)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDTO = _mapper.Map<OrderDTO>(order);

            return Ok(orderDTO);
        }

        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public async Task<ActionResult<OrderDTO>> PostOrder(CreatedOrderDTO createdOrderDTO)
        {
            double total = 0.0;
            List<OrderProduct> orderProducts = new List<OrderProduct>();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized($"User with ID {userId} does not exist.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Unauthorized($"User with ID {userId} does not exist.");
            }

            var order = _mapper.Map<Order>(createdOrderDTO);
            order.UserExtend = user;

            if (order.OrderDetail == null)
            {
                return BadRequest("Order detail is missing.");
            }

            if (order.OrderDetail.Status == null)
            {
                return BadRequest("Order status is missing.");
            }

            var status = await _context.Status.AsNoTracking().FirstOrDefaultAsync(x => x.Id == order.OrderDetail.Status.Id);
            if (status == null)
            {
                return BadRequest($"Status with ID {order.OrderDetail.Status.Id} does not exist.");
            }

            _context.Attach(status);
            order.OrderDetail.Status = status;

            if (order.OrderDetail.PaymentMethod == null)
            {
                return BadRequest("Payment method is missing.");
            }

            var paymentMethod = await _context.PaymentMethods.AsNoTracking().FirstOrDefaultAsync(x => x.Id == order.OrderDetail.PaymentMethod.Id);
            if (paymentMethod == null)
            {
                return BadRequest($"PaymentMethod with ID {order.OrderDetail.PaymentMethod.Id} does not exist.");
            }

            _context.Attach(paymentMethod);
            order.OrderDetail.PaymentMethod = paymentMethod;

            DateTime createdDate = DateTime.Now;
            DateTime updatedDate = createdDate;
            order.CreatedDate = createdDate;
            order.UpdatedDate = updatedDate;
            order.OrderDetail.CreatedDate = createdDate;
            order.OrderDetail.UpdatedDate = updatedDate;

            if (order.OrderDetail.OrderProducts == null || !order.OrderDetail.OrderProducts.Any())
            {
                return BadRequest("Order must contain at least one product.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            foreach (var orderProduct in order.OrderDetail.OrderProducts)
            {
                if (orderProduct.Product == null)
                {
                    return BadRequest("Order product is missing.");
                }

                Product? product = await _context.Products.FirstOrDefaultAsync(x => x.Id == orderProduct.Product.Id);
                if (product == null)
                {
                    return BadRequest($"Product with ID {orderProduct.Product.Id} does not exist.");
                }

                if (orderProduct.UserSelectedQuantity > product.Quantity)
                {
                    return BadRequest($"The selected quantity for product {product.Name} exceeds available stock. Available stock: {product.Quantity}.");
                }

                product.Quantity -= orderProduct.UserSelectedQuantity;

                _context.Products.Update(product);

                orderProduct.Product = product;
                orderProduct.CreatedDate = createdDate;
                orderProduct.UpdatedDate = updatedDate;

                total += orderProduct.UserSelectedQuantity * orderProduct.Product.Price;
            }

            order.OrderDetail.Total = total;

            _context.OrderDetails.Add(order.OrderDetail);
            _context.Orders.Add(order);

            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            var orderDTO = _mapper.Map<OrderDTO>(order);

            return CreatedAtAction("GetOrder", new { id = order.Id }, orderDTO);
        }


    }
}

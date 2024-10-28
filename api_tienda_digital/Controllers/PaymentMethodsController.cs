using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_tienda_digital.Context.Models;
using api_tienda_digital.Context;
using AutoMapper;
using api_tienda_digital.DTOs.CreatedPaymentMethodDTO;
using api_tienda_digital.DTOs.UpdatedPaymentMethodDTO;
using Microsoft.AspNetCore.Authorization;

namespace api_tienda_digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly TiendaDigitalContext _context;
        private readonly IMapper _mapper;

        public PaymentMethodsController(TiendaDigitalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/PaymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        // GET: api/PaymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(Guid id)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return paymentMethod;
        }

        // PUT: api/PaymentMethods/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentMethod(Guid id, UpdatedPaymentMethodDTO updatedPaymentMethodDTO)
        {
            if (id != updatedPaymentMethodDTO.Id)
            {
                return BadRequest();
            }

            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedPaymentMethodDTO, paymentMethod);
            paymentMethod.UpdatedDate = DateTime.Now;

            _context.Entry(paymentMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentMethodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PaymentMethods
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<CreatedPaymentMethodDTO>> PostPaymentMethod(CreatedPaymentMethodDTO createdPaymentMethodDTO)
        {
            var paymentMethod = _mapper.Map<PaymentMethod>(createdPaymentMethodDTO);
            paymentMethod.CreatedDate = DateTime.Now;
            paymentMethod.UpdatedDate = DateTime.Now;

            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.Id }, paymentMethod);
        }

        // DELETE: api/PaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(Guid id)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentMethodExists(Guid id)
        {
            return _context.PaymentMethods.Any(e => e.Id == id);
        }
    }
}

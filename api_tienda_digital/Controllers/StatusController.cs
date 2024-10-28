using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_tienda_digital.Context.Models;
using api_tienda_digital.Context;
using AutoMapper;
using api_tienda_digital.Context.UpdatedStatusOrderDTO;
using api_tienda_digital.Context.CreatedStatusDTO;
using Microsoft.AspNetCore.Authorization;

namespace api_tienda_digital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly TiendaDigitalContext _context;
        private readonly IMapper _mapper; 

        public StatusController(TiendaDigitalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            return await _context.Status.ToListAsync();
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(Guid id)
        {
            var status = await _context.Status.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        // PUT: api/Status/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(Guid id, UpdatedStatusDTO updatedStatusDTO)
        {
            var status = await _context.Status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedStatusDTO, status);
            status.UpdatedDate = DateTime.Now;

            if (id != status.Id)
            {
                return BadRequest();
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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

        // POST: api/Status
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<CreatedStatusDTO>> PostStatus(CreatedStatusDTO createdStatusDTO)
        {
            var status = _mapper.Map<Status>(createdStatusDTO);
            status.CreatedDate = DateTime.Now;
            status.UpdatedDate = DateTime.Now;

            _context.Status.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = status.Id }, status);
        }

        // DELETE: api/Status/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            var status = await _context.Status.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.Status.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(Guid id)
        {
            return _context.Status.Any(e => e.Id == id);
        }
    }
}

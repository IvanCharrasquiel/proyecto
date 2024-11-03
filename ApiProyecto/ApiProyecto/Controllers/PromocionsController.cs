using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB;

namespace ApiProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PromocionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Promocions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promocion>>> GetOPromocion()
        {
            return await _context.OPromocion.ToListAsync();
        }

        // GET: api/Promocions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Promocion>> GetPromocion(int id)
        {
            var promocion = await _context.OPromocion.FindAsync(id);

            if (promocion == null)
            {
                return NotFound();
            }

            return promocion;
        }

        // PUT: api/Promocions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromocion(int id, Promocion promocion)
        {
            if (id != promocion.IdPromocion)
            {
                return BadRequest();
            }

            _context.Entry(promocion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromocionExists(id))
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

        // POST: api/Promocions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Promocion>> PostPromocion(Promocion promocion)
        {
            _context.OPromocion.Add(promocion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPromocion", new { id = promocion.IdPromocion }, promocion);
        }

        // DELETE: api/Promocions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromocion(int id)
        {
            var promocion = await _context.OPromocion.FindAsync(id);
            if (promocion == null)
            {
                return NotFound();
            }

            _context.OPromocion.Remove(promocion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PromocionExists(int id)
        {
            return _context.OPromocion.Any(e => e.IdPromocion == id);
        }
    }
}

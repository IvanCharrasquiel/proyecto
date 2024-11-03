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
    public class PagoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PagoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Pagoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetOPago()
        {
            return await _context.OPago.ToListAsync();
        }

        // GET: api/Pagoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.OPago.FindAsync(id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        // PUT: api/Pagoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.IdPago)
            {
                return BadRequest();
            }

            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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

        // POST: api/Pagoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            _context.OPago.Add(pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPago", new { id = pago.IdPago }, pago);
        }

        // DELETE: api/Pagoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.OPago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.OPago.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagoExists(int id)
        {
            return _context.OPago.Any(e => e.IdPago == id);
        }
    }
}

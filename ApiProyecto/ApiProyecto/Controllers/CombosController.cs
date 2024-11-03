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
    public class CombosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CombosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Combos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Combos>>> GetOCombos()
        {
            return await _context.OCombos.ToListAsync();
        }

        // GET: api/Combos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Combos>> GetCombos(int id)
        {
            var combos = await _context.OCombos.FindAsync(id);

            if (combos == null)
            {
                return NotFound();
            }

            return combos;
        }

        // PUT: api/Combos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCombos(int id, Combos combos)
        {
            if (id != combos.IdCombos)
            {
                return BadRequest();
            }

            _context.Entry(combos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CombosExists(id))
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

        // POST: api/Combos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Combos>> PostCombos(Combos combos)
        {
            _context.OCombos.Add(combos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCombos", new { id = combos.IdCombos }, combos);
        }

        // DELETE: api/Combos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombos(int id)
        {
            var combos = await _context.OCombos.FindAsync(id);
            if (combos == null)
            {
                return NotFound();
            }

            _context.OCombos.Remove(combos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CombosExists(int id)
        {
            return _context.OCombos.Any(e => e.IdCombos == id);
        }
    }
}

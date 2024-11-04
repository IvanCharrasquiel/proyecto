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
    public class ComboesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComboesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Comboes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Combo>>> GetCombo()
        {
            return await _context.Combo.ToListAsync();
        }

        // GET: api/Comboes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Combo>> GetCombo(int id)
        {
            var combo = await _context.Combo.FindAsync(id);

            if (combo == null)
            {
                return NotFound();
            }

            return combo;
        }

        // PUT: api/Comboes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCombo(int id, Combo combo)
        {
            if (id != combo.IdCombo)
            {
                return BadRequest();
            }

            _context.Entry(combo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComboExists(id))
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

        // POST: api/Comboes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Combo>> PostCombo(Combo combo)
        {
            _context.Combo.Add(combo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ComboExists(combo.IdCombo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCombo", new { id = combo.IdCombo }, combo);
        }

        // DELETE: api/Comboes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombo(int id)
        {
            var combo = await _context.Combo.FindAsync(id);
            if (combo == null)
            {
                return NotFound();
            }

            _context.Combo.Remove(combo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComboExists(int id)
        {
            return _context.Combo.Any(e => e.IdCombo == id);
        }
    }
}

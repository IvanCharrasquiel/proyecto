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
    public class ComboServiciosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComboServiciosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ComboServicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComboServicio>>> GetComboServicio()
        {
            return await _context.ComboServicio.ToListAsync();
        }

        // GET: api/ComboServicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComboServicio>> GetComboServicio(int id)
        {
            var comboServicio = await _context.ComboServicio.FindAsync(id);

            if (comboServicio == null)
            {
                return NotFound();
            }

            return comboServicio;
        }

        // PUT: api/ComboServicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComboServicio(int id, ComboServicio comboServicio)
        {
            if (id != comboServicio.Id)
            {
                return BadRequest();
            }

            _context.Entry(comboServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComboServicioExists(id))
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

        // POST: api/ComboServicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComboServicio>> PostComboServicio(ComboServicio comboServicio)
        {
            _context.ComboServicio.Add(comboServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComboServicio", new { id = comboServicio.Id }, comboServicio);
        }

        // DELETE: api/ComboServicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComboServicio(int id)
        {
            var comboServicio = await _context.ComboServicio.FindAsync(id);
            if (comboServicio == null)
            {
                return NotFound();
            }

            _context.ComboServicio.Remove(comboServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComboServicioExists(int id)
        {
            return _context.ComboServicio.Any(e => e.Id == id);
        }
    }
}

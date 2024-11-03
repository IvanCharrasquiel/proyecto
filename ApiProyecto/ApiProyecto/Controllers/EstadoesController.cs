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
    public class EstadoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstadoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Estadoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetOEstado()
        {
            return await _context.OEstado.ToListAsync();
        }

        // GET: api/Estadoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstado(int id)
        {
            var estado = await _context.OEstado.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        // PUT: api/Estadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Estado estado)
        {
            if (id != estado.IdEstado)
            {
                return BadRequest();
            }

            _context.Entry(estado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
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

        // POST: api/Estadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
            _context.OEstado.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.IdEstado }, estado);
        }

        // DELETE: api/Estadoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.OEstado.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            _context.OEstado.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoExists(int id)
        {
            return _context.OEstado.Any(e => e.IdEstado == id);
        }
    }
}

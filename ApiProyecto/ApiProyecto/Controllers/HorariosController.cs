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
    public class HorariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HorariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Horarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horario>>> GetOHorario()
        {
            return await _context.OHorario.ToListAsync();
        }

        // GET: api/Horarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Horario>> GetHorario(int id)
        {
            var horario = await _context.OHorario.FindAsync(id);

            if (horario == null)
            {
                return NotFound();
            }

            return horario;
        }

        // PUT: api/Horarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, Horario horario)
        {
            if (id != horario.IdHorario)
            {
                return BadRequest();
            }

            _context.Entry(horario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioExists(id))
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

        // POST: api/Horarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Horario>> PostHorario(Horario horario)
        {
            _context.OHorario.Add(horario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorario", new { id = horario.IdHorario }, horario);
        }

        // DELETE: api/Horarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _context.OHorario.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }

            _context.OHorario.Remove(horario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorarioExists(int id)
        {
            return _context.OHorario.Any(e => e.IdHorario == id);
        }
    }
}

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
    public class CitaServiciosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CitaServiciosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CitaServicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaServicio>>> GetOCitaServicio()
        {
            return await _context.OCitaServicio.ToListAsync();
        }

        // GET: api/CitaServicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitaServicio>> GetCitaServicio(int id)
        {
            var citaServicio = await _context.OCitaServicio.FindAsync(id);

            if (citaServicio == null)
            {
                return NotFound();
            }

            return citaServicio;
        }

        // PUT: api/CitaServicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitaServicio(int id, CitaServicio citaServicio)
        {
            if (id != citaServicio.Id)
            {
                return BadRequest();
            }

            _context.Entry(citaServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaServicioExists(id))
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

        // POST: api/CitaServicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CitaServicio>> PostCitaServicio(CitaServicio citaServicio)
        {
            _context.OCitaServicio.Add(citaServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitaServicio", new { id = citaServicio.Id }, citaServicio);
        }

        // DELETE: api/CitaServicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitaServicio(int id)
        {
            var citaServicio = await _context.OCitaServicio.FindAsync(id);
            if (citaServicio == null)
            {
                return NotFound();
            }

            _context.OCitaServicio.Remove(citaServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitaServicioExists(int id)
        {
            return _context.OCitaServicio.Any(e => e.Id == id);
        }
    }
}

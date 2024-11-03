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
    public class CargoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CargoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cargoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetOCargo()
        {
            return await _context.OCargo.ToListAsync();
        }

        // GET: api/Cargoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(int id)
        {
            var cargo = await _context.OCargo.FindAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            return cargo;
        }

        // PUT: api/Cargoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCargo(int id, Cargo cargo)
        {
            if (id != cargo.IdCargo)
            {
                return BadRequest();
            }

            _context.Entry(cargo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CargoExists(id))
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

        // POST: api/Cargoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cargo>> PostCargo(Cargo cargo)
        {
            _context.OCargo.Add(cargo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCargo", new { id = cargo.IdCargo }, cargo);
        }

        // DELETE: api/Cargoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargo(int id)
        {
            var cargo = await _context.OCargo.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }

            _context.OCargo.Remove(cargo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CargoExists(int id)
        {
            return _context.OCargo.Any(e => e.IdCargo == id);
        }
    }
}

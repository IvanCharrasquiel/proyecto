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
    public class SexoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SexoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sexoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sexo>>> GetOSexo()
        {
            return await _context.OSexo.ToListAsync();
        }

        // GET: api/Sexoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sexo>> GetSexo(int id)
        {
            var sexo = await _context.OSexo.FindAsync(id);

            if (sexo == null)
            {
                return NotFound();
            }

            return sexo;
        }

        // PUT: api/Sexoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSexo(int id, Sexo sexo)
        {
            if (id != sexo.IdSexo)
            {
                return BadRequest();
            }

            _context.Entry(sexo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SexoExists(id))
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

        // POST: api/Sexoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sexo>> PostSexo(Sexo sexo)
        {
            _context.OSexo.Add(sexo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSexo", new { id = sexo.IdSexo }, sexo);
        }

        // DELETE: api/Sexoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSexo(int id)
        {
            var sexo = await _context.OSexo.FindAsync(id);
            if (sexo == null)
            {
                return NotFound();
            }

            _context.OSexo.Remove(sexo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SexoExists(int id)
        {
            return _context.OSexo.Any(e => e.IdSexo == id);
        }
    }
}

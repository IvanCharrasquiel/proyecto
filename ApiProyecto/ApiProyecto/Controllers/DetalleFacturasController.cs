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
    public class DetalleFacturasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DetalleFacturasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DetalleFacturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFactura>>> GetODetalleFactura()
        {
            return await _context.ODetalleFactura.ToListAsync();
        }

        // GET: api/DetalleFacturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleFactura>> GetDetalleFactura(int id)
        {
            var detalleFactura = await _context.ODetalleFactura.FindAsync(id);

            if (detalleFactura == null)
            {
                return NotFound();
            }

            return detalleFactura;
        }

        // PUT: api/DetalleFacturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleFactura(int id, DetalleFactura detalleFactura)
        {
            if (id != detalleFactura.IdDetalleFactura)
            {
                return BadRequest();
            }

            _context.Entry(detalleFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleFacturaExists(id))
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

        // POST: api/DetalleFacturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleFactura>> PostDetalleFactura(DetalleFactura detalleFactura)
        {
            _context.ODetalleFactura.Add(detalleFactura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleFactura", new { id = detalleFactura.IdDetalleFactura }, detalleFactura);
        }

        // DELETE: api/DetalleFacturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleFactura(int id)
        {
            var detalleFactura = await _context.ODetalleFactura.FindAsync(id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            _context.ODetalleFactura.Remove(detalleFactura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleFacturaExists(int id)
        {
            return _context.ODetalleFactura.Any(e => e.IdDetalleFactura == id);
        }
    }
}

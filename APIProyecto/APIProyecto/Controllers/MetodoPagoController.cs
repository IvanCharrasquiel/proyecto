using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;

[Route("api/[controller]")]
[ApiController]
public class MetodoPagoController : ControllerBase
{
    private readonly AppDbContext _context;

    public MetodoPagoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/MetodoPago
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MetodopagoDTO>>> GetMetodosPago()
    {
        var metodosPago = await _context.Metodopagos
            .Select(m => new MetodopagoDTO
            {
                IdMetodoPago = m.IdMetodoPago,
                Nombre = m.Nombre
            })
            .ToListAsync();

        return Ok(metodosPago);
    }

    // GET: api/MetodoPago/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<MetodopagoDTO>> GetMetodoPago(int id)
    {
        var metodoPago = await _context.Metodopagos.FindAsync(id);
        if (metodoPago == null)
            return NotFound();

        var metodoPagoDTO = new MetodopagoDTO
        {
            IdMetodoPago = metodoPago.IdMetodoPago,
            Nombre = metodoPago.Nombre
        };

        return Ok(metodoPagoDTO);
    }

    // POST: api/MetodoPago
    [HttpPost]
    public async Task<ActionResult<MetodopagoDTO>> PostMetodoPago(MetodopagoDTO metodoPagoDTO)
    {
        var metodoPago = new Metodopago
        {
            Nombre = metodoPagoDTO.Nombre
        };

        _context.Metodopagos.Add(metodoPago);
        await _context.SaveChangesAsync();

        metodoPagoDTO.IdMetodoPago = metodoPago.IdMetodoPago;

        return CreatedAtAction(nameof(GetMetodoPago), new { id = metodoPago.IdMetodoPago }, metodoPagoDTO);
    }

    // PUT: api/MetodoPago/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMetodoPago(int id, MetodopagoDTO metodoPagoDTO)
    {
        if (id != metodoPagoDTO.IdMetodoPago)
            return BadRequest();

        var metodoPago = await _context.Metodopagos.FindAsync(id);
        if (metodoPago == null)
            return NotFound();

        metodoPago.Nombre = metodoPagoDTO.Nombre;

        _context.Entry(metodoPago).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/MetodoPago/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMetodoPago(int id)
    {
        var metodoPago = await _context.Metodopagos.FindAsync(id);
        if (metodoPago == null)
            return NotFound();

        _context.Metodopagos.Remove(metodoPago);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;

[Route("api/[controller]")]
[ApiController]
public class DetalleFacturaController : ControllerBase
{
    private readonly AppDbContext _context;

    public DetalleFacturaController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/DetalleFactura
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalleFacturaDTO>>> GetDetalleFacturas()
    {
        var detalleFacturas = await _context.Detallefacturas
            .Select(d => new DetalleFacturaDTO
            {
                IdDetalleFactura = d.IdDetalleFactura,
                Subtotal = d.Subtotal,
                PrecioServicio = d.PrecioServicio,
                CantidadServicio = d.CantidadServicio,
                IdFactura = d.IdFactura,
                IdServicioReserva = d.IdServicioReserva
            })
            .ToListAsync();

        return Ok(detalleFacturas);
    }

    // GET: api/DetalleFactura/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<DetalleFacturaDTO>> GetDetalleFactura(int id)
    {
        var detalleFactura = await _context.Detallefacturas.FindAsync(id);
        if (detalleFactura == null)
            return NotFound();

        var detalleFacturaDTO = new DetalleFacturaDTO
        {
            IdDetalleFactura = detalleFactura.IdDetalleFactura,
            Subtotal = detalleFactura.Subtotal,
            PrecioServicio = detalleFactura.PrecioServicio,
            CantidadServicio = detalleFactura.CantidadServicio,
            IdFactura = detalleFactura.IdFactura,
            IdServicioReserva = detalleFactura.IdServicioReserva
        };

        return Ok(detalleFacturaDTO);
    }

    // POST: api/DetalleFactura
    [HttpPost]
    public async Task<ActionResult<DetalleFacturaDTO>> PostDetalleFactura(DetalleFacturaDTO detalleFacturaDTO)
    {
        var detalleFactura = new Detallefactura
        {
            Subtotal = detalleFacturaDTO.Subtotal,
            PrecioServicio = detalleFacturaDTO.PrecioServicio,
            CantidadServicio = detalleFacturaDTO.CantidadServicio,
            IdFactura = detalleFacturaDTO.IdFactura,
            IdServicioReserva = detalleFacturaDTO.IdServicioReserva
        };

        _context.Detallefacturas.Add(detalleFactura);
        await _context.SaveChangesAsync();

        detalleFacturaDTO.IdDetalleFactura = detalleFactura.IdDetalleFactura;

        return CreatedAtAction(nameof(GetDetalleFactura), new { id = detalleFactura.IdDetalleFactura }, detalleFacturaDTO);
    }

    // PUT: api/DetalleFactura/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDetalleFactura(int id, DetalleFacturaDTO detalleFacturaDTO)
    {
        if (id != detalleFacturaDTO.IdDetalleFactura)
            return BadRequest();

        var detalleFactura = await _context.Detallefacturas.FindAsync(id);
        if (detalleFactura == null)
            return NotFound();

        detalleFactura.Subtotal = detalleFacturaDTO.Subtotal;
        detalleFactura.PrecioServicio = detalleFacturaDTO.PrecioServicio;
        detalleFactura.CantidadServicio = detalleFacturaDTO.CantidadServicio;
        detalleFactura.IdFactura = detalleFacturaDTO.IdFactura;
        detalleFactura.IdServicioReserva = detalleFacturaDTO.IdServicioReserva;

        _context.Entry(detalleFactura).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/DetalleFactura/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDetalleFactura(int id)
    {
        var detalleFactura = await _context.Detallefacturas.FindAsync(id);
        if (detalleFactura == null)
            return NotFound();

        _context.Detallefacturas.Remove(detalleFactura);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;

[Route("api/[controller]")]
[ApiController]
public class FacturaController : ControllerBase
{
    private readonly AppDbContext _context;

    public FacturaController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Factura
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FacturaDTO>>> GetFacturas()
    {
        var facturas = await _context.Facturas
            .Select(f => new FacturaDTO
            {
                IdFactura = f.IdFactura,
                NumeroDocumento = f.NumeroDocumento,
                FechaEmision = f.FechaEmision,
                MontoTotal = f.MontoTotal,
                Estado = f.Estado,
                IdReserva = f.IdReserva
            })
            .ToListAsync();

        return Ok(facturas);
    }

    // GET: api/Factura/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<FacturaDTO>> GetFactura(int id)
    {
        var factura = await _context.Facturas.FindAsync(id);
        if (factura == null)
            return NotFound();

        var facturaDTO = new FacturaDTO
        {
            IdFactura = factura.IdFactura,
            NumeroDocumento = factura.NumeroDocumento,
            FechaEmision = factura.FechaEmision,
            MontoTotal = factura.MontoTotal,
            Estado = factura.Estado,
            IdReserva = factura.IdReserva
        };

        return Ok(facturaDTO);
    }

    // POST: api/Factura
    [HttpPost]
    public async Task<ActionResult<FacturaDTO>> PostFactura(FacturaDTO facturaDTO)
    {
        var factura = new Factura
        {
            NumeroDocumento = facturaDTO.NumeroDocumento,
            FechaEmision = facturaDTO.FechaEmision,
            MontoTotal = facturaDTO.MontoTotal,
            Estado = facturaDTO.Estado,
            IdReserva = facturaDTO.IdReserva
        };

        _context.Facturas.Add(factura);
        await _context.SaveChangesAsync();

        facturaDTO.IdFactura = factura.IdFactura;

        return CreatedAtAction(nameof(GetFactura), new { id = factura.IdFactura }, facturaDTO);
    }

    // PUT: api/Factura/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFactura(int id, FacturaDTO facturaDTO)
    {
        if (id != facturaDTO.IdFactura)
            return BadRequest();

        var factura = await _context.Facturas.FindAsync(id);
        if (factura == null)
            return NotFound();

        factura.NumeroDocumento = facturaDTO.NumeroDocumento;
        factura.FechaEmision = facturaDTO.FechaEmision;
        factura.MontoTotal = facturaDTO.MontoTotal;
        factura.Estado = facturaDTO.Estado;
        factura.IdReserva = facturaDTO.IdReserva;

        _context.Entry(factura).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Factura/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFactura(int id)
    {
        var factura = await _context.Facturas.FindAsync(id);
        if (factura == null)
            return NotFound();

        _context.Facturas.Remove(factura);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

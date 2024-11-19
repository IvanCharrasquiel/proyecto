using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;

[Route("api/[controller]")]
[ApiController]
public class PagoController : ControllerBase
{
    private readonly AppDbContext _context;

    public PagoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Pago
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PagoDTO>>> GetPagos()
    {
        var pagos = await _context.Pagos
            .Select(p => new PagoDTO
            {
                IdPago = p.IdPago,
                Monto = p.Monto,
                FechaPago = p.FechaPago,
                Estado = p.Estado,
                IdFactura = p.IdFactura,
                IdMetodoPago = p.IdMetodoPago
            })
            .ToListAsync();

        return Ok(pagos);
    }

    // GET: api/Pago/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PagoDTO>> GetPago(int id)
    {
        var pago = await _context.Pagos.FindAsync(id);
        if (pago == null)
            return NotFound();

        var pagoDTO = new PagoDTO
        {
            IdPago = pago.IdPago,
            Monto = pago.Monto,
            FechaPago = pago.FechaPago,
            Estado = pago.Estado,
            IdFactura = pago.IdFactura,
            IdMetodoPago = pago.IdMetodoPago
        };

        return Ok(pagoDTO);
    }

    // POST: api/Pago
    [HttpPost]
    public async Task<ActionResult<PagoDTO>> PostPago(PagoDTO pagoDTO)
    {
        var pago = new Pago
        {
            Monto = pagoDTO.Monto,
            FechaPago = pagoDTO.FechaPago,
            Estado = pagoDTO.Estado,
            IdFactura = pagoDTO.IdFactura,
            IdMetodoPago = pagoDTO.IdMetodoPago
        };

        _context.Pagos.Add(pago);
        await _context.SaveChangesAsync();

        pagoDTO.IdPago = pago.IdPago;

        return CreatedAtAction(nameof(GetPago), new { id = pago.IdPago }, pagoDTO);
    }

    // PUT: api/Pago/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPago(int id, PagoDTO pagoDTO)
    {
        if (id != pagoDTO.IdPago)
            return BadRequest();

        var pago = await _context.Pagos.FindAsync(id);
        if (pago == null)
            return NotFound();

        pago.Monto = pagoDTO.Monto;
        pago.FechaPago = pagoDTO.FechaPago;
        pago.Estado = pagoDTO.Estado;
        pago.IdFactura = pagoDTO.IdFactura;
        pago.IdMetodoPago = pagoDTO.IdMetodoPago;

        _context.Entry(pago).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Pago/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePago(int id)
    {
        var pago = await _context.Pagos.FindAsync(id);
        if (pago == null)
            return NotFound();

        _context.Pagos.Remove(pago);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

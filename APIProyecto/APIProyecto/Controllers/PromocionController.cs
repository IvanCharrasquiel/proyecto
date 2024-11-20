using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;

[Route("api/[controller]")]
[ApiController]
public class PromocionController : ControllerBase
{
    private readonly AppDbContext _context;

    public PromocionController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Promocion
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PromocionDTO>>> GetPromociones()
    {
        var promociones = await _context.Promocions
            .Select(p => new PromocionDTO
            {
                IdPromocion = p.IdPromocion,
                Descripcion = p.Descripcion,
                FechaInicio = p.FechaInicio,
                FechaFinal = p.FechaFinal,
                Descuento = p.Descuento,
                Estado = p.Estado,
                IdServicio = p.IdServicio
            })
            .ToListAsync();

        return Ok(promociones);
    }

    // GET: api/Promocion/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PromocionDTO>> GetPromocion(int id)
    {
        var promocion = await _context.Promocions.FindAsync(id);
        if (promocion == null)
            return NotFound();

        var promocionDTO = new PromocionDTO
        {
            IdPromocion = promocion.IdPromocion,
            Descripcion = promocion.Descripcion,
            FechaInicio = promocion.FechaInicio,
            FechaFinal = promocion.FechaFinal,
            Descuento = promocion.Descuento,
            Estado = promocion.Estado,
            IdServicio = promocion.IdServicio
        };

        return Ok(promocionDTO);
    }

    // POST: api/Promocion
    [HttpPost]
    public async Task<ActionResult<PromocionDTO>> PostPromocion(PromocionDTO promocionDTO)
    {
        var promocion = new Promocion
        {
            Descripcion = promocionDTO.Descripcion,
            FechaInicio = promocionDTO.FechaInicio,
            FechaFinal = promocionDTO.FechaFinal,
            Descuento = promocionDTO.Descuento,
            Estado = promocionDTO.Estado,
            IdServicio = promocionDTO.IdServicio
        };

        _context.Promocions.Add(promocion);
        await _context.SaveChangesAsync();

        promocionDTO.IdPromocion = promocion.IdPromocion;

        return CreatedAtAction(nameof(GetPromocion), new { id = promocion.IdPromocion }, promocionDTO);
    }

    // PUT: api/Promocion/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPromocion(int id, PromocionDTO promocionDTO)
    {
        if (id != promocionDTO.IdPromocion)
            return BadRequest();

        var promocion = await _context.Promocions.FindAsync(id);
        if (promocion == null)
            return NotFound();

        promocion.Descripcion = promocionDTO.Descripcion;
        promocion.FechaInicio = promocionDTO.FechaInicio;
        promocion.FechaFinal = promocionDTO.FechaFinal;
        promocion.Descuento = promocionDTO.Descuento;
        promocion.Estado = promocionDTO.Estado;
        promocion.IdServicio = promocionDTO.IdServicio;

        _context.Entry(promocion).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Promocion/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePromocion(int id)
    {
        var promocion = await _context.Promocions.FindAsync(id);
        if (promocion == null)
            return NotFound();

        _context.Promocions.Remove(promocion);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("PorServicio/{idServicio}")]
    public async Task<ActionResult<IEnumerable<PromocionDTO>>> GetPromocionesPorServicio(int idServicio)
    {
        var promociones = await _context.Promocions
            .Where(p => p.IdServicio == idServicio)
            .Select(p => new PromocionDTO
            {
                IdPromocion = p.IdPromocion,
                Descripcion = p.Descripcion,
                FechaInicio = p.FechaInicio,
                FechaFinal = p.FechaFinal,
                Descuento = p.Descuento,
                Estado = p.Estado,
                IdServicio = p.IdServicio
            }).ToListAsync();

        if (promociones == null || !promociones.Any())
        {
            return NotFound(new { message = "No se encontraron promociones para este servicio." });
        }



        return Ok(promociones);
    }

}

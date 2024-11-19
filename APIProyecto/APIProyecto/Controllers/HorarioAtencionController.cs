using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;

[Route("api/[controller]")]
[ApiController]
public class HorarioAtencionController : ControllerBase
{
    private readonly AppDbContext _context;

    public HorarioAtencionController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/HorarioAtencion
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HorarioAtencionDTO>>> GetHorariosAtencion()
    {
        var horariosAtencion = await _context.Horarioatencions
            .Select(h => new HorarioAtencionDTO
            {
                IdHorario = h.IdHorario,
                HoraInicio = h.HoraInicio,
                HoraFin = h.HoraFin
            })
            .ToListAsync();

        return Ok(horariosAtencion);
    }

    // GET: api/HorarioAtencion/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<HorarioAtencionDTO>> GetHorarioAtencion(int id)
    {
        var horarioAtencion = await _context.Horarioatencions.FindAsync(id);
        if (horarioAtencion == null)
            return NotFound();

        var horarioAtencionDTO = new HorarioAtencionDTO
        {
            IdHorario = horarioAtencion.IdHorario,
            HoraInicio = horarioAtencion.HoraInicio,
            HoraFin = horarioAtencion.HoraFin
        };

        return Ok(horarioAtencionDTO);
    }

    // POST: api/HorarioAtencion
    [HttpPost]
    public async Task<ActionResult<HorarioAtencionDTO>> PostHorarioAtencion([FromBody] HorarioAtencionDTO horarioAtencionDTO)
    {
        var horarioAtencion = new Horarioatencion
        {
            HoraInicio = horarioAtencionDTO.HoraInicio,
            HoraFin = horarioAtencionDTO.HoraFin
        };

        _context.Horarioatencions.Add(horarioAtencion);
        await _context.SaveChangesAsync();

        horarioAtencionDTO.IdHorario = horarioAtencion.IdHorario;

        return CreatedAtAction(nameof(GetHorarioAtencion), new { id = horarioAtencion.IdHorario }, horarioAtencionDTO);
    }

    // PUT: api/HorarioAtencion/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHorarioAtencion(int id, HorarioAtencionDTO horarioAtencionDTO)
    {
        if (id != horarioAtencionDTO.IdHorario)
            return BadRequest();

        var horarioAtencion = await _context.Horarioatencions.FindAsync(id);
        if (horarioAtencion == null)
            return NotFound();

        horarioAtencion.HoraInicio = horarioAtencionDTO.HoraInicio;
        horarioAtencion.HoraFin = horarioAtencionDTO.HoraFin;

        _context.Entry(horarioAtencion).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/HorarioAtencion/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHorarioAtencion(int id)
    {
        var horarioAtencion = await _context.Horarioatencions.FindAsync(id);
        if (horarioAtencion == null)
            return NotFound();

        _context.Horarioatencions.Remove(horarioAtencion);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

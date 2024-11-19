using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;

[Route("api/[controller]")]
[ApiController]
public class ReservaController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReservaController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Reserva
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservas()
    {
        var reservas = await _context.Reservas
            .Select(r => new ReservaDTO
            {
                IdReserva = r.IdReserva,
                Fecha = r.Fecha,
                HoraInicio = r.HoraInicio,
                HoraFin = r.HoraFin,
                IdCliente = r.IdCliente,
                IdEmpleado = r.IdEmpleado,
                EstadoReserva = r.EstadoReserva
            })
            .ToListAsync();

        return Ok(reservas);
    }

    // GET: api/Reserva/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ReservaDTO>> GetReserva(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null)
            return NotFound();

        var reservaDTO = new ReservaDTO
        {
            IdReserva = reserva.IdReserva,
            Fecha = reserva.Fecha,
            HoraInicio = reserva.HoraInicio,
            HoraFin = reserva.HoraFin,
            IdCliente = reserva.IdCliente,
            IdEmpleado = reserva.IdEmpleado,
            EstadoReserva = reserva.EstadoReserva
        };

        return Ok(reservaDTO);
    }

    // POST: api/Reserva
    [HttpPost]
    public async Task<ActionResult<ReservaDTO>> PostReserva( ReservaDTO reservaDTO)
    {
        var reserva = new Reserva
        {
            Fecha = reservaDTO.Fecha,
            HoraInicio = reservaDTO.HoraInicio,
            HoraFin = reservaDTO.HoraFin,
            IdCliente = reservaDTO.IdCliente,
            IdEmpleado = reservaDTO.IdEmpleado,
            EstadoReserva = reservaDTO.EstadoReserva
        };

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();

        reservaDTO.IdReserva = reserva.IdReserva;

        return CreatedAtAction(nameof(GetReserva), new { id = reserva.IdReserva }, reservaDTO);
    }

    // PUT: api/Reserva/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReserva(int id, ReservaDTO reservaDTO)
    {
        if (id != reservaDTO.IdReserva)
            return BadRequest();

        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null)
            return NotFound();

        reserva.Fecha = reservaDTO.Fecha;
        reserva.HoraInicio = reservaDTO.HoraInicio;
        reserva.HoraFin = reservaDTO.HoraFin;
        reserva.IdCliente = reservaDTO.IdCliente;
        reserva.IdEmpleado = reservaDTO.IdEmpleado;
        reserva.EstadoReserva = reservaDTO.EstadoReserva;

        _context.Entry(reserva).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Reserva/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReserva(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null)
            return NotFound();

        _context.Reservas.Remove(reserva);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{idReserva}/servicios/{idServicio}")]
    public async Task<IActionResult> AgregarServicioAReserva(int idReserva, int idServicio)
    {
        var servicioReserva = new Servicioreserva
        {
            IdReserva = idReserva,
            IdServicio = idServicio
        };

        _context.Servicioreservas.Add(servicioReserva);
        await _context.SaveChangesAsync();

        return NoContent();
    }


}

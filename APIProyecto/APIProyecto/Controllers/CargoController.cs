using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;

[Route("api/[controller]")]
[ApiController]
public class CargoController : ControllerBase
{
    private readonly AppDbContext _context;

    public CargoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Cargo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CargoDTO>>> GetCargos()
    {
        var cargos = await _context.Cargos
            .Select(c => new CargoDTO
            {
                IdCargo = c.IdCargo,
                NombreCargo = c.NombreCargo
            })
            .ToListAsync();

        return Ok(cargos);
    }

    // GET: api/Cargo/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CargoDTO>> GetCargo(int id)
    {
        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo == null)
            return NotFound();

        var cargoDTO = new CargoDTO
        {
            IdCargo = cargo.IdCargo,
            NombreCargo = cargo.NombreCargo
        };

        return Ok(cargoDTO);
    }

    // POST: api/Cargo
    [HttpPost]
    public async Task<ActionResult<CargoDTO>> PostCargo(CargoDTO cargoDTO)
    {
        var cargo = new Cargo
        {
            NombreCargo = cargoDTO.NombreCargo
        };

        _context.Cargos.Add(cargo);
        await _context.SaveChangesAsync();

        cargoDTO.IdCargo = cargo.IdCargo;

        return CreatedAtAction(nameof(GetCargo), new { id = cargo.IdCargo }, cargoDTO);
    }

    // PUT: api/Cargo/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCargo(int id, CargoDTO cargoDTO)
    {
        if (id != cargoDTO.IdCargo)
            return BadRequest();

        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo == null)
            return NotFound();

        cargo.NombreCargo = cargoDTO.NombreCargo;

        _context.Entry(cargo).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Cargo/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCargo(int id)
    {
        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo == null)
            return NotFound();

        _context.Cargos.Remove(cargo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

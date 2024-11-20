using Microsoft.AspNetCore.Mvc;
using APIProyecto.DTO;
using APIProyecto.Models;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PersonaController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonaController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Persona
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonaDTO>>> GetPersonas()
    {
        var personas = await _context.Personas.Select(p => new PersonaDTO
        {
            IdPersona = p.IdPersona,
            Cedula = p.Cedula,
            Nombre = p.Nombre,
            Apellido = p.Apellido,
            Telefono = p.Telefono,
            Email = p.Email,
            Direccion = p.Direccion,
            FotoPerfil = p.FotoPerfil
        }).ToListAsync();

        return Ok(personas);
    }

    // GET: api/Persona/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonaDTO>> GetPersona(int id)
    {
        var persona = await _context.Personas.FindAsync(id);
        if (persona == null)
            return NotFound();

        var personaDTO = new PersonaDTO
        {
            IdPersona = persona.IdPersona,
            Cedula = persona.Cedula,
            Nombre = persona.Nombre,
            Apellido = persona.Apellido,
            Telefono = persona.Telefono,
            Email = persona.Email,
            Direccion = persona.Direccion,
            FotoPerfil = persona.FotoPerfil
        };

        return Ok(personaDTO);
    }

    // POST: api/Persona
    [HttpPost]
    public async Task<ActionResult<PersonaDTO>> PostPersona(PersonaDTO personaDTO)
    {
        var persona = new Persona
        {
            Cedula = personaDTO.Cedula,
            Nombre = personaDTO.Nombre,
            Apellido = personaDTO.Apellido,
            Telefono = personaDTO.Telefono,
            Email = personaDTO.Email,
            Direccion = personaDTO.Direccion,
            FotoPerfil = personaDTO.FotoPerfil
        };

        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();

        personaDTO.IdPersona = persona.IdPersona;
        return CreatedAtAction(nameof(GetPersona), new { id = persona.IdPersona }, personaDTO);
    }

    // PUT: api/Persona/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPersona(int id, PersonaDTO personaDTO)
    {
        if (id != personaDTO.IdPersona)
            return BadRequest();

        var persona = await _context.Personas.FindAsync(id);
        if (persona == null)
            return NotFound();

        persona.Cedula = personaDTO.Cedula;
        persona.Nombre = personaDTO.Nombre;
        persona.Apellido = personaDTO.Apellido;
        persona.Telefono = personaDTO.Telefono;
        persona.Email = personaDTO.Email;
        persona.Direccion = personaDTO.Direccion;
        persona.FotoPerfil = personaDTO.FotoPerfil;

        _context.Entry(persona).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Persona/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersona(int id)
    {
        var persona = await _context.Personas.FindAsync(id);
        if (persona == null)
            return NotFound();

        _context.Personas.Remove(persona);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

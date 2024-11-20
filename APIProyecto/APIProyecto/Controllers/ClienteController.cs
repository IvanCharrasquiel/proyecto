using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http; // Asegúrate de incluir este using para IFormFile

namespace APIProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Protege todas las acciones por defecto
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ClienteController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Cliente
        [HttpGet]
        [AllowAnonymous] // Permitir acceso público si es necesario
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes()
        {
            var clientes = await _context.Clientes
                .Include(c => c.IdPersonaNavigation)
                .Select(c => new ClienteDTO
                {
                    IdCliente = c.IdCliente,
                    FechaRegistro = c.FechaRegistro,
                    IdPersona = c.IdPersona,
                    Persona = new PersonaDTO
                    {
                        IdPersona = c.IdPersonaNavigation.IdPersona,
                        Cedula = c.IdPersonaNavigation.Cedula,
                        Nombre = c.IdPersonaNavigation.Nombre,
                        Apellido = c.IdPersonaNavigation.Apellido,
                        Telefono = c.IdPersonaNavigation.Telefono,
                        Email = c.IdPersonaNavigation.Email,
                        Direccion = c.IdPersonaNavigation.Direccion,
                        FotoPerfil = c.IdPersonaNavigation.FotoPerfil
                    }
                })
                .ToListAsync();

            return Ok(clientes);
        }

        // GET: api/Cliente/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            var clienteDTO = new ClienteDTO
            {
                IdCliente = cliente.IdCliente,
                FechaRegistro = cliente.FechaRegistro,
                IdPersona = cliente.IdPersona,
                Persona = new PersonaDTO
                {
                    IdPersona = cliente.IdPersonaNavigation.IdPersona,
                    Cedula = cliente.IdPersonaNavigation.Cedula,
                    Nombre = cliente.IdPersonaNavigation.Nombre,
                    Apellido = cliente.IdPersonaNavigation.Apellido,
                    Telefono = cliente.IdPersonaNavigation.Telefono,
                    Email = cliente.IdPersonaNavigation.Email,
                    Direccion = cliente.IdPersonaNavigation.Direccion,
                    FotoPerfil = cliente.IdPersonaNavigation.FotoPerfil
                }
            };

            return Ok(clienteDTO);
        }

        // POST: api/Cliente/Register
        [HttpPost("Register")]
        [AllowAnonymous] // Permitir registro sin autenticación
        public async Task<ActionResult<ClienteDTO>> RegisterCliente(ClienteRegistroDTO clienteRegistroDTO)
        {
            // Validar el DTO
            if (clienteRegistroDTO == null || clienteRegistroDTO.Persona == null)
            {
                return BadRequest("Datos de registro inválidos.");
            }

            // Verificar si el email ya está registrado
            var personaExistente = await _context.Personas.FirstOrDefaultAsync(p => p.Email == clienteRegistroDTO.Persona.Email);
            if (personaExistente != null)
            {
                return Conflict("El email ya está registrado.");
            }

            // Crear la entidad Persona
            var persona = new Persona
            {
                Cedula = clienteRegistroDTO.Persona.Cedula,
                Nombre = clienteRegistroDTO.Persona.Nombre,
                Apellido = clienteRegistroDTO.Persona.Apellido,
                Telefono = clienteRegistroDTO.Persona.Telefono,
                Email = clienteRegistroDTO.Persona.Email,
                Direccion = clienteRegistroDTO.Persona.Direccion,
                FotoPerfil = clienteRegistroDTO.Persona.FotoPerfil
            };

            // Agregar Persona al contexto
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            // Crear la entidad Cliente
            var cliente = new Cliente
            {
                IdPersona = persona.IdPersona,
                FechaRegistro = DateTime.Now,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(clienteRegistroDTO.Contraseña) // Usar hash para la contraseña
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Preparar el DTO de respuesta
            var clienteDTO = new ClienteDTO
            {
                IdCliente = cliente.IdCliente,
                FechaRegistro = cliente.FechaRegistro,
                IdPersona = cliente.IdPersona,
                Persona = new PersonaDTO
                {
                    IdPersona = persona.IdPersona,
                    Cedula = persona.Cedula,
                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido,
                    Telefono = persona.Telefono,
                    Email = persona.Email,
                    Direccion = persona.Direccion,
                    FotoPerfil = persona.FotoPerfil
                }
            };

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, clienteDTO);
        }

        // PUT: api/Cliente/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, ClienteDTO clienteDTO)
        {
            if (id != clienteDTO.IdCliente)
                return BadRequest("El ID del cliente no coincide.");

            var cliente = await _context.Clientes
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            // Actualizar información del cliente
            cliente.FechaRegistro = clienteDTO.FechaRegistro;

            // Actualizar información de la persona
            cliente.IdPersonaNavigation.Cedula = clienteDTO.Persona.Cedula;
            cliente.IdPersonaNavigation.Nombre = clienteDTO.Persona.Nombre;
            cliente.IdPersonaNavigation.Apellido = clienteDTO.Persona.Apellido;
            cliente.IdPersonaNavigation.Telefono = clienteDTO.Persona.Telefono;
            cliente.IdPersonaNavigation.Email = clienteDTO.Persona.Email;
            cliente.IdPersonaNavigation.Direccion = clienteDTO.Persona.Direccion;
            cliente.IdPersonaNavigation.FotoPerfil = clienteDTO.Persona.FotoPerfil;

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Cliente/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Cliente/Perfil
        [Authorize(Roles = "Cliente")]
        [HttpGet("Perfil")]
        public async Task<ActionResult<ClienteDTO>> GetPerfil()
        {
            var idPersonaStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idPersonaStr, out int idPersona))
            {
                return Unauthorized();
            }

            var rol = User.FindFirstValue(ClaimTypes.Role);

            if (rol != "Cliente")
                return Forbid();

            var cliente = await _context.Clientes
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(c => c.IdPersona == idPersona);

            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            var clienteDTO = new ClienteDTO
            {
                IdCliente = cliente.IdCliente,
                FechaRegistro = cliente.FechaRegistro,
                IdPersona = cliente.IdPersona,
                Persona = new PersonaDTO
                {
                    IdPersona = cliente.IdPersonaNavigation.IdPersona,
                    Cedula = cliente.IdPersonaNavigation.Cedula,
                    Nombre = cliente.IdPersonaNavigation.Nombre,
                    Apellido = cliente.IdPersonaNavigation.Apellido,
                    Telefono = cliente.IdPersonaNavigation.Telefono,
                    Email = cliente.IdPersonaNavigation.Email,
                    Direccion = cliente.IdPersonaNavigation.Direccion,
                    FotoPerfil = cliente.IdPersonaNavigation.FotoPerfil
                }
            };

            return Ok(clienteDTO);
        }

        // POST: api/Cliente/{id}/FotoPerfil
        [Authorize(Roles = "Cliente")]
        [HttpPost("{id}/FotoPerfil")]
        public async Task<IActionResult> UploadFotoPerfil(int id, IFormFile foto)
        {
            var idPersonaStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idPersonaStr, out int idPersona))
            {
                return Unauthorized();
            }

            if (idPersona != _context.Clientes.FirstOrDefault(c => c.IdCliente == id)?.IdPersona)
            {
                return Forbid();
            }

            var cliente = await _context.Clientes
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            if (foto == null || foto.Length == 0)
                return BadRequest("No se ha proporcionado una foto válida.");

            // Guardar la foto en el servidor o en un servicio de almacenamiento
            var rutaFoto = await GuardarFotoAsync(foto);

            // Actualizar la ruta de la foto en la entidad Persona
            cliente.IdPersonaNavigation.FotoPerfil = rutaFoto;

            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Foto de perfil actualizada correctamente." });
        }

        private async Task<string> GuardarFotoAsync(IFormFile foto)
        {
            // Implementa la lógica para guardar la foto y retornar la ruta o URL
            // Por ejemplo, guardarla en una carpeta en el servidor
            var carpetaFotos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fotos");
            if (!Directory.Exists(carpetaFotos))
            {
                Directory.CreateDirectory(carpetaFotos);
            }

            var nombreArchivo = $"{Guid.NewGuid()}_{Path.GetFileName(foto.FileName)}";
            var rutaCompleta = Path.Combine(carpetaFotos, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            // Retornar la ruta relativa o URL según tu configuración
            return $"/fotos/{nombreArchivo}";
        }

        // GET: api/Cliente/{id}/Reservas
        [Authorize(Roles = "Cliente")]
        [HttpGet("{id}/Reservas")]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservas(int id)
        {
            var idPersonaStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idPersonaStr, out int idPersona))
            {
                return Unauthorized();
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id);
            if (cliente == null || cliente.IdPersona != idPersona)
            {
                return Forbid();
            }

            var reservas = await _context.Reservas
                .Where(r => r.IdCliente == id)
                .Select(r => new ReservaDTO
                {
                    IdReserva = r.IdReserva,
                    Fecha = r.Fecha,
                    HoraInicio = r.HoraInicio,
                    HoraFin = r.HoraFin,
                    EstadoReserva = r.EstadoReserva
                    // Agrega más campos si es necesario
                })
                .ToListAsync();

            return Ok(reservas);
        }


    }
}

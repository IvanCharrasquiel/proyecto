// Controllers/ServiciosController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiciosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Servicios
        [HttpGet]
        [AllowAnonymous] // Permitir acceso público si es necesario
        public async Task<ActionResult<IEnumerable<ServicioDTO>>> GetServicios()
        {
            var servicios = await _context.Servicios
                .Include(s => s.Promocions)
                .Select(s => new ServicioDTO
                {
                    IdServicio = s.IdServicio,
                    NombreServicio = s.NombreServicio,
                    Descripcion = s.Descripcion,
                    Precio = s.Precio,
                    Duracion = s.Duracion,
                    Promociones = s.Promocions.Select(p => new PromocionDTO
                    {
                        IdPromocion = p.IdPromocion,
                        Descripcion = p.Descripcion,
                        Descuento = p.Descuento
                    }).ToList()
                })
                .ToListAsync();

            if (!servicios.Any())
            {
                return NotFound("No hay servicios disponibles.");
            }

            return Ok(servicios);
        }

        // GET: api/Servicios/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServicioDTO>> GetServicio(int id)
        {
            var servicio = await _context.Servicios
                .Include(s => s.Promocions)
                .FirstOrDefaultAsync(s => s.IdServicio == id);

            if (servicio == null)
            {
                return NotFound("El servicio no existe.");
            }

            var servicioDTO = new ServicioDTO
            {
                IdServicio = servicio.IdServicio,
                NombreServicio = servicio.NombreServicio,
                Descripcion = servicio.Descripcion,
                Precio = servicio.Precio,
                Duracion = servicio.Duracion,
                Promociones = servicio.Promocions.Select(p => new PromocionDTO
                {
                    IdPromocion = p.IdPromocion,
                    Descripcion = p.Descripcion,
                    Descuento = p.Descuento
                }).ToList()
            };

            return Ok(servicioDTO);
        }

        // GET: api/Servicios/{id}/Empleados
        [HttpGet("{id}/Empleados")]
        public async Task<ActionResult<IEnumerable<EmpleadoDTO>>> GetEmpleadosPorServicio(int id)
        {
            var servicio = await _context.Servicios
                .Include(s => s.IdEmpleados)
                    .ThenInclude(e => e.IdPersonaNavigation)
                .Include(s => s.IdEmpleados)
                    .ThenInclude(e => e.IdCargoNavigation)
                .FirstOrDefaultAsync(s => s.IdServicio == id);

            if (servicio == null)
            {
                return NotFound("Servicio no encontrado.");
            }

            var empleadosDTO = servicio.IdEmpleados.Select(e => new EmpleadoDTO
            {
                IdEmpleado = e.IdEmpleado,
                FechaContrato = e.FechaContrato,
                Comision = e.Comision,
                IdPersona = e.IdPersona,
                IdCargo = e.IdCargo,
                Persona = new PersonaDTO
                {
                    IdPersona = e.IdPersonaNavigation.IdPersona,
                    Cedula = e.IdPersonaNavigation.Cedula,
                    Nombre = e.IdPersonaNavigation.Nombre,
                    Apellido = e.IdPersonaNavigation.Apellido,
                    Telefono = e.IdPersonaNavigation.Telefono,
                    Email = e.IdPersonaNavigation.Email,
                    Direccion = e.IdPersonaNavigation.Direccion,
                    FotoPerfil = e.IdPersonaNavigation.FotoPerfil
                },
                Cargo = e.IdCargoNavigation != null ? new CargoDTO
                {
                    IdCargo = e.IdCargoNavigation.IdCargo,
                    NombreCargo = e.IdCargoNavigation.NombreCargo
                } : null
            }).ToList();

            if (!empleadosDTO.Any())
            {
                return NotFound("No hay empleados asociados a este servicio.");
            }

            return Ok(empleadosDTO);
        }

        // POST: api/Servicios
        [HttpPost]
        public async Task<ActionResult<ServicioDTO>> CrearServicio([FromBody] ServicioDTO servicioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos inválidos para el servicio.");
            }

            var servicio = new Servicio
            {
                NombreServicio = servicioDTO.NombreServicio,
                Descripcion = servicioDTO.Descripcion,
                Precio = servicioDTO.Precio,
                Duracion = servicioDTO.Duracion
            };

            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            // Preparar el DTO de respuesta
            var servicioCreadoDTO = new ServicioDTO
            {
                IdServicio = servicio.IdServicio,
                NombreServicio = servicio.NombreServicio,
                Descripcion = servicio.Descripcion,
                Precio = servicio.Precio,
                Duracion = servicio.Duracion,
                Promociones = new List<PromocionDTO>() // Inicializar vacío
            };

            return CreatedAtAction(nameof(GetServicio), new { id = servicio.IdServicio }, servicioCreadoDTO);
        }

        // PUT: api/Servicios/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ActualizarServicio(int id, [FromBody] ServicioDTO servicioDTO)
        {
            if (id != servicioDTO.IdServicio)
            {
                return BadRequest("El ID del servicio no coincide.");
            }

            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return NotFound("El servicio no existe.");
            }

            servicio.NombreServicio = servicioDTO.NombreServicio;
            servicio.Descripcion = servicioDTO.Descripcion;
            servicio.Precio = servicioDTO.Precio;
            servicio.Duracion = servicioDTO.Duracion;

            _context.Entry(servicio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Servicios/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> EliminarServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return NotFound("El servicio no existe.");
            }

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

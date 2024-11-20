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
                .Include(s => s.EmpleadosServicio) // Incluye la relación EmpleadosServicio
                    .ThenInclude(es => es.Empleado) // Incluye el empleado relacionado
                    .ThenInclude(e => e.IdPersonaNavigation) // Incluye la persona asociada al empleado
                .Include(s => s.EmpleadosServicio)
                    .ThenInclude(es => es.Empleado)
                    .ThenInclude(e => e.IdCargoNavigation) // Incluye el cargo asociado al empleado
                .FirstOrDefaultAsync(s => s.IdServicio == id);

            if (servicio == null)
            {
                return NotFound("Servicio no encontrado.");
            }

            // Extraer los empleados asociados al servicio
            var empleadosDTO = servicio.EmpleadosServicio.Select(es => new EmpleadoDTO
            {
                IdEmpleado = es.Empleado.IdEmpleado,
                FechaContrato = es.Empleado.FechaContrato,
                Comision = es.Empleado.Comision,
                IdPersona = es.Empleado.IdPersona,
                IdCargo = es.Empleado.IdCargo,
                Persona = new PersonaDTO
                {
                    IdPersona = es.Empleado.IdPersonaNavigation.IdPersona,
                    Cedula = es.Empleado.IdPersonaNavigation.Cedula,
                    Nombre = es.Empleado.IdPersonaNavigation.Nombre,
                    Apellido = es.Empleado.IdPersonaNavigation.Apellido,
                    Telefono = es.Empleado.IdPersonaNavigation.Telefono,
                    Email = es.Empleado.IdPersonaNavigation.Email,
                    Direccion = es.Empleado.IdPersonaNavigation.Direccion,
                    FotoPerfil = es.Empleado.IdPersonaNavigation.FotoPerfil
                },
                Cargo = es.Empleado.IdCargoNavigation != null ? new CargoDTO
                {
                    IdCargo = es.Empleado.IdCargoNavigation.IdCargo,
                    NombreCargo = es.Empleado.IdCargoNavigation.NombreCargo
                } : null
            }).ToList();

            if (!empleadosDTO.Any())
            {
                return NotFound("No hay empleados asociados a este servicio.");
            }

            return Ok(empleadosDTO);
        }

        [HttpGet("ServiciosPorEmpleado/{idEmpleado}")]
        public async Task<ActionResult<IEnumerable<ServicioDTO>>> GetServiciosPorEmpleado(int idEmpleado)
        {
            try
            {
                var servicios = await _context.EmpleadoServicios
                    .Where(es => es.IdEmpleado == idEmpleado)
                    .Join(_context.Servicios,
                        es => es.IdServicio,
                        s => s.IdServicio,
                        (es, s) => new ServicioDTO
                        {
                            IdServicio = s.IdServicio,
                            NombreServicio = s.NombreServicio,
                            Descripcion = s.Descripcion,
                            Precio = s.Precio,
                            Duracion = s.Duracion
                        })
                    .ToListAsync();

                return Ok(servicios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener servicios: {ex.Message}");
            }
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

        [HttpPost("{id}/Servicios")]
        public async Task<IActionResult> AsignarServicios(int id, [FromBody] List<int> idServicios)
            {
                if (idServicios == null || !idServicios.Any())
                    return BadRequest("No se proporcionaron servicios para asignar.");

                var empleado = await _context.Empleados
                    .Include(e => e.EmpleadosServicio)
                    .FirstOrDefaultAsync(e => e.IdEmpleado == id);

                if (empleado == null)
                    return NotFound("Empleado no encontrado.");

                // Obtener los servicios a asignar
                var servicios = await _context.Servicios
                    .Where(s => idServicios.Contains(s.IdServicio))
                    .ToListAsync();

                if (servicios.Count != idServicios.Count)
                    return BadRequest("Algunos servicios no existen.");

                // Asignar servicios evitando duplicados
                foreach (var servicio in servicios)
                {
                    if (!empleado.EmpleadosServicio.Any(es => es.IdServicio == servicio.IdServicio))
                    {
                        empleado.EmpleadosServicio.Add(new EmpleadoServicio
                        {
                            IdEmpleado = empleado.IdEmpleado,
                            IdServicio = servicio.IdServicio
                        });
                    }
                }

                await _context.SaveChangesAsync();

                return Ok("Servicios asignados correctamente.");
            }


        


    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace APIProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservasController(AppDbContext context)
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
                    Fecha = r.Fecha, // Ahora Fecha es no-nullable
                    HoraInicio = r.HoraInicio, // Ahora HoraInicio es no-nullable
                    HoraFin = r.HoraFin, // Ahora HoraFin es no-nullable
                    IdCliente = r.IdCliente,
                    IdEmpleado = r.IdEmpleado,
                    EstadoReserva = r.EstadoReserva,
                    IdServicios = r.Servicioreservas.Select(sr => sr.IdServicio).ToList()
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(reservas);
        }

        // GET: api/Reserva/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaDTO>> GetReserva(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Servicioreservas)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
                return NotFound("Reserva no encontrada.");

            var reservaDTO = new ReservaDTO
            {
                IdReserva = reserva.IdReserva,
                Fecha = reserva.Fecha,
                HoraInicio = reserva.HoraInicio,
                HoraFin = reserva.HoraFin,
                IdCliente = reserva.IdCliente,
                IdEmpleado = reserva.IdEmpleado,
                EstadoReserva = reserva.EstadoReserva,
                IdServicios = reserva.Servicioreservas.Select(sr => sr.IdServicio).ToList()
            };

            return Ok(reservaDTO);
        }

        // POST: api/Reserva
        [HttpPost]
        public async Task<ActionResult<ReservaDTO>> PostReserva(ReservaDTO reservaDTO)
        {
            if (reservaDTO == null)
            {
                return BadRequest("Datos de reserva inválidos.");
            }

            // Validar que el Cliente y Empleado existan
            var cliente = await _context.Clientes.FindAsync(reservaDTO.IdCliente);
            if (cliente == null)
                return BadRequest("Cliente no existe.");

            var empleado = await _context.Empleados.FindAsync(reservaDTO.IdEmpleado);
            if (empleado == null)
                return BadRequest("Empleado no existe.");

            // Validar que la reserva tenga al menos un servicio asignado
            if (reservaDTO.IdServicios == null || !reservaDTO.IdServicios.Any())
            {
                return BadRequest("La reserva debe tener al menos un servicio asignado.");
            }

            // Obtener los servicios para calcular la duración total
            var servicios = await _context.Servicios
                .Where(s => reservaDTO.IdServicios.Contains(s.IdServicio))
                .ToListAsync();

            if (servicios.Count != reservaDTO.IdServicios.Count)
            {
                return BadRequest("Algunos servicios no existen.");
            }

            int duracionTotal = servicios.Sum(s => s.Duracion); // Duración total en minutos

            // Calcular HoraFin
            TimeSpan horaInicio = reservaDTO.HoraInicio;
            TimeSpan horaFinCalculada = horaInicio.Add(TimeSpan.FromMinutes(duracionTotal));

            // Validar que HoraFin no exceda las 24 horas
            if (horaFinCalculada.TotalHours > 24)
            {
                return BadRequest("La hora de fin excede las 24 horas del día.");
            }

            // Validar que la hora seleccionada no esté ocupada
            bool horarioOcupado = await _context.Reservas
                .AnyAsync(r => r.IdEmpleado == reservaDTO.IdEmpleado &&
                               r.Fecha.Date == reservaDTO.Fecha.Date &&
                               ((horaInicio >= r.HoraInicio && horaInicio < r.HoraFin) ||
                                (horaFinCalculada > r.HoraInicio && horaFinCalculada <= r.HoraFin) ||
                                (horaInicio <= r.HoraInicio && horaFinCalculada >= r.HoraFin)));

            if (horarioOcupado)
                return Conflict("El horario seleccionado ya está ocupado.");

            var reserva = new Reserva
            {
                Fecha = reservaDTO.Fecha,
                HoraInicio = horaInicio,
                HoraFin = horaFinCalculada,
                IdCliente = reservaDTO.IdCliente,
                IdEmpleado = reservaDTO.IdEmpleado,
                EstadoReserva = reservaDTO.EstadoReserva
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            // Asignar servicios a la reserva
            foreach (var servicioId in reservaDTO.IdServicios)
            {
                var servicioReserva = new Servicioreserva
                {
                    IdReserva = reserva.IdReserva,
                    IdServicio = servicioId
                };
                _context.Servicioreservas.Add(servicioReserva);
            }

            await _context.SaveChangesAsync();

            // Actualizar DTO con valores calculados
            reservaDTO.IdReserva = reserva.IdReserva;
            reservaDTO.HoraFin = reserva.HoraFin;

            return CreatedAtAction(nameof(GetReserva), new { id = reserva.IdReserva }, reservaDTO);
        }

        // PUT: api/Reserva/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, ReservaDTO reservaDTO)
        {
            if (id != reservaDTO.IdReserva)
                return BadRequest("El ID de la reserva no coincide.");

            var reserva = await _context.Reservas
                .Include(r => r.Servicioreservas)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
                return NotFound("Reserva no encontrada.");

            // Validar que la reserva tenga al menos un servicio asignado
            if (reservaDTO.IdServicios == null || !reservaDTO.IdServicios.Any())
            {
                return BadRequest("La reserva debe tener al menos un servicio asignado.");
            }

            // Obtener los servicios para calcular la duración total
            var servicios = await _context.Servicios
                .Where(s => reservaDTO.IdServicios.Contains(s.IdServicio))
                .ToListAsync();

            if (servicios.Count != reservaDTO.IdServicios.Count)
            {
                return BadRequest("Algunos servicios no existen.");
            }

            int duracionTotal = servicios.Sum(s => s.Duracion); // Duración total en minutos

            // Calcular HoraFin
            TimeSpan horaInicio = reservaDTO.HoraInicio;
            TimeSpan horaFinCalculada = horaInicio.Add(TimeSpan.FromMinutes(duracionTotal));

            // Validar que HoraFin no exceda las 24 horas
            if (horaFinCalculada.TotalHours > 24)
            {
                return BadRequest("La hora de fin excede las 24 horas del día.");
            }

            // Validar que la hora seleccionada no esté ocupada
            bool horarioOcupado = await _context.Reservas
                .AnyAsync(r => r.IdEmpleado == reservaDTO.IdEmpleado &&
                               r.IdReserva != id &&
                               r.Fecha.Date == reservaDTO.Fecha.Date &&
                               ((horaInicio >= r.HoraInicio && horaInicio < r.HoraFin) ||
                                (horaFinCalculada > r.HoraInicio && horaFinCalculada <= r.HoraFin) ||
                                (horaInicio <= r.HoraInicio && horaFinCalculada >= r.HoraFin)));

            if (horarioOcupado)
                return Conflict("El horario seleccionado ya está ocupado.");

            // Actualizar campos básicos
            reserva.Fecha = reservaDTO.Fecha;
            reserva.HoraInicio = horaInicio;
            reserva.HoraFin = horaFinCalculada;
            reserva.IdCliente = reservaDTO.IdCliente;
            reserva.IdEmpleado = reservaDTO.IdEmpleado;
            reserva.EstadoReserva = reservaDTO.EstadoReserva;

            // Actualizar servicios asignados
            // Remover servicios que ya no están en la actualización
            var serviciosActuales = reserva.Servicioreservas.Select(sr => sr.IdServicio).ToList();
            var serviciosParaRemover = serviciosActuales.Except(reservaDTO.IdServicios).ToList();
            var serviciosParaAgregar = reservaDTO.IdServicios.Except(serviciosActuales).ToList();

            foreach (var servicioId in serviciosParaRemover)
            {
                var servicioReserva = await _context.Servicioreservas
                    .FirstOrDefaultAsync(sr => sr.IdReserva == id && sr.IdServicio == servicioId);
                if (servicioReserva != null)
                {
                    _context.Servicioreservas.Remove(servicioReserva);
                }
            }

            foreach (var servicioId in serviciosParaAgregar)
            {
                var servicioReserva = new Servicioreserva
                {
                    IdReserva = id,
                    IdServicio = servicioId
                };
                _context.Servicioreservas.Add(servicioReserva);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
                {
                    return NotFound("Reserva no encontrada durante la actualización.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Reserva/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Servicioreservas)
                .FirstOrDefaultAsync(r => r.IdReserva == id);

            if (reserva == null)
                return NotFound("Reserva no encontrada.");

            // Remover servicios asociados
            _context.Servicioreservas.RemoveRange(reserva.Servicioreservas);

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Reserva/{idReserva}/servicios/{idServicio}
        [HttpPost("{idReserva}/servicios/{idServicio}")]
        public async Task<IActionResult> AgregarServicioAReserva(int idReserva, int idServicio)
        {
            var reserva = await _context.Reservas.FindAsync(idReserva);
            if (reserva == null)
                return NotFound("Reserva no encontrada.");

            var servicio = await _context.Servicios.FindAsync(idServicio);
            if (servicio == null)
                return NotFound("Servicio no encontrado.");

            // Verificar si ya existe la relación
            var existe = await _context.Servicioreservas
                .AnyAsync(sr => sr.IdReserva == idReserva && sr.IdServicio == idServicio);

            if (existe)
                return Conflict("El servicio ya está asignado a esta reserva.");

            var servicioReserva = new Servicioreserva
            {
                IdReserva = idReserva,
                IdServicio = idServicio
            };

            _context.Servicioreservas.Add(servicioReserva);
            await _context.SaveChangesAsync();

            return Ok("Servicio asignado a la reserva correctamente.");
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }
    }
}

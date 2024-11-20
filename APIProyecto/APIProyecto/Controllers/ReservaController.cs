using APIProyecto.DTO;
using APIProyecto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoPeluqueria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Crea una nueva reserva.
        /// </summary>
        /// <param name="reservaInputDTO">Datos de la reserva.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("CrearReserva")]
        [Authorize] // Asegura que el usuario esté autenticado
        public async Task<ActionResult<ReservaDTO>> CrearReserva([FromBody] ReservaInputDTO reservaInputDTO)
        {
            if (reservaInputDTO == null)
            {
                return BadRequest("Datos de reserva inválidos.");
            }

            // Obtener el email del usuario autenticado
            var emailClaim = User.FindFirst(ClaimTypes.Email);
            if (emailClaim == null)
            {
                return Unauthorized("El usuario no está autenticado.");
            }

            var email = emailClaim.Value;

            // Buscar la persona en la base de datos usando el email
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.Email == email);
            if (persona == null)
            {
                return Unauthorized("El usuario no está registrado.");
            }

            // Verificar si la persona es un cliente
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.IdPersona == persona.IdPersona);
            if (cliente == null)
            {
                return Unauthorized("Solo los clientes pueden realizar reservas.");
            }

            // Validar servicios seleccionados
            if (reservaInputDTO.Servicios == null || !reservaInputDTO.Servicios.Any())
            {
                return BadRequest("Debe seleccionar al menos un servicio.");
            }

            var servicios = await _context.Servicios
                .Where(s => reservaInputDTO.Servicios.Contains(s.IdServicio))
                .ToListAsync();

            if (servicios.Count != reservaInputDTO.Servicios.Count)
            {
                return NotFound("Uno o más servicios no encontrados.");
            }

            // Calcular la duración total de los servicios seleccionados
            int duracionTotal = servicios.Sum(s => s.Duracion);

            // Calcular la hora de finalización en base a la hora de inicio y la duración total
            if (!reservaInputDTO.HoraInicio.HasValue)
            {
                return BadRequest("Debe proporcionar una hora de inicio.");
            }

            var horaInicio = reservaInputDTO.HoraInicio.Value;
            var horaFinCalculada = horaInicio + TimeSpan.FromMinutes(duracionTotal);

            // Determinar el día de la semana (1=Lunes, ..., 7=Domingo)
            var diaSemana = ((int)reservaInputDTO.Fecha.DayOfWeek == 0) ? 7 : (int)reservaInputDTO.Fecha.DayOfWeek;

            // Verificar disponibilidad del empleado en el horario seleccionado
            var conflicto = await _context.Reservas
                .AnyAsync(r => r.IdEmpleado == reservaInputDTO.IdEmpleado &&
                               r.Fecha.Date == reservaInputDTO.Fecha.Date &&
                               r.HoraInicio < horaFinCalculada &&
                               r.HoraFin > horaInicio &&
                               r.EstadoReserva != "Cancelada");

            if (conflicto)
            {
                return BadRequest("El horario seleccionado no está disponible.");
            }

            // Verificar que el horario está dentro de los horarios de atención del empleado
            var horariosEmpleado = await _context.Empleadohorarios
                .Include(eh => eh.IdHorarioNavigation)
                .Where(eh => eh.IdEmpleado == reservaInputDTO.IdEmpleado &&
                             eh.DiaSemana == diaSemana &&
                             eh.FechaInicio.Date <= reservaInputDTO.Fecha.Date &&
                             (eh.FechaFin == null || eh.FechaFin.Value.Date >= reservaInputDTO.Fecha.Date) &&
                             eh.Disponible)
                .ToListAsync();

            if (!horariosEmpleado.Any())
            {
                return BadRequest("El empleado no tiene horarios disponibles en la fecha seleccionada.");
            }

            bool dentroHorarioAtencion = horariosEmpleado.Any(eh =>
                horaInicio >= eh.IdHorarioNavigation.HoraInicio &&
                horaFinCalculada <= eh.IdHorarioNavigation.HoraFin
            );

            if (!dentroHorarioAtencion)
            {
                return BadRequest("El horario seleccionado está fuera del horario de atención del empleado.");
            }

            // Crear la reserva
            var nuevaReserva = new Reserva
            {
                Fecha = reservaInputDTO.Fecha.Date,
                HoraInicio = horaInicio,
                HoraFin = horaFinCalculada,
                IdCliente = cliente.IdCliente,
                IdEmpleado = reservaInputDTO.IdEmpleado,
                EstadoReserva = reservaInputDTO.EstadoReserva ?? "Pendiente"
            };

            _context.Reservas.Add(nuevaReserva);
            await _context.SaveChangesAsync();

            // Asociar los servicios a la reserva
            var servicioReservas = reservaInputDTO.Servicios.Select(idServicio => new Servicioreserva
            {
                IdReserva = nuevaReserva.IdReserva,
                IdServicio = idServicio
            });

            _context.Servicioreservas.AddRange(servicioReservas);
            await _context.SaveChangesAsync();

            // Devolver la reserva creada en formato ReservaDTO
            var reservaDTO = new ReservaDTO
            {
                IdReserva = nuevaReserva.IdReserva,
                Fecha = nuevaReserva.Fecha,
                HoraInicio = nuevaReserva.HoraInicio,
                HoraFin = nuevaReserva.HoraFin,
                IdEmpleado = nuevaReserva.IdEmpleado,
                EstadoReserva = nuevaReserva.EstadoReserva,
                Servicios = reservaInputDTO.Servicios
            };

            return CreatedAtAction(nameof(CrearReserva), new { id = nuevaReserva.IdReserva }, reservaDTO);
        }

        // Otros métodos del controlador (ObtenerReservasPorCliente, CancelarReserva, etc.) pueden añadirse aquí
    }
}

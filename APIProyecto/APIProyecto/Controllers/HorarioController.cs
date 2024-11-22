// Controllers/HorarioController.cs
using APIProyecto.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HorarioController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene los horarios disponibles para un empleado en una fecha específica, considerando la duración total de los servicios.
        /// </summary>
        /// <param name="idEmpleado">ID del empleado.</param>
        /// <param name="fecha">Fecha para la cual se buscan horarios.</param>
        /// <param name="duracionTotal">Duración total requerida en minutos.</param>
        /// <returns>Lista de horarios disponibles.</returns>
        [HttpGet("Disponibilidad")]
        public async Task<ActionResult<IEnumerable<HorarioDisponibleDTO>>> GetHorariosDisponibles(int idEmpleado, DateTime fecha, int duracionTotal)
        {
            // Validar horarios asignados al empleado
            var horariosAsignados = await _context.Empleadohorarios
                .Where(eh => eh.IdEmpleado == idEmpleado &&
                             eh.FechaInicio.Date <= fecha.Date &&
                             (eh.FechaFin == null || eh.FechaFin >= fecha.Date) &&
                             eh.Disponible == true)
                .Include(eh => eh.IdHorarioNavigation)
                .ToListAsync();

            if (horariosAsignados == null || horariosAsignados.Count == 0)
            {
                return NotFound("No hay horarios asignados para este empleado en la fecha seleccionada.");
            }

            // Obtener todas las reservas para el empleado en la fecha
            var reservas = await _context.Reservas
                .Where(r => r.IdEmpleado == idEmpleado && r.Fecha.Date == fecha.Date)
                .Select(r => new
                {
                    HoraInicio = new TimeSpan(r.HoraInicio.Hours, r.HoraInicio.Minutes, 0),
                    HoraFin = new TimeSpan(r.HoraFin.Hours, r.HoraFin.Minutes, 0)
                })
                .ToListAsync();

            var horariosDisponibles = new List<HorarioDisponibleDTO>();

            // Generar bloques disponibles considerando la duración total
            foreach (var horario in horariosAsignados)
            {
                var horaInicio = new TimeSpan(horario.IdHorarioNavigation.HoraInicio.Hours, horario.IdHorarioNavigation.HoraInicio.Minutes, 0);
                var horaFin = new TimeSpan(horario.IdHorarioNavigation.HoraFin.Hours, horario.IdHorarioNavigation.HoraFin.Minutes, 0);

                var bloquesHorario = GenerarBloquesHorario(horaInicio, horaFin, duracionTotal);

                foreach (var bloque in bloquesHorario)
                {
                    // Verificar si el bloque está reservado
                    var reservado = reservas.Any(r =>
                        (r.HoraInicio < bloque.Fin && r.HoraFin > bloque.Inicio));

                    if (!reservado)
                    {
                        horariosDisponibles.Add(new HorarioDisponibleDTO
                        {
                            HoraInicio = bloque.Inicio,
                            HoraFin = bloque.Fin,
                            Disponible = true
                        });
                    }
                }
            }

            if (!horariosDisponibles.Any())
            {
                return NotFound("No hay horarios disponibles para este empleado en la fecha seleccionada que puedan acomodar la duración requerida.");
            }

            return Ok(horariosDisponibles.OrderBy(h => h.HoraInicio));
        }

        // Método auxiliar para generar bloques considerando la duración total
        private List<(TimeSpan Inicio, TimeSpan Fin)> GenerarBloquesHorario(TimeSpan horaInicio, TimeSpan horaFin, int duracionTotal)
        {
            var bloques = new List<(TimeSpan Inicio, TimeSpan Fin)>();

            var tiempoDuracion = TimeSpan.FromMinutes(duracionTotal);
            var bloqueInicio = horaInicio;

            while (bloqueInicio + tiempoDuracion <= horaFin)
            {
                var bloqueFin = bloqueInicio + tiempoDuracion;

                bloques.Add((bloqueInicio, bloqueFin));

                // Avanzar en intervalos de 30 minutos
                bloqueInicio = bloqueInicio.Add(TimeSpan.FromMinutes(30));
            }

            return bloques;
        }

        // Método auxiliar para generar bloques considerando la duración total
        
        [HttpGet("HorariosAsignados")]
        public async Task<ActionResult<IEnumerable<EmpleadoHorarioDTO>>> GetHorariosAsignados(int idEmpleado, DateTime fecha)
        {
            var horarios = await _context.Empleadohorarios
                .Where(eh => eh.IdEmpleado == idEmpleado &&
                             eh.FechaInicio <= fecha &&
                             (eh.FechaFin == null || eh.FechaFin >= fecha))
                .Include(eh => eh.IdHorarioNavigation) // Incluir detalles del horario
                .Select(eh => new EmpleadoHorarioDTO
                {
                    IdEmpleado = eh.IdEmpleado,
                    IdHorario = eh.IdHorario,
                    FechaInicio = eh.FechaInicio,
                    FechaFin = eh.FechaFin,
                    DiaSemana = eh.DiaSemana,
                    Disponible = eh.Disponible
                })
                .ToListAsync();

            if (!horarios.Any())
            {
                return NotFound("No hay horarios asignados para este empleado en la fecha especificada.");
            }

            return Ok(horarios);
        }


        [HttpPost("AsignarHorarios")]
        public async Task<IActionResult> AsignarHorario([FromBody] List<EmpleadoHorarioDTO> horariosDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mensajes = new List<string>();

            foreach (var dto in horariosDTO)
            {
                try
                {
                    // Validar existencia de empleado
                    var empleadoExiste = await _context.Empleados.AnyAsync(e => e.IdEmpleado == dto.IdEmpleado);
                    if (!empleadoExiste)
                    {
                        mensajes.Add($"Empleado con ID {dto.IdEmpleado} no encontrado.");
                        continue;
                    }

                    // Validar existencia de horario
                    var horarioExiste = await _context.Horarioatencions.AnyAsync(h => h.IdHorario == dto.IdHorario);
                    if (!horarioExiste)
                    {
                        mensajes.Add($"Horario con ID {dto.IdHorario} no encontrado.");
                        continue;
                    }

                    // Verificar duplicados
                    var horarioAsignado = await _context.Empleadohorarios.AnyAsync(eh =>
                        eh.IdEmpleado == dto.IdEmpleado &&
                        eh.IdHorario == dto.IdHorario &&
                        eh.FechaInicio.Date == dto.FechaInicio.Date);

                    if (horarioAsignado)
                    {
                        mensajes.Add($"El horario con ID {dto.IdHorario} ya está asignado al empleado con ID {dto.IdEmpleado} para la fecha {dto.FechaInicio:yyyy-MM-dd}.");
                        continue;
                    }

                    // Crear nuevo registro
                    var empleadoHorario = new Empleadohorario
                    {
                        IdEmpleado = dto.IdEmpleado,
                        IdHorario = dto.IdHorario,
                        FechaInicio = dto.FechaInicio,
                        FechaFin = dto.FechaFin,
                        DiaSemana = dto.DiaSemana,
                        Disponible = dto.Disponible
                    };

                    _context.Empleadohorarios.Add(empleadoHorario);
                    await _context.SaveChangesAsync();

                    mensajes.Add($"Horario con ID {dto.IdHorario} asignado al empleado con ID {dto.IdEmpleado} para la fecha {dto.FechaInicio:yyyy-MM-dd}.");
                }
                catch (Exception ex)
                {
                    mensajes.Add($"Error al asignar horario con ID {dto.IdHorario} al empleado con ID {dto.IdEmpleado}: {ex.Message}");
                }
            }

            return Ok(mensajes);
        }

    }
}
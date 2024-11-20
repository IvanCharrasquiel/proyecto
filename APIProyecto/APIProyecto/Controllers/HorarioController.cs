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
        /// Obtiene los horarios disponibles para un empleado en una fecha específica.
        /// </summary>
        /// <param name="idEmpleado">ID del empleado.</param>
        /// <param name="fecha">Fecha para la cual se buscan horarios.</param>
        /// <returns>Lista de horarios disponibles.</returns>
        [HttpGet("Disponibilidad")]
        public async Task<ActionResult<IEnumerable<HorarioDisponibleDTO>>> GetHorariosDisponibles(int idEmpleado, DateTime fecha)
        {
            // Validar horarios asignados al empleado
            var horariosAsignados = await _context.Empleadohorarios
                .Where(eh => eh.IdEmpleado == idEmpleado &&
                             eh.FechaInicio <= fecha &&
                             (eh.FechaFin == null || eh.FechaFin >= fecha) &&
                             eh.Disponible == true)
                .Include(eh => eh.IdHorarioNavigation)
                .ToListAsync();

            if (!horariosAsignados.Any())
            {
                return NotFound("No hay horarios asignados para este empleado en la fecha seleccionada.");
            }

            // Obtener todas las reservas para el empleado en la fecha
            var reservas = await _context.Reservas
                .Where(r => r.IdEmpleado == idEmpleado && r.Fecha == fecha.Date)
                .ToListAsync();

            var horariosDisponibles = new List<HorarioDisponibleDTO>();

            // Generar bloques de 30 minutos dentro de cada horario asignado
            foreach (var horario in horariosAsignados)
            {
                var horaInicio = horario.IdHorarioNavigation.HoraInicio;
                var horaFin = horario.IdHorarioNavigation.HoraFin;

                while (horaInicio < horaFin)
                {
                    var bloqueFin = horaInicio.Add(TimeSpan.FromMinutes(30));

                    // Verificar si el bloque está reservado
                    var reservado = reservas.Any(r =>
                        (r.HoraInicio >= horaInicio && r.HoraInicio < bloqueFin) || // Inicio dentro del bloque
                        (r.HoraFin > horaInicio && r.HoraFin <= bloqueFin) ||       // Fin dentro del bloque
                        (r.HoraInicio <= horaInicio && r.HoraFin >= bloqueFin));   // Bloque englobado

                    if (!reservado)
                    {
                        horariosDisponibles.Add(new HorarioDisponibleDTO
                        {
                            HoraInicio = horaInicio,
                            HoraFin = bloqueFin,
                            Disponible = true
                        });
                    }

                    horaInicio = bloqueFin; // Avanzar al siguiente bloque
                }
            }

            if (!horariosDisponibles.Any())
            {
                return NotFound("No hay horarios disponibles para este empleado en la fecha seleccionada.");
            }

            return Ok(horariosDisponibles);
        }


        // Método auxiliar para dividir horarios en bloques
        private List<(TimeSpan Inicio, TimeSpan Fin)> GenerarBloquesHorario(TimeSpan horaInicio, TimeSpan horaFin)
        {
            var bloques = new List<(TimeSpan Inicio, TimeSpan Fin)>();
            var bloqueInicio = horaInicio;

            while (bloqueInicio < horaFin)
            {
                var bloqueFin = bloqueInicio + TimeSpan.FromMinutes(30); // Bloques de 30 minutos
                if (bloqueFin > horaFin)
                {
                    bloqueFin = horaFin;
                }
                bloques.Add((bloqueInicio, bloqueFin));
                bloqueInicio = bloqueFin;
            }

            return bloques;
        }


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
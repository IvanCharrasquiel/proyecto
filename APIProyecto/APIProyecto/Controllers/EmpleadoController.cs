﻿// Controllers/EmpleadoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BCrypt.Net;
using System.IO;
using System.Linq;

namespace APIProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpleadoController(AppDbContext context)
        {
            _context = context;
        }

        #region CRUD Básico

        // GET: api/Empleado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoDTO>>> GetEmpleados()
        {
            var empleados = await _context.Empleados
                .Where(e => e.IdPersonaNavigation != null) // Filtrar empleados con Persona asociada
                .Select(e => new EmpleadoDTO
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
                    } : null // Manejar posibles valores nulos en Cargo
                })
                .ToListAsync();

            return Ok(empleados);
        }

        // GET: api/Empleado/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoDTO>> GetEmpleado(int id)
        {
            var empleado = await _context.Empleados
                .Include(e => e.IdPersonaNavigation)
                .Include(e => e.IdCargoNavigation)
                .FirstOrDefaultAsync(e => e.IdEmpleado == id);

            if (empleado == null)
                return NotFound("Empleado no encontrado.");

            var empleadoDTO = new EmpleadoDTO
            {
                IdEmpleado = empleado.IdEmpleado,
                FechaContrato = empleado.FechaContrato,
                Comision = empleado.Comision,
                IdPersona = empleado.IdPersona,
                IdCargo = empleado.IdCargo,
                Persona = new PersonaDTO
                {
                    IdPersona = empleado.IdPersonaNavigation.IdPersona,
                    Cedula = empleado.IdPersonaNavigation.Cedula,
                    Nombre = empleado.IdPersonaNavigation.Nombre,
                    Apellido = empleado.IdPersonaNavigation.Apellido,
                    Telefono = empleado.IdPersonaNavigation.Telefono,
                    Email = empleado.IdPersonaNavigation.Email,
                    Direccion = empleado.IdPersonaNavigation.Direccion,
                    FotoPerfil = empleado.IdPersonaNavigation.FotoPerfil
                },
                Cargo = empleado.IdCargoNavigation != null ? new CargoDTO
                {
                    IdCargo = empleado.IdCargoNavigation.IdCargo,
                    NombreCargo = empleado.IdCargoNavigation.NombreCargo,
                } : null
            };

            return Ok(empleadoDTO);
        }

        // POST: api/Empleado/Register
        [HttpPost("Register")]
        public async Task<ActionResult<EmpleadoDTO>> RegisterEmpleado(EmpleadoRegistroDTO empleadoRegistroDTO)
        {
            // Validar el DTO
            if (empleadoRegistroDTO == null || empleadoRegistroDTO.Persona == null)
            {
                return BadRequest("Datos de registro inválidos.");
            }

            // Verificar si el email ya está registrado
            var personaExistente = await _context.Personas.FirstOrDefaultAsync(p => p.Email == empleadoRegistroDTO.Persona.Email);
            if (personaExistente != null)
            {
                return Conflict("El email ya está registrado.");
            }

            // Crear la entidad Persona
            var persona = new Persona
            {
                Cedula = empleadoRegistroDTO.Persona.Cedula,
                Nombre = empleadoRegistroDTO.Persona.Nombre,
                Apellido = empleadoRegistroDTO.Persona.Apellido,
                Telefono = empleadoRegistroDTO.Persona.Telefono,
                Email = empleadoRegistroDTO.Persona.Email,
                Direccion = empleadoRegistroDTO.Persona.Direccion,
                FotoPerfil = empleadoRegistroDTO.Persona.FotoPerfil
            };

            // Agregar Persona al contexto
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            // Crear la entidad Empleado
            var empleado = new Empleado
            {
                IdPersona = persona.IdPersona,
                FechaContrato = DateTime.Now,
                Comision = empleadoRegistroDTO.Comision,
                IdCargo = empleadoRegistroDTO.IdCargo,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(empleadoRegistroDTO.Contraseña) // Usar hash para la contraseña
            };

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            // Preparar el DTO de respuesta
            var empleadoDTO = new EmpleadoDTO
            {
                IdEmpleado = empleado.IdEmpleado,
                FechaContrato = empleado.FechaContrato,
                Comision = empleado.Comision,
                IdPersona = empleado.IdPersona,
                IdCargo = empleado.IdCargo,
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
                },
                Cargo = empleado.IdCargoNavigation != null ? new CargoDTO
                {
                    IdCargo = empleado.IdCargoNavigation.IdCargo,
                    NombreCargo = empleado.IdCargoNavigation.NombreCargo
                } : null
            };

            return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.IdEmpleado }, empleadoDTO);
        }

        // PUT: api/Empleado/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpleado(int id, EmpleadoDTO empleadoDTO)
        {
            if (id != empleadoDTO.IdEmpleado)
                return BadRequest("El ID del empleado no coincide.");

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
                return NotFound("Empleado no encontrado.");

            // Actualizar campos
            empleado.IdPersona = empleadoDTO.IdPersona;
            empleado.FechaContrato = empleadoDTO.FechaContrato;
            empleado.Comision = empleadoDTO.Comision;
            empleado.IdCargo = empleadoDTO.IdCargo;

            // Verificar si la persona existe
            var persona = await _context.Personas.FindAsync(empleadoDTO.IdPersona);
            if (persona == null)
                return BadRequest("La persona asociada no existe.");

            // Actualizar la entidad Persona si es necesario
            persona.Cedula = empleadoDTO.Persona.Cedula;
            persona.Nombre = empleadoDTO.Persona.Nombre;
            persona.Apellido = empleadoDTO.Persona.Apellido;
            persona.Telefono = empleadoDTO.Persona.Telefono;
            persona.Email = empleadoDTO.Persona.Email;
            persona.Direccion = empleadoDTO.Persona.Direccion;
            persona.FotoPerfil = empleadoDTO.Persona.FotoPerfil;

            try
            {
                _context.Entry(empleado).State = EntityState.Modified;
                _context.Entry(persona).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error al actualizar el empleado.");
            }

            return NoContent();
        }

        // DELETE: api/Empleado/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
                return NotFound("Empleado no encontrado.");

            // Opcional: Eliminar las reservas asociadas o manejar de otra manera
            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Asignación de Servicios

        // POST: api/Empleado/{id}/Servicios
        [HttpPost("{id}/Servicios")]
        public async Task<IActionResult> AsignarServicios(int id, [FromBody] List<int> idServicios)
        {
            if (idServicios == null || !idServicios.Any())
                return BadRequest("No se proporcionaron servicios para asignar.");

            var empleado = await _context.Empleados
                .Include(e => e.IdServicios)
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
                if (!empleado.IdServicios.Contains(servicio))
                {
                    empleado.IdServicios.Add(servicio);
                }
            }

            await _context.SaveChangesAsync();

            return Ok("Servicios asignados correctamente.");
        }

        // GET: api/Empleado/{id}/Servicios
        [HttpGet("{id}/Servicios")]
        public async Task<ActionResult<IEnumerable<ServicioDTO>>> GetServiciosEmpleado(int id)
        {
            var empleado = await _context.Empleados
                .Include(e => e.IdServicios)
                .FirstOrDefaultAsync(e => e.IdEmpleado == id);

            if (empleado == null)
                return NotFound("Empleado no encontrado.");

            var serviciosDTO = empleado.IdServicios.Select(s => new ServicioDTO
            {
                IdServicio = s.IdServicio,
                NombreServicio = s.NombreServicio,
                Descripcion = s.Descripcion,
                Duracion = s.Duracion
            }).ToList();

            return Ok(serviciosDTO);
        }

        // DELETE: api/Empleado/{id}/Servicios/{idServicio}
        [HttpDelete("{id}/Servicios/{idServicio}")]
        public async Task<IActionResult> RemoverServicio(int id, int idServicio)
        {
            var empleado = await _context.Empleados
                .Include(e => e.IdServicios)
                .FirstOrDefaultAsync(e => e.IdEmpleado == id);

            if (empleado == null)
                return NotFound("Empleado no encontrado.");

            var servicio = empleado.IdServicios.FirstOrDefault(s => s.IdServicio == idServicio);
            if (servicio == null)
                return NotFound("El servicio no está asignado al empleado.");

            empleado.IdServicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return Ok("Servicio removido correctamente.");
        }

        #endregion

        #region Asignación de Horarios

        // POST: api/Empleado/{id}/Horarios
        [HttpPost("{id}/Horarios")]
        public async Task<IActionResult> AsignarHorarios(int id, [FromBody] List<Empleadohorario> horariosDTO)
        {
            if (horariosDTO == null || !horariosDTO.Any())
                return BadRequest("No se proporcionaron horarios para asignar.");

            var empleado = await _context.Empleados
                .Include(e => e.Empleadohorarios)
                .FirstOrDefaultAsync(e => e.IdEmpleado == id);

            if (empleado == null)
                return NotFound("Empleado no encontrado.");

            foreach (var horarioDTO in horariosDTO)
            {
                var existeHorario = await _context.Horarioatencions.FindAsync(horarioDTO.IdHorario);
                if (existeHorario == null)
                    return BadRequest($"El horario con ID {horarioDTO.IdHorario} no existe.");

                var existeRelacion = empleado.Empleadohorarios
                    .Any(eh => eh.IdHorario == horarioDTO.IdHorario && eh.Fecha.Date == horarioDTO.Fecha.Date);

                if (!existeRelacion)
                {
                    empleado.Empleadohorarios.Add(new Empleadohorario
                    {
                        IdEmpleado = id,
                        IdHorario = horarioDTO.IdHorario,
                        Fecha = horarioDTO.Fecha
                    });
                }
            }

            await _context.SaveChangesAsync();

            return Ok("Horarios asignados correctamente.");
        }

        #endregion

        #region Gestión de Contraseña

        // PATCH: api/Empleado/{id}/ActualizarContraseña
        [HttpPatch("{id}/ActualizarContraseña")]
        public async Task<IActionResult> ActualizarContraseña(int id, [FromBody] ActualizarContraseñaDTO actualizarContraseñaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
                return NotFound("Empleado no encontrado.");

            // Verificar si el usuario autenticado es el propio empleado o un administrador
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuarioRol = User.FindFirstValue(ClaimTypes.Role);

            if (usuarioRol != "Administrador" && empleado.IdPersona.ToString() != usuarioId)
                return Forbid();

            // Verificar la contraseña actual
            bool esContraseñaValida = BCrypt.Net.BCrypt.Verify(actualizarContraseñaDTO.ContraseñaActual, empleado.Contraseña);
            if (!esContraseñaValida)
                return Unauthorized("La contraseña actual es incorrecta.");

            // Hashear la nueva contraseña
            empleado.Contraseña = BCrypt.Net.BCrypt.HashPassword(actualizarContraseñaDTO.NuevaContraseña);

            _context.Entry(empleado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Contraseña actualizada correctamente.");
        }

        #endregion

        #region Perfil y Foto de Perfil

        // GET: api/Empleado/Perfil
        [HttpGet("Perfil")]
        public async Task<ActionResult<EmpleadoDTO>> GetPerfil()
        {
            var idPersonaStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idPersonaStr, out int idPersona))
                return BadRequest("ID de persona inválido.");

            var empleado = await _context.Empleados
                .Include(e => e.IdPersonaNavigation)
                .Include(e => e.IdCargoNavigation)
                .FirstOrDefaultAsync(e => e.IdPersona == idPersona);

            if (empleado == null)
                return NotFound("Empleado no encontrado.");

            var empleadoDTO = new EmpleadoDTO
            {
                IdEmpleado = empleado.IdEmpleado,
                FechaContrato = empleado.FechaContrato,
                Comision = empleado.Comision,
                IdPersona = empleado.IdPersona,
                IdCargo = empleado.IdCargo,
                Persona = new PersonaDTO
                {
                    IdPersona = empleado.IdPersonaNavigation.IdPersona,
                    Cedula = empleado.IdPersonaNavigation.Cedula,
                    Nombre = empleado.IdPersonaNavigation.Nombre,
                    Apellido = empleado.IdPersonaNavigation.Apellido,
                    Telefono = empleado.IdPersonaNavigation.Telefono,
                    Email = empleado.IdPersonaNavigation.Email,
                    Direccion = empleado.IdPersonaNavigation.Direccion,
                    FotoPerfil = empleado.IdPersonaNavigation.FotoPerfil
                },
                Cargo = empleado.IdCargoNavigation != null ? new CargoDTO
                {
                    IdCargo = empleado.IdCargoNavigation.IdCargo,
                    NombreCargo = empleado.IdCargoNavigation.NombreCargo
                } : null
            };

            return Ok(empleadoDTO);
        }

        // POST: api/Empleado/{id}/FotoPerfil
        [HttpPost("{id}/FotoPerfil")]
        public async Task<IActionResult> UploadFotoPerfil(int id, IFormFile foto)
        {
            var empleado = await _context.Empleados
                .Include(e => e.IdPersonaNavigation)
                .FirstOrDefaultAsync(e => e.IdEmpleado == id);

            if (empleado == null)
                return NotFound("Empleado no encontrado.");

            // Verificar si el usuario autenticado es el propio empleado o un administrador
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuarioRol = User.FindFirstValue(ClaimTypes.Role);

            if (usuarioRol != "Administrador" && empleado.IdPersona.ToString() != usuarioId)
                return Forbid();

            if (foto == null || foto.Length == 0)
                return BadRequest("No se ha proporcionado una foto válida.");

            // Guardar la foto en el servidor o en un servicio de almacenamiento
            var rutaFoto = await GuardarFotoAsync(foto);

            // Actualizar la ruta de la foto en la entidad Persona
            empleado.IdPersonaNavigation.FotoPerfil = rutaFoto;

            await _context.SaveChangesAsync();

            return Ok(new { Mensaje = "Foto de perfil actualizada correctamente." });
        }

        private async Task<string> GuardarFotoAsync(IFormFile foto)
        {
            // Define la carpeta donde se guardarán las fotos
            var carpetaFotos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fotos");
            if (!Directory.Exists(carpetaFotos))
            {
                Directory.CreateDirectory(carpetaFotos);
            }

            // Genera un nombre único para la foto
            var nombreArchivo = $"{Guid.NewGuid()}_{Path.GetFileName(foto.FileName)}";
            var rutaCompleta = Path.Combine(carpetaFotos, nombreArchivo);

            // Guarda la foto en la carpeta
            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            // Retorna la ruta relativa
            return $"/fotos/{nombreArchivo}";
        }

        #endregion

        #region Asignación de Reservas

        // GET: api/Empleado/{id}/Reservas
        [HttpGet("{id}/Reservas")]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservas(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
                return NotFound("Empleado no encontrado.");

            var reservas = await _context.Reservas
                .Where(r => r.IdEmpleado == id)
                .Select(r => new ReservaDTO
                {
                    IdReserva = r.IdReserva,
                    Fecha = r.Fecha,
                    HoraInicio = r.HoraInicio,
                    HoraFin = r.HoraFin,
                    EstadoReserva = r.EstadoReserva
                })
                .ToListAsync();

            return Ok(reservas);
        }

        #endregion

        #region Obtención de Horarios Disponibles

        // GET: api/Empleado/{idEmpleado}/HorariosDisponibles?fecha=yyyy-MM-dd&idServicio={idServicio}
        [HttpGet("{idEmpleado}/HorariosDisponibles")]
        [AllowAnonymous] // Permitir acceso público si es necesario
        public async Task<ActionResult<IEnumerable<HorarioAtencionDTO>>> GetHorariosDisponibles(int idEmpleado, [FromQuery] DateTime fecha, [FromQuery] int idServicio)
        {
            // 1. Verificar si el Empleado existe
            var empleado = await _context.Empleados
                .Include(e => e.Empleadohorarios)
                    .ThenInclude(eh => eh.IdHorarioNavigation)
                .FirstOrDefaultAsync(e => e.IdEmpleado == idEmpleado);

            if (empleado == null)
            {
                return NotFound("Empleado no encontrado.");
            }

            // 2. Verificar si el Servicio existe
            var servicio = await _context.Servicios.FindAsync(idServicio);
            if (servicio == null)
            {
                return NotFound("Servicio no encontrado.");
            }

            TimeSpan duracionServicio = TimeSpan.FromMinutes(servicio.Duracion);

            // 3. Obtener todos los horarios asignados al empleado para la fecha especificada
            var horariosAsignados = empleado.Empleadohorarios
                .Where(eh => eh.Fecha.Date == fecha.Date)
                .Select(eh => eh.IdHorarioNavigation)
                .Where(h => h.HoraInicio != null && h.HoraFin != null) // Opcional, ya que son no nullable
                .ToList();

            if (!horariosAsignados.Any())
            {
                return NotFound("El empleado no tiene horarios asignados para esta fecha.");
            }

            // 4. Obtener todas las reservas del empleado para la fecha y servicio especificados
            // Dado que Reserva no tiene IdServicio directamente, debemos acceder a través de Servicioreserva
            var reservasDelServicio = await _context.Reservas
                .Where(r => r.IdEmpleado == idEmpleado && r.Fecha.Date == fecha.Date)
                .Include(r => r.Servicioreservas)
                    .ThenInclude(sr => sr.IdServicioNavigation)
                .Where(r => r.Servicioreservas.Any(sr => sr.IdServicio == idServicio))
                .SelectMany(r => r.Servicioreservas.Where(sr => sr.IdServicio == idServicio))
                .Select(sr => new
                {
                    Inicio = sr.IdReservaNavigation.HoraInicio,
                    Fin = sr.IdReservaNavigation.HoraFin
                })
                .ToListAsync();

            // 5. Calcular los horarios disponibles
            var horariosDisponibles = new List<HorarioAtencionDTO>();

            foreach (var horario in horariosAsignados)
            {
                // Hora de inicio y fin del horario
                TimeSpan horarioInicio = horario.HoraInicio;
                TimeSpan horarioFin = horario.HoraFin;

                // Crear una lista de intervalos ocupados
                var intervalosOcupados = reservasDelServicio
                    .Select(r => new
                    {
                        Inicio = r.Inicio,
                        Fin = r.Fin
                    })
                    .OrderBy(r => r.Inicio)
                    .ToList();

                // Generar intervalos disponibles dentro del horario
                var intervalosDisponibles = new List<(TimeSpan Inicio, TimeSpan Fin)>();

                TimeSpan currentStart = horarioInicio;

                foreach (var reserva in intervalosOcupados)
                {
                    if (reserva.Inicio > currentStart)
                    {
                        var disponibleFin = reserva.Inicio;
                        intervalosDisponibles.Add((currentStart, disponibleFin));
                    }

                    if (reserva.Fin > currentStart)
                    {
                        currentStart = reserva.Fin;
                    }
                }

                // Agregar el último intervalo si hay tiempo restante
                if (currentStart + duracionServicio <= horarioFin)
                {
                    var disponibleFin = horarioFin;
                    intervalosDisponibles.Add((currentStart, disponibleFin));
                }

                // Filtrar los intervalos disponibles que pueden acomodar la duración del servicio
                foreach (var intervalo in intervalosDisponibles)
                {
                    TimeSpan disponibleInicio = intervalo.Inicio;
                    TimeSpan disponibleFin = intervalo.Fin;

                    while (disponibleInicio + duracionServicio <= disponibleFin)
                    {
                        horariosDisponibles.Add(new HorarioAtencionDTO
                        {
                            IdHorario = horario.IdHorario,
                            HoraInicio = disponibleInicio,
                            HoraFin = disponibleInicio + duracionServicio
                        });

                        // Incrementar el inicio para evitar solapamientos
                        disponibleInicio += duracionServicio;
                    }
                }
            }

            // Eliminar duplicados y ordenar los horarios disponibles
            var horariosDisponiblesUnicos = horariosDisponibles
                .GroupBy(h => new { h.HoraInicio, h.HoraFin })
                .Select(g => g.First())
                .OrderBy(h => h.HoraInicio)
                .ToList();

            if (!horariosDisponiblesUnicos.Any())
            {
                return NotFound("No hay horarios disponibles para este empleado en la fecha y servicio especificados.");
            }

            return Ok(horariosDisponiblesUnicos);
        }

        #endregion
    }
}
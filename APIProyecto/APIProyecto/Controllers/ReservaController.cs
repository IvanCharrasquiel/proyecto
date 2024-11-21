// Controllers/ReservaController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace APIProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservaController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Reserva
        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] ReservaDTO reservaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar si el cliente existe en la base de datos
            var cliente = await _context.Clientes
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(c => c.IdCliente == reservaDto.IdCliente);

            if (cliente == null)
                return NotFound(new { Message = "Cliente no encontrado." });

            // Validar disponibilidad del empleado en el horario seleccionado
            var reservasExistentes = await _context.Reservas
                .Where(r => r.IdEmpleado == reservaDto.IdEmpleado && r.Fecha == reservaDto.Fecha)
                .ToListAsync();

            bool hayConflicto = reservasExistentes.Any(r =>
                (reservaDto.HoraInicio >= r.HoraInicio && reservaDto.HoraInicio < r.HoraFin) ||
                (reservaDto.HoraFin > r.HoraInicio && reservaDto.HoraFin <= r.HoraFin));

            if (hayConflicto)
                return BadRequest(new { Message = "El horario seleccionado no está disponible." });

            
            // Crear la reserva
            var reserva = new Reserva
            {
                Fecha = reservaDto.Fecha,
                HoraInicio = reservaDto.HoraInicio,
                HoraFin = reservaDto.HoraFin,
                IdCliente = reservaDto.IdCliente,
                IdEmpleado = reservaDto.IdEmpleado,
                EstadoReserva = "Pendiente"
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            // Asociar servicios a la reserva
            foreach (var idServicio in reservaDto.IdServicios)
            {
                _context.Servicioreservas.Add(new Servicioreserva
                {
                    IdReserva = reserva.IdReserva,
                    IdServicio = idServicio
                });
                await _context.SaveChangesAsync();
            }

            return Ok(new { Message = "Reserva creada exitosamente." });
        }


        // GET: api/Reserva/Cliente/{idCliente}
        [HttpGet("Cliente/{idCliente}")]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservasPorCliente(int idCliente)
        {
            // Validar que el cliente es el dueño del perfil
            var emailCliente = User.FindFirstValue(ClaimTypes.Email);
            var cliente = await _context.Clientes
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(c => c.IdCliente == idCliente && c.IdPersonaNavigation.Email == emailCliente);

            if (cliente == null)
                return Unauthorized("No tienes permiso para ver esta información.");

            var reservas = await _context.Reservas
                .Where(r => r.IdCliente == idCliente)
                .Include(r => r.Servicioreservas)
                    .ThenInclude(sr => sr.IdServicioNavigation)
                .Include(r => r.IdEmpleadoNavigation)
                    .ThenInclude(e => e.IdPersonaNavigation)
                .Select(r => new ReservaDTO
                {
                    IdReserva = r.IdReserva,
                    Fecha = r.Fecha,
                    HoraInicio = r.HoraInicio,
                    HoraFin = r.HoraFin,
                    EstadoReserva = r.EstadoReserva,
                    IdEmpleado = r.IdEmpleado,
                    IdServicios = r.Servicioreservas.Select(sr => sr.IdServicio).ToList()
                })
                .ToListAsync();

            return Ok(reservas);
        }

        // GET: api/Reserva/{idReserva}
        [HttpGet("{idReserva}")]
        public async Task<ActionResult<ReservaDTO>> GetReserva(int idReserva)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Servicioreservas)
                    .ThenInclude(sr => sr.IdServicioNavigation)
                .Include(r => r.IdEmpleadoNavigation)
                    .ThenInclude(e => e.IdPersonaNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == idReserva);

            if (reserva == null)
                return NotFound("Reserva no encontrada.");

            var reservaDto = new ReservaDTO
            {
                IdReserva = reserva.IdReserva,
                Fecha = reserva.Fecha,
                HoraInicio = reserva.HoraInicio,
                HoraFin = reserva.HoraFin,
                EstadoReserva = reserva.EstadoReserva,
                IdEmpleado = reserva.IdEmpleado,
                IdServicios = reserva.Servicioreservas.Select(sr => sr.IdServicio).ToList()
            };

            return Ok(reservaDto);
        }

        // PUT: api/Reserva/Cancelar/{idReserva}
        [HttpPut("Cancelar/{idReserva}")]
        public async Task<IActionResult> CancelarReserva(int idReserva)
        {
            var reserva = await _context.Reservas.FindAsync(idReserva);
            if (reserva == null)
                return NotFound("Reserva no encontrada.");

            // Validar que el usuario tiene permiso para cancelar la reserva
            var emailUsuario = User.FindFirstValue(ClaimTypes.Email);
            var rolUsuario = User.FindFirstValue(ClaimTypes.Role);

            if (rolUsuario == "Cliente")
            {
                var cliente = await _context.Clientes
                    .Include(c => c.IdPersonaNavigation)
                    .FirstOrDefaultAsync(c => c.IdCliente == reserva.IdCliente && c.IdPersonaNavigation.Email == emailUsuario);

                if (cliente == null)
                    return Unauthorized("No tienes permiso para cancelar esta reserva.");
            }
            else if (rolUsuario == "Empleado")
            {
                var empleado = await _context.Empleados
                    .Include(e => e.IdPersonaNavigation)
                    .FirstOrDefaultAsync(e => e.IdEmpleado == reserva.IdEmpleado && e.IdPersonaNavigation.Email == emailUsuario);

                if (empleado == null)
                    return Unauthorized("No tienes permiso para cancelar esta reserva.");
            }

            reserva.EstadoReserva = "Cancelada";
            _context.Entry(reserva).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Reserva cancelada exitosamente." });
        }



        [HttpPost("Confirmar/{idReserva}")]
        public async Task<IActionResult> ConfirmarReserva(int idReserva)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Servicioreservas)
                    .ThenInclude(sr => sr.IdServicioNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == idReserva);

            if (reserva == null)
                return NotFound("Reserva no encontrada.");

            if (reserva.EstadoReserva != "Pendiente")
                return BadRequest("La reserva ya ha sido confirmada o cancelada.");

            // Calcular el monto total y aplicar promociones
            decimal montoTotal = 0;
            foreach (var sr in reserva.Servicioreservas)
            {
                var servicio = sr.IdServicioNavigation;
                decimal precioServicio = servicio.Precio;

                // Aplicar promociones
                var promocion = await _context.Promocions
                    .FirstOrDefaultAsync(p => p.IdServicio == servicio.IdServicio && p.Estado == "Activa");

                if (promocion != null)
                {
                    decimal descuento = promocion.Descuento ?? 0;
                    precioServicio -= (precioServicio * descuento / 100);
                }

                montoTotal += precioServicio;

                // Crear detalle de factura
                var detalleFactura = new Detallefactura
                {
                    PrecioServicio = precioServicio,
                    CantidadServicio = 1,
                    Subtotal = precioServicio,
                    IdServicioReserva = sr.IdServicioReserva
                };

                _context.Detallefacturas.Add(detalleFactura);
            }

            // Generar número de documento (simplemente incrementamos el último número)
            var ultimoNumero = await _context.Numerodocumentos.OrderByDescending(nd => nd.IdNumeroDocumento).FirstOrDefaultAsync();
            int nuevoNumero = (ultimoNumero?.UltimoNumero ?? 0) + 1;

            var numeroDocumento = $"FAC-{nuevoNumero:D6}";

            // Crear la factura
            var factura = new Factura
            {
                NumeroDocumento = numeroDocumento,
                FechaEmision = DateTime.Now,
                MontoTotal = montoTotal,
                Estado = "Pendiente de Pago",
                IdReserva = reserva.IdReserva,
                IdCliente = reserva.IdCliente
            };

            _context.Facturas.Add(factura);

            // Actualizar el último número de documento
            var nuevoNumeroDocumento = new Numerodocumento
            {
                UltimoNumero = nuevoNumero,
                FechaRegistro = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.Numerodocumentos.Add(nuevoNumeroDocumento);

            reserva.EstadoReserva = "Confirmada";

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Reserva confirmada y factura generada exitosamente." });
        }

    }
}

// Controllers/PagoController.cs
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
    public class PagoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PagoController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Pago
        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> RegistrarPago([FromBody] PagoDTO pagoDto)
        {
            // Validar el DTO
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Obtener el ID del cliente desde el token
            var emailCliente = User.FindFirstValue(ClaimTypes.Email);
            var cliente = await _context.Clientes
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(c => c.IdPersonaNavigation.Email == emailCliente);

            if (cliente == null)
                return Unauthorized("Cliente no encontrado.");

            // Validar que la factura pertenece al cliente
            var factura = await _context.Facturas
                .FirstOrDefaultAsync(f => f.IdFactura == pagoDto.IdFactura && f.IdCliente == cliente.IdCliente);

            if (factura == null)
                return BadRequest("La factura no pertenece al cliente.");

            // Crear el pago
            var pago = new Pago
            {
                Monto = pagoDto.Monto,
                FechaPago = DateTime.Now,
                Estado = "Completado",
                IdFactura = pagoDto.IdFactura,
                IdMetodoPago = pagoDto.IdMetodoPago
            };

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Pago registrado exitosamente." });
        }

        // GET: api/Pago/Cliente/{idCliente}
        [HttpGet("Cliente/{idCliente}")]
        [Authorize(Roles = "Cliente")]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> GetPagosPorCliente(int idCliente)
        {
            // Validar que el cliente es el dueño del perfil
            var emailCliente = User.FindFirstValue(ClaimTypes.Email);
            var cliente = await _context.Clientes
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(c => c.IdCliente == idCliente && c.IdPersonaNavigation.Email == emailCliente);

            if (cliente == null)
                return Unauthorized("No tienes permiso para ver esta información.");

            var pagos = await _context.Pagos
                .Include(p => p.IdFacturaNavigation)
                .Where(p => p.IdFacturaNavigation.IdCliente == idCliente)
                .Select(p => new PagoDTO
                {
                    IdPago = p.IdPago,
                    Monto = p.Monto,
                    FechaPago = p.FechaPago ?? DateTime.Now,
                    Estado = p.Estado,
                    IdMetodoPago = p.IdMetodoPago,
                    IdFactura = p.IdFactura
                })
                .ToListAsync();

            return Ok(pagos);
        }

        // GET: api/Pago/{idPago}
        [HttpGet("{idPago}")]
        [Authorize(Roles = "Cliente,Empleado")]
        public async Task<ActionResult<PagoDTO>> GetPago(int idPago)
        {
            var pago = await _context.Pagos
                .Include(p => p.IdFacturaNavigation)
                .FirstOrDefaultAsync(p => p.IdPago == idPago);

            if (pago == null)
                return NotFound("Pago no encontrado.");

            // Validar que el usuario tiene permiso para ver el pago
            var emailUsuario = User.FindFirstValue(ClaimTypes.Email);
            var rolUsuario = User.FindFirstValue(ClaimTypes.Role);

            if (rolUsuario == "Cliente")
            {
                var cliente = await _context.Clientes
                    .Include(c => c.IdPersonaNavigation)
                    .FirstOrDefaultAsync(c => c.IdCliente == pago.IdFacturaNavigation.IdCliente && c.IdPersonaNavigation.Email == emailUsuario);

                if (cliente == null)
                    return Unauthorized("No tienes permiso para ver este pago.");
            }
            else if (rolUsuario == "Empleado")
            {
                // Los empleados pueden ver los pagos
            }

            var pagoDto = new PagoDTO
            {
                IdPago = pago.IdPago,
                Monto = pago.Monto,
                FechaPago = pago.FechaPago ?? DateTime.Now,
                Estado = pago.Estado,
                IdMetodoPago = pago.IdMetodoPago,
                IdFactura = pago.IdFactura
            };

            return Ok(pagoDto);
        }
    }
}

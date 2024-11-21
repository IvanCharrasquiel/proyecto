// Controllers/FacturaController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;
using System.Linq;
using System.Threading.Tasks;

namespace APIProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturaController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Factura
        [HttpPost]
        public async Task<IActionResult> CrearFactura([FromBody] FacturaDTO facturaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var factura = new Factura
            {
                NumeroDocumento = facturaDto.NumeroDocumento,
                FechaEmision = facturaDto.FechaEmision,
                MontoTotal = facturaDto.MontoTotal,
                Estado = facturaDto.Estado,
                IdReserva = facturaDto.IdReserva,
                IdCliente = facturaDto.IdCliente
            };

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            // Agregar detalles de factura si existen
            if (facturaDto.Detalles != null && facturaDto.Detalles.Any())
            {
                foreach (var detalleDto in facturaDto.Detalles)
                {
                    var detalle = new Detallefactura
                    {
                        PrecioServicio = detalleDto.PrecioServicio,
                        CantidadServicio = detalleDto.CantidadServicio,
                        Subtotal = detalleDto.Subtotal,
                        IdFactura = factura.IdFactura,
                        IdServicioReserva = detalleDto.IdServicioReserva
                    };

                    _context.Detallefacturas.Add(detalle);
                }

                await _context.SaveChangesAsync();
            }

            facturaDto.IdFactura = factura.IdFactura;

            return CreatedAtAction(nameof(GetFactura), new { id = factura.IdFactura }, facturaDto);
        }

        // GET: api/Factura/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaDTO>> GetFactura(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Detallefacturas)
                .FirstOrDefaultAsync(f => f.IdFactura == id);

            if (factura == null)
                return NotFound();

            var facturaDto = new FacturaDTO
            {
                IdFactura = factura.IdFactura,
                NumeroDocumento = factura.NumeroDocumento,
                FechaEmision = factura.FechaEmision,
                MontoTotal = factura.MontoTotal,
                Estado = factura.Estado,
                IdReserva = factura.IdReserva,
                IdCliente = factura.IdCliente,
                Detalles = factura.Detallefacturas.Select(d => new DetalleFacturaDTO
                {
                    IdDetalleFactura = d.IdDetalleFactura,
                    PrecioServicio = d.PrecioServicio,
                    CantidadServicio = d.CantidadServicio,
                    Subtotal = d.Subtotal,
                    IdFactura = d.IdFactura,
                    IdServicioReserva = d.IdServicioReserva
                }).ToList()
            };

            return Ok(facturaDto);
        }

        // PUT: api/Factura/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarFactura(int id, [FromBody] FacturaDTO facturaDto)
        {
            if (id != facturaDto.IdFactura)
                return BadRequest();

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
                return NotFound();

            factura.NumeroDocumento = facturaDto.NumeroDocumento;
            factura.FechaEmision = facturaDto.FechaEmision;
            factura.MontoTotal = facturaDto.MontoTotal;
            factura.Estado = facturaDto.Estado;
            factura.IdReserva = facturaDto.IdReserva;
            factura.IdCliente = facturaDto.IdCliente;

            _context.Entry(factura).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Actualizar detalles de factura si es necesario
            // Aquí podrías agregar lógica para actualizar los detalles asociados

            return NoContent();
        }
    }
}

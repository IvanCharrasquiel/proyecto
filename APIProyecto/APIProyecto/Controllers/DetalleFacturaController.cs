// Controllers/DetalleFacturaController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProyecto.DTO;
using APIProyecto.Models;
using System.Threading.Tasks;

namespace APIProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleFacturaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DetalleFacturaController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/DetalleFactura
        [HttpPost]
        public async Task<IActionResult> CrearDetalleFactura([FromBody] DetalleFacturaDTO detalleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detalle = new Detallefactura
            {
                PrecioServicio = detalleDto.PrecioServicio,
                CantidadServicio = detalleDto.CantidadServicio,
                Subtotal = detalleDto.Subtotal,
                IdFactura = detalleDto.IdFactura,
                IdServicioReserva = detalleDto.IdServicioReserva
            };

            _context.Detallefacturas.Add(detalle);
            await _context.SaveChangesAsync();

            detalleDto.IdDetalleFactura = detalle.IdDetalleFactura;

            return CreatedAtAction(nameof(GetDetalleFactura), new { id = detalle.IdDetalleFactura }, detalleDto);
        }

        // GET: api/DetalleFactura/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleFacturaDTO>> GetDetalleFactura(int id)
        {
            var detalle = await _context.Detallefacturas.FindAsync(id);

            if (detalle == null)
                return NotFound();

            var detalleDto = new DetalleFacturaDTO
            {
                IdDetalleFactura = detalle.IdDetalleFactura,
                PrecioServicio = detalle.PrecioServicio,
                CantidadServicio = detalle.CantidadServicio,
                Subtotal = detalle.Subtotal,
                IdFactura = detalle.IdFactura,
                IdServicioReserva = detalle.IdServicioReserva
            };

            return Ok(detalleDto);
        }

        // PUT: api/DetalleFactura/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDetalleFactura(int id, [FromBody] DetalleFacturaDTO detalleDto)
        {
            if (id != detalleDto.IdDetalleFactura)
                return BadRequest();

            var detalle = await _context.Detallefacturas.FindAsync(id);
            if (detalle == null)
                return NotFound();

            detalle.PrecioServicio = detalleDto.PrecioServicio;
            detalle.CantidadServicio = detalleDto.CantidadServicio;
            detalle.Subtotal = detalleDto.Subtotal;
            detalle.IdFactura = detalleDto.IdFactura;
            detalle.IdServicioReserva = detalleDto.IdServicioReserva;

            _context.Entry(detalle).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

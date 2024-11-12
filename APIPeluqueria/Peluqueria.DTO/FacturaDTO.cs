using System.Collections.Generic;

namespace Peluqueria.DTO
{
    public class FacturaDTO
    {
        public int IdFactura { get; set; }
        public string NumeroDocumento { get; set; } // Corrige el nombre de la propiedad
        public DateOnly? FechaEmision { get; set; }
        public decimal? MontoTotal { get; set; }
        public int? IdReserva { get; set; }
        public List<DetalleFacturaDTO> DetalleFacturas { get; set; } = new List<DetalleFacturaDTO>();

        // Agregar la propiedad Pagos
        public List<PagoDTO> Pagos { get; set; } = new List<PagoDTO>();

        // Nueva propiedad Estado para el estado de la factura
        public string? Estado { get; set; }
    }
}

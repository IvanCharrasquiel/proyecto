// DTO/FacturaDTO.cs
using System;
using System.Collections.Generic;

namespace ProyectoO.DTO
{
    public class FacturaDTO
    {
        public int IdFactura { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public DateTime? FechaEmision { get; set; }
        public decimal? MontoTotal { get; set; }
        public string Estado { get; set; } = null!;
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }

        public List<DetalleFacturaDTO> Detalles { get; set; } = new List<DetalleFacturaDTO>();
    }
}

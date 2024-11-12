using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peluqueria.DTO
{
    public class DetalleFacturaDTO
    {
        public int IdDetalleFactura { get; set; }
        public decimal? Subtotal { get; set; }
        public int? PrecioServicio { get; set; }
        public int IdFactura { get; set; }
        public int? CantidadServicio { get; set; }
        public int? IdServicio { get; set; }
        public ServicioDTO? Servicio { get; set; } // Navegación simplificada para mostrar detalles del servicio
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }  // Llave primaria del detalle de factura

        public decimal? Subtotal { get; set; }

        public double? PrecioServicio { get; set; }

        public int? CantidadServicio { get; set; }

        // Clave foránea hacia Factura
        public int? IdFactura { get; set; }

        // Propiedad de navegación hacia Factura
        public virtual Factura IdFacturaNavigation { get; set; }  // Relación con la entidad Factura

        // Clave foránea hacia Servicio (si aplica)
        public int? IdServicio { get; set; }

        // Propiedad de navegación hacia Servicio
        public virtual Servicio IdServicioNavigation { get; set; }  // Relación con la entidad Servicio (si aplica)
    }
}

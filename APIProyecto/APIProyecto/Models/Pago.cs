using System;

namespace APIProyecto.Models
{
    public partial class Pago
    {
        public int IdPago { get; set; }
        public decimal? Monto { get; set; }
        public DateTime? FechaPago { get; set; }  // Cambiado a DateTime
        public string? Estado { get; set; }
        public int IdFactura { get; set; }
        public int? IdMetodoPago { get; set; }

        // Propiedades de navegación
        public virtual Factura IdFacturaNavigation { get; set; } = null!;
        public virtual Metodopago? IdMetodoPagoNavigation { get; set; }  // Nueva propiedad
    }
}

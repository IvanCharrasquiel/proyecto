using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class DetalleFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetalleFactura { get; set; }

        public int? CantidadServicio { get; set; }

        public double? PrecioServicio { get; set; }

        public decimal? Subtotal { get; set; }

        public int? IdFactura { get; set; }

        public int? IdServicio { get; set; }
        public virtual Factura IdFacturaNavigation { get; set; } = null!;
    }
}

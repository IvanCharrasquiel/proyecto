using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    [Table("Pago")]
    public class Pago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPago { get; set; }

        public decimal? Monto { get; set; }

        public DateTime? FechaPago { get; set; }

        public string Estado { get; set; }

        public int IdFactura { get; set; }

        public int IdMetodoPago { get; set; }

        // Propiedades de navegación
        public virtual MetodoPago MetodoPago { get; set; }
        public virtual Factura Factura { get; set; } // Asumiendo que tienes una clase Factura
    }
}

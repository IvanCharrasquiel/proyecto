using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class MetodoPago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMetodoPago { get; set; }

        [Column("MetodoPago")]
        [StringLength(50)]
        public string MetodoPago1 { get; set; } // O simplemente "MetodoPago"

        // Colección de pagos que usan este método de pago
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}

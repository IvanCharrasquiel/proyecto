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

        public float Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string Estado { get; set; }

        public int IdFactura { get; set; }

        public int IdMetodoPago { get; set; }
    }
}

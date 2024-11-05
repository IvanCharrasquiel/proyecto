using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Factura
    {
        public Factura()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaEmision { get; set; }

        public decimal? MontoTotal { get; set; }

        public int? IdCita { get; set; }

        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

        public virtual Cita? IdCitaNavigation { get; set; }

    }
}

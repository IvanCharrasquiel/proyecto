using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Combo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCombo { get; set; }

        public double? Precio { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFin { get; set; }

        public int? DuracionTotal { get; set; }

        public int? IdPromocion { get; set; }

        public virtual Promocion IdPromocionNavigation { get; set; } = null!;

    }
}

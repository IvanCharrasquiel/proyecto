using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Promocion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPromocion { get; set; }

        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }

        [Range(0, 100)]
        public double Descuento { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; }

        public virtual ICollection<Combo> Combos { get; set; } = new List<Combo>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategoria { get; set; }

        [Column("Categoria")]
        [StringLength(50)]
        public string Categoria1 { get; set; }

        public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
    }
}

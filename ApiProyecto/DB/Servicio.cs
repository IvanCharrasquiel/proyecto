using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdServicio { get; set; }

        [Column("Servicio")]
        [StringLength(50)]
        public string Servicio1 { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public double? Precio { get; set; }

        public int? Minutos { get; set; }

        public int? IdCategoria { get; set; }
        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    }
}

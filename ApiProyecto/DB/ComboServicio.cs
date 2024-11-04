using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class ComboServicio
    {
        // Clave primaria independiente
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Llaves foráneas para la relación de muchos a muchos entre Combo y Servicio
        public int IdCombo { get; set; }
        public int IdServicio { get; set; }

        // Navegación para Combo
        [ForeignKey("IdCombo")]
        public virtual Combo Combo { get; set; }

        // Navegación para Servicio
        [ForeignKey("IdServicio")]
        public virtual Servicio Servicio { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DB
{
    public class ComboServicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComboServicio { get; set; }  // Llave primaria única

        // Clave foránea hacia Combo
        public int IdCombo { get; set; }
        public virtual Combo IdComboNavigation { get; set; }  // Propiedad de navegación hacia Combo


        // Clave foránea hacia Servicio
        public int IdServicio { get; set; }
        public virtual Servicio IdServicioNavigation { get; set; }  // Propiedad de navegación hacia Servicio
    }
}

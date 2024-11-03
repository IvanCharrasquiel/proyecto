using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Combos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCombos { get; set; }
        public float Precio { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public int DuracionTotal { get; set; }

        // Relación de muchos a muchos con Servicio a través de ComboServicio
        public virtual ICollection<ComboServicio> ComboServicios { get; set; }
    }
}

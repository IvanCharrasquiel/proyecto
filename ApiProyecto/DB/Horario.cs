using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Horario
    {
        public Horario()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHorario { get; set; }

        [StringLength(50)]
        public string Dia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HoraInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HoraFin { get; set; }

        

    }
}

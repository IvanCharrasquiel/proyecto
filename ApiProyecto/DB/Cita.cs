using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Cita
    {
        
        public Cita()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCita { get; set; }

        public int? IdCliente { get; set; }

        public int? IdEmpleado { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }

        public int? IdHorario { get; set; }

        

        
    }

}

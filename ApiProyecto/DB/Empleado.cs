using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Empleado
    {
        public Empleado()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }

        public double? Comision { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaContrato { get; set; }

        public int? IdCargo { get; set; }

        public int? IdPersonaEmpleado { get; set; }


        
    }
}

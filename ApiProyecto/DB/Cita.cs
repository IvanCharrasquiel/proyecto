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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCita { get; set; }

        public int? IdCliente { get; set; }

        public int? IdPersonaCliente { get; set; }

        public int? IdEmpleado { get; set; }

        public int? IdPersonaEmpleado { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha { get; set; }

        public int? IdHorario { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Empleado Empleado { get; set; }

        public virtual Horario Horario { get; set; }

        public virtual Persona Persona { get; set; }

        public virtual Persona Persona1 { get; set; }

        
    }

}

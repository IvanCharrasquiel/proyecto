using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura { get; set; }
        public DateTime FechaComision { get; set; }
        public float MontoTotal { get; set; }


        [ForeignKey("IdCita")]
        public int IdCita { get; set; }
        [ForeignKey("IdCliente")]
        public int IdCliente { get; set; }
        [ForeignKey("IdPersona")]
        public int IdPersonaCliente { get; set; }
        [ForeignKey("IdEmpleado")]
        public int IdEmpleado { get; set; }
        [ForeignKey("IdPersona")]
        public string IdPersonaEmpleado { get; set; }
        
        
    }
}

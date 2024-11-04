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
        public Factura()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaEmision { get; set; }

        public double? Monto { get; set; }

        public int? IdCita { get; set; }

        public int? IdCliente { get; set; }

        public int? IdEmpleado { get; set; }

       

    }
}

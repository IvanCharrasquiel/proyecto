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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }
        public int Comision { get; set; }
        public DateTime FechaContrato { get; set; }

        public virtual ICollection<Cita> Citas { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}

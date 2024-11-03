using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Estado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstado { get; set; }
        public string estado { get; set; }

        public virtual ICollection<Cita> Citas { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
        public virtual ICollection<Promocion> Promociones { get; set; }

    }
}

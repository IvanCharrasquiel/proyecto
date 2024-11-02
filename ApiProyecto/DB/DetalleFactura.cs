using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class DetalleFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDetalleFactura { get; set; }
        public float PrecioUnidad { get; set; }
        public float Subtotal { get; set; }


        [ForeignKey("IdFactura")]
        public int IdFactura { get; set; }

        [ForeignKey("IdCita")]
        public int IdCita { get; set; }

        [ForeignKey("IdCliente")]
        public int IdCliente { get; set; }

        [ForeignKey("IdPersona")]
        public int IdPersonaCliente { get; set; }

        [ForeignKey("IdEmpleado")]
        public int IdEmpleado { get; set; }

        [ForeignKey("IdPersona")]
        public int IdPersonaEmpleado { get; set; }

        [ForeignKey("IdServicio")]
        public int IdServicio { get; set; }

        [ForeignKey("IdCategoriaServicio")]
        public int IdCategoriaServicio { get; set; }
    }
}

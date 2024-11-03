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

        // Relación con Cita
        public int IdCita { get; set; }
        [ForeignKey("IdCita")]
        public virtual Cita Cita { get; set; }

        // Relación con Cliente
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        // Relación con Persona del Cliente
        public int IdPersonaCliente { get; set; }
        [ForeignKey("IdPersonaCliente")]
        public virtual Persona PersonaCliente { get; set; }

        // Relación con Empleado
        public int IdEmpleado { get; set; }
        [ForeignKey("IdEmpleado")]
        public virtual Empleado Empleado { get; set; }

        // Relación con Persona del Empleado
        public int IdPersonaEmpleado { get; set; }
        [ForeignKey("IdPersonaEmpleado")]
        public virtual Persona PersonaEmpleado { get; set; }


        public virtual ICollection<DetalleFactura> Detalles { get; set; }

    }
}

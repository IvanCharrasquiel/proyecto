using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Pago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPago { get; set; }

        public float Monto { get; set; }
        public DateTime FechaPago { get; set; }

        // Relación con Estado
        public int IdEstado { get; set; }
        [ForeignKey("IdEstado")]
        public virtual Estado Estado { get; set; }

        // Relación con Factura
        public int IdFactura { get; set; }
        [ForeignKey("IdFactura")]
        public virtual Factura Factura { get; set; }

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
    }
}

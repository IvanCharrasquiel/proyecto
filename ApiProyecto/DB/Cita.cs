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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCita { get; set; }

        // Relación con Cliente
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }

        // Relación con Persona para el cliente
        public int IdPersonaCliente { get; set; }
        [ForeignKey("IdPersonaCliente")]
        public virtual Persona PersonaCliente { get; set; }

        // Relación con Empleado
        public int IdEmpleado { get; set; }
        [ForeignKey("IdEmpleado")]
        public virtual Empleado Empleado { get; set; }

        // Relación con Persona para el empleado
        public int IdPersonaEmpleado { get; set; }
        [ForeignKey("IdPersonaEmpleado")]
        public virtual Persona PersonaEmpleado { get; set; }

        // Relación con Estado de la Cita
        public int IdEstado { get; set; }
        [ForeignKey("IdEstado")]
        public virtual Estado Estado { get; set; }

        // Relación con Horario
        public int IdHorario { get; set; }
        [ForeignKey("IdHorario")]
        public virtual Horario Horario { get; set; }

        // Relación con Sexo
        public int IdSexo { get; set; }
        [ForeignKey("IdSexo")]
        public virtual Sexo Sexo { get; set; }

        public virtual ICollection<DetalleFactura> DetallesFactura { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }

        // Relación de muchos a muchos
        public virtual ICollection<CitaServicio> CitaServicios { get; set; } = new List<CitaServicio>();
    }

}

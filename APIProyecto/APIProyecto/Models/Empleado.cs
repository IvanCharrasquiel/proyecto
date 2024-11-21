using System;
using System.Collections.Generic;

namespace APIProyecto.Models
{
    public partial class Empleado
    {
        public int IdEmpleado { get; set; }

        public DateTime? FechaContrato { get; set; }

        public decimal Comision { get; set; }

        public int IdPersona { get; set; }

        public int IdCargo { get; set; }

        public string Contraseña { get; set; } = null!;

        // Propiedades de navegación
        public virtual Persona IdPersonaNavigation { get; set; } = null!;

        public virtual Cargo? IdCargoNavigation { get; set; }

        // Relación con EmpleadoServicio
        public virtual ICollection<EmpleadoServicio> EmpleadosServicio { get; set; } = new List<EmpleadoServicio>();

        // Relación con Servicios
        public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();

        public virtual ICollection<Empleadohorario> Empleadohorarios { get; set; } = new List<Empleadohorario>();

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public partial class Empleado
    {
        public int IdEmpleado { get; set; }

        public double? Comision { get; set; }

        public DateTime? FechaContrato { get; set; }

        public int? IdCargo { get; set; }

        public int? IdPersonaEmpleado { get; set; }

        public string? Contraseña { get; set; }

        // Relación muchos a uno con Cargo
        public virtual Cargo IdCargoNavigation { get; set; } = null!;

        // Relación uno a uno con Persona (cada empleado tiene una persona asociada)
        public virtual Persona IdPersonaEmpleadoNavigation { get; set; } = null!;

        public virtual ICollection<EmpleadoHorario> EmpleadoHorarios { get; set; } = new List<EmpleadoHorario>();
        public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
    }
}

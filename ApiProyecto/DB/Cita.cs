using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Cita
    {
        public int IdCita { get; set; }

        public int? IdCliente { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdHorario { get; set; }
        public DateTime? Fecha { get; set; }

        // Propiedades de navegación para relaciones con otras tablas
        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual Empleado? IdEmpleadoNavigation { get; set; }
        public virtual Horario? IdHorarioNavigation { get; set; }

        // Colección de facturas asociadas a la cita
        public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

        // Relación muchos a muchos con Servicio a través de CitaServicio
        public virtual ICollection<CitaServicio> CitaServicios { get; set; } = new List<CitaServicio>();

    }

}

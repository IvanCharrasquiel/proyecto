using System.ComponentModel.DataAnnotations;

namespace DB
{
    public class CitaServicio
    {
        [Key]
        public int Id { get; set; }  // Esta es la clave primaria única

        public int IdCita { get; set; }  // Clave foránea a Cita
        public int IdServicio { get; set; }  // Clave foránea a Servicio

        // Propiedades de navegación para las entidades relacionadas
        public virtual Cita IdCitaNavigation { get; set; }
        public virtual Servicio IdServicioNavigation { get; set; }
    
    }
}

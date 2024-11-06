using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class Horario
    {
        [Key]  // Establece la clave primaria
        public int IdHorario { get; set; }

        [Required]  // Es obligatorio tener un día
        [StringLength(20)]  // Limita la longitud de la cadena
        public string Dia { get; set; }

        [DataType(DataType.Time)]  // Especifica que es solo una hora
        public TimeSpan? HoraInicio { get; set; }

        [DataType(DataType.Time)]  // Especifica que es solo una hora
        public TimeSpan? HoraFin { get; set; }

        // Relación con EmpleadoHorario (si aplica)
        public virtual ICollection<EmpleadoHorario> EmpleadoHorarios { get; set; } = new List<EmpleadoHorario>();

        // Relación con Cita (si aplica)
        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();

    }
}

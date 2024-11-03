using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class CitaServicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Relación con Cita
        public int IdCita { get; set; }
        [ForeignKey("IdCita")]
        public virtual Cita Cita { get; set; }

        // Relación con Servicio
        public int IdServicio { get; set; }
        [ForeignKey("IdServicio")]
        public virtual Servicio Servicio { get; set; }
    }
}

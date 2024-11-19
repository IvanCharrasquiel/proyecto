using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace APIProyecto.DTO
{
    public class ReservaDTO
    {
        public int IdReserva { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        // HoraFin será calculada automáticamente
        public TimeSpan HoraFin { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdEmpleado { get; set; }

        [Required]
        [StringLength(50)]
        public string EstadoReserva { get; set; }

        // Lista de servicios asociados
        [Required]
        public List<int> IdServicios { get; set; }
    }
}

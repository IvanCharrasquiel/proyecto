using System.ComponentModel.DataAnnotations;

namespace APIProyecto.DTO
{
    public class EmpleadoHorarioDTO
    {
        [Required]
        public int IdHorario { get; set; }

        [Required]
        [Range(2000, 2100, ErrorMessage = "El año debe estar entre 2000 y 2100.")]
        public DateTime Fecha { get; set; }
    }
}
